namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDb4 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.OrderItems", name: "Order_Id", newName: "OrderId");
            RenameIndex(table: "dbo.OrderItems", name: "IX_Order_Id", newName: "IX_OrderId");
            AlterColumn("dbo.Orders", "FullName", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "Phone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "Phone", c => c.String());
            AlterColumn("dbo.Orders", "Address", c => c.String());
            AlterColumn("dbo.Orders", "FullName", c => c.String());
            RenameIndex(table: "dbo.OrderItems", name: "IX_OrderId", newName: "IX_Order_Id");
            RenameColumn(table: "dbo.OrderItems", name: "OrderId", newName: "Order_Id");
        }
    }
}
