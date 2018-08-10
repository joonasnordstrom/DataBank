namespace DocSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedProducts1 : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT [dbo].[Products] ON INSERT INTO[dbo].[Products]([Id], [Name]) VALUES(1, 'TimePub')");
            Sql("INSERT INTO[dbo].[Products]([Id], [Name]) VALUES(2, 'TimeBase')");
            Sql("INSERT INTO[dbo].[Products]([Id], [Name]) VALUES(3, 'TimeShop')");
            Sql("INSERT INTO[dbo].[Products]([Id], [Name]) VALUES(4, 'Solutions')");
            Sql("SET IDENTITY_INSERT[dbo].[Products] OFF");
        }
        
        public override void Down()
        {
        }
    }
}
