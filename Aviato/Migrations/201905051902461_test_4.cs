namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test_4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jezik", "Jezik", c => c.String(maxLength: 20));
            AlterColumn("dbo.Zaposleni", "IdentityId", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Zaposleni", "IdentityId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Jezik", "Jezik", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
