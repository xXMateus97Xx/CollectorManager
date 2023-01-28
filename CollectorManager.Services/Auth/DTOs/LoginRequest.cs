namespace CollectorManager.Services.Auth.DTOs;

public class LoginRequest
{
    public LoginRequest()
    {
        Username = string.Empty;
        Password = string.Empty;
    }

    public string Username { get; set; }
    public string Password { get; set; }
    public bool Remember { get; set; }
}
