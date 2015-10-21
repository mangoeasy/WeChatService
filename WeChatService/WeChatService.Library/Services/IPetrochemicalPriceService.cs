using System;
using System.Linq;
using WeChatService.Library.Models;

namespace WeChatService.Library.Services
{
    public interface IPetrochemicalPriceService : IDisposable
    {
        IQueryable<PetrochemicalPrice> GetPetrochemicalPrices();
    }
}
