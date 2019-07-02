namespace EscuelaTCSDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class objetotipopersona : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Personas", "TipoPersonaId");
            AddForeignKey("dbo.Personas", "TipoPersonaId", "dbo.TipoPersonas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Personas", "TipoPersonaId", "dbo.TipoPersonas");
            DropIndex("dbo.Personas", new[] { "TipoPersonaId" });
        }
    }
}
