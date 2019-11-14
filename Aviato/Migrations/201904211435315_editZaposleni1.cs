namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editZaposleni1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EditZaposleniViewModels", new[] { "Mehanicar_MehanicarId", "Mehanicar_Licenca" }, "dbo.Mehanicar");
            DropForeignKey("dbo.EditZaposleniViewModels", new[] { "Stjuard_JezikId", "Stjuard_StjuardId" }, "dbo.Stjuard");
            DropIndex("dbo.EditZaposleniViewModels", new[] { "Mehanicar_MehanicarId", "Mehanicar_Licenca" });
            DropIndex("dbo.EditZaposleniViewModels", new[] { "Stjuard_JezikId", "Stjuard_StjuardId" });
            RenameColumn(table: "dbo.Mehanicar", name: "Mehanicar_MehanicarId", newName: "Mehanicar_MehanicarId");
            RenameColumn(table: "dbo.Stjuard", name: "Stjuard_JezikId", newName: "Stjuard_JezikId");
            CreateIndex("dbo.Stjuard", "Stjuard_JezikId");
            CreateIndex("dbo.Mehanicar", "Mehanicar_MehanicarId");
            AddForeignKey("dbo.Mehanicar", "Mehanicar_MehanicarId", "dbo.EditZaposleniViewModels", "Id");
            AddForeignKey("dbo.Stjuard", "Stjuard_JezikId", "dbo.EditZaposleniViewModels", "Id");
            DropColumn("dbo.EditZaposleniViewModels", "Mehanicar_MehanicarId");
            DropColumn("dbo.EditZaposleniViewModels", "Mehanicar_Licenca");
            DropColumn("dbo.EditZaposleniViewModels", "Stjuard_JezikId");
            DropColumn("dbo.EditZaposleniViewModels", "Stjuard_StjuardId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EditZaposleniViewModels", "Stjuard_StjuardId", c => c.Int());
            AddColumn("dbo.EditZaposleniViewModels", "Stjuard_JezikId", c => c.Int());
            AddColumn("dbo.EditZaposleniViewModels", "Mehanicar_Licenca", c => c.Int());
            AddColumn("dbo.EditZaposleniViewModels", "Mehanicar_MehanicarId", c => c.Int());
            DropForeignKey("dbo.Stjuard", "EditZaposleniViewModel_Id", "dbo.EditZaposleniViewModels");
            DropForeignKey("dbo.Mehanicar", "EditZaposleniViewModel_Id", "dbo.EditZaposleniViewModels");
            DropIndex("dbo.Mehanicar", new[] { "EditZaposleniViewModel_Id" });
            DropIndex("dbo.Stjuard", new[] { "EditZaposleniViewModel_Id" });
            RenameColumn(table: "dbo.Stjuard", name: "EditZaposleniViewModel_Id", newName: "Stjuard_JezikId");
            RenameColumn(table: "dbo.Mehanicar", name: "EditZaposleniViewModel_Id", newName: "Mehanicar_MehanicarId");
            CreateIndex("dbo.EditZaposleniViewModels", new[] { "Stjuard_JezikId", "Stjuard_StjuardId" });
            CreateIndex("dbo.EditZaposleniViewModels", new[] { "Mehanicar_MehanicarId", "Mehanicar_Licenca" });
            AddForeignKey("dbo.EditZaposleniViewModels", new[] { "Stjuard_JezikId", "Stjuard_StjuardId" }, "dbo.Stjuard", new[] { "JezikId", "StjuardId" });
            AddForeignKey("dbo.EditZaposleniViewModels", new[] { "Mehanicar_MehanicarId", "Mehanicar_Licenca" }, "dbo.Mehanicar", new[] { "MehanicarId", "Licenca" });
        }
    }
}
