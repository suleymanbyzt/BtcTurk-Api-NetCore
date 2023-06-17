using BtcTurkAPI.Proxies.BtcTurkApi;
using BtcTurkAPI.Proxies.BtcTurkApi.Models.Enums;
using BtcTurkAPI.Proxies.BtcTurkApi.Models.Requests;
using BtcTurkAPI.Proxies.BtcTurkApi.Models.Responses;
using BtcTurkAPI.Proxies.BtcTurkGraphApi;
using BtcTurkAPI.Proxies.BtcTurkGraphApi.Models.Requests;
using BtcTurkAPI.Proxies.BtcTurkGraphApi.Models.Responses;

namespace BtcTurkAPI;

public class Samples : BackgroundService
{
    private readonly IBtcTurkApiProxy _btcTurkApiProxy;
    private readonly IBtcTurkGraphApiProxy _graphApiProxy;

    public Samples(
        IBtcTurkApiProxy btcTurkApiProxy,
        IBtcTurkGraphApiProxy graphApiProxy)
    {
        _btcTurkApiProxy = btcTurkApiProxy;
        _graphApiProxy = graphApiProxy;
    }

    public async Task PublicEndpoints()
    {
        Ticker tickers = await _btcTurkApiProxy.GetTickers();
        
        ExchangeInfo exchangeInfo = await _btcTurkApiProxy.GetExchangeInfo();
        
        OrderBook orderBook = await _btcTurkApiProxy.GetOrderBook("BTCTRY", 25);
        
        Trade trades = await _btcTurkApiProxy.GetTrades("BTCTRY");

        QueryKlineRequest klineRequest = new QueryKlineRequest()
        {
            PairSymbol = "BTCTRY",
            From = 1602925320,
            To = 1603152000,
            Resolution = 60
        };
        Kline kline = await _graphApiProxy.GetKlines(klineRequest);

        QueryOhlcRequest ohlcRequest = new QueryOhlcRequest()
        {
            From = 1602925320,
            To = 1603152000,
            PairSymbol = "BTCTRY"
        };
        List<Ohlc> ohlc = await _graphApiProxy.GetOhlc(ohlcRequest);
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