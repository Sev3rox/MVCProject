namespace ForumDyskusyjne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Messages", new[] { "User_Id1" });
            DropColumn("dbo.Messages", "User_Id");
            RenameColumn(table: "dbo.Messages", name: "User_Id1", newName: "User_Id");
            AddColumn("dbo.Messages", "AccountId", c => c.String());
            AlterColumn("dbo.Messages", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Messages", "User_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Messages", new[] { "User_Id" });
            AlterColumn("dbo.Messages", "User_Id", c => c.String());
            DropColumn("dbo.Messages", "AccountId");
            RenameColumn(table: "dbo.Messages", name: "User_Id", newName: "User_Id1");
            AddColumn("dbo.Messages", "User_Id", c => c.String());
            CreateIndex("dbo.Messages", "User_Id1");
        }
    }
}
