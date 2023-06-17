namespace BtcTurkAPI.Proxies.BtcTurkGraphApi.Models.Requests;

public class QueryOhlcRequest
{
    public string PairSymbol { get; set; }

    public long From { get; set; }

    public long To { get; set; }
}