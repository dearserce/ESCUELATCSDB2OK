namespace EscuelaTCSDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class arregloIdTipoPersona : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TipoPersonas", "PersonaId", "dbo.Personas");
            DropIndex("dbo.TipoPersonas", new[] { "PersonaId" });
            AddColumn("dbo.Personas", "TipoPersonaId", c => c.Int(nullable: false));
            DropColumn("dbo.TipoPersonas", "PersonaId");
            Sql("SET IDENTITY_INSERT TipoPersonas ON");
            Sql("Insert into TipoPersonas (Id,descripcion) values(1,'Alumno')");
            Sql("Insert into TipoPersonas (Id,descripcion) values(2,'Profesor')");
            Sql("Insert into TipoPersonas (Id,descripcion) values(3,'Directivo')");
            Sql("SET IDENTITY_INSERT TipoPersonas OFF");

        }

        public override void Down()
        {
            AddColumn("dbo.TipoPersonas", "PersonaId", c => c.Int());
            DropColumn("dbo.Personas", "TipoPersonaId");
            CreateIndex("dbo.TipoPersonas", "PersonaId");
            AddForeignKey("dbo.TipoPersonas", "PersonaId", "dbo.Personas", "Id");
        }
    }
}
