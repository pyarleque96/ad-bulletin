using AdBulletin.Infrastructure.Transport;

namespace AdBulletin.Domain.Managers;

public interface IAdCategoryManager
{
    Task<BaseResponse<ListAdCategoryResult<AdCategoryDto>>> ListAsync();
}
