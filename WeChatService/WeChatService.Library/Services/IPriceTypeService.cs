using System;
using System.Linq;
using WeChatService.Library.Models;

namespace WeChatService.Library.Services
{
    public interface IPriceTypeService : IDisposable
    {
        PriceType GetPriceType(Guid id);
        IQueryable<PriceType> GetPriceTypes();
    }
}
