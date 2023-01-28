namespace CollectorManager.Services.Security;

public class SecuritySettings
{
    public SecuritySettings()
    {
        HashAlgorithm = string.Empty;
    }

    public int PasswordSaltSize { get; set; }
    public string HashAlgorithm { get; set; }
}
