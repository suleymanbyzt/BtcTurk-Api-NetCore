using BtcTurkAPI.Websocket.Models;
using MediatR;

namespace BtcTurkAPI.Handlers;

public class UserTradeHandler : INotificationHandler<UserTrade>
{
    public async Task Handle(UserTrade userTrade, CancellationToken cancellationToken)
    {
        //do something
        throw new NotImplementedException();
    }
}