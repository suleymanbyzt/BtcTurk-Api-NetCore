using System.Text;
using BtcTurkAPI.Extensions;
using BtcTurkAPI.Proxies.BtcTurkApi.Models.Requests;
using BtcTurkAPI.Proxies.BtcTurkApi.Models.Responses;
using Newtonsoft.Json;

namespace BtcTurkAPI.Proxies.BtcTurkApi;

public class BtcTurkApiProxy : IBtcTurkApiProxy
{
    private readonly HttpClient _httpClient;

    public BtcTurkApiProxy(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<ExchangeInfo> GetExchangeInfo()
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "v2/server/exchangeinfo");
        return await _httpClient.SendRequestAsync<ExchangeInfo>(request);
    }

    public async Task<Ticker> GetTickers()
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "v2/ticker");
        return await _httpClient.SendRequestAsync<Ticker>(request);
    }

    public async Task<OrderBook> GetOrderBook(string pairSymbol, int? limit)
    {
        List<string> query = new List<string>();

        if (!string.IsNullOrWhiteSpace(pairSymbol))
        {
            query.Add( $"pairSymbol={pairSymbol}");
        }

        if (limit != null)
        {
            query.Add( $"limit={limit}");
        }
        
        string queryString = string.Join("&", query);
        
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"v2/orderbook?{queryString}");
        return await _httpClient.SendRequestAsync<OrderBook>(request);
    }

    public async Task<Trade> GetTrades(string pairSymbol)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"v2/trades?pairSymbol={pairSymbol}");
        return await _httpClient.SendRequestAsync<Trade>(request);
    }

    public async Task<OpenOrder> GetOpenOrders(string pairSymbol)
    {
        string query = null;
        
        if (!string.IsNullOrWhiteSpace(pairSymbol))
        {
            query = $"pairSymbol={pairSymbol}";
        }
        
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"v1/openOrders?{query}");
        return await _httpClient.SendRequestAsync<OpenOrder>(request);
    }

    public async Task<UserTransactionTrade> GetUserTransactionTrades(long? orderId)
    {
        string query = null;
        
        if (orderId != null)
        {
            query = $"orderId={orderId}";
        }
        
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"v1/users/transactions/trade?{query}");
        return await _httpClient.SendRequestAsync<UserTransactionTrade>(request);
    }
    
    public async Task<UserTransactionTrade> GetUserTransactionTrades(string pairSymbol, long? startDate, long? endDate)
    {
        List<string> query = new List<string>();
        
        if (!string.IsNullOrWhiteSpace(pairSymbol))
        {
            query.Add($"pairSymbol={pairSymbol}");
        }

        if (startDate != null && endDate != null)
        {
            query.Add($"startDate={startDate}");
            query.Add($"endDate={endDate}");
        }
        var queryString = string.Join("&", query);

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"v1/users/transactions/trade?{queryString}");
        return await _httpClient.SendRequestAsync<UserTransactionTrade>(request);
    }

    public async Task<UserBalance> GetBalances()
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "v1/users/balances");
        return await _httpClient.SendRequestAsync<UserBalance>(request);
    }

    public async Task<CreateOrderResponse> CreateOrderRequest(CreateOrderRequest createOrderRequest)
    {
        string serialize = JsonConvert.SerializeObject(createOrderRequest);
        StringContent content = new StringContent(serialize, Encoding.UTF8, "application/json");
        
        HttpRequestMessage request = new HttpRequestMessage()
        {
            Content = content,
            Method = HttpMethod.Post,
            RequestUri = new Uri(_httpClient.BaseAddress + "v1/order")
        };

        return await _httpClient.SendRequestAsync<CreateOrderResponse>(request);
    }

    public async Task<Base> DeleteOrderRequest(string id)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"v1/order?id={id}");
        return await _httpClient.SendRequestAsync<Base>(request);
    }
}