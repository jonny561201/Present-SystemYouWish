using System.Net.Http.Json;
using System.Text.Json;
using WishSystem.External.Config;

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

public class SqsExternalClient(SqsQueue queue) : IExternalClient
{
    public async Task Submit(List<Guid> users)
    {
        await queue.Client.SendMessageAsync(queue.Url, JsonSerializer.Serialize(users));

        Console.WriteLine("----- Queued Successfully -----");
    }
}
