namespace DocSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrganizationsToDocuments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Document_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Documents", t => t.Document_Id)
                .Index(t => t.Document_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Organizations", "Document_Id", "dbo.Documents");
            DropIndex("dbo.Organizations", new[] { "Document_Id" });
            DropTable("dbo.Organizations");
        }
    }
}
