namespace ForumDyskusyjne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v9 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ranks", "Image", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ranks", "Image", c => c.Binary());
        }
    }
}
