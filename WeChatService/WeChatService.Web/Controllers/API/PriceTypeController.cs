using System;
using System.Linq;
using AutoMapper;
using WeChatService.Library.Models;
using WeChatService.Library.Services;
using WeChatService.Web.Models;

namespace WeChatService.Web.Controllers.API
{
    public class PriceTypeController : BaseApiController
    {
        private readonly IPriceTypeService _priceTypeService;
        public PriceTypeController(IPriceTypeService priceTypeService)
        {
            _priceTypeService = priceTypeService;
        }
        // GET: Price
        public object Get()
        {
            Mapper.Reset();
            Mapper.CreateMap<PriceType, PriceTypeModel>();
            return _priceTypeService.GetPriceTypes().Select(Mapper.Map<PriceType, PriceTypeModel>);
        }

        public object Get(Guid id)
        {
            Mapper.Reset();
            Mapper.CreateMap<PriceType, PriceTypeModel>();
            return Mapper.Map<PriceType, PriceTypeModel>(_priceTypeService.GetPriceType(id));
        }
    }
}