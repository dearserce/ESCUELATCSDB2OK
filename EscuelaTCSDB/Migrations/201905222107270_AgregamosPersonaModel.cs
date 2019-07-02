namespace EscuelaTCSDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregamosPersonaModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Personas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        email = c.String(maxLength: 100),
                        nombre = c.String(),
                        apellido = c.String(),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.email, unique: true);
            
            AddColumn("dbo.TipoPersonas", "PersonaId", c => c.Int());
            CreateIndex("dbo.TipoPersonas", "PersonaId");
            AddForeignKey("dbo.TipoPersonas", "PersonaId", "dbo.Personas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TipoPersonas", "PersonaId", "dbo.Personas");
            DropIndex("dbo.TipoPersonas", new[] { "PersonaId" });
            DropIndex("dbo.Personas", new[] { "email" });
            DropColumn("dbo.TipoPersonas", "PersonaId");
            DropTable("dbo.Personas");
        }
    }
}
