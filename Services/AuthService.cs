using Microsoft.EntityFrameworkCore;
using No_Overspend_Api.DTOs.Auth.Request;
using No_Overspend_Api.DTOs.Auth.Response;
using No_Overspend_Api.Infra.Constants;
using No_Overspend_Api.Infra.Models;

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
        private readonly NoOverspendContext _context;
        public AuthService(NoOverspendContext context)
        {

            _context = context;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var account = await _context.accounts
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.email == request.email);
            if (account == null) throw new BadHttpRequestException(ErrorMessages.AccountNotExisted);

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
