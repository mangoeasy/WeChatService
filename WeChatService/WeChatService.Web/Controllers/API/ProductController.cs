using System;
using System.Linq;
using AutoMapper;
using WeChatService.Library.Models;
using WeChatService.Library.Services;
using WeChatService.Web.Models;

namespace WeChatService.Web.Controllers.API
{
    public class ProductController : BaseApiController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: Price
        public object Get()
        {
            Mapper.Reset();
            Mapper.CreateMap<Product, ProductModel>();
            return _productService.GetProducts().OrderBy(n=>n.Sort).Select(Mapper.Map<Product, ProductModel>);
        }

        public object Get(Guid id)
        {
            Mapper.Reset();
            Mapper.CreateMap<Product, ProductModel>();
            return Mapper.Map<Product, ProductModel>(_productService.GetProduct(id));
        }
    }
}