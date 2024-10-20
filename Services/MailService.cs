namespace No_Overspend_Api.Services
{
    public interface IMailService
    {
        public Task SendResetPasswordToken(string email, string token);
    }
    public class MailService : IMailService
    {
        public Task SendResetPasswordToken(string email, string token)
        {
            throw new NotImplementedException();
        }
    }
}
