namespace AdBulletin.Infrastructure.Transport;

public class AdCategoryDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string InternalCode { get; set; }
    public string Icon { get; set; }
    public string? MiniIcon { get; set; }
    public int TotalAds { get; set; }
    public string? Keywords { get; set; }
    public int? Hierarchy { get; set; }
    public int StatusId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}