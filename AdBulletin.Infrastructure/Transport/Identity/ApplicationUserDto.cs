namespace AdBulletin.Infrastructure.Transport;

public class ApplicationUserDto
{
    public long UserId { get; set; }
    public string? GivenName { get; set; }
    public string? LastName { get; set; }
    public string? AvatarUrl { get; set; }
    public int StatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
