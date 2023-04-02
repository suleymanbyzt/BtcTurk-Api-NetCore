using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using BtcTurkAPI.Extensions;
using BtcTurkAPI.Websocket.Models;
using MediatR;
using Newtonsoft.Json.Linq;

namespace BtcTurkAPI.Websocket;

public class BtcTurkPrivateWebsocket
{
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;

    public BtcTurkPrivateWebsocket(
        IHostApplicationLifetime applicationLifetime,
        IConfiguration configuration,
        IMediator mediator)
    {
        _applicationLifetime = applicationLifetime;
        _configuration = configuration;
        _mediator = mediator;
    }

    public async Task Initialize()
    {
        Task.Run(async () =>
        {
            while (!_applicationLifetime.ApplicationStopping.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(3), _applicationLifetime.ApplicationStopping);

                try
                {
                    await GetPrivateSocketMessages();
                }
                catch (Exception e)
                {
                    //log here
                }
            }
        }, _applicationLifetime.ApplicationStopping);
    }

    private async Task GetPrivateSocketMessages()
    {
        Uri uri = new Uri(_configuration["BtcTurkWebsocketUrl"]);
        ClientWebSocket client = new ClientWebSocket();

        await client.ConnectAsync(uri, CancellationToken.None);
        long nonce = 3000;

        IConfigurationSection apiKeys = _configuration.GetSection("ApiKey");
        string publicKey = apiKeys["publicKey"];
        string privateKey = apiKeys["privateKey"];
        string baseString = $"{publicKey}{nonce}";

        string message = ComputeHash(privateKey, baseString, publicKey, nonce);

        await client.SendAsync(new ArraySegment<byte>(array: Encoding.UTF8.GetBytes(message),
                0,
                message.Length),
            WebSocketMessageType.Text,
            true,
            CancellationToken.None);

        while (!_applicationLifetime.ApplicationStopping.IsCancellationRequested)
        {
            try
            {
                string resultMessage = await ReceiveMessageAsync(client);

                int? messageId = SocketMessageId(resultMessage);

                if (messageId == null || messageId == 991)
                {
                    continue;
                }

                if (messageId == 114)
                {
                    UserLogin? userLoginResult = await GetResponseAsync<UserLogin>(resultMessage);
                    if (!userLoginResult.Ok)
                    {
                        throw new ApplicationException("Problem occurred during authentication.");
                    }
                }

                switch (messageId)
                {
                    case 423:
                        UserTrade? userTrade = await GetResponseAsync<UserTrade>(resultMessage);
                        //Message handlers for private socket messages
                        //BtcTurkAPI > Handlers > UserTradeHandler
                        await _mediator.Publish(userTrade);
                        break;

                    case 441:
                        OrderMatched? orderMatched = await GetResponseAsync<OrderMatched>(resultMessage);
                        await _mediator.Publish(orderMatched);
                        break;

                    case 451:
                        OrderInserted? orderInserted = await GetResponseAsync<OrderInserted>(resultMessage);
                        await _mediator.Publish(orderInserted);
                        break;

                    case 452:
                        OrderDeleted? orderDeleted = await GetResponseAsync<OrderDeleted>(resultMessage);
                        await _mediator.Publish(orderDeleted);
                        break;

                    case 453:
                        OrderUpdated? orderUpdated = await GetResponseAsync<OrderUpdated>(resultMessage);
                        await _mediator.Publish(orderUpdated);
                        break;
                }
            }
            catch (Exception e)
            {
                // log here
                break;
            }
        }
    }

    private string ComputeHash(string privateKey, string baseString, string publicKey, long nonce)
    {
        byte[] key = Convert.FromBase64String(privateKey);
        string signature;

        using (HMACSHA256 hmac = new HMACSHA256(key))
        {
            byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(baseString));
            signature = Convert.ToBase64String(hash);
        }

        object[] hmacMessageObject =
        {
            114, new
            {
                type = 114,
                publicKey = publicKey,
                timestamp = DateTime.UtcNow.ToUnixTime(),
                nonce = nonce,
                signature = signature
            }
        };

        return JsonSerializer.Serialize(hmacMessageObject);
    }

    private async Task<string> ReceiveMessageAsync(ClientWebSocket client)
    {
        byte[] buffer = new byte[1024 * 16];

        WebSocketReceiveResult result;
        StringBuilder resultMessageBuilder = new StringBuilder();

        do
        {
            result = await client.ReceiveAsync(buffer, CancellationToken.None);
            string bufferMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);

            resultMessageBuilder.Append(bufferMessage);
        } while (!result.EndOfMessage);

        return resultMessageBuilder.ToString();
    }

    private int? SocketMessageId(string resultMessage)
    {
        if (string.IsNullOrWhiteSpace(resultMessage))
        {
            return null;
        }

        JArray resultArray = JArray.Parse(resultMessage);

        if (resultArray.Count == 0)
        {
            return 0;
        }

        return resultArray.First().Value<int>();
    }

    private Task<T?> GetResponseAsync<T>(string resultMessage)
    {
        JArray resultArray = JArray.Parse(resultMessage);

        if (resultArray.Count == 0)
        {
            return Task.FromResult<T?>(default);
        }

        return Task.FromResult(resultArray.Last().ToObject<T>());
    }
}