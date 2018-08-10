namespace DocSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrganizationListRemoved : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Organizations", "DocumentID", "dbo.Documents");
            DropIndex("dbo.Organizations", new[] { "DocumentID" });
            DropTable("dbo.Organizations");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DocumentID = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Organizations", "DocumentID");
            AddForeignKey("dbo.Organizations", "DocumentID", "dbo.Documents", "Id", cascadeDelete: true);
        }
    }
}
