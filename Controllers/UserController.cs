﻿using Microsoft.AspNetCore.Mvc;
using No_Overspend_Api.Base;

namespace No_Overspend_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : AuthorizeController
    {
    }
}
