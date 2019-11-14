namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editZaposlenog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EditZaposleniViewModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Mehanicar_MehanicarId = c.Int(),
                        Mehanicar_Licenca = c.Int(),
                        Pilot_PilotId = c.Int(),
                        RoleViewModel_Id = c.String(maxLength: 128),
                        Stjuard_JezikId = c.Int(),
                        Stjuard_StjuardId = c.Int(),
                        Zaposleni_ZaposleniId = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Mehanicar", t => new { t.Mehanicar_MehanicarId, t.Mehanicar_Licenca })
                .ForeignKey("dbo.Pilot", t => t.Pilot_PilotId)
                .ForeignKey("dbo.RoleViewModels", t => t.RoleViewModel_Id)
                .ForeignKey("dbo.Stjuard", t => new { t.Stjuard_JezikId, t.Stjuard_StjuardId })
                .ForeignKey("dbo.Zaposleni", t => t.Zaposleni_ZaposleniId)
                .Index(t => new { t.Mehanicar_MehanicarId, t.Mehanicar_Licenca })
                .Index(t => t.Pilot_PilotId)
                .Index(t => t.RoleViewModel_Id)
                .Index(t => new { t.Stjuard_JezikId, t.Stjuard_StjuardId })
                .Index(t => t.Zaposleni_ZaposleniId);
            
            CreateTable(
                "dbo.RoleViewModels",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EditZaposleniViewModels", "Zaposleni_ZaposleniId", "dbo.Zaposleni");
            DropForeignKey("dbo.EditZaposleniViewModels", new[] { "Stjuard_JezikId", "Stjuard_StjuardId" }, "dbo.Stjuard");
            DropForeignKey("dbo.EditZaposleniViewModels", "RoleViewModel_Id", "dbo.RoleViewModels");
            DropForeignKey("dbo.EditZaposleniViewModels", "Pilot_PilotId", "dbo.Pilot");
            DropForeignKey("dbo.EditZaposleniViewModels", new[] { "Mehanicar_MehanicarId", "Mehanicar_Licenca" }, "dbo.Mehanicar");
            DropIndex("dbo.EditZaposleniViewModels", new[] { "Zaposleni_ZaposleniId" });
            DropIndex("dbo.EditZaposleniViewModels", new[] { "Stjuard_JezikId", "Stjuard_StjuardId" });
            DropIndex("dbo.EditZaposleniViewModels", new[] { "RoleViewModel_Id" });
            DropIndex("dbo.EditZaposleniViewModels", new[] { "Pilot_PilotId" });
            DropIndex("dbo.EditZaposleniViewModels", new[] { "Mehanicar_MehanicarId", "Mehanicar_Licenca" });
            DropTable("dbo.RoleViewModels");
            DropTable("dbo.EditZaposleniViewModels");
        }
    }
}
