using System;
using System.Web.Mvc;
using AutoMapper;
using WeChatService.Library.Models;
using WeChatService.Library.Services;
using WeChatService.Web.Models;
using WeChatService.Library.Models;

namespace WeChatService.Web.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        [HttpGet]
        public ActionResult Index(Guid id)
        {
            Mapper.Reset();
            Mapper.CreateMap<Article, ArticleModel>()
                .ForMember(n => n.ArticleTypeId, opt => opt.MapFrom(src => src.ArticleType.Id))
                .ForMember(n => n.Site, opt => opt.MapFrom(src => src.Site.Name));
            var model = _articleService.GetArticle(id);
            model.ViewCount = model.ViewCount + 1;
            _articleService.Update();
            return View(Mapper.Map<Article, ArticleModel>(model));
        }
       
    }
}