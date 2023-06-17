using BtcTurkAPI.Proxies.BtcTurkGraphApi.Models.Requests;
using BtcTurkAPI.Proxies.BtcTurkGraphApi.Models.Responses;

namespace BtcTurkAPI.Proxies.BtcTurkGraphApi;

public interface IBtcTurkGraphApiProxy
{
    Task<List<Ohlc>> GetOhlc(QueryOhlcRequest request);

    Task<Kline> GetKlines(QueryKlineRequest request);
}