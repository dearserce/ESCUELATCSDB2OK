namespace EscuelaTCSDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validacionesVistapersona : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Personas", new[] { "email" });
            AlterColumn("dbo.Personas", "email", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.Personas", "email", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Personas", new[] { "email" });
            AlterColumn("dbo.Personas", "email", c => c.String(maxLength: 100));
            CreateIndex("dbo.Personas", "email", unique: true);
        }
    }
}
