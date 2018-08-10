namespace DocSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productIdToInt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Documents", "ProductId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Documents", "ProductId", c => c.Byte());
        }
    }
}
