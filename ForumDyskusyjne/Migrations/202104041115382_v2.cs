namespace ForumDyskusyjne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Role = c.Int(nullable: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.AccountId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Order = c.Int(nullable: false),
                        ThreadId = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.Threads", t => t.ThreadId, cascadeDelete: true)
                .Index(t => t.ThreadId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Threads",
                c => new
                    {
                        ThreadId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Order = c.Int(nullable: false),
                        Views = c.Int(nullable: false),
                        Glued = c.Int(nullable: false),
                        ForumId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ThreadId)
                .ForeignKey("dbo.Fora", t => t.ForumId, cascadeDelete: true)
                .Index(t => t.ForumId);
            
            CreateTable(
                "dbo.Fora",
                c => new
                    {
                        ForumId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Permission = c.Int(nullable: false),
                        ForumCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ForumId)
                .ForeignKey("dbo.ForumCategories", t => t.ForumCategoryId, cascadeDelete: true)
                .Index(t => t.ForumCategoryId);
            
            CreateTable(
                "dbo.ForumCategories",
                c => new
                    {
                        ForumCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ForumCategoryId);
            
            CreateTable(
                "dbo.PrivateMessages",
                c => new
                    {
                        PrivateMessageId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        SenderId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                        Attachment = c.Binary(),
                        Receiver_AccountId = c.Int(),
                        Sender_AccountId = c.Int(),
                        Account_AccountId = c.Int(),
                        Account_AccountId1 = c.Int(),
                    })
                .PrimaryKey(t => t.PrivateMessageId)
                .ForeignKey("dbo.Accounts", t => t.Receiver_AccountId)
                .ForeignKey("dbo.Accounts", t => t.Sender_AccountId)
                .ForeignKey("dbo.Accounts", t => t.Account_AccountId)
                .ForeignKey("dbo.Accounts", t => t.Account_AccountId1)
                .Index(t => t.Receiver_AccountId)
                .Index(t => t.Sender_AccountId)
                .Index(t => t.Account_AccountId)
                .Index(t => t.Account_AccountId1);
            
            CreateTable(
                "dbo.Annoucements",
                c => new
                    {
                        AnnoucementId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.AnnoucementId);
            
            CreateTable(
                "dbo.BannedWords",
                c => new
                    {
                        BannedWordId = c.Int(nullable: false, identity: true),
                        Word = c.String(),
                    })
                .PrimaryKey(t => t.BannedWordId);
            
            DropTable("dbo.Examples");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Examples",
                c => new
                    {
                        ExampleId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ExampleId);
            
            DropForeignKey("dbo.PrivateMessages", "Account_AccountId1", "dbo.Accounts");
            DropForeignKey("dbo.PrivateMessages", "Account_AccountId", "dbo.Accounts");
            DropForeignKey("dbo.PrivateMessages", "Sender_AccountId", "dbo.Accounts");
            DropForeignKey("dbo.PrivateMessages", "Receiver_AccountId", "dbo.Accounts");
            DropForeignKey("dbo.Messages", "ThreadId", "dbo.Threads");
            DropForeignKey("dbo.Threads", "ForumId", "dbo.Fora");
            DropForeignKey("dbo.Fora", "ForumCategoryId", "dbo.ForumCategories");
            DropForeignKey("dbo.Messages", "AccountId", "dbo.Accounts");
            DropIndex("dbo.PrivateMessages", new[] { "Account_AccountId1" });
            DropIndex("dbo.PrivateMessages", new[] { "Account_AccountId" });
            DropIndex("dbo.PrivateMessages", new[] { "Sender_AccountId" });
            DropIndex("dbo.PrivateMessages", new[] { "Receiver_AccountId" });
            DropIndex("dbo.Fora", new[] { "ForumCategoryId" });
            DropIndex("dbo.Threads", new[] { "ForumId" });
            DropIndex("dbo.Messages", new[] { "AccountId" });
            DropIndex("dbo.Messages", new[] { "ThreadId" });
            DropTable("dbo.BannedWords");
            DropTable("dbo.Annoucements");
            DropTable("dbo.PrivateMessages");
            DropTable("dbo.ForumCategories");
            DropTable("dbo.Fora");
            DropTable("dbo.Threads");
            DropTable("dbo.Messages");
            DropTable("dbo.Accounts");
        }
    }
}
