namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CoverType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CoverType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CoverType");
        }
    }
}
