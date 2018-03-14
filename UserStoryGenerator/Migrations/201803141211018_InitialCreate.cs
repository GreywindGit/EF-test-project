namespace UserStoryGenerator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserStories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Actor = c.String(),
                        Goal = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserStories");
        }
    }
}
