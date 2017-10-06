namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDb2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "BookId", "dbo.Books");
            DropIndex("dbo.Comments", new[] { "BookId" });
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropColumn("dbo.Comments", "UserId");
            RenameColumn(table: "dbo.Comments", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Comments", "BookId", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Comments", "BookId");
            CreateIndex("dbo.Comments", "UserId");
            AddForeignKey("dbo.Comments", "BookId", "dbo.Books", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "BookId", "dbo.Books");
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "BookId" });
            AlterColumn("dbo.Comments", "UserId", c => c.Int());
            AlterColumn("dbo.Comments", "BookId", c => c.Int());
            RenameColumn(table: "dbo.Comments", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Comments", "UserId", c => c.Int());
            CreateIndex("dbo.Comments", "User_Id");
            CreateIndex("dbo.Comments", "BookId");
            AddForeignKey("dbo.Comments", "BookId", "dbo.Books", "Id");
        }
    }
}
