namespace BtcTurkAPI.Proxies.BtcTurkApi.Models.Responses;

public class CreateOrderResponse : Base
{
    public CreateOrderResponseData Data { get; set; }
}

public class CreateOrderResponseData
{
    public long Id { get; set; }

    public long Datetime { get; set; }

    public string Type { get; set; }

    public string Method { get; set; }

    public string Price { get; set; }

    public string StopPrice { get; set; }

    public string Quantity { get; set; }

    public string PairSymbol { get; set; }

    public string NewOrderClientId { get; set; }
}