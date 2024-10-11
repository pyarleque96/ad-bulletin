namespace AdBulletin.Infrastructure.Transport;

public class ListAdParameter : BaseParameter
{
    public Guid? AdCategoryId { get; set; }
    public string? SearchString { get; set; }
    public bool PremiumFlag { get; set; }
}