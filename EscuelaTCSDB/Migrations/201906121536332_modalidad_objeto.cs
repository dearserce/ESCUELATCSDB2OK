namespace EscuelaTCSDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modalidad_objeto : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Cicloes", "ModalidadId");
            AddForeignKey("dbo.Cicloes", "ModalidadId", "dbo.Modalidads", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cicloes", "ModalidadId", "dbo.Modalidads");
            DropIndex("dbo.Cicloes", new[] { "ModalidadId" });
        }
    }
}
