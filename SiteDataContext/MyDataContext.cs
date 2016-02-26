namespace SiteDataContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyDataContext : DbContext
    {
        public MyDataContext()
            : base("name=MyDataContext")
        {
        }

        public virtual DbSet<Ads> Ads { get; set; }
        public virtual DbSet<Ads2KeyWords> Ads2KeyWords { get; set; }
        public virtual DbSet<Candidates> Candidates { get; set; }
        public virtual DbSet<Candidates2KeyWords> Candidates2KeyWords { get; set; }
        public virtual DbSet<Chats> Chats { get; set; }
        public virtual DbSet<Employers> Employers { get; set; }
        public virtual DbSet<KeyWords> KeyWords { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ads>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<Ads>()
                .Property(e => e.Company)
                .IsUnicode(false);

            modelBuilder.Entity<Ads>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<Ads>()
                .Property(e => e.Position)
                .IsUnicode(false);

            modelBuilder.Entity<Ads>()
                .HasMany(e => e.Ads2KeyWords)
                .WithRequired(e => e.Ads)
                .HasForeignKey(e => e.AdId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Candidates>()
                .Property(e => e.ExpectedPosition)
                .IsUnicode(false);

            modelBuilder.Entity<Candidates>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<Candidates>()
                .Property(e => e.ExperienceDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Candidates>()
                .Property(e => e.Skills)
                .IsUnicode(false);

            modelBuilder.Entity<Candidates>()
                .HasMany(e => e.Candidates2KeyWords)
                .WithRequired(e => e.Candidates)
                .HasForeignKey(e => e.CandidateId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Chats>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.Chats)
                .HasForeignKey(e => e.ChatId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Employers>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Employers>()
                .Property(e => e.ContactInformation)
                .IsUnicode(false);

            modelBuilder.Entity<Employers>()
                .Property(e => e.Company)
                .IsUnicode(false);

            modelBuilder.Entity<Employers>()
                .HasMany(e => e.Ads)
                .WithRequired(e => e.Employers)
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KeyWords>()
                .Property(e => e.Value)
                .IsUnicode(false);

            modelBuilder.Entity<KeyWords>()
                .HasMany(e => e.Ads2KeyWords)
                .WithRequired(e => e.KeyWords)
                .HasForeignKey(e => e.KeyWordId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KeyWords>()
                .HasMany(e => e.Candidates2KeyWords)
                .WithRequired(e => e.KeyWords)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Messages>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Candidates)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Chats)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Chats1)
                .WithRequired(e => e.Users1)
                .HasForeignKey(e => e.UserId2)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Employers)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.AuthorId)
                .WillCascadeOnDelete(false);
        }
    }
}
