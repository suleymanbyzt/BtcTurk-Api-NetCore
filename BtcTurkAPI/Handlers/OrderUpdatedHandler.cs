using BtcTurkAPI.Websocket.Models;
using MediatR;

namespace BtcTurkAPI.Handlers;

public class OrderUpdatedHandler : INotificationHandler<OrderUpdated>
{
    public async Task Handle(OrderUpdated orderUpdated, CancellationToken cancellationToken)
    {
        //do something
        throw new NotImplementedException();
    }
}