namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nepotpunaPosada : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Let", "NepotpunaPosada", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Let", "NepotpunaPosada");
        }
    }
}
