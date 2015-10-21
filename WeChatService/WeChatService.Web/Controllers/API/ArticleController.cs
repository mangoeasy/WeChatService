using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using WeChatService.Library.Models;
using WeChatService.Library.Services;
using WeChatService.Web.Enums;
using WeChatService.Web.Infrastructure;
using WeChatService.Web.Models;
using WeChatService.Library.Models;

namespace WeChatService.Web.Controllers.API
{
    public class ArticleController : BaseApiController
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public object Get()
        {
            var pageIndex = string.IsNullOrEmpty(HttpContext.Current.Request["pageIndex"])
                 ? 1
                 : Convert.ToInt32(HttpContext.Current.Request["pageIndex"]);
            var pageSize = string.IsNullOrEmpty(HttpContext.Current.Request["pageSize"])
                 ? 1
                 : Convert.ToInt32(HttpContext.Current.Request["pageSize"]);
            var articleType = string.IsNullOrEmpty(HttpContext.Current.Request["articleTypeId"])
                ? Guid.Empty
                : new Guid(HttpContext.Current.Request["articleTypeId"]);
            var title = HttpContext.Current.Request["title"] ?? string.Empty;
            var description = HttpContext.Current.Request["description"] ?? string.Empty;
            var body = HttpContext.Current.Request["body"] ?? string.Empty;
            var publishDate = string.IsNullOrEmpty(HttpContext.Current.Request["publishDate"])
                ? DateTime.MinValue
                : Convert.ToDateTime(HttpContext.Current.Request["publishDate"]);
            var isPublish = (IsPublish)Enum.Parse(typeof(IsPublish), string.IsNullOrEmpty(HttpContext.Current.Request["isPublish"]) ? IsPublish.全部.ToString() : HttpContext.Current.Request["isPublish"]);
            var isPush = (IsPush)Enum.Parse(typeof(IsPush), string.IsNullOrEmpty(HttpContext.Current.Request["isPush"]) ? IsPush.全部.ToString() : HttpContext.Current.Request["isPush"]);
            var isRecommend = (IsRecommend)Enum.Parse(typeof(IsRecommend), string.IsNullOrEmpty(HttpContext.Current.Request["isRecommend"]) ? IsRecommend.全部.ToString() : HttpContext.Current.Request["isRecommend"]);
            Mapper.Reset();
            Mapper.CreateMap<Article, ArticleModel>()
                .ForMember(n => n.ArticleTypeId, opt => opt.MapFrom(src => src.ArticleTypeId))
                .ForMember(n => n.Site, opt => opt.MapFrom(src => src.Site.Name))
                .ForMember(n => n.ArticleTypeName, opt => opt.MapFrom(src => src.ArticleType.Name))
                .ForMember(n => n.Body, opt => opt.Ignore());

            var allArticles = _articleService.GetArticles();
            var result = allArticles
                .Where(
                    n =>
                        (articleType == Guid.Empty || n.ArticleType.Id == articleType)
                        && (publishDate == DateTime.MinValue || n.PublishDate == publishDate)
                        && n.Title.Contains(title)
                        && (string.IsNullOrEmpty(description) || n.Description.Contains(description))
                        && (string.IsNullOrEmpty(body) || n.Body.Contains(body))
                        && (isPublish == IsPublish.全部 || (isPublish == IsPublish.已发布 ? n.PublishDate != null : n.PublishDate == null))
                        && (isPush == IsPush.全部 || (isPush == IsPush.已推送 ? n.IsPush : n.IsPush == false))
                        && (isRecommend == IsRecommend.全部 || (isRecommend == IsRecommend.已推荐 ? n.IsRecommend : n.IsRecommend == false))
                );

            var model = new ArticleModels
            {
                Models =
                    result
                        .OrderByDescending(n => n.CreatedTime)
                        .ThenBy(n => n.ArticleTypeId)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize).ToArray().Select(Mapper.Map<Article, ArticleModel>).ToArray(),
                CurrentPageIndex = pageIndex,
                AllPage = (result.Count() / pageSize) + (result.Count() % pageSize == 0 ? 0 : 1),
                PageSize = pageSize
            };
            return model;
        }
        public object Get(Guid id)
        {
            Mapper.Reset();
            Mapper.CreateMap<Article, ArticleModel>()
                .ForMember(n => n.ArticleTypeId, opt => opt.MapFrom(src => src.ArticleType.Id))
                .ForMember(n => n.Site, opt => opt.MapFrom(src => src.Site.Name));
            return Mapper.Map<Article, ArticleModel>(_articleService.GetArticle(id));
        }
        [Authorize]
        public object Put(Guid id, ArticleModel model)
        {
            var item = _articleService.GetArticle(model.Id);
            try
            {
                item.Title = model.Title;
                item.Description = model.Description;
                item.Body = model.Body;
                item.PublishDate = DateTime.Now;
                item.IsPublish = true;
                item.IsPush = model.IsPush;
                item.IsRecommend = model.IsRecommend;
                item.Thumbnail = model.Thumbnail;
                _articleService.Update();
            }
            catch (Exception ex)
            {
                return Failed(ex.Message);
            }
            return Success();
        }
        [Authorize]
        public object Post(ArticleModel model)
        {
            try
            {
                var currentUser = HttpContext.Current.User.Identity.GetUser();
                var item = new Article
                {
                    Id = Guid.NewGuid(),
                    ArticleTypeId = model.ArticleTypeId,
                    Body = model.Body,
                    Title = model.Title,
                    Description = model.Description,
                    PublishDate = DateTime.Now,
                    IsPublish = true,
                    AuthorId = currentUser.Id,
                    IsPush = model.IsPush,
                    IsRecommend = model.IsRecommend,
                    Thumbnail = model.Thumbnail
                };
                _articleService.Insert(item);
            }
            catch (Exception ex)
            {
                return Failed(ex.Message);
            }
            return Success();
        }

        [Authorize]
        public object Delete(Guid id)
        {
            try
            {
                _articleService.Delete(id);
            }
            catch (Exception ex)
            {
                return Failed(ex.Message);
            }
            return Success();
        }
    }
    public class ArticleNoBodyController : BaseApiController
    {
        private readonly IArticleService _articleService;
        public ArticleNoBodyController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public object Get()
        {
            var pageIndex = string.IsNullOrEmpty(HttpContext.Current.Request["pageIndex"])
                 ? 1
                 : Convert.ToInt32(HttpContext.Current.Request["pageIndex"]);
            var pageSize = string.IsNullOrEmpty(HttpContext.Current.Request["pageSize"])
                 ? 1
                 : Convert.ToInt32(HttpContext.Current.Request["pageSize"]);
            var articleType = HttpContext.Current.Request["articleTypeId"];
            var title = HttpContext.Current.Request["title"] ?? string.Empty;
            var description = HttpContext.Current.Request["description"] ?? string.Empty;
            var body = HttpContext.Current.Request["body"] ?? string.Empty;
            var publishDate = string.IsNullOrEmpty(HttpContext.Current.Request["publishDate"])
                ? DateTime.MinValue
                : Convert.ToDateTime(HttpContext.Current.Request["publishDate"]);

            Mapper.Reset();
            Mapper.CreateMap<Article, ArticleModel>()
                .ForMember(n => n.ArticleTypeId, opt => opt.MapFrom(src => src.ArticleType.Id))
                .ForMember(n => n.Site, opt => opt.MapFrom(src => src.Site.Name))
                .ForMember(n => n.Body, opt => opt.Ignore())
                .ForMember(n => n.FromUrl, opt => opt.Ignore())
                .ForMember(n => n.Site, opt => opt.Ignore())
                .ForMember(n => n.CollectDate, opt => opt.Ignore());

            var allArticles = _articleService.GetArticles();
            var result = allArticles
                .Where(
                    n =>
                        (string.IsNullOrEmpty(articleType) || n.ArticleType.Id.ToString() == articleType)
                        && (publishDate == DateTime.MinValue || n.PublishDate == publishDate)
                        && n.Title.Contains(title)
                        && n.Description.Contains(description)
                        && n.Body.Contains(body)
                        && n.IsPublish
                );
            var model = new ArticleModels
            {
                Models =
                    result
                        .OrderByDescending(n => n.PublishDate)
                        .ThenBy(n => n.ArticleType.Id)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize).ToArray()
                        .Select(Mapper.Map<Article, ArticleModel>).ToArray(),
                CurrentPageIndex = pageIndex,
                AllPage = (result.Count() / pageSize) + (result.Count() % pageSize == 0 ? 0 : 1),
                PageSize = pageSize
            };
            return model;
        }
    }
}