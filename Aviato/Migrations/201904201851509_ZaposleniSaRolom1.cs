namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ZaposleniSaRolom1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ZaposleniViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImeIPrezime = c.String(),
                        GodinaRodjenja = c.Int(nullable: false),
                        JMBG = c.String(),
                        Email = c.String(),
                        Pozicija = c.String(),
                        ZaposleniId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ZaposleniViewModels");
        }
    }
}
