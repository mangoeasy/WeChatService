using System;
using System.Linq;
using AutoMapper;
using WeChatService.Library.Models;
using WeChatService.Library.Services;
using WeChatService.Web.Models;

namespace WeChatService.Web.Controllers.API
{
    public class ArticleTypeController : BaseApiController
    {
        private readonly IArticleTypeService _articleTypeService;
        public ArticleTypeController(IArticleTypeService articleTypeService)
        {
            _articleTypeService = articleTypeService;
        }

        public object Get()
        {
            Mapper.Reset();
            Mapper.CreateMap<ArticleType, ArticleTypeModel>();
            return _articleTypeService.GetArticleTypes().ToArray().Select(Mapper.Map<ArticleType, ArticleTypeModel>);
        }
        public object Get(Guid id)
        {
            Mapper.Reset();
            Mapper.CreateMap<ArticleType, ArticleTypeModel>();
            return Mapper.Map<ArticleType, ArticleTypeModel>(_articleTypeService.GetArticleType(id));
        }
    }
}