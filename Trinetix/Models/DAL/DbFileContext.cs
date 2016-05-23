namespace Trinetix
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DbFileContext : DbContext
    {
        public DbFileContext()
            : base("name=DbFileContext")
        {
        }

        public virtual DbSet<Dirrectories> Dirrectories { get; set; }
        public virtual DbSet<FileData> FileData { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<FileTypes> FileTypes { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Words> Words { get; set; }
        public virtual DbSet<WordsGrouped> WordsGrouped { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dirrectories>()
                .HasMany(e => e.Files)
                .WithRequired(e => e.Dirrectories)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Files>()
                .HasOptional(e => e.FileData)
                .WithRequired(e => e.Files);

            modelBuilder.Entity<Files>()
                .HasMany(e => e.Words)
                .WithRequired(e => e.Files)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FileTypes>()
                .HasMany(e => e.Files)
                .WithRequired(e => e.FileTypes)
                .WillCascadeOnDelete(false);
        }
    }
}
