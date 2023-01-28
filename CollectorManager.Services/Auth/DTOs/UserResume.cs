namespace CollectorManager.Services.Auth.DTOs;

public class UserResume
{
    public UserResume()
    {
        Name = string.Empty;
        UserName = string.Empty;
    }

    public string Name { get; set; }
    public string UserName { get; set; }
}
