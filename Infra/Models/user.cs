using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace No_Overspend_Api.Infra.Models
{
    public class user : ModelBase
    {
        public string account_id { get; set; } = null!;
        public string fullname { get; set; } = null!;
        public string phone { get; set; } = null!;
        public DateTime dob { get; set; }
        public decimal total_balance { get; set; } = 0;
        public account account { get; set; } = null!;
        public ICollection<notification> notifications { get; set; } = null!;
    }
    public class user_configuration : IEntityTypeConfiguration<user>
    {
        public void Configure(EntityTypeBuilder<user> builder)
        {
            builder.HasOne(user => user.account)
                .WithOne(account => account.user)
                .HasForeignKey<user>(e => e.account_id)
                .IsRequired();
            builder.HasMany(e => e.notifications)
                .WithOne(e => e.user)
                .HasForeignKey(e => e.user_id);
        }
    }
}
