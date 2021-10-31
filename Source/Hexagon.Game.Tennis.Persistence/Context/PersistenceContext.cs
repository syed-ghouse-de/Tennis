using Hexagon.Game.Tennis.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.Game.Tennis.Persistence.Context
{
    /// <summary>
    /// Persistence context for database operations
    /// </summary>
    public class PersistenceContext : DbContext
    {
        public virtual DbSet<MatchEntity> Match { get; set; }

        /// <summary>
        /// Cofiguration for opening database
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=.\SQLEXPRESS01;initial catalog=Tennis;integrated security=True;MultipleActiveResultSets=True;");
        }

        /// <summary>
        /// Entity mapping to database objects
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StatusEntity>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("Id");

                entity.ToTable("Status", "public");
                entity.Property(e => e.Name).HasColumnName("Name");

            });

            modelBuilder.Entity<MatchEntity>(entity =>
            {
                entity.HasKey(m => m.Id)
                    .HasName("Id");

                entity.ToTable("Match");

                entity.Property(m => m.Name).HasColumnName("Name");
                entity.Property(m => m.StartedOn).HasColumnName("StartedOn");
                entity.Property(m => m.CompletedOn).HasColumnName("CompletedOn");
                entity.Property(m => m.Status).HasColumnName("Status");

                entity.HasOne(s => s.StatusNavigation)
                    .WithMany(m => m.Match)
                    .HasForeignKey(m => m.Status)
                    .HasConstraintName("FK_Match_From_Status");
            });
        }
    }
}
