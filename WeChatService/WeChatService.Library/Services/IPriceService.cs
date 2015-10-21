using System;
using System.Linq;
using WeChatService.Library.Models;

namespace WeChatService.Library.Services
{
    public interface IPriceService : IDisposable
    {
        void Insert(Price price);
        void Update();
        Price GetPrice(Guid id);
        IQueryable<Price> GetPrices();
    }
}
