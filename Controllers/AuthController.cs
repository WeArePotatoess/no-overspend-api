using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using No_Overspend_Api.Base;
using No_Overspend_Api.DTOs.Auth.Request;
using No_Overspend_Api.DTOs.Auth.Response;
using No_Overspend_Api.DTOs.Base;
using No_Overspend_Api.Infra.Routes;
using No_Overspend_Api.Services;

namespace No_Overspend_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost(AuthRoutes.Login)]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            return Ok(new Response<LoginResponse>(await _authService.LoginAsync(request)));
        }
        [HttpPost(AuthRoutes.Register)]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            return Ok(new Response<LoginResponse>(await _authService.RegisterAsync(request)));
        }
        [HttpPost(AuthRoutes.ResetPassword)]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            return Ok(new Response<bool>(await _authService.ResetPasswordAsync(request)));
        }
    }
}
