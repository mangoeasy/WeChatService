using System;
using System.Linq;
using WeChatService.Library.Models;
using WeChatService.Library.Models;

namespace WeChatService.Library.Services
{
    public interface IArticleService : IDisposable
    {
        void Insert(Article article);
        void Update();
        Article GetArticle(Guid id);
        IQueryable<Article> GetArticles();
        void Delete(Guid id);
    }
}
