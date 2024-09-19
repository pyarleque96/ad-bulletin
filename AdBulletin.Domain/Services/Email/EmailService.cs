using AdBulletin.Domain.Services.Email.Provider;
using Microsoft.Extensions.Logging;
using System.Reflection;
using static AdBulletin.Common.Constants.Constants.Email;

namespace AdBulletin.Domain.Services.Email;

public class EmailService : IEmailService
{
    private readonly IEmailSender _emailProvider;
    private readonly ILogger<EmailService> _logger;

    //Config. de librería a utilizar en el constructor
    public EmailService(IEmailSender emailProvider,
                        ILogger<EmailService> logger)
    {
        _emailProvider = emailProvider;
        _logger = logger;
    }

    public async Task<bool> SendEmail(string From, List<string> To, string Subject,
                                      EmailTemplates Template, Dictionary<string, string> TemplatesValues)
    {
        string fromMail = ProcessFrom(From);
        List<EmailAddress> toMail = new();
        To.ForEach(e => toMail.Add(new EmailAddress() { email = e }));

        try
        {
            var email = new EmailDto()
            {
                from = fromMail.Contains(";") ?
                        new EmailAddress() { name = fromMail.Split(";")[0], email = fromMail.Split(";")[1] } :
                        new EmailAddress() { email = fromMail },
                to = To.Select(x => new EmailAddress() { email = x }).ToList(),
                subject = Subject,
                body_html = ProcessTemplate(Template, TemplatesValues),
            };

            return await _emailProvider.SendEmailAsync(email);
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"EmailService - SendEmail() -- {ex.Message} -- {ex.StackTrace}");
            return false;
        }
    }

    private string ProcessTemplate(EmailTemplates template, Dictionary<string, string> templatesValues)
    {
        string body = string.Empty;
        var basePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var templatePath = $"{basePath}/{GetTemplatePath(template)}";

        _logger.LogInformation($"EmailService - ProcessTemplate() -- {templatePath}");
        Console.WriteLine(templatePath);

        using (StreamReader reader = new StreamReader(templatePath)) { body = reader.ReadToEnd(); }

        foreach (string key in templatesValues.Keys)
        {
            body = body.Replace(key, templatesValues[key]);
        }

        return body;
    }

    private static string GetTemplatePath(EmailTemplates template) => template switch
    {
        EmailTemplates.Welcoming => @"Services/Email/Templates/Welcoming.html",
        EmailTemplates.ResetPassword => @"Services/Email/Templates/ResetPassword.html"
    };

    private string ProcessFrom(string From)
    {
        if (From.Contains("<") && From.Contains(">") && From.Split("<").Count() == 2 && From.Split(">").Count() == 1)
            return $"{From.Split("<")[0]};{From.Split("<")[1].Replace(">", string.Empty)}"; //Usuario<usuario@lirmi.com> => Usuario;usuario@lirmi.com
        else
            return From.Replace("<", string.Empty).Replace(">", string.Empty);
    }

}