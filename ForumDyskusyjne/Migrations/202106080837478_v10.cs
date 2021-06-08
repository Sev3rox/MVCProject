namespace ForumDyskusyjne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PrivateMessages", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.Annoucements", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.BannedWords", "Word", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BannedWords", "Word", c => c.String());
            AlterColumn("dbo.Annoucements", "Text", c => c.String());
            AlterColumn("dbo.PrivateMessages", "Text", c => c.String());
        }
    }
}
