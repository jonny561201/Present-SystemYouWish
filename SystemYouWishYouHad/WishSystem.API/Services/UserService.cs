using WishSystem.Data.Config;
using WishSystem.External;
using WishSystem.Shared.Models;

namespace WishSystem.API.Services;

public interface IUserService
{
    Task SubmitUsers(List<Guid> userIds);
    IEnumerable<UserResponse> GetUsers();
}

public class UserService(SystemYouWishContext context, IExternalClient client) : IUserService
{

    public async Task SubmitUsers(List<Guid> userIds)
    {
        var users = context.Users.Where(x => userIds.Contains(x.Id));

        var updatedUsers = users.Select(x => x.Id).ToList();

        await client.Submit(updatedUsers);
    }

    public IEnumerable<UserResponse> GetUsers()
    {
        return context.Users.Select(x => new UserResponse { Id = x.Id, FamilyName = x.FamilyName, GivenName = x.GivenName, Email = x.Email, MiddleName = x.MiddleName });
    }
}