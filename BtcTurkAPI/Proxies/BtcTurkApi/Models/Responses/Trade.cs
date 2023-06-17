using System.Text.Json.Serialization;

namespace BtcTurkAPI.Proxies.BtcTurkApi.Models.Responses;

public class Trade : Base
{
    public List<TradesData> Data { get; set; }
}

public class TradesData
{
    [JsonPropertyName("pair")]
    public string Pair { get; set; }

    [JsonPropertyName("pairNormalized")]
    public string PairNormalized { get; set; }
    
    [JsonPropertyName("numerator")]
    public string Numerator { get; set; }

    [JsonPropertyName("denominator")]
    public string Denominator { get; set; }

    [JsonPropertyName("date")]
    public long Date { get; set; }

    [JsonPropertyName("price")]
    public string Price { get; set; }

    [JsonPropertyName("amount")]
    public string Amount { get; set; }
}