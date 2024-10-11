using AdBulletin.Common.Constants;
using AdBulletin.Domain.Data.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AdBulletin.Core.Services;

public class CustomSignInManager : SignInManager<ApplicationUser>
{
    public CustomSignInManager(
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
        IOptions<IdentityOptions> optionsAccessor,
        ILogger<SignInManager<ApplicationUser>> logger,
        IAuthenticationSchemeProvider schemes,
        IUserConfirmation<ApplicationUser> confirmation)
        : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
    {
    }

    public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
    {
        var user = await UserManager.FindByNameAsync(userName);

        // If not exists the user return Failed
        if (user == null)
        {
            return SignInResult.Failed;
        }

        // If User is Deleted Inactive will Not Allowed.
        if (user.StatusId == (int)Constants.Status.BasicStatus.Inactive ||
            user.StatusId == (int)Constants.Status.BasicStatus.Draft ||
            user.StatusId == (int)Constants.Status.BasicStatus.ToValidate ||
            user.StatusId == (int)Constants.Status.BasicStatus.Deleted)
        {
            return SignInResult.NotAllowed;
        }

        // If the user is not deleted, we continue with the normal login process.
        return await base.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
    }
}