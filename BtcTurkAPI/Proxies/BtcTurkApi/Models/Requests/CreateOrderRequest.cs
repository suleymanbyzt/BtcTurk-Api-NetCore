using BtcTurkAPI.Proxies.BtcTurkApi.Models.Enums;

namespace BtcTurkAPI.Proxies.BtcTurkApi.Models.Requests;

public class CreateOrderRequest
{
    public string Quantity { get; set; }

    public string? Price { get; set; }

    public string? NewOrderClientId { get; set; }

    public OrderMethod OrderMethod { get; set; }

    public OrderType OrderType { get; set; }

    public string PairSymbol { get; set; }
}