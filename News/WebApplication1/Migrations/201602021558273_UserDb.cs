namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Content = c.String(),
                        DateCreate = c.String(),
                        CoverType = c.String(),
                        Cover = c.Binary(),
                        ViewCount = c.Int(nullable: false),
                        CategoriesID = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Categories", t => t.CategoriesID, cascadeDelete: true)
                .Index(t => t.CategoriesID)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "CategoriesID", "dbo.Categories");
            DropForeignKey("dbo.Articles", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Articles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Articles", new[] { "CategoriesID" });
            DropTable("dbo.Categories");
            DropTable("dbo.Articles");
        }
    }
}
