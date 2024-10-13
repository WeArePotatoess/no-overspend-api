using No_Overspend_Api.DTOs.Auth.Request;

namespace No_Overspend_Api.Services
{
    public interface IAuthService
    {
        public Task<bool> LoginAsync(LoginRequest request);
        public Task<bool> RegisterAsync(RegisterRequest request);
        public Task<bool> ResetPasswordAsync(ResetPasswordRequest request);
    }
    public class AuthService : IAuthService
    {
        public Task<bool> LoginAsync(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterAsync(RegisterRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ResetPasswordAsync(ResetPasswordRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
