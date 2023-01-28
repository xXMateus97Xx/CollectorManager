namespace CollectorManager.Services.Users.DTOs;

internal class UserPassword
{
    public UserPassword()
    {
        Password = string.Empty;
        PasswordSalt = string.Empty;
    }

    public string Password { get; set; }
    public string PasswordSalt { get; set; }
}
