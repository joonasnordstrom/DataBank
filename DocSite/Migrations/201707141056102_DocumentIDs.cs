namespace DocSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DocumentIDs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Organizations", "Document_Id", "dbo.Documents");
            DropIndex("dbo.Organizations", new[] { "Document_Id" });
            RenameColumn(table: "dbo.Organizations", name: "Document_Id", newName: "DocumentID");
            AddColumn("dbo.Documents", "DocumentID", c => c.Int(nullable: false));
            AlterColumn("dbo.Organizations", "DocumentID", c => c.Int(nullable: false));
            CreateIndex("dbo.Organizations", "DocumentID");
            AddForeignKey("dbo.Organizations", "DocumentID", "dbo.Documents", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Organizations", "DocumentID", "dbo.Documents");
            DropIndex("dbo.Organizations", new[] { "DocumentID" });
            AlterColumn("dbo.Organizations", "DocumentID", c => c.Int());
            DropColumn("dbo.Documents", "DocumentID");
            RenameColumn(table: "dbo.Organizations", name: "DocumentID", newName: "Document_Id");
            CreateIndex("dbo.Organizations", "Document_Id");
            AddForeignKey("dbo.Organizations", "Document_Id", "dbo.Documents", "Id");
        }
    }
}
