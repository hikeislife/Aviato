namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aviatoMigration_2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Avion",
                c => new
                    {
                        AvionId = c.Int(nullable: false, identity: true),
                        GodinaProizvodnje = c.Int(nullable: false),
                        ServisniStatus = c.Boolean(nullable: false),
                        TipAviona = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AvionId)
                .ForeignKey("dbo.Tip", t => t.TipAviona)
                .Index(t => t.TipAviona);
            
            CreateTable(
                "dbo.Let",
                c => new
                    {
                        LetId = c.Int(nullable: false, identity: true),
                        Destinacija = c.Int(nullable: false),
                        Avion = c.Int(nullable: false),
                        VremePoletanja = c.DateTime(nullable: false),
                        Pilot = c.Int(nullable: false),
                        Kopilot = c.Int(nullable: false),
                        Stjuard1 = c.Int(nullable: false),
                        Stjuard2 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LetId)
                .ForeignKey("dbo.Zaposleni", t => t.Kopilot)
                .ForeignKey("dbo.Zaposleni", t => t.Pilot)
                .ForeignKey("dbo.Zaposleni", t => t.Stjuard1)
                .ForeignKey("dbo.Zaposleni", t => t.Stjuard2)
                .ForeignKey("dbo.Destinacija", t => t.Destinacija)
                .ForeignKey("dbo.Avion", t => t.Avion)
                .Index(t => t.Destinacija)
                .Index(t => t.Avion)
                .Index(t => t.Pilot)
                .Index(t => t.Kopilot)
                .Index(t => t.Stjuard1)
                .Index(t => t.Stjuard2);
            
            CreateTable(
                "dbo.Destinacija",
                c => new
                    {
                        DestinacijaId = c.Int(nullable: false, identity: true),
                        Naziv = c.String(nullable: false, maxLength: 20),
                        TrajanjeLeta = c.Int(nullable: false),
                        Jezik = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DestinacijaId)
                .ForeignKey("dbo.Jezik", t => t.Jezik)
                .Index(t => t.Jezik);
            
            CreateTable(
                "dbo.Jezik",
                c => new
                    {
                        JezikId = c.Int(nullable: false, identity: true),
                        Jezik = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.JezikId);
            
            CreateTable(
                "dbo.Stjuard",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JezikId = c.Int(nullable: false),
                        StjuardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jezik", t => t.JezikId, cascadeDelete: true)
                .ForeignKey("dbo.Zaposleni", t => t.StjuardId, cascadeDelete: true)
                .Index(t => t.JezikId)
                .Index(t => t.StjuardId);
            
            CreateTable(
                "dbo.Zaposleni",
                c => new
                    {
                        ZaposleniId = c.Int(nullable: false, identity: true),
                        JMBG = c.String(nullable: false, maxLength: 13),
                        Ime = c.String(nullable: false, maxLength: 20),
                        Prezime = c.String(nullable: false, maxLength: 20),
                        GodinaRodjenja = c.Int(nullable: false),
                        IdentityId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ZaposleniId)
                .ForeignKey("dbo.AspNetUsers", t => t.IdentityId);
            
            CreateTable(
                "dbo.Mehanicar",
                c => new
                    {
                        MehanicarId = c.Int(nullable: false),
                        Licenca = c.Int(nullable: false),
                        DatumLicence = c.DateTime(nullable: false, storeType: "smalldatetime"),
                    })
                .PrimaryKey(t => new { t.MehanicarId, t.Licenca })
                .ForeignKey("dbo.Tip", t => t.Licenca)
                .ForeignKey("dbo.Zaposleni", t => t.MehanicarId)
                .Index(t => t.MehanicarId)
                .Index(t => t.Licenca);
            
            CreateTable(
                "dbo.Tip",
                c => new
                    {
                        TipId = c.Int(nullable: false, identity: true),
                        NazivTipa = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.TipId);
            
            CreateTable(
                "dbo.Pilot",
                c => new
                    {
                        PilotId = c.Int(nullable: false, identity: true),
                        PoslednjiMedicinski = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        OcenaZS = c.Boolean(nullable: false),
                        SatiLetenja = c.Int(nullable: false),
                        SifraPilota = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PilotId)
                .ForeignKey("dbo.Zaposleni", t => t.SifraPilota)
                .Index(t => t.SifraPilota);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Let", "Avion", "dbo.Avion");
            DropForeignKey("dbo.Let", "Destinacija", "dbo.Destinacija");
            DropForeignKey("dbo.Stjuard", "StjuardId", "dbo.Zaposleni");
            DropForeignKey("dbo.Pilot", "SifraPilota", "dbo.Zaposleni");
            DropForeignKey("dbo.Mehanicar", "MehanicarId", "dbo.Zaposleni");
            DropForeignKey("dbo.Mehanicar", "Licenca", "dbo.Tip");
            DropForeignKey("dbo.Avion", "TipAviona", "dbo.Tip");
            DropForeignKey("dbo.Let", "Stjuard2", "dbo.Zaposleni");
            DropForeignKey("dbo.Let", "Stjuard1", "dbo.Zaposleni");
            DropForeignKey("dbo.Let", "Pilot", "dbo.Zaposleni");
            DropForeignKey("dbo.Let", "Kopilot", "dbo.Zaposleni");
            DropForeignKey("dbo.Stjuard", "JezikId", "dbo.Jezik");
            DropForeignKey("dbo.Destinacija", "Jezik", "dbo.Jezik");
            DropForeignKey("dbo.AspNetUsers","IdentityId", "dbo.Zaposleni");
            DropIndex("dbo.Pilot", new[] { "SifraPilota" });
            DropIndex("dbo.Mehanicar", new[] { "Licenca" });
            DropIndex("dbo.Mehanicar", new[] { "MehanicarId" });
            DropIndex("dbo.Stjuard", new[] { "StjuardId" });
            DropIndex("dbo.Stjuard", new[] { "JezikId" });
            DropIndex("dbo.Destinacija", new[] { "Jezik" });
            DropIndex("dbo.Let", new[] { "Stjuard2" });
            DropIndex("dbo.Let", new[] { "Stjuard1" });
            DropIndex("dbo.Let", new[] { "Kopilot" });
            DropIndex("dbo.Let", new[] { "Pilot" });
            DropIndex("dbo.Let", new[] { "Avion" });
            DropIndex("dbo.Let", new[] { "Destinacija" });
            DropIndex("dbo.Avion", new[] { "TipAviona" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Pilot");
            DropTable("dbo.Tip");
            DropTable("dbo.Mehanicar");
            DropTable("dbo.Zaposleni");
            DropTable("dbo.Stjuard");
            DropTable("dbo.Jezik");
            DropTable("dbo.Destinacija");
            DropTable("dbo.Let");
            DropTable("dbo.Avion");
        }
    }
}
