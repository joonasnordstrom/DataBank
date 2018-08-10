namespace DocSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filePathToOrgranizationModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organizations", "FilePath", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organizations", "FilePath");
        }
    }
}
