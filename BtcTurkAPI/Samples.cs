using BtcTurkAPI.Proxies.BtcTurkApi;
using BtcTurkAPI.Proxies.BtcTurkApi.Enums;
using BtcTurkAPI.Proxies.BtcTurkApi.Models;

namespace BtcTurkAPI;

public class Samples : BackgroundService
{
    private readonly IBtcTurkApiProxy _btcTurkApiProxy;

    public Samples(IBtcTurkApiProxy btcTurkApiProxy)
    {
        _btcTurkApiProxy = btcTurkApiProxy;
    }

    public async Task PublicEndpoints()
    {
        Ticker tickers = await _btcTurkApiProxy.GetTickers();
        ExchangeInfo exchangeInfo = await _btcTurkApiProxy.GetExchangeInfo();
        OrderBook orderBook = await _btcTurkApiProxy.GetOrderBook("BTCTRY", 25);
        Trade trades = await _btcTurkApiProxy.GetTrades("BTCTRY");
    }

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

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await PublicEndpoints();
        await PrivateEndpoints(); 
    }
}