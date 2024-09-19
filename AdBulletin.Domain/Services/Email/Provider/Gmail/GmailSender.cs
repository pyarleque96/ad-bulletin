using AdBulletin.Common.Constants;
using AdBulletin.Infrastructure.CrossCutting.AppSettings;
using AdBulletin.Infrastructure.CrossCutting.AppSettings.Services;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;

namespace AdBulletin.Domain.Services.Email.Provider.Gmail;

public class GmailSender : IEmailSender
{
    private readonly GmailSetting _gmailOptions;
    private readonly ILogger<GmailSender> _logger;

    public GmailSender(IOptions<ExternalServicesSetting> options,
                       ILogger<GmailSender> logger)
    {
        _gmailOptions = options.Value.Email.Providers.Gmail;
        _logger = logger;
    }

    public async Task<bool> SendEmailAsync(EmailDto email)
    {
        try
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(Constants.System.App.Title, string.IsNullOrEmpty(email.from.email) ? _gmailOptions.Username : email.from.email));
            email.to.ForEach(x =>
            {
                message.To.Add(new MailboxAddress(x.name, x.email));
            });
            
            message.Subject = email.subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = email.body_html };
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(_gmailOptions.Host, _gmailOptions.Port, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate(_gmailOptions.Username, _gmailOptions.PasswordKey);

                _logger.LogInformation($"SMTPGmailProvider - SendEmailAsync() -- sending message");
                client.Send(message);
                client.Disconnect(true);
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"SMTPGmailProvider - SendEmailAsync() -- {ex.Message} -- {ex.StackTrace}");
            return false;
        }

    }
}
