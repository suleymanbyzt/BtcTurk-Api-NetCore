using BtcTurkAPI.Extensions;
using BtcTurkAPI.Proxies.BtcTurkApi.Models;

namespace BtcTurkAPI.Proxies.BtcTurkGraphApi;

public class BtcTurkGraphApiProxy : IBtcTurkGraphApiProxy
{
    private readonly HttpClient _httpClient;

    public BtcTurkGraphApiProxy(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<List<Ohlc>> GetOhlc(string pairSymbol, long from, long to)
    {
        List<string> query = new List<string>();

        if (string.IsNullOrWhiteSpace(pairSymbol) || (from == null && to == null))
        {
            throw new ApplicationException("parameters cannot be null");
        }

        query.Add($"pair={pairSymbol}");
        
        if (from != null)
        {
            query.Add($"from={from}");
        }

        if (to != null)
        {
            query.Add($"to={to}");
        }

        string queryString = string.Join("&", query);
        
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"v1/ohlcs?{queryString}");
        return await _httpClient.SendRequestAsync<List<Ohlc>>(request);
    }
}