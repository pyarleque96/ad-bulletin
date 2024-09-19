namespace AdBulletin.Infrastructure.CrossCutting.AppSettings.Services;

public class EmailServiceSetting
{
    public ProvidersSettings Providers { get; set; }
}

public class ProvidersSettings
{
    public MailChimpSetting MailChimp { get; set; }
    public GmailSetting Gmail { get; set; }
}

public class MailChimpSetting
{
    public string Host { get; set; }
    public string Port { get; set; }
    public string NoReplyEmailFrom { get; set; }
    public string NameFrom { get; set; }
    public string APIKey { get; set; }
}

public class GmailSetting
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string NoReplyEmailFrom { get; set; }
    public string NameFrom { get; set; }
    public string Username { get; set; }
    public string PasswordKey { get; set; }
}