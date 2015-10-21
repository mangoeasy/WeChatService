using System;
using System.Linq;
using WeChatService.Library.Models;

namespace WeChatService.Library.Services
{
    public interface IProductService : IDisposable
    {
        Product GetProduct(Guid id);
        IQueryable<Product> GetProducts();
    }
}
