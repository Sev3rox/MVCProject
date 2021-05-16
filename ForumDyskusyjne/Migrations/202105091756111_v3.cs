namespace ForumDyskusyjne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "User_Id" });
            AddColumn("dbo.Messages", "User_Id1", c => c.String(maxLength: 128));
            AlterColumn("dbo.Messages", "User_Id", c => c.String());
            CreateIndex("dbo.Messages", "User_Id1");
            AddForeignKey("dbo.Messages", "User_Id1", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Messages", "AccountId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "AccountId", c => c.String());
            DropForeignKey("dbo.Messages", "User_Id1", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "User_Id1" });
            AlterColumn("dbo.Messages", "User_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Messages", "User_Id1");
            CreateIndex("dbo.Messages", "User_Id");
            AddForeignKey("dbo.Messages", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
