using No_Overspend_Api.DTOs.Auth.Request;

namespace No_Overspend_Api.Services
{
    public interface IAuthService
    {
        public Task<bool> Login(LoginRequest request);
        public Task<bool> Register(string email, string password);
    }
    public class AuthService : IAuthService
    {
    }
}
