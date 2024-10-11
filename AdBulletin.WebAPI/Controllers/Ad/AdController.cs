using AdBulletin.Common.Constants;
using AdBulletin.Domain.Data.Entities;
using AdBulletin.Domain.Managers;
using AdBulletin.Infrastructure.Transport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdBulletin.WebAPI.Controllers;

[ApiController]
[Route("ad")]
public class AdController : BaseController
{
    private readonly IAdManager _adManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<AdCategoryController> _logger;

    public AdController(UserManager<ApplicationUser> userManager,
                        IAdManager adManager,
                        ILogger<AdCategoryController> logger)
    {
        _adManager = adManager;
        _userManager = userManager;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpGet("list")]
    public async Task<BaseResponse<BaseResult<IEnumerable<AdDto>>>> List(ListAdRequest request)
    {
        if (User.IsInRole(Constants.Roles.ADMIN))
        {
            return await _adManager.ListForAdminAsync(request);
        }

        return await _adManager.ListAsync(request);
    }
}
