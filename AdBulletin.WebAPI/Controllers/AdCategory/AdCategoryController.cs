using AdBulletin.Domain.Managers;
using AdBulletin.Infrastructure.Transport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdBulletin.WebAPI.Controllers;

[ApiController]
[Route("ad_category")]
public class AdCategoryController : BaseController
{
    private readonly ILogger<AdCategoryController> _logger;
    private readonly IAdCategoryManager _adCategoryManager;

    public AdCategoryController(ILogger<AdCategoryController> logger,
                                     IAdCategoryManager adCategoryManager)
    {
        _logger = logger;
        _adCategoryManager = adCategoryManager;
    }

    [AllowAnonymous]
    [HttpGet("list")]
    public async Task<BaseResponse<ListAdCategoryResult<AdCategoryDto>>> List()
    {
        return await _adCategoryManager.ListAsync();
    }
}
