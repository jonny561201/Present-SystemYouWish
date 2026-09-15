using System.Net.Http.Json;
using WishSystem.Shared.Models;

namespace WishSystem.External;

public interface IExternalClient
{
    Task<IEnumerable<UserResponse>> Submit(IEnumerable<UserResponse> users);
}

public class ExternalClient(HttpClient httpClient) : IExternalClient
{
    public async Task<IEnumerable<UserResponse>> Submit(IEnumerable<UserResponse> users)
    {
        var response = await httpClient.PostAsJsonAsync("post", users);

        response.EnsureSuccessStatusCode();
        
        return await response.Content.ReadFromJsonAsync<IEnumerable<UserResponse>>() ??  new List<UserResponse>();
    }
}