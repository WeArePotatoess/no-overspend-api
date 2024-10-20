using No_Overspend_Api.Infra.Enums;
using System.ComponentModel.DataAnnotations;

namespace No_Overspend_Api.DTOs.Transaction
{
    public class TransactionBase
    {
        [Required]
        public string category_id { get; set; } = null!;
        [Required]
        public decimal amount { get; set; }
        [Required]
        public eType_Transaction type { get; set; }
        [Required]
        public string title { get; set; } = null!;
        public string? message { get; set; }
        [Required]
        public DateTime transaction_date { get; set; }
    }
}
