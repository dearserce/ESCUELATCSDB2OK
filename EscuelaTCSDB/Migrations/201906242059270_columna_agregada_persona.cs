namespace EscuelaTCSDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class columna_agregada_persona : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PersonaId", c => c.Int(nullable: true));
            CreateIndex("dbo.AspNetUsers", "PersonaId");
            AddForeignKey("dbo.AspNetUsers", "PersonaId", "dbo.Personas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "PersonaId", "dbo.Personas");
            DropIndex("dbo.AspNetUsers", new[] { "PersonaId" });
            DropColumn("dbo.AspNetUsers", "PersonaId");
        }
    }
}
