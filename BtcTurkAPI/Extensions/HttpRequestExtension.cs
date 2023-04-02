using Newtonsoft.Json;

namespace BtcTurkAPI.Extensions;

public static class HttpRequestExtension
{
    public static async Task<T> SendRequestAsync<T>(this HttpClient httpClient, HttpRequestMessage request)
    {
        HttpResponseMessage response = await httpClient.SendAsync(request);
        
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content);
        }

        //Log here
        throw new HttpRequestException("");
    }
}