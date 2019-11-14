namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodatAvionUezvm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Avion", "EditZaposleniViewModel_Id", c => c.Int());
            CreateIndex("dbo.Avion", "EditZaposleniViewModel_Id");
            AddForeignKey("dbo.Avion", "EditZaposleniViewModel_Id", "dbo.EditZaposleniViewModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Avion", "EditZaposleniViewModel_Id", "dbo.EditZaposleniViewModels");
            DropIndex("dbo.Avion", new[] { "EditZaposleniViewModel_Id" });
            DropColumn("dbo.Avion", "EditZaposleniViewModel_Id");
        }
    }
}
