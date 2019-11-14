namespace Aviato.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zaposleniidentityCascadeDeleteTrue : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Zaposleni", "IdentityId", "dbo.AspNetUsers");
            AddForeignKey("dbo.Zaposleni", "IdentityId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Zaposleni", "IdentityId", "dbo.AspNetUsers");
            AddForeignKey("dbo.Zaposleni", "IdentityId", "dbo.AspNetUsers", "Id");
        }
    }
}
