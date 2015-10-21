using System.Linq;
using WeChatService.Library.Models;
using WeChatService.Library.Services;

namespace WeChatService.Service.Services
{
    public class PetrochemicalPriceService : BaseService, IPetrochemicalPriceService
    {
        public PetrochemicalPriceService(WeChatServiceDataContext dbContext)
            : base(dbContext)
        {
        }

        public IQueryable<PetrochemicalPrice> GetPetrochemicalPrices()
        {
            return DbContext.PetrochemicalPrices;
        }
    }
}
