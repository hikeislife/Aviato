namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editMehanicar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EditZaposleniViewModels", "promenaLicenci", c => c.String());
            AddColumn("dbo.EditZaposleniViewModels", "promenaDatuma", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EditZaposleniViewModels", "promenaDatuma");
            DropColumn("dbo.EditZaposleniViewModels", "promenaLicenci");
        }
    }
}
