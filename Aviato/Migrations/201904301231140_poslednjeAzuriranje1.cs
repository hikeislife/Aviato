namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class poslednjeAzuriranje1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pilot", "PoslednjeAzuriranje", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pilot", "PoslednjeAzuriranje", c => c.DateTime(nullable: false));
        }
    }
}
