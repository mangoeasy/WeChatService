using System.Security.Claims;
using Microsoft.AspNet.Identity;
using WeChatService.Web.Models;

namespace WeChatService.Web.Infrastructure
{
    public static class UserService
    {
        public static ClaimsIdentity CreateIdentity(UserModel user, string authenticationType)
        {
            var identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity"));
            identity.AddClaim(new Claim("DisplayName", user.Name));
            identity.AddClaim(new Claim("Id", user.Id.ToString()));
            return identity;
        }
    }
}