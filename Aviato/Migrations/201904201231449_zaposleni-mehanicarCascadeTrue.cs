namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zaposlenimehanicarCascadeTrue : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Mehanicar", "MehanicarId", "dbo.Zaposleni");
            AddForeignKey("dbo.Mehanicar", "MehanicarId", "dbo.Zaposleni", "ZaposleniId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mehanicar", "MehanicarId", "dbo.Zaposleni");
            AddForeignKey("dbo.Mehanicar", "MehanicarId", "dbo.Zaposleni", "ZaposleniId");
        }
    }
}
