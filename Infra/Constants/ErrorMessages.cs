namespace No_Overspend_Api.Infra.Constants
{
    public static class ErrorMessages
    {
        public const string NotFound = "Không tìm thấy dữ liệu";
        public const string Common = "Có lỗi xuất hiện, vui lòng thử lại";
        public const string RePasswordNotMatch = "Nhập lại mật khẩu không khớp";

        //Auth
        public const string AccountNotExisted = "Tài khoản không tồn tại";
        public const string EmailExisted = "Email đã tồn tại";
        public const string WrongPassword = "Sai mật khẩu";

        //Category
        public const string CategoryExisted = "Danh mục đã tồn tại";

    }
}
