using MySongsApi.Domain.Models;
using MySongsApi.Infrastructure.TypeConfig;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySongsApi.Infrastructure.Context
{
    public class MySongsDbContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Band> Bands { get; set; }

        public MySongsDbContext()
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SongTypeConfig());
            modelBuilder.Configurations.Add(new BandTypeConfig());
            modelBuilder.Configurations.Add(new AlbumTypeConfig());
        }
    }
}
