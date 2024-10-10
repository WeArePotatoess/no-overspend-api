using System.ComponentModel.DataAnnotations;

namespace No_Overspend_Api.Models
{
    public abstract class ModelBase
    {
        [Key]
        public virtual string id { get; set; } = Guid.NewGuid().ToString();
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public DateTime? updated_at { get; set; }
        public DateTime? deleted_at { get; set; }
        public bool deleted { get; set; } = false;
    }
    public static class ModelBaseExtension
    {
        public static void Updated<T>(this T entity) where T : ModelBase
        {
            entity.updated_at = DateTime.UtcNow;
        }
        public static void SoftRemove<T>(this T entity) where T : ModelBase
        {
            entity.deleted = true;
            entity.deleted_at = DateTime.UtcNow;
        }
    }
}
