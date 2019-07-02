namespace EscuelaTCSDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gp_periodo_modificado : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GpPeriodoes", "GrupoPersona_Id", "dbo.GrupoPersonas");
            DropIndex("dbo.GpPeriodoes", new[] { "GrupoPersona_Id" });
            RenameColumn(table: "dbo.GpPeriodoes", name: "GrupoPersona_Id", newName: "GrupoPersonaId");
            AlterColumn("dbo.GpPeriodoes", "periodo", c => c.Int(nullable: false));
            AlterColumn("dbo.GpPeriodoes", "GrupoPersonaId", c => c.Int(nullable: false));
            CreateIndex("dbo.GpPeriodoes", "GrupoPersonaId");
            AddForeignKey("dbo.GpPeriodoes", "GrupoPersonaId", "dbo.GrupoPersonas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GpPeriodoes", "GrupoPersonaId", "dbo.GrupoPersonas");
            DropIndex("dbo.GpPeriodoes", new[] { "GrupoPersonaId" });
            AlterColumn("dbo.GpPeriodoes", "GrupoPersonaId", c => c.Int());
            AlterColumn("dbo.GpPeriodoes", "periodo", c => c.String());
            RenameColumn(table: "dbo.GpPeriodoes", name: "GrupoPersonaId", newName: "GrupoPersona_Id");
            CreateIndex("dbo.GpPeriodoes", "GrupoPersona_Id");
            AddForeignKey("dbo.GpPeriodoes", "GrupoPersona_Id", "dbo.GrupoPersonas", "Id");
        }
    }
}
