namespace JobOffersWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HandlingUserTypes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserTypeId", c => c.Byte(nullable: true));
            CreateIndex("dbo.AspNetUsers", "UserTypeId");
            AddForeignKey("dbo.AspNetUsers", "UserTypeId", "dbo.UserTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "UserTypeId", "dbo.UserTypes");
            DropIndex("dbo.AspNetUsers", new[] { "UserTypeId" });
            DropColumn("dbo.AspNetUsers", "UserTypeId");
        }
    }
}
