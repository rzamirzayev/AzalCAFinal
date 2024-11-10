namespace AzalCAFinal.AppCode.Services
{
    public interface IEmailService
    {
        Task SendEmail(string to,string subject,string body);
    }
}
