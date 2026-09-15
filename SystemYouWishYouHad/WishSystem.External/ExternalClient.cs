using System.Net.Http.Json;
using WishSystem.External.Model;

namespace WishSystem.External;

public interface IExternalClient
{
    Task<IEnumerable<Guid>> Submit(IEnumerable<Guid> users);
}

public class ExternalClient(HttpClient httpClient) : IExternalClient
{
    public async Task<IEnumerable<Guid>> Submit(IEnumerable<Guid> users)
    {
        var response = await httpClient.PostAsJsonAsync("post", users);

        response.EnsureSuccessStatusCode();
        
        var content =  await response.Content.ReadFromJsonAsync<ExternalResponse>();

        return content?.Data ?? new List<Guid>();
    }
}