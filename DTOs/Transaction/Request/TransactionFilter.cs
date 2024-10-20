using No_Overspend_Api.Base;
using No_Overspend_Api.Infra.Enums;

namespace No_Overspend_Api.DTOs.Transaction.Request
{
    public class TransactionFilter : Paging
    {
        public DateTime? from_date { get; set; }
        public DateTime? to_date { get; set; }
        public eType_Transaction? type { get; set; }
    }
}
