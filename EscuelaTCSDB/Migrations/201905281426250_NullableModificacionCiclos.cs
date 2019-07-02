namespace EscuelaTCSDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableModificacionCiclos : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cicloes", "fecha_inicio", c => c.DateTime());
            AlterColumn("dbo.Cicloes", "fecha_fin", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cicloes", "fecha_fin", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Cicloes", "fecha_inicio", c => c.DateTime(nullable: false));
        }
    }
}
