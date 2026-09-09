using System.API.Models;
using System.Data.Config;

namespace System.API.Services;

public interface IUserService
{
    UserResponse? GetUser(Guid userId);
    IEnumerable<UserResponse> SubmitUsers(List<Guid> userIds);
}

public class UserService(SystemYouWishContext context) : IUserService
{
    public UserResponse? GetUser(Guid userId)
    {
        var user = context.Users.FirstOrDefault(x => x.Id == userId);
        
        return user == null 
            ? null 
            : new UserResponse { FamilyName = user.FamilyName, GivenName = user.GivenName, Email =  user.Email, MiddleName = user.MiddleName };
    }

    public IEnumerable<UserResponse> SubmitUsers(List<Guid> userIds)
    {
        var users = context.Users.Where(x => userIds.Contains(x.Id));

        return users.Select(x => new UserResponse { FamilyName = x.FamilyName, GivenName = x.GivenName, Email =  x.Email, MiddleName = x.MiddleName});
    }
}