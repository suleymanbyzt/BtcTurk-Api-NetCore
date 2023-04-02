using MediatR;

namespace BtcTurkAPI.Websocket.Models;

public class OrderDeleted: INotification
{
    public int Type { get; set; }

    public int PairId { get; set; }

    public string Symbol { get; set; }

    public long Id { get; set; }

    public int Method { get; set; }

    public long UserId { get; set; }

    public int OrderType { get; set; }

    public string Price { get; set; }

    public string Amount { get; set; }

    public string NumLeft { get; set; }

    public string DenomLeft { get; set; }

    public string NewOrderClientId { get; set; }
}