using System;
using System.Data.Entity;
using WeChatService.Library.Models;
using WeChatService.Library.Models.Interfaces;
using WeChatService.Library.Services;

namespace WeChatService.Service
{
    //[DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class WeChatServiceDataContext : DbContext, IDataContext
    {
        //public WeChatServiceDataContext()
        //    : base("WeChatServiceDataContext")
        //{
        //}
        public IDbSet<Account> Accounts { get; set; }
        public IDbSet<Article> Articles { get; set; }
        public IDbSet<ArticleType> ArticleTypes { get; set; }
        public IDbSet<Company> Companies { get; set; }
        public IDbSet<PetrochemicalPrice> PetrochemicalPrices { get; set; }
        public IDbSet<Price> Prices { get; set; }
        public IDbSet<PriceType> PriceTypes { get; set; }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<ProductStandard> ProductStandards { get; set; }
        public IDbSet<Province> Provinces { get; set; }
        public IDbSet<QuotedPrice> QuotedPrices { get; set; }
        public IDbSet<Site> Sites { get; set; }
    
       
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
