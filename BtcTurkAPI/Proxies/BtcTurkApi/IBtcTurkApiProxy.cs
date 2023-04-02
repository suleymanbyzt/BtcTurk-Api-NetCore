using BtcTurkAPI.Proxies.BtcTurkApi.Enums;
using BtcTurkAPI.Proxies.BtcTurkApi.Models;

namespace BtcTurkAPI.Proxies.BtcTurkApi;

public interface IBtcTurkApiProxy
{
    Task<ExchangeInfo> GetExchangeInfo();

    Task<Ticker> GetTickers();

    Task<OrderBook> GetOrderBook(string pairSymbol, int? limit);

    Task<Trade> GetTrades(string pairSymbol);

    Task<OpenOrder> GetOpenOrders(string pairSymbol);

    Task<UserTransactionTrade> GetUserTransactionTrades(long? orderId);
    
    Task<UserTransactionTrade> GetUserTransactionTrades(string pairSymbol, long? startDate, long? endDate);
    
    Task<UserBalance> GetBalances();
    
    Task<CreateOrderResponse> CreateOrderRequest(CreateOrderRequest request);

    Task<Base> DeleteOrderRequest(string id);
}