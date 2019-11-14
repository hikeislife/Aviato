namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idLetaZaKalendar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Let", "IdLeta", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Let", "IdLeta");
        }
    }
}
