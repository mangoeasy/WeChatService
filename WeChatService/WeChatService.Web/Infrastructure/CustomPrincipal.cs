using System.Security.Principal;
using WeChatService.Library.Models;

namespace WeChatService.Web.Infrastructure
{
    public class CustomPrincipal : IPrincipal
    {
        public  readonly CustomIdentity _identity;

        public CustomPrincipal(CustomIdentity identity)
        {
            this._identity = identity;
        }
        public IIdentity Identity
        {
            get
            {
                return this._identity;
            }
        }

        public bool IsInRole(string role)
        {
            return true;
        }
    }
}