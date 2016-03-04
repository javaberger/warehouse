namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Articles", "UserCover");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "UserCover", c => c.Binary());
        }
    }
}
