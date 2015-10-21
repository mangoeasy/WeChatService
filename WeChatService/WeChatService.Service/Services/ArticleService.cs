using System;
using System.Linq;
using WeChatService.Library.Models;
using WeChatService.Library.Services;

namespace WeChatService.Service.Services
{
    public class ArticleService :BaseService, IArticleService
    {
        public ArticleService(WeChatServiceDataContext dbContext)
            : base(dbContext)
        {
        }
        public void Insert(Article article)
        {
            DbContext.Articles.Add(article);
            DbContext.SaveChanges();
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }

        public Article GetArticle(Guid id)
        {
            return DbContext.Articles.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<Article> GetArticles()
        {
            return DbContext.Articles;
        }


        public void Delete(Guid id)
        {
            var model = DbContext.Articles.FirstOrDefault(n => n.Id == id);
            if (model != null)
            {
                DbContext.Articles.Remove(model);
                DbContext.SaveChanges();
            }
        }
    }
}
