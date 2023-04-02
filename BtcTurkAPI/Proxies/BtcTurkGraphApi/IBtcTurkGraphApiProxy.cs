using BtcTurkAPI.Proxies.BtcTurkApi.Models;

namespace BtcTurkAPI.Proxies.BtcTurkGraphApi;

public interface IBtcTurkGraphApiProxy
{
    Task<List<Ohlc>> GetOhlc(string pairSymbol, long from, long to);
}