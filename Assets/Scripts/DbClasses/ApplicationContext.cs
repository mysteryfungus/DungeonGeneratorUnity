using System.Data.Entity;

namespace DbClasses
{
    class ApplicationContext : DbContext
    {
        public DbSet<Name> Names { get; set; }

        public DbSet<Noun> Nouns { get; set; }

        public DbSet<Adjective> Adjectives { get; set; }

        public DbSet<Monster> Monsters { get; set; }

        public DbSet<Hazard> Hazards { get; set; }

        public ApplicationContext() : base("DefaultConnection") { }
    }
}