using System;
using System.Linq;
using WeChatService.Library.Models;
using WeChatService.Library.Services;

namespace WeChatService.Service.Services
{
    public class SiteService : BaseService, ISiteService
    {
        public SiteService(WeChatServiceDataContext dbContext)
            : base(dbContext)
        {
        }
        public void Insert(Site site)
        {
            DbContext.Sites.Add(site);
            DbContext.SaveChanges();
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }

        public Site GetSite(Guid id)
        {
            return DbContext.Sites.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<Site> GetSites()
        {
            return DbContext.Sites;
        }
    }
}
