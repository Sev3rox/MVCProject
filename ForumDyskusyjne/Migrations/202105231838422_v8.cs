namespace ForumDyskusyjne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v8 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Fora", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Fora", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.ForumCategories", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Threads", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Messages", "Content", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "Content", c => c.String());
            AlterColumn("dbo.Threads", "Name", c => c.String());
            AlterColumn("dbo.ForumCategories", "Name", c => c.String());
            AlterColumn("dbo.Fora", "Description", c => c.String());
            AlterColumn("dbo.Fora", "Name", c => c.String());
        }
    }
}
