namespace EscuelaTCSDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Personas", "foto", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Personas", "foto");
        }
    }
}
