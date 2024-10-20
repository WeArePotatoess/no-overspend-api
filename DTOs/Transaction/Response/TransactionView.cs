namespace No_Overspend_Api.DTOs.Transaction.Response
{
    public class TransactionView : TransactionBase
    {
        public string id { get; set; } = null!;
        public string? category_name { get; set; }
    }
}
