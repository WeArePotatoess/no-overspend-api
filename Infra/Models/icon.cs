using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace No_Overspend_Api.Infra.Models
{
    public class icon : ModelBase
    {
        public string name { get; set; } = null!;
        public string content { get; set; } = null!;
    }
    public class icon_configuration : IEntityTypeConfiguration<icon>
    {
        public void Configure(EntityTypeBuilder<icon> builder)
        {

        }
    }
}
