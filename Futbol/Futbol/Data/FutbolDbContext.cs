using Futbol.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Futbol.Data
{
    public class FutbolDbContext : DbContext
    {
        public DbSet<Lig> Ligler { get; set; }
        public DbSet<Oyuncu> Oyuncular { get; set; }
        public DbSet<Takim> Takimlar { get; set; }
        public DbSet<TeknikDirektor> TeknikDirektorler { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Futbol;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Takim>(entity =>
            {
                entity.HasOne<Lig>(l => l.Lig).WithMany(t => t.Takimlar).HasForeignKey(a => a.LigId);
                entity.HasOne<TeknikDirektor>(x => x.TeknikDirektor).WithOne(td => td.Takim).HasForeignKey<Takim>(a => a.TeknikDirektorId);
            });

            modelBuilder.Entity<Oyuncu>(entity =>
            {
                entity.HasOne<Takim>(o => o.Takim).WithMany(t => t.Oyuncular).HasForeignKey(f => f.TakimId);
            });

        }
    }
}
