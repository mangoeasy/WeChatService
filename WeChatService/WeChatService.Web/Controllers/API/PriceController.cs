using System;
using System.Linq;
using System.Web;
using AutoMapper;
using WeChatService.Library.Models;
using WeChatService.Library.Services;
using WeChatService.Web.Models;
using WebGrease.Css.Extensions;

namespace WeChatService.Web.Controllers.API
{
    public class PriceController : BaseApiController
    {
        private readonly IPriceService _priceService;
        public PriceController(IPriceService priceService)
        {
            _priceService = priceService;
        }
        // GET: Price
        public object Get()
        {
            var pageIndex = string.IsNullOrEmpty(HttpContext.Current.Request["pageIndex"])
                 ? 1
                 : Convert.ToInt32(HttpContext.Current.Request["pageIndex"]);
            var pageSize = string.IsNullOrEmpty(HttpContext.Current.Request["pageSize"])
                 ? 1
                 : Convert.ToInt32(HttpContext.Current.Request["pageSize"]);
            var priceType = HttpContext.Current.Request["priceTypeId"];
            var product = HttpContext.Current.Request["productId"];
            var productStandard = HttpContext.Current.Request["productStandardId"];
            var unit = HttpContext.Current.Request["unit"] ?? string.Empty;
            var priceTerm = HttpContext.Current.Request["priceTerm"] ?? string.Empty;
            var salesArea = HttpContext.Current.Request["salesArea"] ?? string.Empty;
            var startDate = string.IsNullOrEmpty(HttpContext.Current.Request["startDate"])
                ? DateTime.MinValue
                : Convert.ToDateTime(HttpContext.Current.Request["startDate"]);
            var endDate = string.IsNullOrEmpty(HttpContext.Current.Request["endDate"])
                ? DateTime.MaxValue
                : Convert.ToDateTime(HttpContext.Current.Request["endDate"]);

            Mapper.Reset();
            Mapper.CreateMap<Price, PriceModel>()
                .ForMember(n => n.PriceType, opt => opt.MapFrom(src => src.Type.Name))
                .ForMember(n => n.ProductName, opt => opt.MapFrom(src => src.ProductStandard.Product.Name))
                .ForMember(n => n.ProductStandard, opt => opt.MapFrom(src => src.ProductStandard.Name))
                .ForMember(n => n.ProductStandardId, opt => opt.MapFrom(src => src.ProductStandard.Id));
            var allPrice = _priceService.GetPrices();
            var result = allPrice
                .Where(
                    n =>
                        (string.IsNullOrEmpty(priceType) || n.Type.Id.ToString() == priceType)
                        && n.PriceDate >= startDate
                        && n.PriceDate <= endDate
                        && (string.IsNullOrEmpty(product) || n.ProductStandard.Product.Id.ToString() == product)
                        && (string.IsNullOrEmpty(productStandard) || n.ProductStandard.Id.ToString() == productStandard)
                        && (string.IsNullOrEmpty(unit) || n.Unit == unit)
                        && (string.IsNullOrEmpty(priceTerm) || n.PriceTerm == priceTerm)
                        && (string.IsNullOrEmpty(salesArea) || n.SalesArea == salesArea)
                );
            var model = new PriceModels
            {
                Models =
                    result
                        .OrderByDescending(n => n.PriceDate)
                        .ThenBy(n => n.ProductStandard.Product.Name)
                        .ThenBy(n => n.ProductStandard.Name)
                        .ThenBy(n => n.Type.Id)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .Select(Mapper.Map<Price, PriceModel>).ToArray(),
                CurrentPageIndex = pageIndex,
                AllPage = (result.Count() / pageSize) + (result.Count() % pageSize == 0 ? 0 : 1),
                PageSize = pageSize
            };
            model.Models.ForEach(n =>
            {
                var priceDate = n.PriceDate.AddDays(-1);
                n.YesterdayPrice = allPrice.Where(
                        p =>
                            p.PriceTerm == n.PriceTerm && p.Type.Name == n.PriceType && p.ProductStandard.Product.Name == n.ProductName &&
                            p.SalesArea == n.SalesArea && p.ProductStandard.Name == n.ProductStandard && p.PriceDate == priceDate && p.Factory==n.Factory)
                        .Select(p => p.ProductPrice).FirstOrDefault();

            });
            return model;
        }

        public object Get(Guid id)
        {
            Mapper.Reset();
            Mapper.CreateMap<Price, PriceModel>();
            return Mapper.Map<Price, PriceModel>(_priceService.GetPrice(id));
        }
    }

    public class PriceTrendController : BaseApiController
    {
        private readonly IPriceService _priceService;
        public PriceTrendController(IPriceService priceService)
        {
            _priceService = priceService;
        }

        public object Get()
        {
            var productStandardId = HttpContext.Current.Request["productStandardId"];
            var productId = HttpContext.Current.Request["productId"];
            if (!string.IsNullOrEmpty(productId))
            {
                var id = new Guid(productId);
                return
                    _priceService.GetPrices()
                        .Where(n => n.ProductStandard.Product.Id == id)
                        .GroupBy(n => new { n.ProductStandard.Product, n.PriceDate })
                        .Select(n => new PriceTrendModel
                        {
                            PriceDate = n.Key.PriceDate,
                            ProductPrice = n.Sum(p => p.ProductPrice)
                        }).ToArray();
            }
            if (!string.IsNullOrEmpty(productStandardId))
            {
                var id = new Guid(productStandardId);
                return
                    _priceService.GetPrices()
                        .Where(n => n.ProductStandard.Id == id).OrderByDescending(n=>n.PriceDate)
                        .Select(n => new PriceTrendModel
                        {
                            PriceDate = n.PriceDate,
                            ProductPrice = n.ProductPrice,
                            SalesArea = n.SalesArea
                        }).ToArray();
            }
            return null;
        }
    }

    public class PetrochemicalPriceController : BaseApiController
    {
        private readonly IPetrochemicalPriceService _petrochemicalPriceService;
        public PetrochemicalPriceController(IPetrochemicalPriceService petrochemicalPriceService)
        {
            _petrochemicalPriceService = petrochemicalPriceService;
        }

        public object Get()
        {
            var priceDate = HttpContext.Current.Request["priceDate"];
            Mapper.Reset();
            Mapper.CreateMap<PetrochemicalPrice, PetrochemicalPriceModel>();
            var model = _petrochemicalPriceService.GetPetrochemicalPrices();
            if (!string.IsNullOrEmpty(priceDate))
            {
                var date = Convert.ToDateTime(priceDate);
                model =
                    _petrochemicalPriceService.GetPetrochemicalPrices()
                        .Where(
                            n =>
                                n.CreatedTime.Year == date.Year && n.CreatedTime.Month == date.Month &&
                                n.CreatedTime.Day == date.Day).OrderByDescending(n=>n.CreatedTime).ThenBy(n=>n.Product).ThenBy(n=>n.ProductStandard).ThenBy(n=>n.Factory).ThenBy(n=>n.ProductStandard);
            }
            return model.ToArray().Select(Mapper.Map<PetrochemicalPrice, PetrochemicalPriceModel>);
        }
    }
}