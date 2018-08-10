namespace DocSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateProducts : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Products (Name) VALUES ('TimeBase')");
            Sql("INSERT INTO Products (Name) VALUES ('TimeBase Web-Edit')");
            Sql("INSERT INTO Products (Name) VALUES ('TimeBase InDesign / FrameMaker')");
            Sql("INSERT INTO Products (Name) VALUES ('TimePub')");
            Sql("INSERT INTO Products (Name) VALUES ('TimeCms / TimeShop')");
        }
        
        public override void Down()
        {
        }
    }
}
