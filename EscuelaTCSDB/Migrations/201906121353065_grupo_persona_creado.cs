namespace EscuelaTCSDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class grupo_persona_creado : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GrupoPersonas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GrupoId = c.Int(nullable: false),
                        MateriaId = c.Int(nullable: false),
                        CicloId = c.Int(nullable: false),
                        PersonaId = c.Int(nullable: false),
                        ProfesorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cicloes", t => t.CicloId, cascadeDelete: true)
                .ForeignKey("dbo.Grupoes", t => t.GrupoId, cascadeDelete: true)
                .ForeignKey("dbo.Materias", t => t.MateriaId, cascadeDelete: true)
                .ForeignKey("dbo.Personas", t => t.PersonaId, cascadeDelete: true)
                .ForeignKey("dbo.Personas", t => t.ProfesorId, cascadeDelete: false)
                .Index(t => t.GrupoId)
                .Index(t => t.MateriaId)
                .Index(t => t.CicloId)
                .Index(t => t.PersonaId)
                .Index(t => t.ProfesorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GrupoPersonas", "ProfesorId", "dbo.Personas");
            DropForeignKey("dbo.GrupoPersonas", "PersonaId", "dbo.Personas");
            DropForeignKey("dbo.GrupoPersonas", "MateriaId", "dbo.Materias");
            DropForeignKey("dbo.GrupoPersonas", "GrupoId", "dbo.Grupoes");
            DropForeignKey("dbo.GrupoPersonas", "CicloId", "dbo.Cicloes");
            DropIndex("dbo.GrupoPersonas", new[] { "ProfesorId" });
            DropIndex("dbo.GrupoPersonas", new[] { "PersonaId" });
            DropIndex("dbo.GrupoPersonas", new[] { "CicloId" });
            DropIndex("dbo.GrupoPersonas", new[] { "MateriaId" });
            DropIndex("dbo.GrupoPersonas", new[] { "GrupoId" });
            DropTable("dbo.GrupoPersonas");
        }
    }
}
