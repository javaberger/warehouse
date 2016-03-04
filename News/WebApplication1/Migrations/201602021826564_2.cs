namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "FirstName", c => c.String());
            AddColumn("dbo.Articles", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "LastName");
            DropColumn("dbo.Articles", "FirstName");
        }
    }
}
