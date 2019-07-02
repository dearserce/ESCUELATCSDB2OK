namespace EscuelaTCSDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class calificaciones_agregado : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Calificacions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GpPeriodoId = c.Int(nullable: false),
                        calificacion = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GpPeriodoes", t => t.GpPeriodoId, cascadeDelete: true)
                .Index(t => t.GpPeriodoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Calificacions", "GpPeriodoId", "dbo.GpPeriodoes");
            DropIndex("dbo.Calificacions", new[] { "GpPeriodoId" });
            DropTable("dbo.Calificacions");
        }
    }
}
