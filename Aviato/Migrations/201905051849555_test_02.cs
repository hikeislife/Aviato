namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test_02 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Zaposleni", "JMBG", c => c.String());
            AlterColumn("dbo.Zaposleni", "Ime", c => c.String());
            AlterColumn("dbo.Zaposleni", "Prezime", c => c.String());
            AlterColumn("dbo.Zaposleni", "IdentityId", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Zaposleni", "IdentityId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Zaposleni", "Prezime", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Zaposleni", "Ime", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Zaposleni", "JMBG", c => c.String(nullable: false));
        }
    }
}
