using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DUMPFutsalTournament.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DUMPFutsalTournament.Data
{
    public class FutsalContext : DbContext
    {
        public FutsalContext(DbContextOptions<FutsalContext> futsalContextOptions) : base(futsalContextOptions)
        {

        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchEvent> MatchEvents { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Match>()
                        .HasOne(m => m.HomeTeam)
                        .WithMany(t => t.HomeMatches);

            modelBuilder.Entity<Match>()
                        .HasOne(m => m.AwayTeam)
                        .WithMany(t => t.AwayMatches);
        }
    }
}
