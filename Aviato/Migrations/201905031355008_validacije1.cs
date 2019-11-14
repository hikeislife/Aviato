namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validacije1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jezik", "Jezik", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Tip", "NazivTipa", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.RoleViewModels", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RoleViewModels", "Name", c => c.String());
            AlterColumn("dbo.Tip", "NazivTipa", c => c.String(maxLength: 20));
            AlterColumn("dbo.Jezik", "Jezik", c => c.String(maxLength: 20));
        }
    }
}
