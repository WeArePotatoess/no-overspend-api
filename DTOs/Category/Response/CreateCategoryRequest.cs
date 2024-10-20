using System.ComponentModel.DataAnnotations;

namespace No_Overspend_Api.DTOs.Category.Response
{
    public class CreateCategoryRequest
    {
        [Required]
        public string name { get; set; } = null!;
        public string? description { get; set; }
        public string? icon_id { get; set; }
    }
}
