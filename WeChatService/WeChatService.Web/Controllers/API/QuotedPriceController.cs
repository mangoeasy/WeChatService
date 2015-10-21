using System;
using System.Linq;
using System.Web;
using AutoMapper;
using WeChatService.Library.Models;
using WeChatService.Library.Services;
using WeChatService.Web.Infrastructure;
using WeChatService.Web.Infrastructure.Filters;
using WeChatService.Web.Models;
using WeChatService.Library.Models;
using WeChatService.Library.Models.Enum;

namespace WeChatService.Web.Controllers.API
{
    public class QuotedPriceController : BaseApiController
    {
        private readonly IQuotedPriceService _quotedPriceService;
        public QuotedPriceController(IQuotedPriceService quotedPriceService)
        {
            _quotedPriceService = quotedPriceService;
        }
        [CallApiAuthority]
        public object Get()
        {
            var currentUser = HttpContext.Current.User.Identity.GetUser();
            Mapper.Reset();
            Mapper.CreateMap<Account, UserModel>();
            Mapper.CreateMap<QuotedPrice, QuotedPriceModel>()
                .ForMember(n => n.UserModel, opt => opt.MapFrom(src => src.Account));
            var model = _quotedPriceService.GetQuotedPrices().Where(n => n.Account.Id == currentUser.Id).Select(n => Mapper.Map<QuotedPrice, QuotedPriceModel>(n));
            return model;
        }
        [CallApiAuthority]
        public object Post(QuotedPriceModel model)
        {
            var currentUser = HttpContext.Current.User.Identity.GetUser();
            try
            {
                _quotedPriceService.Insert(new QuotedPrice
                {
                    Id = Guid.NewGuid(),
                    Product = model.Product,
                    Grade = model.Grade,
                    ProductOfPlace = model.ProductOfPlace,
                    PlaceOfDelivery = model.PlaceOfDelivery,
                    Price = model.Price,
                    Qty = model.Qty,
                    Remarks = model.Remarks,
                    AccountId = currentUser.Id,
                    QuotedType = model.QuotedType,
                    Currency = model.Currency,
                    Contact = model.Contact
                });
            }
            catch (Exception ex)
            {
                return Failed(ex.Message);
            }
            return Success();
        }
    }

    public class MarketQuotationController : BaseApiController
    {
        private readonly IQuotedPriceService _quotedPriceService;
        public MarketQuotationController(IQuotedPriceService quotedPriceService)
        {
            _quotedPriceService = quotedPriceService;
        }

        public object Get()
        {
            Mapper.Reset();
            Mapper.CreateMap<Account, UserModel>();
            Mapper.CreateMap<QuotedPrice, QuotedPriceModel>()
                .ForMember(n => n.UserModel, opt => opt.MapFrom(src => src.Account));
            var pageIndex = string.IsNullOrEmpty(HttpContext.Current.Request["pageIndex"])
                 ? 1
                 : Convert.ToInt32(HttpContext.Current.Request["pageIndex"]);
            var pageSize = string.IsNullOrEmpty(HttpContext.Current.Request["pageSize"])
                 ? 1
                 : Convert.ToInt32(HttpContext.Current.Request["pageSize"]);

            var product = HttpContext.Current.Request["product"] ?? string.Empty;
            var productOfPlace = HttpContext.Current.Request["productOfPlace"] ?? string.Empty;
            var placeOfDelivery = HttpContext.Current.Request["placeOfDelivery"] ?? string.Empty;
            var contact = HttpContext.Current.Request["contact"] ?? string.Empty;
            var grade = HttpContext.Current.Request["grade"] ?? string.Empty;
            
            var userId = HttpContext.Current.Request["userid"] ?? string.Empty; 
            var startDate = string.IsNullOrEmpty(HttpContext.Current.Request["startDate"])
                ? DateTime.MinValue
                : Convert.ToDateTime(HttpContext.Current.Request["startDate"]);
            var endDate = string.IsNullOrEmpty(HttpContext.Current.Request["endDate"])
                ? DateTime.MaxValue
                : Convert.ToDateTime(HttpContext.Current.Request["endDate"]);
            var allPrice = _quotedPriceService.GetQuotedPrices();
            var result = allPrice
                .Where(
                    n =>
                        (string.IsNullOrEmpty(product) || n.Product == product)
                        && (string.IsNullOrEmpty(productOfPlace) || n.ProductOfPlace.Contains(productOfPlace))
                        && n.CreatedTime >= startDate
                        && n.CreatedTime <= endDate
                        && (string.IsNullOrEmpty(grade) || n.Grade.Contains(grade))
                        && (string.IsNullOrEmpty(placeOfDelivery) || n.PlaceOfDelivery.Contains(placeOfDelivery))
                        && (string.IsNullOrEmpty(contact) || n.Contact.Contains(contact))
                        && (string.IsNullOrEmpty(userId) || n.AccountId.ToString() == userId)
                );
           
            if (HttpContext.Current.Request["quotedType"] != null)
            {
                var quotedType =(QuotedType)Enum.Parse(typeof(QuotedType), HttpContext.Current.Request["quotedType"]);
                result = result.Where(n => n.QuotedType == quotedType);
            }
            if (HttpContext.Current.Request["currency"] != null)
            {
                var currency = (Currency)Enum.Parse(typeof(Currency), HttpContext.Current.Request["currency"]);
                result = result.Where(n => n.Currency == currency);
            }

            var model = new QuotedPriceModels
            {
                Models =
                    result
                        .OrderByDescending(n => n.CreatedTime)
                        .ThenBy(n => n.Product)
                        .ThenBy(n => n.AccountId)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize).ToArray()
                        .Select(Mapper.Map<QuotedPrice, QuotedPriceModel>).ToArray(),
                CurrentPageIndex = pageIndex,
                AllPage = (result.Count() / pageSize) + (result.Count() % pageSize == 0 ? 0 : 1),
                PageSize = pageSize
            };

            return model;
        }
    }
}