namespace I_Shop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mydb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category_CategoryID = c.Int(),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.ProductCategories", t => t.Category_CategoryID)
                .Index(t => t.Category_CategoryID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        dateTime = c.DateTime(nullable: false),
                        Customer_CustomerID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerID)
                .Index(t => t.Customer_CustomerID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        Order_OrderID = c.Int(nullable: false),
                        Product_ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Order_OrderID, t.Product_ProductID })
                .ForeignKey("dbo.Orders", t => t.Order_OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ProductID, cascadeDelete: true)
                .Index(t => t.Order_OrderID)
                .Index(t => t.Product_ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderProducts", "Product_ProductID", "dbo.Products");
            DropForeignKey("dbo.OrderProducts", "Order_OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "Customer_CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Products", "Category_CategoryID", "dbo.ProductCategories");
            DropIndex("dbo.OrderProducts", new[] { "Product_ProductID" });
            DropIndex("dbo.OrderProducts", new[] { "Order_OrderID" });
            DropIndex("dbo.Orders", new[] { "Customer_CustomerID" });
            DropIndex("dbo.Products", new[] { "Category_CategoryID" });
            DropTable("dbo.OrderProducts");
            DropTable("dbo.Customers");
            DropTable("dbo.Orders");
            DropTable("dbo.Products");
            DropTable("dbo.ProductCategories");
        }
    }
}
