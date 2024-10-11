using AdBulletin.Domain.Data.Entities;

namespace AdBulletin.Domain.Contracts.Aggregates;

public interface IAdCategoryRepository
{
    Task<IEnumerable<AdCategory>> List();
}
