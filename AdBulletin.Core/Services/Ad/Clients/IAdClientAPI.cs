using AdBulletin.Infrastructure.Transport;
using Refit;

namespace AdBulletin.Core.Services.Clients;

public interface IAdClientAPI
{
    [Get("/ad/list")]
    Task<BaseResponse<BaseResult<IEnumerable<AdDto>>>> List([Body] ListAdRequest request);
}
