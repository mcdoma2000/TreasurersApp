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

        public DbSet<Address> Addresses { get; set; } // Address
        public DbSet<AddressType> AddressTypes { get; set; } // AddressType
        public DbSet<CashJournal> CashJournals { get; set; } // CashJournal
        public DbSet<Contributor> Contributors { get; set; } // Contributor
        public DbSet<ContributorAddress> ContributorAddresses { get; set; } // ContributorAddress
        public DbSet<ContributorEmailAddress> ContributorEmailAddresses { get; set; } // ContributorEmailAddress
        public DbSet<ContributorPhoneNumber> ContributorPhoneNumbers { get; set; } // ContributorPhoneNumber
        public DbSet<EmailAddress> EmailAddresses { get; set; } // EmailAddress
        public DbSet<EmailType> EmailTypes { get; set; } // EmailType
        public DbSet<PhoneNumber> PhoneNumbers { get; set; } // PhoneNumber
        public DbSet<PhoneType> PhoneTypes { get; set; } // PhoneType
        public DbSet<Report> Reports { get; set; } // Report
        public DbSet<TransactionCategory> TransactionCategories { get; set; } // TransactionCategory
        public DbSet<TransactionType> TransactionTypes { get; set; } // TransactionType

        public DbSet<Claim> Claims { get; set; } // Claim
        public DbSet<History> Histories { get; set; } // History
        public DbSet<User> Users { get; set; } // User
        public DbSet<UserClaim> UserClaims { get; set; } // UserClaim

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(HOME_CONNECTION_STRING);
                optionsBuilder.UseSqlServer(WORK_CONNECTION_STRING);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(x => x.AddressId);
                entity.Property(x => x.AddressId)
                    .HasColumnName(@"AddressID")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.AddressLine1)
                    .HasColumnName(@"AddressLine1")
                    .HasColumnType("nvarchar")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.AddressLine2)
                    .HasColumnName(@"AddressLine2")
                    .HasColumnType("nvarchar")
                    .IsRequired(false)
                    .HasMaxLength(100);

                entity.Property(x => x.AddressLine3)
                    .HasColumnName(@"AddressLine3")
                    .HasColumnType("nvarchar")
                    .IsRequired(false)
                    .HasMaxLength(100);

                entity.Property(x => x.City)
                    .HasColumnName(@"City")
                    .HasColumnType("nvarchar")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.State)
                    .HasColumnName(@"State")
                    .HasColumnType("nvarchar")
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(x => x.PostalCode)
                    .HasColumnName(@"PostalCode")
                    .HasColumnType("nvarchar")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.CreatedBy)
                    .HasColumnName(@"CreatedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.CreatedDate)
                    .HasColumnName(@"CreatedDate")
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(x => x.LastModifiedBy)
                    .HasColumnName(@"LastModifiedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.LastModifiedDate)
                    .HasColumnName(@"LastModifiedDate")
                    .HasColumnType("datetime")
                    .IsRequired();
            });

            modelBuilder.Entity<AddressType>(entity =>
            {
                entity.HasKey(x => x.AddressTypeId);

                entity.Property(x => x.AddressTypeId)
                    .HasColumnName(@"AddressTypeID")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.Name)
                    .HasColumnName(@"Name")
                    .HasColumnType("varchar")
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(10);

                entity.Property(x => x.Description)
                    .HasColumnName(@"Description")
                    .HasColumnType("varchar")
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(50);

                entity.Property(x => x.Active)
                    .HasColumnName(@"Active")
                    .HasColumnType("bit")
                    .HasDefaultValueSql("((1))")
                    .IsRequired();

                entity.Property(x => x.CreatedBy)
                    .HasColumnName(@"CreatedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.CreatedDate)
                    .HasColumnName(@"CreatedDate")
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(x => x.LastModifiedBy)
                    .HasColumnName(@"LastModifiedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.LastModifiedDate)
                    .HasColumnName(@"LastModifiedDate")
                    .HasColumnType("datetime")
                    .IsRequired();
            });

            modelBuilder.Entity<CashJournal>(entity =>
            {
                entity.HasKey(x => x.CashJournalId);
                entity.Property(x => x.CashJournalId)
                    .HasColumnName(@"CashJournalID")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.CheckNumber)
                    .HasColumnName(@"CheckNumber")
                    .HasColumnType("int")
                    .IsRequired(false);

                entity.Property(x => x.Amount)
                    .HasColumnName(@"Amount")
                    .HasColumnType("decimal")
                    .IsRequired();

                entity.Property(x => x.ContributorId)
                    .HasColumnName(@"ContributorID")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.TransactionTypeId)
                    .HasColumnName(@"TransactionTypeID")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.EffectiveDate)
                    .HasColumnName(@"EffectiveDate")
                    .HasColumnType("datetime")
                    .IsRequired(false);

                entity.Property(x => x.CreatedBy)
                    .HasColumnName(@"CreatedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.CreatedDate)
                    .HasColumnName(@"CreatedDate")
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(x => x.LastModifiedBy)
                    .HasColumnName(@"LastModifiedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.LastModifiedDate)
                    .HasColumnName(@"LastModifiedDate")
                    .HasColumnType("datetime")
                    .IsRequired();

                // Foreign keys
                entity.HasOne(d => d.TransactionType)
                    .WithMany(p => p.CashJournal)
                    .HasForeignKey(d => d.TransactionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CashJournal_TransactionType");

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

                entity.Property(x => x.ClaimId)
                    .HasColumnName(@"ClaimID")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.ClaimType)
                    .HasColumnName(@"ClaimType")
                    .HasColumnType("varchar")
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(100);

                entity.Property(x => x.ClaimValue)
                    .HasColumnName(@"ClaimValue")
                    .HasColumnType("varchar")
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Contributor>(entity =>
            {
                entity.HasKey(x => x.ContributorId);

                entity.Property(x => x.ContributorId)
                    .HasColumnName(@"ContributorID")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.FirstName)
                    .HasColumnName(@"FirstName")
                    .HasColumnType("nvarchar")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.MiddleName)
                    .HasColumnName(@"MiddleName")
                    .HasColumnType("nvarchar")
                    .IsRequired(false)
                    .HasMaxLength(100);

                entity.Property(x => x.LastName)
                    .HasColumnName(@"LastName")
                    .HasColumnType("nvarchar")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.BahaiId)
                    .HasColumnName(@"BahaiID")
                    .HasColumnType("nvarchar")
                    .IsRequired(false)
                    .HasMaxLength(50);

                entity.Property(x => x.CreatedBy)
                    .HasColumnName(@"CreatedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.CreatedDate)
                    .HasColumnName(@"CreatedDate")
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(x => x.LastModifiedBy)
                    .HasColumnName(@"LastModifiedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.LastModifiedDate)
                    .HasColumnName(@"LastModifiedDate")
                    .HasColumnType("datetime")
                    .IsRequired();

            });

            modelBuilder.Entity<ContributorAddress>(entity =>
            {
                entity.HasKey(x => x.ContributorAddressId);

                entity.Property(x => x.ContributorAddressId)
                    .HasColumnName(@"ContributorAddressID")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.ContributorId)
                    .HasColumnName(@"ContributorID")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.AddressId)
                    .HasColumnName(@"AddressID")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.AddressTypeId)
                    .HasColumnName(@"AddressTypeID")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.Preferred)
                    .HasColumnName(@"Preferred")
                    .HasColumnType("bit")
                    .IsRequired();

                entity.Property(x => x.CreatedBy)
                    .HasColumnName(@"CreatedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.CreatedDate)
                    .HasColumnName(@"CreatedDate")
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(x => x.LastModifiedBy)
                    .HasColumnName(@"LastModifiedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.LastModifiedDate)
                    .HasColumnName(@"LastModifiedDate")
                    .HasColumnType("datetime")
                    .IsRequired();

                // Foreign keys
                entity
                    .HasOne(a => a.Address)
                    .WithMany(b => b.ContributorAddresses)
                    .HasForeignKey(c => c.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContributorAddress_Address"); // FK_ContributorAddress_Address

                entity
                    .HasOne(a => a.Contributor)
                    .WithMany(b => b.ContributorAddresses)
                    .HasForeignKey(c => c.ContributorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContributorAddress_Contributor"); // FK_ContributorAddress_Contributor
            });

            modelBuilder.Entity<ContributorEmailAddress>(entity =>
            {
                entity.HasKey(x => x.ContributorEmailAddressId);

                entity.Property(x => x.ContributorEmailAddressId)
                    .HasColumnName(@"ContributorEmailAddressID")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.ContributorId)
                    .HasColumnName(@"ContributorID")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.EmailAddressId)
                    .HasColumnName(@"EmailAddressID")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.Preferred)
                    .HasColumnName(@"Preferred")
                    .HasColumnType("bit")
                    .IsRequired();

                entity.Property(x => x.CreatedBy)
                    .HasColumnName(@"CreatedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.CreatedDate)
                    .HasColumnName(@"CreatedDate")
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(x => x.LastModifiedBy)
                    .HasColumnName(@"LastModifiedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.LastModifiedDate)
                    .HasColumnName(@"LastModifiedDate")
                    .HasColumnType("datetime")
                    .IsRequired();

                // Foreign keys
                entity
                    .HasOne(a => a.Contributor)
                    .WithMany(b => b.ContributorEmailAddresses)
                    .HasForeignKey(c => c.ContributorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContributorEmailAddress_Contributor"); // FK_ContributorEmailAddress_Contributor

                entity
                    .HasOne(a => a.EmailAddress)
                    .WithMany(b => b.ContributorEmailAddresses)
                    .HasForeignKey(c => c.EmailAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContributorEmailAddress_EmailAddress"); // FK_ContributorEmailAddress_EmailAddress
            });

            modelBuilder.Entity<ContributorPhoneNumber>(entity =>
            {
                entity.HasKey(x => x.ContributorPhoneNumberId);

                entity.Property(x => x.ContributorPhoneNumberId).HasColumnName(@"ContributorPhoneNumberID").HasColumnType("int").IsRequired();
                entity.Property(x => x.ContributorId).HasColumnName(@"ContributorID").HasColumnType("int").IsRequired();
                entity.Property(x => x.PhoneNumberId).HasColumnName(@"PhoneNumberID").HasColumnType("int").IsRequired();
                entity.Property(x => x.Preferred).HasColumnName(@"Preferred").HasColumnType("bit").IsRequired();
                entity.Property(x => x.CreatedBy).HasColumnName(@"CreatedBy").HasColumnType("uniqueidentifier").IsRequired();
                entity.Property(x => x.CreatedDate).HasColumnName(@"CreatedDate").HasColumnType("datetime").IsRequired();
                entity.Property(x => x.LastModifiedBy).HasColumnName(@"LastModifiedBy").HasColumnType("uniqueidentifier").IsRequired();
                entity.Property(x => x.LastModifiedDate).HasColumnName(@"LastModifiedDate").HasColumnType("datetime").IsRequired();

                // Foreign keys
                entity
                    .HasOne(a => a.Contributor)
                    .WithMany(b => b.ContributorPhoneNumbers)
                    .HasForeignKey(c => c.ContributorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContributorPhoneNumber_Contributor"); // FK_ContributorPhoneNumber_Contributor

                entity
                    .HasOne(a => a.PhoneNumber)
                    .WithMany(b => b.ContributorPhoneNumbers)
                    .HasForeignKey(c => c.PhoneNumberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContributorPhoneNumber_PhoneNumber"); // FK_ContributorPhoneNumber_PhoneNumber
            });

            modelBuilder.Entity<EmailAddress>(entity =>
            {
                entity.HasKey(x => x.EmailAddressId);

                entity.Property(x => x.EmailAddressId)
                    .HasColumnName(@"EmailAddressID")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.Email)
                    .HasColumnName(@"Email")
                    .HasColumnType("nvarchar")
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(x => x.CreatedBy)
                    .HasColumnName(@"CreatedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.CreatedDate)
                    .HasColumnName(@"CreatedDate")
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(x => x.LastModifiedBy)
                    .HasColumnName(@"LastModifiedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.LastModifiedDate)
                    .HasColumnName(@"LastModifiedDate")
                    .HasColumnType("datetime")
                    .IsRequired();
            });

            modelBuilder.Entity<EmailType>(entity =>
            {
                entity.HasKey(x => x.EmailTypeId);

                entity.Property(x => x.EmailTypeId)
                    .HasColumnName(@"EmailTypeID")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.Name)
                    .HasColumnName(@"Name")
                    .HasColumnType("varchar")
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(10);

                entity.Property(x => x.Description)
                    .HasColumnName(@"Description")
                    .HasColumnType("varchar")
                    .IsRequired(false)
                    .IsUnicode(false)
                    .HasMaxLength(50);

                entity.Property(x => x.Active)
                    .HasColumnName(@"Active")
                    .HasColumnType("bit")
                    .HasDefaultValueSql("((1))")
                    .IsRequired();

                entity.Property(x => x.CreatedBy)
                    .HasColumnName(@"CreatedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.CreatedDate)
                    .HasColumnName(@"CreatedDate")
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(x => x.LastModifiedBy)
                    .HasColumnName(@"LastModifiedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.LastModifiedDate)
                    .HasColumnName(@"LastModifiedDate")
                    .HasColumnType("datetime")
                    .IsRequired();
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.HasKey(x => x.HistoryId);

                entity.Property(x => x.HistoryId)
                    .HasColumnName(@"HistoryID")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.IdChanged)
                    .HasColumnName(@"IdChanged")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.RecordJson)
                    .HasColumnName(@"RecordJson")
                    .HasColumnType("nvarchar(max)")
                    .IsRequired();

                entity.Property(x => x.CreatedBy)
                    .HasColumnName(@"CreatedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.CreatedDate)
                    .HasColumnName(@"CreatedDate")
                    .HasColumnType("datetime")
                    .IsRequired();
            });

            modelBuilder.Entity<PhoneNumber>(entity =>
            {
                entity.HasKey(x => x.PhoneNumberId);

                entity.Property(x => x.PhoneNumberId)
                    .HasColumnName(@"PhoneNumberID")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.PhoneNumber_)
                    .HasColumnName(@"PhoneNumber")
                    .HasColumnType("varchar")
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(50);

                entity.Property(x => x.CreatedBy)
                    .HasColumnName(@"CreatedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.CreatedDate)
                    .HasColumnName(@"CreatedDate")
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(x => x.LastModifiedBy)
                    .HasColumnName(@"LastModifiedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.LastModifiedDate)
                    .HasColumnName(@"LastModifiedDate")
                    .HasColumnType("datetime")
                    .IsRequired();
            });

            modelBuilder.Entity<PhoneType>(entity =>
            {
                entity.HasKey(x => x.PhoneTypeId);

                entity.Property(x => x.PhoneTypeId)
                    .HasColumnName(@"PhoneTypeID")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.Name)
                    .HasColumnName(@"Name")
                    .HasColumnType("varchar")
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(20);

                entity.Property(x => x.Description)
                    .HasColumnName(@"Description")
                    .HasColumnType("varchar")
                    .IsRequired(false)
                    .IsUnicode(false)
                    .HasMaxLength(50);

                entity.Property(x => x.Active)
                    .HasColumnName(@"Active")
                    .HasColumnType("bit")
                    .HasDefaultValueSql("((1))")
                    .IsRequired();

                entity.Property(x => x.CreatedBy)
                    .HasColumnName(@"CreatedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.CreatedDate)
                    .HasColumnName(@"CreatedDate")
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(x => x.LastModifiedBy)
                    .HasColumnName(@"LastModifiedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.LastModifiedDate)
                    .HasColumnName(@"LastModifiedDate")
                    .HasColumnType("datetime")
                    .IsRequired();
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasKey(x => x.ReportId);
                entity.HasIndex(e => e.Name)
                    .HasName("UIX_Report_Name")
                    .IsUnique();

                entity.Property(x => x.ReportId)
                    .HasColumnName(@"ReportID")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.Name)
                    .HasColumnName(@"Name")
                    .HasColumnType("nvarchar")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(x => x.DisplayName)
                    .HasColumnName(@"DisplayName")
                    .HasColumnType("nvarchar")
                    .IsRequired(false)
                    .HasMaxLength(256);

                entity.Property(x => x.ConfigurationJson)
                    .HasColumnName(@"ConfigurationJson")
                    .HasColumnType("nvarchar")
                    .IsRequired(false)
                    .HasMaxLength(2048);

                entity.Property(x => x.Active)
                    .HasColumnName(@"Active")
                    .HasColumnType("bit")
                    .HasDefaultValueSql("((1))")
                    .IsRequired();

                entity.Property(x => x.DisplayOrder)
                    .HasColumnName(@"DisplayOrder")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.CreatedBy)
                    .HasColumnName(@"CreatedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.CreatedDate)
                    .HasColumnName(@"CreatedDate")
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(x => x.LastModifiedBy)
                    .HasColumnName(@"LastModifiedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.LastModifiedDate)
                    .HasColumnName(@"LastModifiedDate")
                    .HasColumnType("datetime")
                    .IsRequired();
            });

            modelBuilder.Entity<TransactionCategory>(entity =>
            {
                entity.HasKey(x => x.TransactionCategoryId);
                entity.HasIndex(e => e.Name)
                    .HasName("UIX_ContributionCategory_Name")
                    .IsUnique();

                entity.Property(x => x.TransactionCategoryId)
                    .HasColumnName(@"TransactionCategoryID")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.Name)
                    .HasColumnName(@"Name")
                    .HasColumnType("nvarchar")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(x => x.Description)
                    .HasColumnName(@"Description")
                    .HasColumnType("nvarchar")
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(x => x.DisplayOrder)
                    .HasColumnName(@"DisplayOrder")
                    .HasColumnType("int")
                    .HasDefaultValueSql("((1))")
                    .IsRequired();

                entity.Property(x => x.Active)
                    .HasColumnName(@"Active")
                    .HasColumnType("bit")
                    .HasDefaultValueSql("((1))")
                    .IsRequired();

                entity.Property(x => x.CreatedBy)
                    .HasColumnName(@"CreatedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.CreatedDate)
                    .HasColumnName(@"CreatedDate")
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(x => x.LastModifiedBy)
                    .HasColumnName(@"LastModifiedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.LastModifiedDate)
                    .HasColumnName(@"LastModifiedDate")
                    .HasColumnType("datetime")
                    .IsRequired();
            });

            modelBuilder.Entity<TransactionType>(entity =>
            {
                entity.HasKey(x => x.TransactionTypeId);

                entity.Property(x => x.TransactionTypeId)
                    .HasColumnName(@"TransactionTypeID")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.TransactionCategoryId)
                    .HasColumnName(@"TransactionCategoryID")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.Name)
                    .HasColumnName(@"Name")
                    .HasColumnType("nvarchar")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.Description)
                    .HasColumnName(@"Description")
                    .HasColumnType("nvarchar")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.DisplayOrder)
                    .HasColumnName(@"DisplayOrder")
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(x => x.Active)
                    .HasColumnName(@"Active")
                    .HasColumnType("bit")
                    .HasDefaultValueSql("((1))")
                    .IsRequired();

                entity.Property(x => x.CreatedBy)
                    .HasColumnName(@"CreatedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.CreatedDate)
                    .HasColumnName(@"CreatedDate")
                    .HasColumnType("datetime")
                    .IsRequired();

                entity.Property(x => x.LastModifiedBy)
                    .HasColumnName(@"LastModifiedBy")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.LastModifiedDate)
                    .HasColumnName(@"LastModifiedDate")
                    .HasColumnType("datetime")
                    .IsRequired();

                // Foreign keys
                entity.HasOne(a => a.TransactionCategory)
                    .WithMany(b => b.TransactionTypes)
                    .HasForeignKey(c => c.TransactionCategoryId)
                    .HasConstraintName("FK_TransactionType_TransactionCategory")
                    .OnDelete(DeleteBehavior.SetNull); // FK_TransactionType_ContributionCategory

                entity.HasIndex(e => e.Name)
                    .HasName("UIX_TransactionType_Name")
                    .IsUnique();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("User", "Security");

                entity.HasIndex(e => e.UserName)
                    .HasName("UIX_SecurityUser_Name")
                    .IsUnique();

                entity.Property(x => x.UserId)
                    .HasColumnName(@"UserID")
                    .HasColumnType("uniqueidentifier")
                    .HasDefaultValueSql("(newid())")
                    .IsRequired();

                entity.Property(x => x.UserName)
                    .HasColumnName(@"UserName")
                    .HasColumnType("nvarchar")
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(x => x.DisplayName)
                    .HasColumnName(@"DisplayName")
                    .HasColumnType("nvarchar")
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(x => x.Password)
                    .HasColumnName(@"Password")
                    .HasColumnType("varchar")
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<UserClaim>(entity =>
            {
                entity.HasKey(e => e.UserClaimId)
                    .ForSqlServerIsClustered(false);

                entity.ToTable("UserClaim", "Security");

                entity.HasIndex(e => new { e.ClaimId, e.UserId })
                    .HasName("UIX_SecurityUserClaim_ClaimID_UserID");

                entity.Property(x => x.UserClaimId)
                    .HasColumnName(@"UserClaimID")
                    .HasColumnType("uniqueidentifier")
                    .HasDefaultValueSql("(newid())")
                    .IsRequired();

                entity.Property(x => x.ClaimId)
                    .HasColumnName(@"ClaimID")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                entity.Property(x => x.UserId)
                    .HasColumnName(@"UserID")
                    .HasColumnType("uniqueidentifier")
                    .IsRequired();

                // Foreign keys
                entity.HasOne(a => a.Claim)
                    .WithMany(b => b.UserClaims)
                    .HasForeignKey(c => c.ClaimId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserClaim_Claim"); // FK_UserClaim_Claim

                entity.HasOne(a => a.User)
                    .WithMany(b => b.UserClaims)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserClaim_User"); // FK_UserClaim_User
            });
        }
    }
}
