using System.ComponentModel.DataAnnotations;

namespace No_Overspend_Api.DTOs.Base
{
    public class GetDetailRequest
    {
        [Required]
        public string id { get; set; } = null!;
    }
}
