using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace No_Overspend_Api.Infra.Models
{
    [Table("savings")]
    public class saving : ModelBase
    {
        public string user_id { get; set; } = null!;
        public string title { get; set; } = null!;
        public decimal target_amount { get; set; }
        public decimal saved_amount { get; set; }
        public string icon_id { get; set; } = null!;
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set; }
        public icon icon { get; set; } = null!;
    }
    public class saving_configuration : IEntityTypeConfiguration<saving>
    {
        public void Configure(EntityTypeBuilder<saving> builder)
        {
            builder.HasOne(e => e.icon).WithMany().HasForeignKey(e => e.icon_id);
        }
    }
}
