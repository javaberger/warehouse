namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Articles", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Articles", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.Articles", "UserID", c => c.String());
            DropColumn("dbo.Articles", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Articles", "UserID");
            CreateIndex("dbo.Articles", "ApplicationUser_Id");
            AddForeignKey("dbo.Articles", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
