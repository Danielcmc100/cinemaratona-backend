using cinemaratona.Models;
using cinemaratona.Repositories;

namespace cinemaratona.Services;

public class UserService(UserRepository userRepository)
{
    private readonly UserRepository _userRepository = userRepository;

    public List<User> List()
    {
        return _userRepository.List();
    }

    public User? Include(User user)
    {
        return _userRepository.Include(user);
    }

    public User? Find(int id)
    {
        return _userRepository.Find(id);
    }

    public User? Delete(int id)
    {
        return _userRepository.Delete(id);
    }

    public User? Update(User user)
    {
        return _userRepository.Update(user);
    }
}