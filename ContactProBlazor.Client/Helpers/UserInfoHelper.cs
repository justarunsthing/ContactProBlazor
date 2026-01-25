using System.Security.Claims;
using ContactProBlazor.Client.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace ContactProBlazor.Client.Helpers
{
    public static class UserInfoHelper
    {
        public static async Task<UserInfo?> GetUserInfoAsync(Task<AuthenticationState>? authStateTask)
        {
            if (authStateTask is null)
            {
                return null;
            }

            AuthenticationState authState = await authStateTask;
            ClaimsPrincipal user = authState.User;

            try
            {
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                var email = user.FindFirst(ClaimTypes.Email)!.Value;
                var firstName = user.FindFirst("FirstName")!.Value;
                var lastName = user.FindFirst("LastName")!.Value;

                return new UserInfo
                {
                    UserId = userId,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName
                };
            }
            catch
            {
                return null;
            }
        }

        public static UserInfo? GetUserInfo(AuthenticationState authState)
        {
            var userId = authState.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var email = authState.User.FindFirst(ClaimTypes.Email)!.Value;
            var firstName = authState.User.FindFirst("FirstName")!.Value;
            var lastName = authState.User.FindFirst("LastName")!.Value;
            var profilePictureUrl = authState.User.FindFirst(nameof(UserInfo.ProfilePictureUrl))!.Value;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(profilePictureUrl))
            {
                return null;
            }

            return new UserInfo
            {
                UserId = userId,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                ProfilePictureUrl = profilePictureUrl
            };
        }
    }
}