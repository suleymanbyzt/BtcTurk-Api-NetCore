using Newtonsoft.Json;

namespace BtcTurkAPI.Proxies.BtcTurkGraphApi.Models.Responses;

public class Kline
{
    [JsonProperty("s")]
    public string Success { get; set; }

    [JsonProperty("t")]
    public List<long> Timestamp { get; set; }

    [JsonProperty("o")]
    public List<decimal> Open { get; set; }
    
    [JsonProperty("h")]
    public List<decimal> High { get; set; }

    [JsonProperty("l")]
    public List<decimal> Low { get; set; }

    [JsonProperty("c")]
    public List<decimal> Close { get; set; }

    [JsonProperty("v")]
    public List<decimal> Volume { get; set; }
}