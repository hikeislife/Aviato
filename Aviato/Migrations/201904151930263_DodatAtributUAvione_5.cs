namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodatAtributUAvione_5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Avion", "SifraAviona", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Avion", "SifraAviona");
        }
    }
}
