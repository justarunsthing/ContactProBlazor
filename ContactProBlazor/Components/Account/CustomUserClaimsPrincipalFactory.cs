using ContactProBlazor.Data;
using System.Security.Claims;
using ContactProBlazor.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using ContactProBlazor.Client.Models;

namespace ContactProBlazor.Components.Account
{
    public class CustomUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, IOptions<IdentityOptions> options)
        : UserClaimsPrincipalFactory<ApplicationUser>(userManager, options)
    {
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var profilePictureUrl = user.ProfilePictureId.HasValue 
                ? $"/uploads/{user.ProfilePictureId}"
                : ImageHelper.DefaultProfilePictureUrl;

            ClaimsIdentity identity = await base.GenerateClaimsAsync(user);
            List<Claim> customClaims =
            [
                new Claim("FirstName", user.FirstName ?? string.Empty),
                new Claim("LastName", user.LastName ?? string.Empty),
                new Claim(nameof(UserInfo.ProfilePictureUrl), profilePictureUrl)
            ];

            identity.AddClaims(customClaims);

            return identity;
        }
    }
}