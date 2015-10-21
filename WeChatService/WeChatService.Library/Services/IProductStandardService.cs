using System;
using System.Linq;
using WeChatService.Library.Models;

namespace WeChatService.Library.Services
{
    public interface IProductStandardService : IDisposable
    {
        ProductStandard GetProductStandard(Guid id);
        IQueryable<ProductStandard> GetProductStandards();
    }
}
