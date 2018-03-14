namespace UserStoryGenerator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCriteria : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Criteria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Header = c.String(),
                        Content = c.String(),
                        UserStoryId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserStories", t => t.UserStoryId)
                .Index(t => t.UserStoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Criteria", "UserStoryId", "dbo.UserStories");
            DropIndex("dbo.Criteria", new[] { "UserStoryId" });
            DropTable("dbo.Criteria");
        }
    }
}
