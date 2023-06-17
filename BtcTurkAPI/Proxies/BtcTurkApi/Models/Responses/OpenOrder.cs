namespace BtcTurkAPI.Proxies.BtcTurkApi.Models.Responses;

public class OpenOrder : Base
{
    public OpenOrderData Data { get; set; }
}

public class OpenOrderData
{
    public List<Side> Asks { get; set; }
    public List<Side> Bids { get; set; }
}

public class Side
{
    public long Id { get; set; }

    public string Price { get; set; }

    public string Amount { get; set; }

    public string StopPrice { get; set; }

    public string PairSymbol { get; set; }

    public string PairSymbolNormalized { get; set; }

    public string Type { get; set; }

    public string Method { get; set; }

    public string OrderClientId { get; set; }

    public long Time  { get; set; }

    public long UpdatedTime { get; set; }

    public string Status { get; set; }

    public string LeftAmount { get; set; }
}