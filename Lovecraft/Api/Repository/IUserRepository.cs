using Lovecraft.Model;
using Lovecraft.Model.PublicApi;

namespace Lovecraft.Api.Repository;

public interface IUserRepository
{
    public User? Login(string email, string password);
    public User? Add(PublicApi_UserModel model);
    public User GetById(int userId);
    public bool UserEmailAlreadyExist(string email);
    public bool IsUserAdmin(int userId);
    public void Update(PublicApi_UserModel model);
}