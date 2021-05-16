namespace ForumDyskusyjne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PrivateMessages", new[] { "Receiver_Id" });
            DropIndex("dbo.PrivateMessages", new[] { "Sender_Id" });
            DropColumn("dbo.PrivateMessages", "ReceiverId");
            DropColumn("dbo.PrivateMessages", "SenderId");
            RenameColumn(table: "dbo.PrivateMessages", name: "Receiver_Id", newName: "ReceiverId");
            RenameColumn(table: "dbo.PrivateMessages", name: "Sender_Id", newName: "SenderId");
            AlterColumn("dbo.AccountForums", "AccountId", c => c.String());
            AlterColumn("dbo.Messages", "AccountId", c => c.String());
            AlterColumn("dbo.PrivateMessages", "SenderId", c => c.String(maxLength: 128));
            AlterColumn("dbo.PrivateMessages", "ReceiverId", c => c.String(maxLength: 128));
            CreateIndex("dbo.PrivateMessages", "SenderId");
            CreateIndex("dbo.PrivateMessages", "ReceiverId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.PrivateMessages", new[] { "ReceiverId" });
            DropIndex("dbo.PrivateMessages", new[] { "SenderId" });
            AlterColumn("dbo.PrivateMessages", "ReceiverId", c => c.Int(nullable: false));
            AlterColumn("dbo.PrivateMessages", "SenderId", c => c.Int(nullable: false));
            AlterColumn("dbo.Messages", "AccountId", c => c.Int(nullable: false));
            AlterColumn("dbo.AccountForums", "AccountId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.PrivateMessages", name: "SenderId", newName: "Sender_Id");
            RenameColumn(table: "dbo.PrivateMessages", name: "ReceiverId", newName: "Receiver_Id");
            AddColumn("dbo.PrivateMessages", "SenderId", c => c.Int(nullable: false));
            AddColumn("dbo.PrivateMessages", "ReceiverId", c => c.Int(nullable: false));
            CreateIndex("dbo.PrivateMessages", "Sender_Id");
            CreateIndex("dbo.PrivateMessages", "Receiver_Id");
        }
    }
}
