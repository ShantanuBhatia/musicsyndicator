using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MSMS.Server.Models;

namespace MSMS.Server.Data
{
    public class ApplicationDBContext: DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<ArtistList> ArtistLists { get; set; }
        public DbSet<SpotifyPlaylist> SpotifyLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ArtistList>()
                .HasMany(e => e.Artists)
                .WithMany(e => e.ArtistLists)
                .UsingEntity("ArtistArtistList");
        }
    }
}
