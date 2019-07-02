namespace EscuelaTCSDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidacionesConAnotationsPersona : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Personas", "password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Personas", "password", c => c.String());
        }
    }
}
