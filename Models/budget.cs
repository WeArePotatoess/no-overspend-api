using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace No_Overspend_Api.Models
{
    [Table("budgets")]
    public class budget : ModelBase
    {
        public string user_id { get; set; } = null!;
        public string category_id { get; set; } = null!;
        public decimal amount { get; set; }
        public DateTime from_date { get; set; }
        public DateTime to_date { get; set; }
    }
    public class budget_configuration : IEntityTypeConfiguration<budget>
    {
        public void Configure(EntityTypeBuilder<budget> builder)
        {

        }
    }
}
