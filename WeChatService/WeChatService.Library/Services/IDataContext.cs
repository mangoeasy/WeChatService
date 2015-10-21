using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WeChatService.Library.Models;
using WeChatService.Library.Models;

namespace WeChatService.Library.Services
{
    public interface IDataContext : IObjectContextAdapter, IDisposable
    {
        IDbSet<Account> Accounts { get; set; }
        IDbSet<Article> Articles { get; set; }
        IDbSet<ArticleType> ArticleTypes { get; set; }
        IDbSet<Company> Companies { get; set; }
        IDbSet<PetrochemicalPrice> PetrochemicalPrices { get; set; }
        IDbSet<Price> Prices { get; set; }
        IDbSet<PriceType> PriceTypes { get; set; }
        IDbSet<Product> Products { get; set; }
        IDbSet<ProductStandard> ProductStandards { get; set; }
        IDbSet<Province> Provinces { get; set; }
        IDbSet<QuotedPrice> QuotedPrices { get; set; }
        IDbSet<Site> Sites { get; set; }
        int SaveChanges();
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
