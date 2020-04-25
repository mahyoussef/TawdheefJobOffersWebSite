namespace JobOffersWebsite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editUserinApplyForJobs : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ApplyForJobs", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.ApplyForJobs", "UserId");
            RenameColumn(table: "dbo.ApplyForJobs", name: "ApplicationUser_Id", newName: "UserId");
            AlterColumn("dbo.ApplyForJobs", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ApplyForJobs", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ApplyForJobs", new[] { "UserId" });
            AlterColumn("dbo.ApplyForJobs", "UserId", c => c.String());
            RenameColumn(table: "dbo.ApplyForJobs", name: "UserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.ApplyForJobs", "UserId", c => c.String());
            CreateIndex("dbo.ApplyForJobs", "ApplicationUser_Id");
        }
    }
}
