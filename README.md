# API and Websocket Examples for BtcTurk API.

<br />
<img width="250" align="left" alt="BtcTurk API NetCore" src="https://pro.btcturk.com/assets/images/media-material/btcturk-pro-logo-light.svg" />
<br /><br />

[![GitHub stars](https://img.shields.io/github/stars/suleymanbyzt/BtcTurk-Api-NetCore.svg?color=blue)](https://github.com/suleymanbyzt/BtcTurk-Api-NetCore/stargazers)
[![GitHub forks](https://img.shields.io/github/forks/suleymanbyzt/BtcTurk-Api-NetCore.svg?color=blue)](https://github.com/suleymanbyzt/BtcTurk-Api-NetCore/network)
[![GitHub contributors](https://img.shields.io/github/contributors/suleymanbyzt/BtcTurk-Api-NetCore.svg?color=blue)](https://github.com/suleymanbyzt/BtcTurk-Api-NetCore/network)


<h3>Built With</h3>

![.NET 7](https://img.shields.io/badge/-.NET%207.0-blueviolet?style=for-the-badge&logoColor=white)

Please open [issues](https://github.com/suleymanbyzt/BtcTurk-Api-NetCore/issues) for your questions or bug reports.

# Examples

First, inject IBtcTurkApiProxy for the class you will use.

Samples.cs
```csharp
public class Samples
{
    private readonly IBtcTurkApiProxy _btcTurkApiProxy;

    public Samples(IBtcTurkApiProxy btcTurkApiProxy)
    {
        _btcTurkApiProxy = btcTurkApiProxy;
    }
}
```
You can use public endpoints as follows.

``` Samples.cs ```
```csharp
 public async Task PublicEndpoints()
  {
      Ticker tickers = await _btcTurkApiProxy.GetTickers();
      ExchangeInfo exchangeInfo = await _btcTurkApiProxy.GetExchangeInfo();
      OrderBook orderBook = await _btcTurkApiProxy.GetOrderBook("BTCTRY", 25);
      Trade trades = await _btcTurkApiProxy.GetTrades("BTCTRY");
  }
```

You need to authenticate for private endpoints. All you need to do for this is to add your key information to the appsettings file.

For Example

```appsettings.Production.json```
```json
{
  "BtcTurkApiUrl": "https://api.btcturk.com/api/",
  "BtcTurkGraphApiUrl": "https://graph-api.btcturk.com/",
  "BtcTurkWebSocketUrl": "wss://ws-feed-pro.btcturk.com",
  "ApiKey" : {
    "PublicKey" : "", //public key here
    "PrivateKey": ""  //private key here
  }
}
```

After adding your key information, you can request private endpoints.

```Samples.cs```

```csharp
    public async Task PrivateEndpoints()
    {
        OpenOrder openOrders = await _btcTurkApiProxy.GetOpenOrders(pairSymbol: null);

        CreateOrderRequest request = new CreateOrderRequest()
        {
            Price = "500000",
            Quantity = "0.001",
            OrderType = OrderType.Buy,
            OrderMethod = OrderMethod.Limit,
            PairSymbol = "BTCTRY",
            NewOrderClientId = "BtcTurkApi-NetCore"
        };
        
        CreateOrderResponse createOrderResponse = await _btcTurkApiProxy.CreateOrderRequest(request);

        var cancelledOrder = await _btcTurkApiProxy.DeleteOrderRequest(createOrderResponse.Data.Id.ToString());

        UserTransactionTrade userTransactionTrade = await _btcTurkApiProxy.GetUserTransactionTrades(orderId: 123456789);

        UserBalance balances = await _btcTurkApiProxy.GetBalances();
    }
```

# Websocket

The websocket connection is automatically started after the application is initialized.

Example websocket join request for public channels.

```BtcTurkWebsocket.cs```
```csharp
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
```

For private websocket messages, you need to add your key information to appsettings.

```BtcTurkPrivateWebsocket.cs```
```csharp
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
```

Created handler for all private socket messages.

You can manage incoming private messages here.

For example:

```UserTradeHandler.cs```
```csharp
public class UserTradeHandler : INotificationHandler<UserTrade>
{
    public async Task Handle(UserTrade userTrade, CancellationToken cancellationToken)
    {
        //do something
        throw new NotImplementedException();
    }
}
```
z
