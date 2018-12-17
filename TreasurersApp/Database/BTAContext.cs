using System;
using Microsoft.EntityFrameworkCore;
using TreasurersApp.Models;

namespace TreasurersApp.Database
{
    public partial class BTAContext : DbContext
    {
        private static readonly string HOME_CONNECTION_STRING = "Server=(localdb)\\MSSQLLocalDB;Database=BTA;AttachDbFilename=H:\\Source\\Repos\\TreasurersApp\\TreasurersApp\\wwwroot\\Content\\Database\\BTA.mdf;MultipleActiveResultSets=true";
        private static readonly string WORK_CONNECTION_STRING = "Server=(localdb)\\MSSQLLocalDB;Database=BTA;AttachDbFilename=F:\\R_D\\mcdoma2000\\TreasurersApp\\TreasurersApp\\wwwroot\\Content\\Database\\BTA.mdf;MultipleActiveResultSets=true";

        public BTAContext()
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public BTAContext(DbContextOptions<BTAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<CashJournal> CashJournal { get; set; }
        public virtual DbSet<Claim> Claim { get; set; }
        public virtual DbSet<ContributionCategory> ContributionCategory { get; set; }
        public virtual DbSet<ContributionType> ContributionType { get; set; }
        public virtual DbSet<Contributor> Contributor { get; set; }
        public virtual DbSet<Report> Report { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserClaim> UserClaim { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(HOME_CONNECTION_STRING);
                //optionsBuilder.UseSqlServer(WORK_CONNECTION_STRING);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.AddressLine1)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.AddressLine2).HasMaxLength(100);

                entity.Property(e => e.AddressLine3).HasMaxLength(100);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<CashJournal>(entity =>
            {
                entity.Property(e => e.CashJournalId).HasColumnName("CashJournalID");

                entity.Property(e => e.BahaiId)
                    .HasColumnName("BahaiID")
                    .HasMaxLength(100);

                entity.Property(e => e.ContributionTypeId).HasColumnName("ContributionTypeID");

                entity.Property(e => e.ContributorId).HasColumnName("ContributorID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EffectiveDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.ContributionType)
                    .WithMany(p => p.CashJournal)
                    .HasForeignKey(d => d.ContributionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CashJournal_ContributionType");

                entity.HasOne(d => d.Contributor)
                    .WithMany(p => p.CashJournal)
                    .HasForeignKey(d => d.ContributorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CashJournal_Contributor");
            });

            modelBuilder.Entity<Claim>(entity =>
            {
                entity.HasKey(e => e.ClaimId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("Claim", "Security");

                entity.HasIndex(e => e.ClaimType)
                    .HasName("UIX_ContributionType_Name")
                    .IsUnique();

                entity.Property(e => e.ClaimId)
                    .HasColumnName("ClaimID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ClaimType)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ClaimValue)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ContributionCategory>(entity =>
            {
                entity.HasIndex(e => e.ContributionCategoryName)
                    .HasName("UIX_ContributionCategory_Name")
                    .IsUnique();

                entity.Property(e => e.ContributionCategoryId).HasColumnName("ContributionCategoryID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ContributionCategoryName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<ContributionType>(entity =>
            {
                entity.HasIndex(e => e.ContributionTypeName)
                    .HasName("UIX_ContributionType_Name")
                    .IsUnique();

                entity.Property(e => e.ContributionTypeId).HasColumnName("ContributionTypeID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ContributionTypeName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.DisplayOrder).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ContributionType)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContributionType_ContributionCategory");
            });

            modelBuilder.Entity<Contributor>(entity =>
            {
                entity.HasIndex(e => e.AddressId)
                    .HasName("IDX_Contributor_AddressID");

                entity.Property(e => e.ContributorId).HasColumnName("ContributorID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(100);
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UIX_Report_Name")
                    .IsUnique();

                entity.Property(e => e.ReportId).HasColumnName("ReportID");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ConfigurationJson).HasMaxLength(2048);

                entity.Property(e => e.DisplayName).HasMaxLength(256);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("User", "Security");

                entity.HasIndex(e => e.UserName)
                    .HasName("UIX_SecurityUser_Name")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<UserClaim>(entity =>
            {
                entity.HasKey(e => e.UserClaimId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("UserClaim", "Security");

                entity.HasIndex(e => new { e.ClaimId, e.UserId })
                    .HasName("UIX_SecurityUserClaim_ClaimID_UserID");

                entity.Property(e => e.UserClaimId)
                    .HasColumnName("UserClaimID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ClaimId).HasColumnName("ClaimID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Claim)
                    .WithMany(p => p.UserClaim)
                    .HasForeignKey(d => d.ClaimId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserClaim_Claim");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserClaim)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserClaim_User");
            });
        }
    }
}
