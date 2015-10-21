using System;
using System.Linq;
using WeChatService.Library.Models;
using WeChatService.Library.Services;

namespace WeChatService.Service.Services
{
    public class PriceTypeService : BaseService, IPriceTypeService
    {
        public PriceTypeService(WeChatServiceDataContext dbContext)
            : base(dbContext)
        {
        }
        public PriceType GetPriceType(Guid id)
        {
            return DbContext.PriceTypes.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<PriceType> GetPriceTypes()
        {
            return DbContext.PriceTypes;
        }
    }
}
