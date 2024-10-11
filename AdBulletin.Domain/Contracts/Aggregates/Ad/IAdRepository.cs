using AdBulletin.Domain.Data.Entities;
using AdBulletin.Infrastructure.Transport;

namespace AdBulletin.Domain.Contracts.Aggregates;

public interface IAdRepository
{
    Task<Ad?> Get(GetAdParameter parameter);
    Task<Ad?> GetForAdmin(GetAdParameter parameter);
    Task<BaseResult<IEnumerable<Ad>>> List(ListAdParameter parameter);
    Task<BaseResult<IEnumerable<Ad>>> ListForAdmin(ListAdParameter parameter);
}