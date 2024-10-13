using System.ComponentModel.DataAnnotations;

namespace No_Overspend_Api.DTOs.Auth.Request
{
    public class ResetPasswordRequest
    {
        [Required, EmailAddress]
        public string email { get; set; } = null!;
    }
}
