using System;
using System.Linq;
using WeChatService.Library.Models;

namespace WeChatService.Library.Services
{
    public interface IArticleTypeService : IDisposable
    {
        ArticleType GetArticleType(Guid id);
        IQueryable<ArticleType> GetArticleTypes();
    }
}
