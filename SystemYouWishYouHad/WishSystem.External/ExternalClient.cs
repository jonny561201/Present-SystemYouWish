using System.Net.Http.Json;
using System.Text.Json;
using Amazon.SQS;
using WishSystem.Shared.Config;
using WishSystem.External.Model;

namespace WishSystem.External;

public interface IExternalClient
{
    Task<List<Guid>> Submit(List<Guid> users);
}

public class ExternalClient(HttpClient httpClient) : IExternalClient
{
    public async Task<List<Guid>> Submit(List<Guid> users)
    {
        var response = await httpClient.PostAsJsonAsync("post", users);

        response.EnsureSuccessStatusCode();
        
        var content =  await response.Content.ReadFromJsonAsync<ExternalResponse>();
        
        Console.WriteLine("----- Submitted Successfully -----");

        return content?.Data ?? new List<Guid>();
    }
}