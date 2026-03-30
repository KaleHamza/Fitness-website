using System.Security.Cryptography;
using System.Text;
using webbir.Data;
using webbir.Interfaces;
using webbir.Models;

namespace webbir.Services;

public class AuthService : IAuthService
{
    private const string FileName = "users.json";
    private readonly JsonStore _jsonStore;
    private readonly List<User> _users;

    public AuthService(JsonStore jsonStore)
    {
        _jsonStore = jsonStore;
        _users = _jsonStore.Load<User>(FileName);

        if (!_users.Any())
        {
            _users.Add(new User { Id = 1, Username = "admin", Email = "admin@fitness.com", PasswordHash = HashPassword("admin123"), CreatedDate = DateTime.Now });
            Persist();
        }
    }

    public bool Register(string username, string email, string password)
    {
        if (_users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            return false;

        var newUser = new User
        {
            Id = _users.Any() ? _users.Max(u => u.Id) + 1 : 1,
            Username = username,
            Email = email,
            PasswordHash = HashPassword(password),
            CreatedDate = DateTime.Now
        };

        _users.Add(newUser);
        Persist();

        return true;
    }

    public User? Login(string username, string password)
    {
        var user = _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        if (user == null) return null;

        if (VerifyPassword(password, user.PasswordHash))
            return user;

        return null;
    }

    public User? GetUserByUsername(string username) => _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

    public User? GetUserById(int userId) => _users.FirstOrDefault(u => u.Id == userId);

    public bool UpdateUserProfile(int userId, double? heightCm, double? weightKg, string programName, string about)
    {
        var user = GetUserById(userId);
        if (user == null) return false;

        user.HeightCm = heightCm;
        user.WeightKg = weightKg;
        user.ProgramName = programName;
        user.About = about;

        Persist();
        return true;
    }

    private void Persist() => _jsonStore.Save(FileName, _users);

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    private bool VerifyPassword(string password, string hash)
    {
        var hashOfInput = HashPassword(password);
        return hashOfInput.Equals(hash);
    }
}