using static AdBulletin.Common.Constants.Constants.Email;

namespace AdBulletin.Domain.Services.Email;

public interface IEmailService
{
    Task<bool> SendEmail(string From, List<string> To, string Subject, EmailTemplates Template, Dictionary<string, string> TemplatesValues);
}