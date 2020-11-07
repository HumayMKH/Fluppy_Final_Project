namespace Fluppy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pettype : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PetTypeToProducts", "PetTypeId", "dbo.PetTypes");
            DropIndex("dbo.PetTypeToProducts", new[] { "PetTypeId" });
            CreateTable(
                "dbo.PetTypePetTypeToProducts",
                c => new
                    {
                        PetType_Id = c.Int(nullable: false),
                        PetTypeToProduct_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PetType_Id, t.PetTypeToProduct_Id })
                .ForeignKey("dbo.PetTypes", t => t.PetType_Id, cascadeDelete: true)
                .ForeignKey("dbo.PetTypeToProducts", t => t.PetTypeToProduct_Id, cascadeDelete: true)
                .Index(t => t.PetType_Id)
                .Index(t => t.PetTypeToProduct_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PetTypePetTypeToProducts", "PetTypeToProduct_Id", "dbo.PetTypeToProducts");
            DropForeignKey("dbo.PetTypePetTypeToProducts", "PetType_Id", "dbo.PetTypes");
            DropIndex("dbo.PetTypePetTypeToProducts", new[] { "PetTypeToProduct_Id" });
            DropIndex("dbo.PetTypePetTypeToProducts", new[] { "PetType_Id" });
            DropTable("dbo.PetTypePetTypeToProducts");
            CreateIndex("dbo.PetTypeToProducts", "PetTypeId");
            AddForeignKey("dbo.PetTypeToProducts", "PetTypeId", "dbo.PetTypes", "Id", cascadeDelete: true);
        }
    }
}
