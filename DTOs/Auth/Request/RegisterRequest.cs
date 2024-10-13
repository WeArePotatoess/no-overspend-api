using No_Overspend_Api.Infra.Constants;
using System.ComponentModel.DataAnnotations;

namespace No_Overspend_Api.DTOs.Auth.Request
{
    public class RegisterRequest
    {
        [Required, StringLength(255)]
        public string fullname { get; set; } = null!;
        [Required, EmailAddress]
        public string email { get; set; } = null!;
        [Required, Phone]
        public string phone { get; set; } = null!;
        [Required]
        public DateTime dob { get; set; }
        [Required]
        public string password { get; set; } = null!;
        [Required, Compare(nameof(password), ErrorMessage = ErrorMessages.RePasswordNotMatch)]
        public string re_password { get; set; } = null!;
    }
}
