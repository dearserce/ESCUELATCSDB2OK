namespace EscuelaTCSDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modalidadGenerada : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Modalidads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        descripcion = c.String(nullable: false, maxLength: 255),
                        n_meses = c.Int(nullable: false),
                        n_periodos = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.descripcion, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Modalidads", new[] { "descripcion" });
            DropTable("dbo.Modalidads");
        }
    }
}
