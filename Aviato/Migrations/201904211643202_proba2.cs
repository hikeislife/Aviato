namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class proba2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Mehanicar", "EditZaposleniViewModel_Id", "dbo.EditZaposleniViewModels");
            DropForeignKey("dbo.EditZaposleniViewModels", "Pilot_PilotId", "dbo.Pilot");
            DropForeignKey("dbo.EditZaposleniViewModels", "RoleViewModel_Id", "dbo.RoleViewModels");
            DropForeignKey("dbo.Stjuard", "EditZaposleniViewModel_Id", "dbo.EditZaposleniViewModels");
            DropForeignKey("dbo.EditZaposleniViewModels", "Zaposleni_ZaposleniId", "dbo.Zaposleni");
            //DropIndex("dbo.Stjuard", new[] { "EditZaposleniViewModel_Id" });
            //DropIndex("dbo.Mehanicar", new[] { "EditZaposleniViewModel_Id" });
            //DropIndex("dbo.EditZaposleniViewModels", new[] { "Pilot_PilotId" });
            //DropIndex("dbo.EditZaposleniViewModels", new[] { "RoleViewModel_Id" });
            //DropIndex("dbo.EditZaposleniViewModels", new[] { "Zaposleni_ZaposleniId" });
            //DropColumn("dbo.Stjuard", "EditZaposleniViewModel_Id");
            //DropColumn("dbo.Mehanicar", "EditZaposleniViewModel_Id");
            DropTable("dbo.EditZaposleniViewModels");
            DropTable("dbo.RoleViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RoleViewModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EditZaposleniViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pilot_PilotId = c.Int(),
                        RoleViewModel_Id = c.String(maxLength: 128),
                        Zaposleni_ZaposleniId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Mehanicar", "EditZaposleniViewModel_Id", c => c.Int());
            AddColumn("dbo.Stjuard", "EditZaposleniViewModel_Id", c => c.Int());
            CreateIndex("dbo.EditZaposleniViewModels", "Zaposleni_ZaposleniId");
            CreateIndex("dbo.EditZaposleniViewModels", "RoleViewModel_Id");
            CreateIndex("dbo.EditZaposleniViewModels", "Pilot_PilotId");
            CreateIndex("dbo.Mehanicar", "EditZaposleniViewModel_Id");
            CreateIndex("dbo.Stjuard", "EditZaposleniViewModel_Id");
            AddForeignKey("dbo.EditZaposleniViewModels", "Zaposleni_ZaposleniId", "dbo.Zaposleni", "ZaposleniId");
            AddForeignKey("dbo.Stjuard", "EditZaposleniViewModel_Id", "dbo.EditZaposleniViewModels", "Id");
            AddForeignKey("dbo.EditZaposleniViewModels", "RoleViewModel_Id", "dbo.RoleViewModels", "Id");
            AddForeignKey("dbo.EditZaposleniViewModels", "Pilot_PilotId", "dbo.Pilot", "PilotId");
            AddForeignKey("dbo.Mehanicar", "EditZaposleniViewModel_Id", "dbo.EditZaposleniViewModels", "Id");
        }
    }
}
