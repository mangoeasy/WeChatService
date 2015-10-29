using System.Security.Principal;
using WeChatService.Library.Models;

namespace WeChatService.Web.Infrastructure
{
    public class CustomIdentity : IIdentity
    {
        private readonly Account _user;

        public CustomIdentity(Account user)
        {
            _user = user;
        }

        public Account User
        {
            get { return _user; }
        }
        
        public string Name
        {
            get
            {
                return _user == null ? "" : _user.Id.ToString();
            }
        }

        public string AuthenticationType
        {
            get
            {
                return "Custom";
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return _user != null;
            }
        }
    }
}