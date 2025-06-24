using cinemaratona.Models;
using cinemaratona.Repositories;

namespace cinemaratona.Services;

public class UserService(UserRepository userRepository, PasswordService passwordService)
{
    private readonly UserRepository _userRepository = userRepository;
    private readonly PasswordService _passwordService = passwordService;

    public List<User> List()
    {
        return _userRepository.List();
    }

    public User? Include(User user)
    {
        // Hash da senha antes de salvar no banco
        user.Password = _passwordService.HashPassword(user.Password);
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
        var existingUser = _userRepository.Find(user.Id);
        if (existingUser == null) return null;

        // Se estiver atualizando a senha, faça o hash novamente
        if (user.Password != existingUser.Password)
        {
            user.Password = _passwordService.HashPassword(user.Password);
        }

        return _userRepository.Update(user);
    }
  
    public User? Authenticate(string email, string password)
    {
        var user = _userRepository.List().FirstOrDefault(u => u.Email == email);
        
        if (user != null && IsPasswordValid(password, user))
        {
            return user;
        }
        
        return null;
    }

    private bool IsPasswordValid(string password, User user)
    {
        return _passwordService.VerifyPassword(password, user.Password);
    }
}