namespace EscuelaTCSDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullable_persona : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "PersonaId", "dbo.Personas");
            DropIndex("dbo.AspNetUsers", new[] { "PersonaId" });
            AlterColumn("dbo.AspNetUsers", "PersonaId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "PersonaId");
            AddForeignKey("dbo.AspNetUsers", "PersonaId", "dbo.Personas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "PersonaId", "dbo.Personas");
            DropIndex("dbo.AspNetUsers", new[] { "PersonaId" });
            AlterColumn("dbo.AspNetUsers", "PersonaId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "PersonaId");
            AddForeignKey("dbo.AspNetUsers", "PersonaId", "dbo.Personas", "Id", cascadeDelete: true);
        }
    }
}
