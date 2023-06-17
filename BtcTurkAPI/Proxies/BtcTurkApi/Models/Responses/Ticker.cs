using System.Text.Json.Serialization;

namespace BtcTurkAPI.Proxies.BtcTurkApi.Models.Responses;

public class Ticker : Base
{
    [JsonPropertyName("data")]
    public List<TickerData> Data { get; set; }
}

public class TickerData
{
    [JsonPropertyName("pair")]
    public string Pair { get; set; }

    [JsonPropertyName("pairNormalized")]
    public string PairNormalized { get; set; }

    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }

    [JsonPropertyName("last")]
    public decimal? Last { get; set; }

    [JsonPropertyName("high")]
    public decimal? High { get; set; }

    [JsonPropertyName("low")]
    public decimal? Low { get; set; }

    [JsonPropertyName("bid")]
    public decimal? Bid { get; set; }

    [JsonPropertyName("ask")]
    public decimal? Ask { get; set; }

    [JsonPropertyName("open")]
    public decimal? Open { get; set; }

    [JsonPropertyName("volume")]
    public decimal? Volume { get; set; }

    [JsonPropertyName("average")]
    public decimal? Avarage { get; set; }

    [JsonPropertyName("daily")]
    public decimal? Daily { get; set; }

    [JsonPropertyName("dailyPercent")]
    public decimal? DailyPercent { get; set; }

    [JsonPropertyName("denominatorSymbol")]
    public string DenominatorSymbol { get; set; }

    [JsonPropertyName("numeratorSymbol")]
    public string NumeratorSymbol { get; set; }
}