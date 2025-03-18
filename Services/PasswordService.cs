using BC = BCrypt.Net.BCrypt;

namespace cinemaratona.Services;

public class PasswordService
{
    public string HashPassword(string password)
    {
        // Usando WorkFactor 12 - este é um bom equilíbrio entre segurança e performance
        return BC.HashPassword(password, workFactor: 12);
    }

    public bool VerifyPassword(string password, string hash)
    {
        return BC.Verify(password, hash);
    }
}
