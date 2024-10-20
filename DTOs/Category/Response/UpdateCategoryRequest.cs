using System.ComponentModel.DataAnnotations;

namespace No_Overspend_Api.DTOs.Category.Response
{
    public class UpdateCategoryRequest : CreateCategoryRequest
    {
        [Required]
        public string id { get; set; } = null!;
    }
}
