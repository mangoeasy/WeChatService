using System;
using System.Linq;
using WeChatService.Library.Models;
using WeChatService.Library.Services;

namespace WeChatService.Service.Services
{
    public class PriceService : BaseService, IPriceService
    {
        public PriceService(WeChatServiceDataContext dbContext)
            : base(dbContext)
        {
        }
        public void Insert(Price price)
        {
            DbContext.Prices.Add(price);
            DbContext.SaveChanges();
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }

        public Price GetPrice(Guid id)
        {
            return DbContext.Prices.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<Price> GetPrices()
        {
            return DbContext.Prices;
        }
    }
}
