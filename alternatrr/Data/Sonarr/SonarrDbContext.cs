using Microsoft.EntityFrameworkCore;

namespace alternatrr
{
    public partial class SonarrDbContext : DbContext
    {
        public SonarrDbContext(DbContextOptions<SonarrDbContext> options) : base(options)
        {
        }

        public virtual DbSet<SceneMapping> SceneMappings { get; set; }
        public virtual DbSet<Series> Series { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SceneMapping>(entity =>
            {
                entity.Property(e => e.ParseTerm).IsRequired();

                entity.Property(e => e.SearchTerm).IsRequired();
            });

            modelBuilder.Entity<Series>(entity =>
            {
                entity.HasIndex(e => e.CleanTitle, "IX_Series_CleanTitle");

                entity.HasIndex(e => e.Path, "IX_Series_Path");

                entity.HasIndex(e => e.TitleSlug, "IX_Series_TitleSlug")
                    .IsUnique();

                entity.HasIndex(e => e.TvMazeId, "IX_Series_TvMazeId");

                entity.HasIndex(e => e.TvRageId, "IX_Series_TvRageId");

                entity.HasIndex(e => e.TvdbId, "IX_Series_TvdbId")
                    .IsUnique();

                entity.Property(e => e.Added).HasColumnType("DATETIME");

                entity.Property(e => e.CleanTitle).IsRequired();

                entity.Property(e => e.FirstAired).HasColumnType("DATETIME");

                entity.Property(e => e.Images).IsRequired();

                entity.Property(e => e.LastDiskSync).HasColumnType("DATETIME");

                entity.Property(e => e.LastInfoSync).HasColumnType("DATETIME");

                entity.Property(e => e.NextAiring).HasColumnType("DATETIME");

                entity.Property(e => e.Path).IsRequired();

                entity.Property(e => e.Title).IsRequired();
            });
        }
    }
}