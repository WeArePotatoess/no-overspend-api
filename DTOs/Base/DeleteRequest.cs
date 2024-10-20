using System.ComponentModel.DataAnnotations;

namespace No_Overspend_Api.DTOs.Base
{
    public class DeleteRequest
    {
        [Required]
        public string id { get; set; } = null!;
    }
}
