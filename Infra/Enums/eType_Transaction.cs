using System.ComponentModel;

namespace No_Overspend_Api.Infra.Enums
{
    public enum eType_Transaction
    {
        [Description("Thu nhập")]
        Income = 0,
        [Description("Chi tiêu")]
        Expense = 1,
    }
}
