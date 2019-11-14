namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validacije2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Zaposleni", "JMBG", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Zaposleni", "JMBG", c => c.String(nullable: false, maxLength: 13));
        }
    }
}
