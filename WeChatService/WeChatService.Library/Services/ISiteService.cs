using System;
using System.Linq;
using WeChatService.Library.Models;

namespace WeChatService.Library.Services
{
    public interface ISiteService : IDisposable
    {
        void Insert(Site site);
        void Update();
        Site GetSite(Guid id);
        IQueryable<Site> GetSites();
    }
}
