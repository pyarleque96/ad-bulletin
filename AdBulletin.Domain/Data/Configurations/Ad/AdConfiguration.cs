using AdBulletin.Common.Constants;
using AdBulletin.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdBulletin.Domain.Data.Configurations;

public class AdConfiguration : IEntityTypeConfiguration<Ad>
{
    public void Configure(EntityTypeBuilder<Ad> builder)
    {
        builder.ToTable("ads");

        builder.Property(i => i.Id).HasColumnName("id");

        builder.Property(e => e.AdCategoryId).HasColumnName("ad_category_id");
        builder.Property(e => e.UserId).HasColumnName("user_id");
        builder.Property(e => e.Slug).HasColumnName("slug");
        builder.Property(e => e.Name).HasColumnName("name");
        builder.Property(e => e.Description).HasColumnName("description");
        builder.Property(e => e.BriefDescription).HasColumnName("brief_description");
        builder.Property(e => e.AddressLocation).HasColumnName("address_location");
        builder.Property(e => e.CityLocation).HasColumnName("city_location");
        builder.Property(e => e.StateLocation).HasColumnName("state_location");
        builder.Property(e => e.TotalViews).HasColumnName("total_views");
        builder.Property(e => e.TotalReviews).HasColumnName("total_reviews");
        builder.Property(e => e.Price).HasColumnName("price");
        builder.Property(e => e.OfferPrice).HasColumnName("offer_price");
        builder.Property(e => e.Thumbnail).HasColumnName("thumbnail");
        builder.Property(e => e.SecondaryImages).HasColumnName("secondary_images");
        builder.Property(e => e.Icon).HasColumnName("icon");
        builder.Property(e => e.BackgroundIconColor).HasColumnName("background_icon_color");

        builder.Property(e => e.Keywords).HasColumnName("keywords");
        builder.Property(e => e.Hierarchy).HasColumnName("hierarchy");
        builder.Property(e => e.IsPremium).HasColumnName("is_premium").HasDefaultValue(false);
        builder.Property(e => e.IsPremiumBackgroundDisable).HasColumnName("is_premium_background_disable").HasDefaultValue(true);
        builder.Property(e => e.TotalReports).HasColumnName("total_reports");
        builder.Property(e => e.FacebookURL).HasColumnName("facebook_url");
        builder.Property(e => e.TikTokURL).HasColumnName("tiktok_url");
        builder.Property(e => e.YoutubeURL).HasColumnName("youtube_url");
        builder.Property(e => e.StatusId).HasColumnName("status_id");
        builder.Property(e => e.EndsAt).HasColumnName("ends_at");

        builder.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("timezone('utc'::text, now())");
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");

        builder.HasOne(e => e.OwnerUser).WithMany().HasForeignKey(e => e.UserId);
        builder.HasOne(e => e.AdCategory).WithMany().HasForeignKey(e => e.AdCategoryId);

        builder.HasIndex(e => e.Slug).IsUnique();

        builder.HasQueryFilter(p => p.StatusId != (int)Constants.Status.BasicStatus.Deleted);
    }
}