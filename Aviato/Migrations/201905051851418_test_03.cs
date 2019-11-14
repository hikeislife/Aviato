namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test_03 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Zaposleni", "JMBG", c => c.String(nullable: false));
            AlterColumn("dbo.Zaposleni", "Ime", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Zaposleni", "Prezime", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Zaposleni", "Prezime", c => c.String());
            AlterColumn("dbo.Zaposleni", "Ime", c => c.String());
            AlterColumn("dbo.Zaposleni", "JMBG", c => c.String());
        }
    }
}
