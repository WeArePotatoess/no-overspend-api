using System.ComponentModel.DataAnnotations;

namespace No_Overspend_Api.DTOs.Transaction.Request
{
    public class UpdateTransactionRequest : TransactionBase
    {
        [Required]
        public string id { get; set; } = null!;
    }
}
