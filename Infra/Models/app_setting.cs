using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace No_Overspend_Api.Infra.Models
{
    [Table("app_settings")]
    public class app_setting : ModelBase
    {
        public string user_id { get; set; } = null!;
        public bool general_notification { get; set; } = false;
        public bool sound { get; set; } = true;
        public bool sound_call { get; set; } = false;
        public bool vibrate { get; set; } = false;
        public bool transaction_update { get; set; } = false;
        public bool expense_reminder { get; set; } = false;
        public bool budget_notifications { get; set; } = false;
        public bool low_balance_alerts { get; set; } = false;
    }
    public class app_setting_configuration : IEntityTypeConfiguration<app_setting>
    {
        public void Configure(EntityTypeBuilder<app_setting> builder)
        {
        }
    }
}
