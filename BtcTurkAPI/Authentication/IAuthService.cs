namespace BtcTurkAPI.Authentication;

public interface IAuthService
{
    Task AuthenticateAsync(HttpRequestMessage request);
}