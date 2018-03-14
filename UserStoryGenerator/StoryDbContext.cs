using System.Data.Entity;

namespace UserStoryGenerator
{
    class StoryDbContext : DbContext
    {
        public StoryDbContext() : base("name=StoryDbContext")
        { }

        public DbSet<UserStory> Stories { get; set; }
        public DbSet<Criteria> Criterias { get; set; }
    }
}
