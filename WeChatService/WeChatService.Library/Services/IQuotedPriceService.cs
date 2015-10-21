using System;
using System.Linq;
using WeChatService.Library.Models;

namespace WeChatService.Library.Services
{
    public interface IQuotedPriceService : IDisposable
    {
        void Insert(QuotedPrice quotedPrice);
        void Update();
        QuotedPrice GetQuotedPrice(Guid id);
        IQueryable<QuotedPrice> GetQuotedPrices();
    }
}
