using BtcTurkAPI.Extensions;
using BtcTurkAPI.Proxies.BtcTurkGraphApi.Models.Requests;
using BtcTurkAPI.Proxies.BtcTurkGraphApi.Models.Responses;

namespace BtcTurkAPI.Proxies.BtcTurkGraphApi;

public class BtcTurkGraphApiProxy : IBtcTurkGraphApiProxy
{
    private readonly HttpClient _httpClient;

    public BtcTurkGraphApiProxy(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<List<Ohlc>> GetOhlc(QueryOhlcRequest request)
    {
        List<string> parameters = new List<string>();

        if (String.IsNullOrWhiteSpace(request.PairSymbol) || (request.From == null && request.To == null))
        {
            throw new ApplicationException("parameters cannot be null");
        }

        parameters.Add($"pair={request.PairSymbol}");
        parameters.Add($"from={request.From}");
        parameters.Add($"to={request.To}");

        string queryString = string.Join("&", parameters);
        
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, $"v1/ohlcs?{queryString}");
        return await _httpClient.SendRequestAsync<List<Ohlc>>(requestMessage);
    }

    public async Task<Kline> GetKlines(QueryKlineRequest request)
    {
        List<string> parameters = new List<string>();

        if (String.IsNullOrWhiteSpace(request.PairSymbol) || (request.From == null && request.To == null) || request.Resolution == null)
        {
            throw new ApplicationException("parameters cannot be null");
        }

        parameters.Add($"symbol={request.PairSymbol}");
        parameters.Add($"from={request.From}");
        parameters.Add($"to={request.To}");
        parameters.Add($"resolution={request.Resolution}");

        string queryString = string.Join("&", parameters);
        
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, $"v1/klines/history?{queryString}");
        return await _httpClient.SendRequestAsync<Kline>(requestMessage);
    }
}