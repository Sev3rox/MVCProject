namespace ForumDyskusyjne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class poprawa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "Attachment", c => c.Binary());
            DropColumn("dbo.PrivateMessages", "Attachment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PrivateMessages", "Attachment", c => c.Binary());
            DropColumn("dbo.Messages", "Attachment");
        }
    }
}
