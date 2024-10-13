using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace No_Overspend_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public AuthController(ILogger<AuthController> logger) : base(logger) { }
    }
}
