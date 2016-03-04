namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Articles", "FirstName");
            DropColumn("dbo.Articles", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "LastName", c => c.String());
            AddColumn("dbo.Articles", "FirstName", c => c.String());
        }
    }
}
