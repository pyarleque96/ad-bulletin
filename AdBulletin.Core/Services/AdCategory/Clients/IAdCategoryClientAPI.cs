using AdBulletin.Infrastructure.Transport;
using Refit;

namespace AdBulletin.Core.Services.Clients;

public interface IAdCategoryClientAPI
{
    [Get("/ad_category/list")]
    Task<BaseResponse<ListAdCategoryResult<AdCategoryDto>>> List();
}
