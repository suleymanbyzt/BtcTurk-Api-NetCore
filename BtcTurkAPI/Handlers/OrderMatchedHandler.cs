using BtcTurkAPI.Websocket.Models;
using MediatR;

namespace BtcTurkAPI.Handlers;

public class OrderMatchedHandler : INotificationHandler<OrderMatched>
{
    public async Task Handle(OrderMatched orderMatched, CancellationToken cancellationToken)
    {
        //do something
        throw new NotImplementedException();
    }
}