namespace Plastic.Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RongCloudUserToken = c.String(),
                        Email = c.String(),
                        Token = c.Guid(nullable: false),
                        Password = c.String(),
                        Name = c.String(),
                        Gender = c.Int(nullable: false),
                        Phone = c.String(),
                        Valid = c.Boolean(nullable: false),
                        VerificationCode = c.String(),
                        UpdateTime = c.DateTime(),
                        CreatedTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        City_Id = c.Guid(),
                        Company_Id = c.Guid(),
                        QQInfo_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.City_Id)
                .ForeignKey("dbo.Companies", t => t.Company_Id)
                .ForeignKey("dbo.QQInfoes", t => t.QQInfo_Id)
                .Index(t => t.City_Id)
                .Index(t => t.Company_Id)
                .Index(t => t.QQInfo_Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Province_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provinces", t => t.Province_Id)
                .Index(t => t.Province_Id);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        CityId = c.Guid(nullable: false),
                        UpdateTime = c.DateTime(),
                        CreatedTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.QQInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Userid = c.String(),
                        City = c.String(),
                        Figureurl = c.String(),
                        Figureurl1 = c.String(),
                        Figureurl2 = c.String(),
                        FigureurlQQ1 = c.String(),
                        FigureurlQQ2 = c.String(),
                        Gender = c.String(),
                        IsLost = c.String(),
                        IsYellowVip = c.String(),
                        IsYellowYearVip = c.String(),
                        Level = c.String(),
                        Msg = c.String(),
                        Nickname = c.String(),
                        Province = c.String(),
                        Ret = c.String(),
                        Vip = c.String(),
                        Year = c.String(),
                        YellowVipLevel = c.String(),
                        AutomaticPassword = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        ViewCount = c.Int(nullable: false),
                        Description = c.String(),
                        Body = c.String(),
                        FromUrl = c.String(),
                        SiteId = c.Guid(),
                        AuthorId = c.Guid(),
                        IsPublish = c.Boolean(nullable: false),
                        IsRecommend = c.Boolean(nullable: false),
                        IsPush = c.Boolean(nullable: false),
                        Thumbnail = c.String(),
                        PublishDate = c.DateTime(),
                        CollectDate = c.DateTime(),
                        ArticleTypeId = c.Guid(nullable: false),
                        UpdateTime = c.DateTime(),
                        CreatedTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ArticleTypes", t => t.ArticleTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Accounts", t => t.AuthorId)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .Index(t => t.SiteId)
                .Index(t => t.AuthorId)
                .Index(t => t.ArticleTypeId);
            
            CreateTable(
                "dbo.ArticleTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Paren_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ArticleTypes", t => t.Paren_Id)
                .Index(t => t.Paren_Id);
            
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Domain = c.String(),
                        UpdateTime = c.DateTime(),
                        CreatedTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PetrochemicalPrices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Price = c.Decimal(precision: 18, scale: 2),
                        SalesArea = c.String(),
                        Factory = c.String(),
                        Product = c.String(),
                        ProductStandard = c.String(),
                        Url = c.String(),
                        UpdateTime = c.DateTime(),
                        CreatedTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Site_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.Site_Id)
                .Index(t => t.Site_Id);
            
            CreateTable(
                "dbo.Prices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProductPrice = c.Decimal(precision: 18, scale: 2),
                        Unit = c.String(),
                        PriceTerm = c.String(),
                        SalesArea = c.String(),
                        PriceDate = c.DateTime(nullable: false),
                        Url = c.String(),
                        UpdateTime = c.DateTime(),
                        CreatedTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        ProductStandard_Id = c.Guid(),
                        Site_Id = c.Guid(),
                        Type_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductStandards", t => t.ProductStandard_Id)
                .ForeignKey("dbo.Sites", t => t.Site_Id)
                .ForeignKey("dbo.PriceTypes", t => t.Type_Id)
                .Index(t => t.ProductStandard_Id)
                .Index(t => t.Site_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.ProductStandards",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Product_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Sort = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PriceTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QuotedPrices",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Product = c.String(),
                        Grade = c.String(),
                        ProductOfPlace = c.String(),
                        PlaceOfDelivery = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Qty = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remarks = c.String(),
                        QuotedType = c.Int(nullable: false),
                        Currency = c.Int(nullable: false),
                        Contact = c.String(),
                        AccountId = c.Guid(nullable: false),
                        UpdateTime = c.DateTime(),
                        CreatedTime = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuotedPrices", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Prices", "Type_Id", "dbo.PriceTypes");
            DropForeignKey("dbo.Prices", "Site_Id", "dbo.Sites");
            DropForeignKey("dbo.Prices", "ProductStandard_Id", "dbo.ProductStandards");
            DropForeignKey("dbo.ProductStandards", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.PetrochemicalPrices", "Site_Id", "dbo.Sites");
            DropForeignKey("dbo.Articles", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.Articles", "AuthorId", "dbo.Accounts");
            DropForeignKey("dbo.ArticleTypes", "Paren_Id", "dbo.ArticleTypes");
            DropForeignKey("dbo.Articles", "ArticleTypeId", "dbo.ArticleTypes");
            DropForeignKey("dbo.Accounts", "QQInfo_Id", "dbo.QQInfoes");
            DropForeignKey("dbo.Companies", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Accounts", "Company_Id", "dbo.Companies");
            DropForeignKey("dbo.Accounts", "City_Id", "dbo.Cities");
            DropForeignKey("dbo.Cities", "Province_Id", "dbo.Provinces");
            DropIndex("dbo.QuotedPrices", new[] { "AccountId" });
            DropIndex("dbo.ProductStandards", new[] { "Product_Id" });
            DropIndex("dbo.Prices", new[] { "Type_Id" });
            DropIndex("dbo.Prices", new[] { "Site_Id" });
            DropIndex("dbo.Prices", new[] { "ProductStandard_Id" });
            DropIndex("dbo.PetrochemicalPrices", new[] { "Site_Id" });
            DropIndex("dbo.ArticleTypes", new[] { "Paren_Id" });
            DropIndex("dbo.Articles", new[] { "ArticleTypeId" });
            DropIndex("dbo.Articles", new[] { "AuthorId" });
            DropIndex("dbo.Articles", new[] { "SiteId" });
            DropIndex("dbo.Companies", new[] { "CityId" });
            DropIndex("dbo.Cities", new[] { "Province_Id" });
            DropIndex("dbo.Accounts", new[] { "QQInfo_Id" });
            DropIndex("dbo.Accounts", new[] { "Company_Id" });
            DropIndex("dbo.Accounts", new[] { "City_Id" });
            DropTable("dbo.QuotedPrices");
            DropTable("dbo.PriceTypes");
            DropTable("dbo.Products");
            DropTable("dbo.ProductStandards");
            DropTable("dbo.Prices");
            DropTable("dbo.PetrochemicalPrices");
            DropTable("dbo.Sites");
            DropTable("dbo.ArticleTypes");
            DropTable("dbo.Articles");
            DropTable("dbo.QQInfoes");
            DropTable("dbo.Companies");
            DropTable("dbo.Provinces");
            DropTable("dbo.Cities");
            DropTable("dbo.Accounts");
        }
    }
}
