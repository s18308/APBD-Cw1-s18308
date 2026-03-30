using APBD_Cw1_s18308.Model;

namespace APBD_Cw1_s18308.Services;

public class UserService : IUserService
{
    private readonly List<User> _users = new List<User>();

    public void Add(User user)
    {
        _users.Add(user);
    }

    public List<User> GetAll()
    {
        return new List<User>(_users);
    }

    public User? GetById(int id)
    {
        foreach (User user in _users)
        {
            if (user.Id == id)
            {
                return user;
            }
        }
        return null;
    }
}

