using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace No_Overspend_Api.Infra.Models
{
    [Table("accounts")]
    public class account : ModelBase
    {
        public string email { get; set; } = null!;
        public string password_hashed { get; set; } = null!;
        public string? forgot_password_token { get; set; }
        public string? refresh_token { get; set; }
        public string? security_pin { get; set; }
        public string user_id { get; set; } = null!;
        public string role_id { get; set; } = null!;
        public user user { get; set; } = null!;
        public role role { get; set; } = null!;
    }
    public class account_configuration : IEntityTypeConfiguration<account>
    {
        public void Configure(EntityTypeBuilder<account> builder)
        {
            builder.HasOne(e => e.user)
                .WithOne(e => e.account)
                .HasForeignKey<account>(e => e.user_id)
                .IsRequired();
            builder.HasOne(e => e.role)
                .WithMany()
                .HasForeignKey(e => e.role_id)
                .IsRequired();
        }
    }
}
