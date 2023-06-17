using System.Text.Json.Serialization;

namespace BtcTurkAPI.Proxies.BtcTurkApi.Models.Responses;

public class OrderBook
{
    [JsonPropertyName("data")]
    public OrderBookData Data { get; set; }

    public string Message { get; set; }

    public bool Success { get; set; }
}

public class OrderBookData
{
    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }

    [JsonPropertyName("bids")]
    public List<List<string>> Bids { get; set; }

    [JsonPropertyName("asks")]
    public List<List<string>> Asks { get; set; }
}