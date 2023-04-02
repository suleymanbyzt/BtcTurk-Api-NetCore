using System.Text.Json.Serialization;

namespace BtcTurkAPI.Proxies.BtcTurkApi.Models;

public class ExchangeInfo : Base
{
    [JsonPropertyName("data")]
    public ExchangeInfoData Data { get; set; }
}

public class ExchangeInfoData
{
    [JsonPropertyName("timeZone")]
    public string TimeZone { get; set; }

    [JsonPropertyName("serverTime")]
    public long ServerTime { get; set; }

    [JsonPropertyName("symbols")]
    public List<Symbols> Symbols { get; set; }
}

public class Symbols
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("nameNormalized")]
    public string NameNormalized { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }
    
    [JsonPropertyName("numerator")]
    public string Numerator { get; set; }
    
    [JsonPropertyName("denominator")]
    public string Denominator { get; set; }
    
    [JsonPropertyName("denominatorScale")]
    public int DenominatorScale { get; set; }
    
    [JsonPropertyName("numeratorScale")]
    public int NumeratorScale { get; set; }
    
    [JsonPropertyName("hasFraction")]
    public bool HasFraction { get; set; }
    
    [JsonPropertyName("orderMethods")]
    public List<string> OrderMethods { get; set; }
    
    [JsonPropertyName("maximumOrderAmount")]
    public decimal? MaximumOrderAmount { get; set; }
    
    [JsonPropertyName("maximumLimitOrderPrice")]
    public decimal? MaximumLimitOrderPrice { get; set; }
    
    [JsonPropertyName("minimumLimitOrderPrice")]
    public decimal? MinimumLimitOrderPrice { get; set; }
}