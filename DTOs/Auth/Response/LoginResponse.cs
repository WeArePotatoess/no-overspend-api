namespace No_Overspend_Api.DTOs.Auth.Response
{
    public class LoginResponse
    {
        public string access_token { get; set; } = null!;
        public string refresh_token { get; set; } = null!;
        public string user_id { get; set; } = null!;
        public string fullname { get; set; } = null!;
    }
}
