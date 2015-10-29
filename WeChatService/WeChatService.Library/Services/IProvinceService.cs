using System;
using System.Linq;
using WeChatService.Library.Models;

namespace WeChatService.Library.Services
{
    public interface IProvinceService : IDisposable
    {
        Province GetProvince(Guid id);
        IQueryable<Province> GetProvinces();
    }
}
