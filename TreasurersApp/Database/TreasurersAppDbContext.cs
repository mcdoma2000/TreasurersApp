using Microsoft.EntityFrameworkCore;
using System;
using TreasurersApp.Models;

namespace TreasurersApp.Database
{
    public class TreasurersAppDbContext : DbContext
    {
        private readonly string _dbPath;
        public string DbPath
        {
            get { return _dbPath; }
            set { }
        }

        public TreasurersAppDbContext()
        {
            string region = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            string dbLoc = string.Format("Database:{0}:DatabaseDirectory", region);
            _dbPath = dbLoc;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<CashJournal> CashJournals { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Contributor> Contributors { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ContributionType> ContributionTypes { get; set; }
        public DbSet<ContributionCategory> ContributionCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string CONN = string.Format(@"Server=(localdb)\MSSQLLocalDB;Database=BTA;AttachDbFilename={0};MultipleActiveResultSets=true", DbPath);
            optionsBuilder
                .UseSqlServer(CONN, ob =>
                {
                    ob.EnableRetryOnFailure();
                })
                .EnableSensitiveDataLogging(true);
        }
    }
}
