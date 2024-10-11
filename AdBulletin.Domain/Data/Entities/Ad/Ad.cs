using System.ComponentModel.DataAnnotations;

namespace AdBulletin.Domain.Data.Entities;

public class Ad
{
    [Key]
    public int Id { get; set; }
    public Guid AdCategoryId { get; set; }
    public long UserId { get; set; }
    public string Slug { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string BriefDescription { get; set; }
    public string? AddressLocation { get; set; }
    public string CityLocation { get; set; }
    public string? StateLocation { get; set; }
    public int TotalViews { get; set; }
    public int TotalReviews { get; set; }
    public decimal? Price { get; set; }
    public decimal? OfferPrice { get; set; }
    public string Thumbnail { get; set; }
    public string? SecondaryImages { get; set; }
    public string? Icon { get; set; }
    public string? BackgroundIconColor { get; set; }

    public string? Keywords { get; set; }
    public int? Hierarchy { get; set; }
    public bool IsPremium { get; set; }
    public bool IsPremiumBackgroundDisable { get; set; }
    public int TotalReports { get; set; }
    public string? FacebookURL { get; set; }
    public string? TikTokURL { get; set; }
    public string? YoutubeURL { get; set; }
    public int StatusId { get; set; }
    public DateTime EndsAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public AdCategory AdCategory { get; set; }
    public ApplicationUser OwnerUser { get; set; }
}