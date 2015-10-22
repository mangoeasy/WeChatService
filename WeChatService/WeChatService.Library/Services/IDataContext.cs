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
        IDbSet<Company> Companies { get; set; }
        IDbSet<Province> Provinces { get; set; }
        int SaveChanges();
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
