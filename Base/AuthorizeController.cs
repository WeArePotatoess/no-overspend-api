using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace No_Overspend_Api.Base
{
    [ApiController]
    [Authorize]
    public class AuthorizeController : ControllerBase
    {
        protected UserHeader UserHeader
        {
            get
            {
                try
                {
                    return new UserHeader
                    {
                        user_id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "",
                        fullname = User.FindFirst(ClaimTypes.Name)?.Value ?? "",
                        role = User.FindFirst(ClaimTypes.Role)?.Value ?? "",
                        email = User.FindFirst(ClaimTypes.Email)?.Value ?? "",
                    };
                }
                catch (Exception)
                {
                    return new UserHeader();
                }
            }
        }
    }
}
