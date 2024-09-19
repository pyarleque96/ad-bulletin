namespace AdBulletin.Domain.Services.Email;

public class EmailDto
{
    public Guid? email_id { get; set; }
    public EmailAddress from { get; set; }
    public List<EmailAddress> to { get; set; }
    public string subject { get; set; }
    public string body_plain_text { get; set; }
    public string body_html { get; set; }
    public Dictionary<string, string> templateValues { get; set; }
}

public class EmailAddress
{
    public string name { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
}