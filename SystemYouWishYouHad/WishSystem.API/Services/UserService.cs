using WishSystem.Data.Config;
using WishSystem.External;
using WishSystem.Shared.Models;

namespace WishSystem.API.Services;

public interface IUserService
{
    UserResponse? GetUser(Guid userId);
    Task<List<Guid>> SubmitUsers(List<Guid> userIds);
    IEnumerable<UserResponse> GetUsers();
}

public class UserService(SystemYouWishContext context, IExternalClient client) : IUserService
{
    public UserResponse? GetUser(Guid userId)
    {
        var user = context.Users.FirstOrDefault(x => x.Id == userId);
        
        return user == null 
            ? null 
            : new UserResponse { Id =user.Id, FamilyName = user.FamilyName, GivenName = user.GivenName, Email =  user.Email, MiddleName = user.MiddleName };
    }

    public async Task<List<Guid>> SubmitUsers(List<Guid> userIds)
    {
        var users = context.Users.Where(x => userIds.Contains(x.Id));

        var updatedUsers = users.Select(x => x.Id).ToList();

        return await client.Submit(updatedUsers);
    }

    public IEnumerable<UserResponse> GetUsers()
    {
        return context.Users.Select(x => new UserResponse { Id = x.Id, FamilyName = x.FamilyName, GivenName = x.GivenName, Email = x.Email, MiddleName = x.MiddleName });
    }
}