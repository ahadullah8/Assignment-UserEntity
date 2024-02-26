namespace Assignment_UserEntity.Services.Contract
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
