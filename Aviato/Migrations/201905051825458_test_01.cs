namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test_01 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jezik", "Jezik", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jezik", "Jezik", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
