using APBD_Cw1_s18308.Model;

namespace APBD_Cw1_s18308.Services;

public interface IUserService
{
    void Add(User user);
    List<User> GetAll();
    User? GetById(int id);
}
