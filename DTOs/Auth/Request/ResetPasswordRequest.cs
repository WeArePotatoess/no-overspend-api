using No_Overspend_Api.Infra.Constants;
using System.ComponentModel.DataAnnotations;

namespace No_Overspend_Api.DTOs.Auth.Request
{
    public class ForgetPasswordRequest
    {
        [Required, EmailAddress]
        public string email { get; set; } = null!;
    }
    public class ResetPasswordRequest
    {
        [Required, EmailAddress]
        public string email { set; get; } = null!;
        [Required]
        public string reset_password_token { get; set; } = null!;
        [Required]
        public string password { get; set; } = null!;
        [Required, Compare(nameof(password), ErrorMessage = ErrorMessages.RePasswordNotMatch)]
        public string re_password { get; set; } = null!;
    }
}
