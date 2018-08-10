namespace DocSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedProducts : DbMigration
    {
        public override void Up()
        {

            //Sql("INSERT INTO Products (Name) Values ('TimePub')");
            //Sql("INSERT INTO Products (Name) Values ('TimeShop')");
            //Sql("INSERT INTO Products (Name) Values ('Other Products')");
        }
        
        public override void Down()
        {
        }
    }
}
