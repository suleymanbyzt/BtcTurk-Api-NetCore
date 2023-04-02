using BtcTurkAPI.Websocket.Models;
using MediatR;

namespace BtcTurkAPI.Handlers;

public class OrderDeletedHandler : INotificationHandler<OrderDeleted>
{
    public async Task Handle(OrderDeleted notification, CancellationToken cancellationToken)
    {
        //do something
        throw new NotImplementedException();
    }
}