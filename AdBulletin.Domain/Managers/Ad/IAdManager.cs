using AdBulletin.Infrastructure.Transport;

namespace AdBulletin.Domain.Managers;

public interface IAdManager
{
    Task<BaseResponse<BaseResult<IEnumerable<AdDto>>>> ListAsync(ListAdRequest request);
    Task<BaseResponse<BaseResult<IEnumerable<AdDto>>>> ListForAdminAsync(ListAdRequest request);
}
