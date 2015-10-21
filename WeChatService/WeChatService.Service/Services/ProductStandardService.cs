using System;
using System.Linq;
using WeChatService.Library.Models;
using WeChatService.Library.Services;

namespace WeChatService.Service.Services
{
    public class ProductStandardService : BaseService, IProductStandardService
    {
        public ProductStandardService(WeChatServiceDataContext dbContext)
            : base(dbContext)
        {
        }
        public ProductStandard GetProductStandard(Guid id)
        {
            return DbContext.ProductStandards.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<ProductStandard> GetProductStandards()
        {
            return DbContext.ProductStandards;
        }
    }
}
