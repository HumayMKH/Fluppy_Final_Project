namespace Fluppy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iconblog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogCategories", "Icon", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BlogCategories", "Icon");
        }
    }
}
