namespace No_Overspend_Api.DTOs.Category.Response
{
    public class CategoryView
    {
        public string id { get; set; } = null!;
        public string name { get; set; } = null!;
        public string? description { get; set; }
        public string icon_id { get; set; } = null!;
        public string icon_content { get; set; } = null!;
    }
}
