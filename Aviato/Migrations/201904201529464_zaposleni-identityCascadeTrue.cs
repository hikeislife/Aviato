namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zaposleniidentityCascadeTrue : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pilot", "SifraPilota", "dbo.Zaposleni");
            AddForeignKey("dbo.Pilot", "SifraPilota", "dbo.Zaposleni", "ZaposleniId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pilot", "SifraPilota", "dbo.Zaposleni");
            AddForeignKey("dbo.Pilot", "SifraPilota", "dbo.Zaposleni", "ZaposleniId");
        }
    }
}
