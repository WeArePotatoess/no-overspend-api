using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace No_Overspend_Api.Infra.Models
{
    [Table("transactions")]
    public class transaction : ModelBase
    {
        public string user_id { get; set; } = null!;
        public string category_id { get; set; } = null!;
        public decimal amount { get; set; }
        public int type { get; set; }
        public DateTime transaction_date { get; set; }
    }
    public class transaction_configuration : IEntityTypeConfiguration<transaction>
    {
        public void Configure(EntityTypeBuilder<transaction> builder)
        {

        }
    }
}
