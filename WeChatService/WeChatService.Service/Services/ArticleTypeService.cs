using System;
using System.Linq;
using WeChatService.Library.Models;
using WeChatService.Library.Services;

namespace WeChatService.Service.Services
{
    public class ArticleTypeService : BaseService, IArticleTypeService
    {
        public ArticleTypeService(WeChatServiceDataContext dbContext)
            : base(dbContext)
        {
        }
        public ArticleType GetArticleType(Guid id)
        {
            return DbContext.ArticleTypes.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<ArticleType> GetArticleTypes()
        {
            return DbContext.ArticleTypes;
        }
    }
}
