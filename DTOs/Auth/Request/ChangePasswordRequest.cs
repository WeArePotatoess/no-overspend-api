using No_Overspend_Api.Infra.Constants;
using System.ComponentModel.DataAnnotations;

namespace No_Overspend_Api.DTOs.Auth.Request
{
    public class ChangePasswordRequest
    {
        [Required]
        public string old_password { get; set; } = null!;
        [Required]
        public string new_password { get; set; } = null!;
        [Compare(nameof(new_password), ErrorMessage = ErrorMessages.RePasswordNotMatch),]
        public string re_new_password { get; set; } = null!;
    }
}
