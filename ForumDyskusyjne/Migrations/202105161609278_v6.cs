namespace ForumDyskusyjne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ranks",
                c => new
                    {
                        RankId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.RankId);
            
            AddColumn("dbo.AspNetUsers", "Rank_RankId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Rank_RankId");
            AddForeignKey("dbo.AspNetUsers", "Rank_RankId", "dbo.Ranks", "RankId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Rank_RankId", "dbo.Ranks");
            DropIndex("dbo.AspNetUsers", new[] { "Rank_RankId" });
            DropColumn("dbo.AspNetUsers", "Rank_RankId");
            DropTable("dbo.Ranks");
        }
    }
}
