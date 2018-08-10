namespace DocSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocumentLastModifiedToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Documents", "LastModified", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Documents", "LastModified", c => c.DateTime(nullable: false));
        }
    }
}
