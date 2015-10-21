using System;
using System.Linq;
using WeChatService.Library.Models;
using WeChatService.Library.Services;

namespace WeChatService.Service.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(WeChatServiceDataContext dbContext)
            : base(dbContext)
        {
        }
        public Product GetProduct(Guid id)
        {
            return DbContext.Products.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<Product> GetProducts()
        {
            return DbContext.Products;
        }
    }
}
