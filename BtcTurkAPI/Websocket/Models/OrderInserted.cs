using MediatR;

namespace BtcTurkAPI.Websocket.Models;

public class OrderInserted: INotification
{
    public string Symbol { get; set; }

    public long Id { get; set; }

    public string Amount { get; set; }

    public string Price { get; set; }
        
    public string NewOrderClientId { get; set; }
}