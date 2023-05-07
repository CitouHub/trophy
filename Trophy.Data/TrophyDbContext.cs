using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Trophy.Data
{
    public partial class TrophyDbContext : DbContext
    {
        public TrophyDbContext()
        {
        }

        public TrophyDbContext(DbContextOptions<TrophyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Game> Games { get; set; } = null!;
        public virtual DbSet<Player> Players { get; set; } = null!;
        public virtual DbSet<PlayerResult> PlayerResults { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS02;Initial Catalog=Trophy;persist security info=True;Integrated Security=SSPI;MultipleActiveResultSets=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("Game");

                entity.Property(e => e.InsertByUser).HasDefaultValueSql("((1))");

                entity.Property(e => e.InsertDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.Property(e => e.MatchDate)
                    .HasColumnType("smalldatetime")
                    .HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.ToTable("Player");

                entity.Property(e => e.InsertByUser).HasDefaultValueSql("((1))");

                entity.Property(e => e.InsertDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<PlayerResult>(entity =>
            {
                entity.ToTable("PlayerResult");

                entity.Property(e => e.InsertByUser).HasDefaultValueSql("((1))");

                entity.Property(e => e.InsertDate).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.PlayerResults)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("PlayerResult_GameFK");

                entity.HasOne(d => d.Player)
                    .WithMany(p => p.PlayerResults)
                    .HasForeignKey(d => d.PlayerId)
                    .HasConstraintName("PlayerResult_PlayerFK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
