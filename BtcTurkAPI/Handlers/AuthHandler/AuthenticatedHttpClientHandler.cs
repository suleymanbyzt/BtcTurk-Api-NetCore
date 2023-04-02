using BtcTurkAPI.Authentication;

namespace BtcTurkAPI.Handlers.AuthHandler;

public class AuthenticatedHttpClientHandler :  DelegatingHandler
{
    private readonly IAuthService _authService;

    public AuthenticatedHttpClientHandler(IAuthService authService)
    {
        _authService = authService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        //HttpRequest handler
        //Header update handler for each request
        await _authService.AuthenticateAsync(request);

        return await base.SendAsync(request, cancellationToken);
    }
}