namespace Fluppy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(maxLength: 150),
                        Name = c.String(nullable: false, maxLength: 30),
                        Surname = c.String(nullable: false, maxLength: 30),
                        Username = c.String(nullable: false, maxLength: 30),
                        Password = c.String(maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 50),
                        MainAdmin = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Image = c.String(maxLength: 150),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        Date = c.DateTime(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        AdminId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: true)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Image = c.String(maxLength: 150),
                        Date = c.DateTime(nullable: false),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        ViewCount = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        BlogCategoryId = c.Int(nullable: false),
                        AdminId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: false)
                .ForeignKey("dbo.BlogCategories", t => t.BlogCategoryId, cascadeDelete: false)
                .Index(t => t.BlogCategoryId)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.BlogCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 500),
                        CreatedDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        BlogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blogs", t => t.BlogId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.BlogId);
            
            CreateTable(
                "dbo.CommentReplies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommentId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Reply = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.CommentId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.CommentId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Surname = c.String(nullable: false, maxLength: 30),
                        Username = c.String(maxLength: 30),
                        Password = c.String(maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 30),
                        Phone = c.String(nullable: false, maxLength: 30),
                        City = c.String(nullable: false, maxLength: 30),
                        Address = c.String(nullable: false, maxLength: 150),
                        PostCode = c.String(nullable: false, maxLength: 10),
                        Notes = c.String(maxLength: 300),
                        IsRegistered = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Decimal(nullable: false, storeType: "money"),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        CreatedDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Price = c.Decimal(nullable: false, storeType: "money"),
                        Desc = c.String(nullable: false, maxLength: 300),
                        Quantity = c.Decimal(nullable: false, storeType: "money"),
                        CreatedDate = c.DateTime(nullable: false),
                        ProductCategoryId = c.Int(nullable: false),
                        AdminId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: false)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryId, cascadeDelete: false)
                .Index(t => t.ProductCategoryId)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.PetTypeToProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        PetTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PetTypes", t => t.PetTypeId, cascadeDelete: false)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: false)
                .Index(t => t.ProductId)
                .Index(t => t.PetTypeId);
            
            CreateTable(
                "dbo.PetTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(maxLength: 150),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.AdoptRules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, storeType: "ntext"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Adopts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Neutered = c.Boolean(nullable: false),
                        Age = c.Int(nullable: false),
                        Address = c.String(nullable: false, maxLength: 150),
                        Breed = c.String(nullable: false, maxLength: 50),
                        Vaccinated = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        GenderId = c.Int(nullable: false),
                        PetSizeId = c.Int(nullable: false),
                        AgeTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AgeTypes", t => t.AgeTypeId, cascadeDelete: false)
                .ForeignKey("dbo.Genders", t => t.GenderId, cascadeDelete: false)
                .ForeignKey("dbo.PetSizes", t => t.PetSizeId, cascadeDelete: false)
                .Index(t => t.GenderId)
                .Index(t => t.PetSizeId)
                .Index(t => t.AgeTypeId);
            
            CreateTable(
                "dbo.AdoptSocials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Link = c.String(nullable: false, maxLength: 150),
                        SocialTypeId = c.Int(nullable: false),
                        AdoptId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Adopts", t => t.AdoptId, cascadeDelete: false)
                .ForeignKey("dbo.SocialTypes", t => t.SocialTypeId, cascadeDelete: false)
                .Index(t => t.SocialTypeId)
                .Index(t => t.AdoptId);
            
            CreateTable(
                "dbo.SocialTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 15),
                        Icon = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HomeSocials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Link = c.String(nullable: false, maxLength: 150),
                        SocialTypeId = c.Int(nullable: false),
                        HomeSettingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HomeSettings", t => t.HomeSettingId, cascadeDelete: false)
                .ForeignKey("dbo.SocialTypes", t => t.SocialTypeId, cascadeDelete: false)
                .Index(t => t.SocialTypeId)
                .Index(t => t.HomeSettingId);
            
            CreateTable(
                "dbo.HomeSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Logo = c.String(maxLength: 150),
                        FooterLogo = c.String(maxLength: 150),
                        Email = c.String(nullable: false, maxLength: 30),
                        Phone = c.String(nullable: false, maxLength: 30),
                        Address = c.String(nullable: false, maxLength: 50),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        SatStartTime = c.DateTime(nullable: false),
                        SatEndTime = c.DateTime(nullable: false),
                        FooterContent = c.String(nullable: false, maxLength: 150),
                        CopyRight = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeamSocials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Link = c.String(nullable: false, maxLength: 150),
                        SocialTypeId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                        TeamSocial_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SocialTypes", t => t.SocialTypeId, cascadeDelete: false)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: false)
                .ForeignKey("dbo.TeamSocials", t => t.TeamSocial_Id)
                .Index(t => t.SocialTypeId)
                .Index(t => t.TeamId)
                .Index(t => t.TeamSocial_Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fullname = c.String(nullable: false, maxLength: 50),
                        Image = c.String(maxLength: 150),
                        PositionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Positions", t => t.PositionId, cascadeDelete: true)
                .Index(t => t.PositionId);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AgeTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false, maxLength: 30),
                        DateAndTime = c.DateTime(nullable: false),
                        Message = c.String(nullable: false, maxLength: 500),
                        CreatedDate = c.DateTime(nullable: false),
                        ServiceId = c.Int(),
                        AdoptId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Adopts", t => t.AdoptId)
                .ForeignKey("dbo.Services", t => t.ServiceId)
                .Index(t => t.ServiceId)
                .Index(t => t.AdoptId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Image = c.String(maxLength: 150),
                        Content = c.String(nullable: false, maxLength: 300),
                        PriceDayBig = c.Decimal(nullable: false, storeType: "money"),
                        PriceWeekBig = c.Decimal(nullable: false, storeType: "money"),
                        PriceMonthBig = c.Decimal(nullable: false, storeType: "money"),
                        PriceDaySmall = c.Decimal(nullable: false, storeType: "money"),
                        PriceWeekSmall = c.Decimal(nullable: false, storeType: "money"),
                        PriceMonthSmall = c.Decimal(nullable: false, storeType: "money"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PetSizes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Size = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SlideAdopts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(maxLength: 150),
                        AdoptId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Adopts", t => t.AdoptId, cascadeDelete: true)
                .Index(t => t.AdoptId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Fullname = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false, maxLength: 30),
                        Message = c.String(nullable: false, maxLength: 500),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HeaderImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Page = c.String(maxLength: 20),
                        Image = c.String(maxLength: 150),
                        Title = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HomeSliders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Image = c.String(maxLength: 150),
                        Content = c.String(nullable: false, maxLength: 200),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subscribes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 30),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Testimonials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(maxLength: 150),
                        Content = c.String(nullable: false, maxLength: 500),
                        Fullname = c.String(nullable: false, maxLength: 50),
                        Position = c.String(nullable: false, maxLength: 50),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SlideAdopts", "AdoptId", "dbo.Adopts");
            DropForeignKey("dbo.Adopts", "PetSizeId", "dbo.PetSizes");
            DropForeignKey("dbo.Adopts", "GenderId", "dbo.Genders");
            DropForeignKey("dbo.Appointments", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Appointments", "AdoptId", "dbo.Adopts");
            DropForeignKey("dbo.Adopts", "AgeTypeId", "dbo.AgeTypes");
            DropForeignKey("dbo.TeamSocials", "TeamSocial_Id", "dbo.TeamSocials");
            DropForeignKey("dbo.TeamSocials", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Teams", "PositionId", "dbo.Positions");
            DropForeignKey("dbo.TeamSocials", "SocialTypeId", "dbo.SocialTypes");
            DropForeignKey("dbo.HomeSocials", "SocialTypeId", "dbo.SocialTypes");
            DropForeignKey("dbo.HomeSocials", "HomeSettingId", "dbo.HomeSettings");
            DropForeignKey("dbo.AdoptSocials", "SocialTypeId", "dbo.SocialTypes");
            DropForeignKey("dbo.AdoptSocials", "AdoptId", "dbo.Adopts");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Sales", "UserId", "dbo.Users");
            DropForeignKey("dbo.Sales", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.PetTypeToProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.PetTypeToProducts", "PetTypeId", "dbo.PetTypes");
            DropForeignKey("dbo.Products", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.CommentReplies", "UserId", "dbo.Users");
            DropForeignKey("dbo.CommentReplies", "CommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "BlogId", "dbo.Blogs");
            DropForeignKey("dbo.Blogs", "BlogCategoryId", "dbo.BlogCategories");
            DropForeignKey("dbo.Blogs", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.Articles", "AdminId", "dbo.Admins");
            DropIndex("dbo.SlideAdopts", new[] { "AdoptId" });
            DropIndex("dbo.Appointments", new[] { "AdoptId" });
            DropIndex("dbo.Appointments", new[] { "ServiceId" });
            DropIndex("dbo.Teams", new[] { "PositionId" });
            DropIndex("dbo.TeamSocials", new[] { "TeamSocial_Id" });
            DropIndex("dbo.TeamSocials", new[] { "TeamId" });
            DropIndex("dbo.TeamSocials", new[] { "SocialTypeId" });
            DropIndex("dbo.HomeSocials", new[] { "HomeSettingId" });
            DropIndex("dbo.HomeSocials", new[] { "SocialTypeId" });
            DropIndex("dbo.AdoptSocials", new[] { "AdoptId" });
            DropIndex("dbo.AdoptSocials", new[] { "SocialTypeId" });
            DropIndex("dbo.Adopts", new[] { "AgeTypeId" });
            DropIndex("dbo.Adopts", new[] { "PetSizeId" });
            DropIndex("dbo.Adopts", new[] { "GenderId" });
            DropIndex("dbo.ProductImages", new[] { "ProductId" });
            DropIndex("dbo.PetTypeToProducts", new[] { "PetTypeId" });
            DropIndex("dbo.PetTypeToProducts", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "AdminId" });
            DropIndex("dbo.Products", new[] { "ProductCategoryId" });
            DropIndex("dbo.Sales", new[] { "ProductId" });
            DropIndex("dbo.Sales", new[] { "UserId" });
            DropIndex("dbo.CommentReplies", new[] { "UserId" });
            DropIndex("dbo.CommentReplies", new[] { "CommentId" });
            DropIndex("dbo.Comments", new[] { "BlogId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Blogs", new[] { "AdminId" });
            DropIndex("dbo.Blogs", new[] { "BlogCategoryId" });
            DropIndex("dbo.Articles", new[] { "AdminId" });
            DropTable("dbo.Testimonials");
            DropTable("dbo.Subscribes");
            DropTable("dbo.HomeSliders");
            DropTable("dbo.HeaderImages");
            DropTable("dbo.Contacts");
            DropTable("dbo.SlideAdopts");
            DropTable("dbo.PetSizes");
            DropTable("dbo.Genders");
            DropTable("dbo.Services");
            DropTable("dbo.Appointments");
            DropTable("dbo.AgeTypes");
            DropTable("dbo.Positions");
            DropTable("dbo.Teams");
            DropTable("dbo.TeamSocials");
            DropTable("dbo.HomeSettings");
            DropTable("dbo.HomeSocials");
            DropTable("dbo.SocialTypes");
            DropTable("dbo.AdoptSocials");
            DropTable("dbo.Adopts");
            DropTable("dbo.AdoptRules");
            DropTable("dbo.ProductImages");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.PetTypes");
            DropTable("dbo.PetTypeToProducts");
            DropTable("dbo.Products");
            DropTable("dbo.Sales");
            DropTable("dbo.Users");
            DropTable("dbo.CommentReplies");
            DropTable("dbo.Comments");
            DropTable("dbo.BlogCategories");
            DropTable("dbo.Blogs");
            DropTable("dbo.Articles");
            DropTable("dbo.Admins");
        }
    }
}
