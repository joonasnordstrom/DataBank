namespace DocSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrganisationToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Organization", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Organization");
        }
    }
}
