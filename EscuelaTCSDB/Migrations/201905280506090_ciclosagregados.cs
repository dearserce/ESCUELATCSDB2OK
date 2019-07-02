namespace EscuelaTCSDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ciclosagregados : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cicloes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ModalidadId = c.Int(nullable: false),
                        fecha_inicio = c.DateTime(nullable: false),
                        fecha_fin = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cicloes");
        }
    }
}
