namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editStjuard : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Stjuard");
            AddPrimaryKey("dbo.Stjuard", new[] { "JezikId", "StjuardId" });
            DropColumn("dbo.Stjuard", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stjuard", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Stjuard");
            AddPrimaryKey("dbo.Stjuard", "Id");
        }
    }
}
