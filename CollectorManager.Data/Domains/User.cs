namespace CollectorManager.Data.Domains;

public class User : BaseEntity
{
    public User()
    {
        Name = string.Empty;
        UserName = string.Empty;
        Password = string.Empty;
        PasswordSalt = string.Empty;
    }

    public string Name { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string PasswordSalt { get; set; }
}
