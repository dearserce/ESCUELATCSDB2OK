namespace EscuelaTCSDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validacionparatIpopersona : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TipoPersonas", new[] { "descripcion" });
            AlterColumn("dbo.TipoPersonas", "descripcion", c => c.String(nullable: false, maxLength: 100));
            CreateIndex("dbo.TipoPersonas", "descripcion", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.TipoPersonas", new[] { "descripcion" });
            AlterColumn("dbo.TipoPersonas", "descripcion", c => c.String(maxLength: 100));
            CreateIndex("dbo.TipoPersonas", "descripcion", unique: true);
        }
    }
}
