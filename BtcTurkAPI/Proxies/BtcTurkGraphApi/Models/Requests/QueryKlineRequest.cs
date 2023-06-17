namespace BtcTurkAPI.Proxies.BtcTurkGraphApi.Models.Requests;

public class QueryKlineRequest
{
    public string PairSymbol { get; set; }

    public int Resolution { get; set; }

    public long From { get; set; }

    public long To { get; set; }
}