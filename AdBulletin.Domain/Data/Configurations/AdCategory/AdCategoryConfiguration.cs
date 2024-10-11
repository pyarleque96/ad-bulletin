using AdBulletin.Common.Constants;
using AdBulletin.Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdBulletin.Domain.Data.Configurations;

public class AdCategoryConfiguration : IEntityTypeConfiguration<AdCategory>
{
    public void Configure(EntityTypeBuilder<AdCategory> builder)
    {
        builder.ToTable("ad_categories");

        builder.Property(i => i.Id).HasColumnName("id");

        builder.Property(e => e.Name).HasColumnName("name");
        builder.Property(e => e.InternalCode).HasColumnName("internal_code");
        builder.Property(e => e.Icon).HasColumnName("icon");
        builder.Property(e => e.MiniIcon).HasColumnName("mini_icon");
        builder.Property(e => e.Hierarchy).HasColumnName("hierarchy");
        builder.Property(e => e.TotalAds).HasColumnName("total_ads");
        builder.Property(e => e.Keywords).HasColumnName("keywords");
        builder.Property(e => e.StatusId).HasColumnName("status_id");

        builder.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("timezone('utc'::text, now())");
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at");

        builder.HasQueryFilter(p => p.StatusId == (int)Constants.Status.BasicStatus.Active);
    }
}
