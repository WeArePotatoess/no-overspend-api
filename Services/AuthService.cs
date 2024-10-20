using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.EntityFrameworkCore;
using No_Overspend_Api.DTOs.Auth.Request;
using No_Overspend_Api.DTOs.Auth.Response;
using No_Overspend_Api.HttpExceptions;
using No_Overspend_Api.Infra.Constants;
using No_Overspend_Api.Infra.Models;
using No_Overspend_Api.Util;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace No_Overspend_Api.Services
{
    public interface IAuthService
    {
        public Task<LoginResponse> LoginAsync(LoginRequest request);
        public Task<LoginResponse> RegisterAsync(RegisterRequest request);
        public Task<bool> ForgetPasswordAsync(ForgetPasswordRequest request);
        public Task<bool> ChangePasswordAsync(string userId, ChangePasswordRequest request);
        public Task<bool> ResetPasswordAsync(ResetPasswordRequest request);

    }
    public class AuthService : IAuthService
    {
        private readonly NoOverspendContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        public AuthService(NoOverspendContext context, IConfiguration configuration, IMailService mailService)
        {
            _configuration = configuration;
            _context = context;
            _mailService = mailService;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var account = await _context.accounts
                .AsNoTracking()
                .Include(e => e.user)
                .FirstOrDefaultAsync(e => e.email == request.email);
            if (account == null) throw new BadHttpRequestException(ErrorMessages.AccountNotExisted);
            var validLogin = Helper.Verify(request.password, account.password_hashed);
            if (!validLogin) throw new UnauthorizedAccessException(ErrorMessages.WrongPassword);

            var secretKey = _configuration["KeyHeaderToken:SecretKey"];
            var secretKeyRefreshToken = _configuration["KeyHeaderToken:SecretKeyRefreshToken"];
            var accessToken = Helper.CreateToken(account.user, secretKey);
            var refreshToken = Helper.CreateToken(account.user, secretKeyRefreshToken);
            return new LoginResponse
            {
                access_token = accessToken,
                refresh_token = refreshToken,
                user_id = account.user.id,
                fullname = account.user.fullname
            };
        }

        public async Task<LoginResponse> RegisterAsync(RegisterRequest request)
        {
            var emailExisted = await _context.accounts
                .AnyAsync(e => e.email == request.email);
            if (emailExisted) throw new BadHttpRequestException(ErrorMessages.EmailExisted);

            var passwordHashed = Helper.HashPassword(request.password);
            var userRole = await _context.roles
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.name == "USER");
            if (userRole == null) throw new Exception(ErrorMessages.Common);


            var user = new user
            {
                dob = request.dob,
                fullname = request.fullname,
                phone = request.phone,
                total_balance = 0,
                total_expense = 0,
            };

            var secretKey = _configuration["KeyHeaderToken:SecretKey"];
            var secretKeyRefreshToken = _configuration["KeyHeaderToken:SecretKeyRefreshToken"];
            var accessToken = Helper.CreateToken(user, secretKey);
            var refreshToken = Helper.CreateToken(user, secretKeyRefreshToken);

            var account = new account
            {
                email = request.email,
                password_hashed = passwordHashed,
                role_id = userRole.id,
                refresh_token = refreshToken,
            };
            user.account_id = account.id;
            account.user_id = user.id;

            return new LoginResponse
            {
                access_token = accessToken,
                refresh_token = refreshToken,
                fullname = user.fullname,
                user_id = user.id,
            };

        }

        public async Task<bool> ForgetPasswordAsync(ForgetPasswordRequest request)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var account = await _context.accounts
                        .FirstOrDefaultAsync(e => e.email == request.email);
                    if (account == null) throw new NotFoundException(ErrorMessages.AccountNotExisted);

                    var resetPasswordToken = Helper.GenerateResetPasswordToken();
                    account.forgot_password_token = Helper.HashPassword(resetPasswordToken);
                    await _mailService.SendResetPasswordToken(account.email, resetPasswordToken);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw ex;
                }
            }
        }

        public async Task<bool> ChangePasswordAsync(string userId, ChangePasswordRequest request)
        {
            var account = await _context.users
                .AsNoTracking()
                .Include(e => e.account)
                .Select(e => e.account)
                .FirstOrDefaultAsync(e => e.id == userId);
            if (account == null) throw new NotFoundException(ErrorMessages.NotFound);

            var correctPassword = Helper.Verify(request.old_password, account.password_hashed);
            if (!correctPassword) throw new BadHttpRequestException(ErrorMessages.WrongPassword);

            var hashedPassword = Helper.HashPassword(request.new_password);
            account.password_hashed = hashedPassword;
            account.Updated();
            _context.accounts.Update(account);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var account = await _context.accounts
                .FirstOrDefaultAsync(e => e.email == request.email);
            if (account == null) throw new BadHttpRequestException(ErrorMessages.AccountNotExisted);
            var correctToken = Helper.Verify(request.reset_password_token, account.forgot_password_token!);

            var hashedPassword = Helper.HashPassword(request.password);
            account.password_hashed = hashedPassword;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
