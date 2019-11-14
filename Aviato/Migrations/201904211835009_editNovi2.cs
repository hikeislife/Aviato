namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editNovi2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EditZaposleniViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pilot_PilotId = c.Int(),
                        RoleViewModel_Id = c.String(maxLength: 128),
                        Zaposleni_ZaposleniId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pilot", t => t.Pilot_PilotId)
                .ForeignKey("dbo.RoleViewModels", t => t.RoleViewModel_Id)
                .ForeignKey("dbo.Zaposleni", t => t.Zaposleni_ZaposleniId)
                .Index(t => t.Pilot_PilotId)
                .Index(t => t.RoleViewModel_Id)
                .Index(t => t.Zaposleni_ZaposleniId);
            
            CreateTable(
                "dbo.RoleViewModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Stjuard", "EditZaposleniViewModel_Id", c => c.Int());
            AddColumn("dbo.Mehanicar", "EditZaposleniViewModel_Id", c => c.Int());
            CreateIndex("dbo.Stjuard", "EditZaposleniViewModel_Id");
            CreateIndex("dbo.Mehanicar", "EditZaposleniViewModel_Id");
            AddForeignKey("dbo.Mehanicar", "EditZaposleniViewModel_Id", "dbo.EditZaposleniViewModels", "Id");
            AddForeignKey("dbo.Stjuard", "EditZaposleniViewModel_Id", "dbo.EditZaposleniViewModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EditZaposleniViewModels", "Zaposleni_ZaposleniId", "dbo.Zaposleni");
            DropForeignKey("dbo.Stjuard", "EditZaposleniViewModel_Id", "dbo.EditZaposleniViewModels");
            DropForeignKey("dbo.EditZaposleniViewModels", "RoleViewModel_Id", "dbo.RoleViewModels");
            DropForeignKey("dbo.EditZaposleniViewModels", "Pilot_PilotId", "dbo.Pilot");
            DropForeignKey("dbo.Mehanicar", "EditZaposleniViewModel_Id", "dbo.EditZaposleniViewModels");
            DropIndex("dbo.EditZaposleniViewModels", new[] { "Zaposleni_ZaposleniId" });
            DropIndex("dbo.EditZaposleniViewModels", new[] { "RoleViewModel_Id" });
            DropIndex("dbo.EditZaposleniViewModels", new[] { "Pilot_PilotId" });
            DropIndex("dbo.Mehanicar", new[] { "EditZaposleniViewModel_Id" });
            DropIndex("dbo.Stjuard", new[] { "EditZaposleniViewModel_Id" });
            DropColumn("dbo.Mehanicar", "EditZaposleniViewModel_Id");
            DropColumn("dbo.Stjuard", "EditZaposleniViewModel_Id");
            DropTable("dbo.RoleViewModels");
            DropTable("dbo.EditZaposleniViewModels");
        }
    }
}
