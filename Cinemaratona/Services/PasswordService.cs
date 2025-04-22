using BC = BCrypt.Net.BCrypt;

namespace cinemaratona.Services;

public class PasswordService
{
    public string HashPassword(string password)
    {
        return BC.HashPassword(password, workFactor: 12);
    }

    public bool VerifyPassword(string password, string hash)
    {
        return BC.Verify(password, hash);
    }
}
