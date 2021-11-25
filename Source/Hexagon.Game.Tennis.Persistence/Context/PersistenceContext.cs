using Hexagon.Game.Tennis.Entity;
using Hexagon.Game.Tennis.Persistence.Model;
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
        public virtual DbSet<GameScoreModel> GameScore { get; set; }
        public virtual DbSet<MatchModel> Match { get; set; }
        public virtual DbSet<MatchPlayerModel> MatchPlayer { get; set; }
        public virtual DbSet<PlayerModel> Player { get; set; }
        public virtual DbSet<PointLookupModel> PointLookup { get; set; }
        public virtual DbSet<PointScoreModel> PointScore { get; set; }
        public virtual DbSet<SetScoreModel> SetScore { get; set; }
        public virtual DbSet<StatusLookupModel> StatusLookup { get; set; }

        /// <summary>
        /// Cofiguration for opening database
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {           
            optionsBuilder.UseSqlServer(@"data source=.\SQLEXPRESS01;initial catalog=Tennis;integrated security=True;MultipleActiveResultSets=True;");
        }

        /// <summary>
        /// Mapping from model to database objects
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {  
            modelBuilder.Entity<GameScoreModel>(entity =>
            {
                entity.ToTable("GAME_SCORE");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CompletedOn)
                    .HasColumnName("COMPLETED_ON")
                    .HasColumnType("datetime");

                entity.Property(e => e.ServedBy).HasColumnName("SERVED_BY");

                entity.Property(e => e.SetId).HasColumnName("SET_ID");

                entity.Property(e => e.StartedOn)
                    .HasColumnName("STARTED_ON")
                    .HasColumnType("datetime");

                entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");

                entity.Property(e => e.WonBy).HasColumnName("WON_BY");

                entity.HasOne(d => d.ServedByNavigation)
                    .WithMany(p => p.GameScoreServedByNavigation)
                    .HasForeignKey(d => d.ServedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GAME_SCORE_FROM_PLAYER_1");

                entity.HasOne(d => d.Set)
                    .WithMany(p => p.GameScore)
                    .HasForeignKey(d => d.SetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GAME_SCORE_FROM_SET_SCORE");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.GameScore)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GAME_SCORE_FROM_STATUS_LOOKUP");

                entity.HasOne(d => d.WonByNavigation)
                    .WithMany(p => p.GameScoreWonByNavigation)
                    .HasForeignKey(d => d.WonBy)
                    .HasConstraintName("FK_GAME_SCORE_FROM_PLAYER_2");
            });

            modelBuilder.Entity<MatchModel>(entity =>
            {
                entity.ToTable("MATCH");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CompletedOn)
                    .HasColumnName("COMPLETED_ON")
                    .HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(100);

                entity.Property(e => e.Court)
                    .IsRequired()
                    .HasColumnName("COURT")
                    .HasMaxLength(100);

                entity.Property(e => e.StartedOn)
                    .HasColumnName("STARTED_ON")
                    .HasColumnType("datetime");

                entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");

                entity.Property(e => e.WonBy).HasColumnName("WON_BY");

                entity.HasOne(d => d.WonByNavigation)
                    .WithMany(p => p.Match)
                    .HasForeignKey(d => d.WonBy)
                    .HasConstraintName("FK_MATCH_FROM_PLAYER");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Match)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MATCH_FROM_STATUS_LOOKUP");
            });

            modelBuilder.Entity<MatchPlayerModel>(entity =>
            {
                entity.ToTable("MATCH_PLAYER");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.MatchId).HasColumnName("MATCH_ID");

                entity.Property(e => e.PlayerId).HasColumnName("PLAYER_ID");

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.MatchPlayer)
                    .HasForeignKey(d => d.MatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MATCH_PLAYER_FROM_MATCH");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.MatchPlayer)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MATCH_PLAYER_FROM_PLAYER");
            });

            modelBuilder.Entity<PlayerModel>(entity =>
            {
                entity.ToTable("PLAYER");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Club)
                    .HasColumnName("CLUB")
                    .HasMaxLength(100);

                entity.Property(e => e.DateOfBirth)
                    .HasColumnName("DATE_OF_BIRTH")
                    .HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("FIRST_NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .HasColumnName("LAST_NAME")
                    .HasMaxLength(50);

                entity.Property(e => e.SurName)
                    .IsRequired()
                    .HasColumnName("SUR_NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PointLookupModel>(entity =>
            {
                entity.ToTable("POINT_LOOKUP");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PointScoreModel>(entity =>
            {
                entity.ToTable("POINT_SCORE");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.GameId).HasColumnName("GAME_ID");

                entity.Property(e => e.PlayerId).HasColumnName("PLAYER_ID");

                entity.Property(e => e.PointId).HasColumnName("POINT_ID");

                entity.Property(e => e.UpdatedOn)
                    .HasColumnName("UPDATED_ON")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.PointScore)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_POINT_SCORE_FROM_GAME_SCORE");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PointScore)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_POINT_SCORE_FROM_PLAYER");

                entity.HasOne(d => d.Point)
                    .WithMany(p => p.PointScore)
                    .HasForeignKey(d => d.PointId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_POINT_SCORE_FROM_POINT_LOOKUP");
            });

            modelBuilder.Entity<SetScoreModel>(entity =>
            {
                entity.ToTable("SET_SCORE");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CompletedOn)
                    .HasColumnName("COMPLETED_ON")
                    .HasColumnType("datetime");

                entity.Property(e => e.MatchId).HasColumnName("MATCH_ID");

                entity.Property(e => e.StartedOn)
                    .HasColumnName("STARTED_ON")
                    .HasColumnType("datetime");

                entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");

                entity.Property(e => e.WonBy).HasColumnName("WON_BY");

                entity.HasOne(d => d.Match)
                    .WithMany(p => p.SetScore)
                    .HasForeignKey(d => d.MatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SET_SCORE_FROM_MATCH");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.SetScore)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SET_SCORE_FROM_STATUS_LOOKUP");

                entity.HasOne(d => d.WonByNavigation)
                    .WithMany(p => p.SetScore)
                    .HasForeignKey(d => d.WonBy)
                    .HasConstraintName("FK_SET_SCORE_FROM_PLAYER");
            });

            modelBuilder.Entity<StatusLookupModel>(entity =>
            {
                entity.ToTable("STATUS_LOOKUP");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);
            });
        }
    }
}
