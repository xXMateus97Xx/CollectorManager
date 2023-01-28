namespace CollectorManager.Services.Security;

public interface ISecurityService
{
    string CreatePasswordSalt();
    string HashPassword(string password, string salt);
    string HashBytes(ReadOnlySpan<byte> bytes);
}
