using No_Overspend_Api.DTOs.Auth.Request;
using No_Overspend_Api.DTOs.Auth.Response;

namespace No_Overspend_Api.Services
{
    public interface IAuthService
    {
        public Task<LoginResponse> LoginAsync(LoginRequest request);
        public Task<LoginResponse> RegisterAsync(RegisterRequest request);
        public Task<bool> ResetPasswordAsync(ResetPasswordRequest request);

    }
    public class AuthService : IAuthService
    {
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResponse> RegisterAsync(RegisterRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ResetPasswordAsync(ResetPasswordRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
