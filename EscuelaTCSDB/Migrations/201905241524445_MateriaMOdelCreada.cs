namespace EscuelaTCSDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MateriaMOdelCreada : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Materias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 120),
                        descripcion = c.String(maxLength: 255),
                        activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.nombre, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Materias", new[] { "nombre" });
            DropTable("dbo.Materias");
        }
    }
}
