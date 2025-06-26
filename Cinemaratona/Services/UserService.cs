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
        if (!IsValidEmail(user.Email))
        {
            throw new ArgumentException("Formato de email inválido.");
        }

        if (!IsStrongPassword(user.Password))
        {
            throw new ArgumentException("A senha não atende aos requisitos de segurança.");
        }

        user.Password = _passwordService.HashPassword(user.Password);
        return _userRepository.Include(user);
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private bool IsStrongPassword(string password)
    {
        return password.Length >= 8
               && System.Text.RegularExpressions.Regex.IsMatch(password, "[A-Z]")
               && System.Text.RegularExpressions.Regex.IsMatch(password, "[a-z]")
               && System.Text.RegularExpressions.Regex.IsMatch(password, "\\d")
               && System.Text.RegularExpressions.Regex.IsMatch(password, "[^a-zA-Z0-9]");
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