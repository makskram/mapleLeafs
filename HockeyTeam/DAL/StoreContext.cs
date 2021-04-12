using HockeyTeam.Models;
using System.Data.Entity;

namespace HockeyTeam.DAL
{
    public class StoreContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PlayerImage> PlayerImages { get; set; }
        public DbSet<PlayerImageMapping> PlayerImageMappings { get; set; }
    }
}