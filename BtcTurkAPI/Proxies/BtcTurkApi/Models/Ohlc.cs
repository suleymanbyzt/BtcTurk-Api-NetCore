using System.Text.Json.Serialization;

namespace BtcTurkAPI.Proxies.BtcTurkApi.Models;

public class Ohlc
{
    [JsonPropertyName("pair")]
    public string Pair { get; set; }

    [JsonPropertyName("time")]
    public long Time { get; set; }

    [JsonPropertyName("open")]
    public decimal? Open { get; set; }

    [JsonPropertyName("high")]
    public decimal? High { get; set; }

    [JsonPropertyName("low")]
    public decimal? Low { get; set; }

    [JsonPropertyName("close")]
    public decimal? Close { get; set; }

    [JsonPropertyName("volume")]
    public decimal? Volume { get; set; }

    [JsonPropertyName("total")]
    public decimal? Total { get; set; }

    [JsonPropertyName("dailyChangeAmount")]
    public decimal? DailyChangeAmount { get; set; }

    [JsonPropertyName("dailyChangePercentage")]
    public decimal? DailyChangePercentage { get; set; }
}