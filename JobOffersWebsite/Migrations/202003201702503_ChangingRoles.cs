namespace JobOffersWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingRoles : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "UserTypeId", "dbo.UserTypes");
            DropIndex("dbo.AspNetUsers", new[] { "UserTypeId" });
            AddColumn("dbo.AspNetUsers", "UserType", c => c.String());
            DropColumn("dbo.AspNetUsers", "UserTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserTypeId", c => c.Byte(nullable: false));
            DropColumn("dbo.AspNetUsers", "UserType");
            CreateIndex("dbo.AspNetUsers", "UserTypeId");
            AddForeignKey("dbo.AspNetUsers", "UserTypeId", "dbo.UserTypes", "Id", cascadeDelete: true);
        }
    }
}
