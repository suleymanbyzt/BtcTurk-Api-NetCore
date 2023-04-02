using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace BtcTurkAPI.Authentication;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public Task AuthenticateAsync(HttpRequestMessage request)
    {
        string? publicKey = _configuration["apiKey:publicKey"];
        string? privateKey = _configuration["apiKey:privateKey"];

        long nonce = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        string message = publicKey + nonce;
        using HMACSHA256 hmac = new HMACSHA256(Convert.FromBase64String(privateKey));
        byte[] signatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
        string signature = Convert.ToBase64String(signatureBytes);

        request.Headers.Add("X-PCK", publicKey);
        request.Headers.Add("X-Signature", signature);
        request.Headers.Add("X-Stamp", nonce.ToString());
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        return Task.CompletedTask;
    }
}