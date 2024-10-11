#nullable disable

using AdBulletin.Infrastructure.CrossCutting.AppSettings.Services;

namespace AdBulletin.Infrastructure.CrossCutting.AppSettings;

public class ExternalServicesSetting
{
    public EmailServiceSetting Email { get; set; }
}
