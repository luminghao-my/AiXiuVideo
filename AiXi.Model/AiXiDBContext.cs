using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AiXi.Model
{
    public partial class AiXiDBContext : DbContext
    {
        public AiXiDBContext()
            : base("name=AiXiDBContext")
        {
        }

        public virtual DbSet<TBLogins> TBLogins { get; set; }
        public virtual DbSet<TBUsers> TBUsers { get; set; }
        public virtual DbSet<TBVideos> TBVideos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TBLogins>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<TBLogins>()
                .Property(e => e.MobileNumber)
                .IsUnicode(false);

            modelBuilder.Entity<TBLogins>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<TBLogins>()
                .HasOptional(e => e.TBUsers)
                .WithRequired(e => e.TBLogins);

            modelBuilder.Entity<TBUsers>()
                .Property(e => e.NickName)
                .IsUnicode(false);

            modelBuilder.Entity<TBUsers>()
                .Property(e => e.Avatar)
                .IsUnicode(false);

            modelBuilder.Entity<TBUsers>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<TBUsers>()
                .Property(e => e.Hobby)
                .IsUnicode(false);

            modelBuilder.Entity<TBUsers>()
                .HasMany(e => e.TBVideos)
                .WithRequired(e => e.TBUsers)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TBVideos>()
                .Property(e => e.VideoId)
                .IsUnicode(false);

            modelBuilder.Entity<TBVideos>()
                .Property(e => e.Headline)
                .IsUnicode(false);

            modelBuilder.Entity<TBVideos>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<TBVideos>()
                .Property(e => e.CoverURL)
                .IsUnicode(false);
        }
    }
}
