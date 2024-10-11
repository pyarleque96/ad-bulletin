using AdBulletin.Common.Constants;
using AdBulletin.Domain.Contracts.Aggregates;
using AdBulletin.Domain.Data.Context;
using AdBulletin.Domain.Data.Entities;
using AdBulletin.Domain.Extensions;
using AdBulletin.Infrastructure.Transport;
using Microsoft.EntityFrameworkCore;
using static AdBulletin.Common.Constants.Constants.Status;

namespace AdBulletin.Domain.Data.Repositories;

public class AdRepository : IAdRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public AdRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Ad?> Get(GetAdParameter parameter)
    {
        var query = GetBase(parameter);

        query = query.Where(x => x.StatusId == (int)BasicStatus.Active);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<Ad?> GetForAdmin(GetAdParameter parameter)
    {
        var query = GetBase(parameter);

        query = query.IgnoreQueryFilters();

        return await query.FirstOrDefaultAsync();
    }

    private IQueryable<Ad> GetBase(GetAdParameter parameter)
    {
        var query = _applicationDbContext.Ads.AsQueryable();

        if (string.IsNullOrEmpty(parameter.Slug))
        {
            var slugSearch = parameter.Slug.ToLowerInvariant();

            query = query.Where(x => !string.IsNullOrEmpty(x.Slug) && x.Slug.ToLower() == slugSearch);
        }

        return query;
    }

    public async Task<BaseResult<IEnumerable<Ad>>> List(ListAdParameter parameter)
    {
        var query = ListBase(parameter);
        query = query.Where(x => x.StatusId == (int)BasicStatus.Active &&
                                 x.TotalReports < Constants.Metrics.MIN_REPORTS_FOR_BAN);

        return await query.PaginateAsync(parameter.Pagination);
    }

    public async Task<BaseResult<IEnumerable<Ad>>> ListForAdmin(ListAdParameter parameter)
    {
        var query = ListBase(parameter);
        query = query.IgnoreQueryFilters();

        return await query.PaginateAsync(parameter.Pagination);
    }

    private IQueryable<Ad> ListBase(ListAdParameter parameter)
    {
        var query = _applicationDbContext.Ads.AsQueryable();

        if (parameter.AdCategoryId.HasValue)
        {
            query = query.Where(x => x.AdCategoryId == parameter.AdCategoryId);
        }

        if (!string.IsNullOrEmpty(parameter.SearchString))
        {
            var search = parameter.SearchString?.ToLowerInvariant();

            query = query.Where(x =>
                (!string.IsNullOrEmpty(x.Keywords) && x.Keywords.ToLower().Contains(search)) ||                // Search by Keywords
                (!string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Contains(search)) ||                        // Search by Name
                (!string.IsNullOrEmpty(x.AddressLocation) && x.AddressLocation.ToLower().Contains(search)) ||  // Search by Location
                (!string.IsNullOrEmpty(x.Slug) && x.Slug.ToLower().Contains(search))                           // Search by Slug
            );
        }

        if (parameter.PremiumFlag)
        {
            query = query.Where(x => x.IsPremium);
        }

        // Ordering based on priorities
        query = query
            .OrderByDescending(x => x.Hierarchy.HasValue)                                           // First, if it has a Hierarchy value
            .ThenByDescending(x => x.Hierarchy)                                                     // Higher Hierarchy value has priority
            .ThenByDescending(x => x.IsPremium)                                                     // Second, prioritize Premium ads
            .ThenByDescending(x => x.TotalReviews)                                                  // Third, more reviews take priority
            .ThenByDescending(x => x.TotalViews >= Constants.Metrics.MIN_VIEWS_FOR_TREND)           // Fourth, only consider ads with "X" or more views
            .ThenBy(x => x.CreatedAt)                                                               // Fifth, prioritize older ads (oldest first)
            .ThenByDescending(x => x.TotalReports > Constants.Metrics.MIN_REPORTS_FOR_SHADOW_BAN)   // Last, ads with more than "X" reports (highest reports go to the bottom)
            .ThenBy(x => x.TotalReports);                                                           // Tiebreaker by number of reports

        return query;
    }
}