namespace JobOffersWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateUserType : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO UserTypes (Id, Type) VALUES (1, 'Publisher')");
            Sql("INSERT INTO UserTypes (Id, Type) VALUES (2, 'JobFinder')");
        }
        
        public override void Down()
        {
        }
    }
}
