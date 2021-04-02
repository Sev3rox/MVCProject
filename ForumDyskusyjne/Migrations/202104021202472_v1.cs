namespace ForumDyskusyjne.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Examples",
                c => new
                    {
                        ExampleId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ExampleId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Examples");
        }
    }
}
