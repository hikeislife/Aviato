namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class poslednjeAzuriranje : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pilot", "PoslednjeAzuriranje", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pilot", "PoslednjeAzuriranje");
        }
    }
}
