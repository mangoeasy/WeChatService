using System;
using System.Data.Entity;
using WeChatService.Library.Models;
using WeChatService.Library.Models.Interfaces;
using WeChatService.Library.Services;

namespace WeChatService.Service
{
    public class WeChatServiceDataContext : DbContext, IDataContext
    {
        public IDbSet<Account> Accounts { get; set; }
        public IDbSet<Company> Companies { get; set; }
        public IDbSet<Province> Provinces { get; set; }
       
        IDbSet<TEntity> IDataContext.Set<TEntity>()
        {
            return this.Set<TEntity>();
        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries<IDtStamped>();

            foreach (var dtStamped in entities)
            {
                if (dtStamped.State == EntityState.Added)
                {
                    dtStamped.Entity.CreatedTime = DateTime.Now;
                }

                if (dtStamped.State == EntityState.Modified)
                {
                    dtStamped.Entity.UpdateTime = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new CategoryMapping());
            //modelBuilder.Configurations.Add(new AvatarMapping());
            //modelBuilder.Configurations.Add(new LetterMapping());
            //modelBuilder.Configurations.Add(new RetailerMapping());
            //modelBuilder.Configurations.Add(new SiteMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
   
}
