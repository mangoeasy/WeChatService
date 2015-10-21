using System;
using System.Linq;
using WeChatService.Library.Models;
using WeChatService.Library.Services;

namespace WeChatService.Service.Services
{
    public class ProvinceService : BaseService, IProvinceService
    {
        public ProvinceService(WeChatServiceDataContext dbContext)
            : base(dbContext)
        {
        }

        public Province GetProvince(Guid id)
        {
            return DbContext.Provinces.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<Province> GetProvinces()
        {
            return DbContext.Provinces;
        }
    }
}
