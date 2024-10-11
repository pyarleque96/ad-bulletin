using AdBulletin.Domain.Contracts.Aggregates;
using AdBulletin.Domain.Data.Context;
using AdBulletin.Domain.Data.Entities;

namespace AdBulletin.Domain.Data.Repositories;

public class AdCategoryRepository : IAdCategoryRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public AdCategoryRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<IEnumerable<AdCategory>> List()
    {
        return _applicationDbContext.AdCategories;
    }
}
