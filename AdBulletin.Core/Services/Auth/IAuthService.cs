namespace AdBulletin.Core.Services;

public interface IAuthService
{
    Task<string> GetJwtTokenFromApi(string email, string password);
}
