namespace EscuelaTCSDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gp_periodo_creado : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GpPeriodoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        periodo = c.String(),
                        GrupoPersona_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GrupoPersonas", t => t.GrupoPersona_Id)
                .Index(t => t.GrupoPersona_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GpPeriodoes", "GrupoPersona_Id", "dbo.GrupoPersonas");
            DropIndex("dbo.GpPeriodoes", new[] { "GrupoPersona_Id" });
            DropTable("dbo.GpPeriodoes");
        }
    }
}
