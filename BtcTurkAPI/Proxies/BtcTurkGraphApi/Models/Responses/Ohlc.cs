using Newtonsoft.Json;

namespace BtcTurkAPI.Proxies.BtcTurkGraphApi.Models.Responses;

public class Ohlc
{
    [JsonProperty("pair")]
    public string Pair { get; set; }

    [JsonProperty("time")]
    public long Time { get; set; }

    [JsonProperty("open")]
    public decimal? Open { get; set; }

    [JsonProperty("high")]
    public decimal? High { get; set; }

    [JsonProperty("low")]
    public decimal? Low { get; set; }

    [JsonProperty("close")]
    public decimal? Close { get; set; }

    [JsonProperty("volume")]
    public decimal? Volume { get; set; }

    [JsonProperty("total")]
    public decimal? Total { get; set; }

    [JsonProperty("dailyChangeAmount")]
    public decimal? DailyChangeAmount { get; set; }

    [JsonProperty("dailyChangePercentage")]
    public decimal? DailyChangePercentage { get; set; }
}