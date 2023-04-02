using MediatR;

namespace BtcTurkAPI.Websocket.Models;

public class OrderMatched: INotification
{
    public string Symbol { get; set; }

    public bool IsBid { get; set; }

    public string Price { get; set; }

    public string Amount { get; set; }

    public string ClientId { get; set; }
}