namespace Fluppy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class type : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PetTypePetTypeToProducts", "PetType_Id", "dbo.PetTypes");
            DropForeignKey("dbo.PetTypePetTypeToProducts", "PetTypeToProduct_Id", "dbo.PetTypeToProducts");
            DropIndex("dbo.PetTypePetTypeToProducts", new[] { "PetType_Id" });
            DropIndex("dbo.PetTypePetTypeToProducts", new[] { "PetTypeToProduct_Id" });
            CreateIndex("dbo.PetTypeToProducts", "PetTypeId");
            AddForeignKey("dbo.PetTypeToProducts", "PetTypeId", "dbo.PetTypes", "Id", cascadeDelete: true);
            DropTable("dbo.PetTypePetTypeToProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PetTypePetTypeToProducts",
                c => new
                    {
                        PetType_Id = c.Int(nullable: false),
                        PetTypeToProduct_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PetType_Id, t.PetTypeToProduct_Id });
            
            DropForeignKey("dbo.PetTypeToProducts", "PetTypeId", "dbo.PetTypes");
            DropIndex("dbo.PetTypeToProducts", new[] { "PetTypeId" });
            CreateIndex("dbo.PetTypePetTypeToProducts", "PetTypeToProduct_Id");
            CreateIndex("dbo.PetTypePetTypeToProducts", "PetType_Id");
            AddForeignKey("dbo.PetTypePetTypeToProducts", "PetTypeToProduct_Id", "dbo.PetTypeToProducts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PetTypePetTypeToProducts", "PetType_Id", "dbo.PetTypes", "Id", cascadeDelete: true);
        }
    }
}
