namespace AdBulletin.Domain.Services.Email.Provider;

public interface IEmailSender
{
    Task<bool> SendEmailAsync(EmailDto email);
}