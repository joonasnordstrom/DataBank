namespace DocSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filePathToOrg : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Organizations", "FilePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Organizations", "FilePath", c => c.String(nullable: false));
        }
    }
}
