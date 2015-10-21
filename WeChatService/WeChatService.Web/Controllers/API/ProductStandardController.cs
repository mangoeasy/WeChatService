using System;
using System.Linq;
using System.Web;
using AutoMapper;
using WeChatService.Library.Models;
using WeChatService.Library.Services;
using WeChatService.Web.Models;

namespace WeChatService.Web.Controllers.API
{
    public class ProductStandardController : BaseApiController
    {
        private readonly IProductStandardService _productStandardService;
        public ProductStandardController(IProductStandardService productStandardService)
        {
            _productStandardService = productStandardService;
        }
        // GET: Price
        public object Get()
        {
            var productId = HttpContext.Current.Request["productId"];
            Mapper.Reset();
            Mapper.CreateMap<ProductStandard, ProductStandardModel>()
                .ForMember(n => n.ProductId, opt => opt.MapFrom(src => src.Product.Id));
            return
                _productStandardService.GetProductStandards()
                    .Where(n => string.IsNullOrEmpty(productId) || n.Product.Id.ToString() == productId)
                    .ToArray()
                    .Select(Mapper.Map<ProductStandard, ProductStandardModel>);
        }

        public object Get(Guid id)
        {
            Mapper.Reset();
            Mapper.CreateMap<ProductStandard, ProductStandardModel>()
              .ForMember(n => n.ProductId, opt => opt.MapFrom(src => src.Product.Id));
            return Mapper.Map<ProductStandard, ProductStandardModel>(_productStandardService.GetProductStandard(id));
        }
    }
}