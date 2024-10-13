using System.ComponentModel.DataAnnotations;

namespace No_Overspend_Api.DTOs.Auth.Request
{
    public class LoginRequest
    {
        [Required, EmailAddress]
        public string email { get; set; } = null!;
        [Required]
        public string password { get; set; } = null!;
    }
}
