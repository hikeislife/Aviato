namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodatLetUEzvm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Let", "EditZaposleniViewModel_Id", c => c.Int());
            CreateIndex("dbo.Let", "EditZaposleniViewModel_Id");
            AddForeignKey("dbo.Let", "EditZaposleniViewModel_Id", "dbo.EditZaposleniViewModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Let", "EditZaposleniViewModel_Id", "dbo.EditZaposleniViewModels");
            DropIndex("dbo.Let", new[] { "EditZaposleniViewModel_Id" });
            DropColumn("dbo.Let", "EditZaposleniViewModel_Id");
        }
    }
}
