namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleManager_6 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Zaposleni", "IdentityId", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Zaposleni", "IdentityId", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
