using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace No_Overspend_Api.Infra.Models
{
    [Table("roles")]
    public class role : ModelBase
    {
        public string name { get; set; } = null!;
        public string? description { get; set; } = null!;
    }
    public class role_configuration : IEntityTypeConfiguration<role>
    {
        public void Configure(EntityTypeBuilder<role> builder)
        {
        }
    }
}
