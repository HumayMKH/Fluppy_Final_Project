namespace Fluppy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class blobigimage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "ImageBig", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blogs", "ImageBig");
        }
    }
}
