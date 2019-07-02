namespace EscuelaTCSDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tablaGRupoGenerada : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Grupoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        codigo = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.codigo, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Grupoes", new[] { "codigo" });
            DropTable("dbo.Grupoes");
        }
    }
}
