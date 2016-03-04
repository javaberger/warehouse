namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "UserCover", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "UserCover");
        }
    }
}
