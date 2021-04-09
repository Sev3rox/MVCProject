namespace ForumDyskusyjne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountForums",
                c => new
                    {
                        AccountForumId = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        ForumId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountForumId)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.Fora", t => t.ForumId, cascadeDelete: true)
                .Index(t => t.AccountId)
                .Index(t => t.ForumId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccountForums", "ForumId", "dbo.Fora");
            DropForeignKey("dbo.AccountForums", "AccountId", "dbo.Accounts");
            DropIndex("dbo.AccountForums", new[] { "ForumId" });
            DropIndex("dbo.AccountForums", new[] { "AccountId" });
            DropTable("dbo.AccountForums");
        }
    }
}
