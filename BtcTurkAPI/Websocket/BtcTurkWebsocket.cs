using System.Net.WebSockets;
using System.Text;
using BtcTurkAPI.Extensions;
using BtcTurkAPI.Proxies.BtcTurkApi;
using BtcTurkAPI.Proxies.BtcTurkApi.Models;

namespace BtcTurkAPI.Websocket;

public class BtcTurkWebsocket
{
    private readonly IHostApplicationLifetime _applicationLifetime;
    private readonly IBtcTurkApiProxy _btcTurkApiProxy;
    private readonly IConfiguration _configuration;

    public BtcTurkWebsocket(
        IHostApplicationLifetime applicationLifetime,
        IBtcTurkApiProxy btcTurkApiProxy,
        IConfiguration configuration)
    {
        _applicationLifetime = applicationLifetime;
        _btcTurkApiProxy = btcTurkApiProxy;
        _configuration = configuration;
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
                    await GetSocketMessages();
                }
                catch (Exception e)
                {
                    //log here
                }
            }
        }, _applicationLifetime.ApplicationStopping);
    }

    private async Task GetSocketMessages()
    {
        ClientWebSocket clientWebSocket = new ClientWebSocket();

        Uri uri = new Uri(_configuration["BtcTurkWebsocketUrl"]);
        await clientWebSocket.ConnectAsync(uri, CancellationToken.None);

        List<string> pairs = new List<string>
        {
            "BTCTRY",
            "ETHTRY",
            "BTCUSDT",
            "ETHUSDT"
        };

        foreach (string pair in pairs)
        {
            string socketJoinRequest = pair.JoinRequest(channel: "orderbook");
            
            await clientWebSocket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.UTF8.GetBytes(socketJoinRequest),
                    offset: 0,
                    count: socketJoinRequest.Length),
                messageType: WebSocketMessageType.Text,
                endOfMessage: true,
                cancellationToken: CancellationToken.None);
        }
        
        byte[] buffer = new byte[1024 * 20];
        
        while (!_applicationLifetime.ApplicationStopping.IsCancellationRequested)
        {
            WebSocketReceiveResult result = await clientWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            string resultMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
            
            Console.WriteLine(resultMessage);
        }
    }
}