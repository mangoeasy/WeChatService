using System;
using System.Linq;
using WeChatService.Library.Models;
using WeChatService.Library.Services;

namespace WeChatService.Service.Services
{
    public class QuotedPriceService : BaseService,  IQuotedPriceService
    {
        public QuotedPriceService(WeChatServiceDataContext dbContext)
            : base(dbContext)
        {
        }

        public void Insert(QuotedPrice quotedPrice)
        {
            DbContext.QuotedPrices.Add(quotedPrice);
            DbContext.SaveChanges();
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }

        public QuotedPrice GetQuotedPrice(Guid id)
        {
            return DbContext.QuotedPrices.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<QuotedPrice> GetQuotedPrices()
        {
            return DbContext.QuotedPrices;
        }
    }
}
