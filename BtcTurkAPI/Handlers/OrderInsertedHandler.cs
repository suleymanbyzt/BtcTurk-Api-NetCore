using BtcTurkAPI.Websocket.Models;
using MediatR;

namespace BtcTurkAPI.Handlers;

public class OrderInsertedHandler : INotificationHandler<OrderInserted>
{
    public async Task Handle(OrderInserted orderInserted, CancellationToken cancellationToken)
    {
        // do something
        throw new NotImplementedException();
    }
}