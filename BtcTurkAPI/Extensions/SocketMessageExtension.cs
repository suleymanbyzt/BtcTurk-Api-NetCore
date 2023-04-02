using Newtonsoft.Json;

namespace BtcTurkAPI.Extensions;

public static class SocketMessageExtension
{
    public static string JoinRequest(this string pair, string channel)
    {
        object[] joinMessage =
        {
            151, new
            {
                type = 151,
                channel = channel,
                join = true,
                @event = pair
            }
        };
        
        return JsonConvert.SerializeObject(joinMessage);
    }
}