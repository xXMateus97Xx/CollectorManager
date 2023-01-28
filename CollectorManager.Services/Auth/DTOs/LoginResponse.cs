namespace CollectorManager.Services.Auth.DTOs;

public class LoginResponse
{
    public LoginResponse()
    {
        Username = string.Empty;
        Password = string.Empty;
        PasswordSalt = string.Empty;
    }

    public string Username { get; set; }
    public string Password { get; set; }
    public string PasswordSalt { get; set; }
}
