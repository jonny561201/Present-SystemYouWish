using System.Net.Http.Json;

namespace WishSystem.External;

public interface IExternalClient
{
    Task Submit(List<Guid> users);
}

public class ExternalClient(HttpClient httpClient) : IExternalClient
{
    public async Task Submit(List<Guid> users)
    {
        var response = await httpClient.PostAsJsonAsync("post", users);

        response.EnsureSuccessStatusCode();
        
        Console.WriteLine("----- Submitted Successfully -----");
    }
}