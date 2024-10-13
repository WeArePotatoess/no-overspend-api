using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace No_Overspend_Api.Infra.Models
{
    [Table("categories")]
    public class category : ModelBase
    {
        public string user_id { get; set; } = null!;
        public string name { get; set; } = null!;
        public string? description { get; set; }
        public string? icon_id { get; set; }
    }
    public class category_configuration : IEntityTypeConfiguration<category>
    {
        public void Configure(EntityTypeBuilder<category> builder)
        {

        }
    }
}
