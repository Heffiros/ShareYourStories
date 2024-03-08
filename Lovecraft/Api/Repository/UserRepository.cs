using Isopoh.Cryptography.Argon2;
using Lovecraft.Datas;
using Lovecraft.Model;
using Lovecraft.Model.PublicApi;

namespace Lovecraft.Api.Repository;

public class UserRepository : IUserRepository
{
    readonly LovecraftDbContext _dbContext = new ();

    public UserRepository(LovecraftDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public User GetById(int userId)
    {
        return _dbContext.Users.FirstOrDefault(u => u.Id == userId);
    }

    public User Add(PublicApi_UserModel model)
    {
        User? user = new ();
        try
        {
            if (model.Password != null)
            {
                user = new User
                {
                    AuthorName = model.AuthorName,
                    Email = model.Email,
                    CreatedDate = DateTime.UtcNow,
                    Password = Argon2.Hash(model.Password),
                    IsAdmin = false
                };

                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return user;
    }

    public User? Login(string email, string password)
    {
        User? user =  _dbContext.Users
            .FirstOrDefault(b => b.Email == email);

        if (user != null)
        {
            if (Argon2.Verify(user.Password, password))
            {
                return user;
            }
        }
        return null;
    }

    public bool UserEmailAlreadyExist(string email)
    {
        return _dbContext.Users.Any(u => u.Email == email);
    }

    public void Update(PublicApi_UserModel model)
    {
	    User? user = _dbContext.Users
		    .FirstOrDefault(b => b.Id == model.Id);
	    if (user != null)
	    {
		    if (user.AuthorName != model.AuthorName)
		    {
                user.AuthorName = model.AuthorName;
		    }

		    if (user.Email != model.Email)
		    {
			    user.Email = model.Email;
		    }

		    if (user.ProfilePictureUrl != model.ProfilePictureUrl)
		    {
			    user.ProfilePictureUrl = model.ProfilePictureUrl;
		    }

			_dbContext.Users.Update(user);
		    _dbContext.SaveChanges();
		}
	}
}