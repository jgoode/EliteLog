using EliteModels;
using System.Data.Entity;

namespace EliteDB {
    public class EliteContext: DbContext
    {
        public DbSet<Expedition> Expeditions { get; set; }
        public DbSet<StarSystem> StarSystems { get; set; }
        public DbSet<StarSystemObject> StarSystemObjects { get; set; }
    }
}
