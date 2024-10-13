using Microsoft.IdentityModel.Tokens;
using No_Overspend_Api.Infra.Models;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace No_Overspend_Api.Util
{
    public class Helper
    {
        public static string CreateToken(user user, string secretKey)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.id),
                new Claim(ClaimTypes.Email, user.account.email),
                new Claim(ClaimTypes.Name, user.fullname),
                new Claim(ClaimTypes.Role, user.account.role.name)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
