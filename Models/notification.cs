using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace No_Overspend_Api.Models
{
    public class notification : ModelBase
    {
        public string user_id { get; set; } = null!;
        public string title { get; set; } = null!;
        public string message { get; set; } = null!;
        public int type { get; set; }
        public int status { get; set; }
        public string transaction_id { get; set; } = null!;
        public string? icon_id { get; set; }
        public user user { get; set; } = null!;
    }
    public class notification_configuration : IEntityTypeConfiguration<notification>
    {
        public void Configure(EntityTypeBuilder<notification> builder)
        {
            builder.HasOne(e => e.user)
                .WithMany(e => e.notifications)
                .HasForeignKey(e => e.user_id)
                .IsRequired();
        }
    }
}
