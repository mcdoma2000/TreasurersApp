using Microsoft.EntityFrameworkCore;
using TreasurersApp.Models;

namespace TreasurersApp.Database
{
    public class TreasurersAppDbContext : DbContext
    {
        private string _dbPath;
        public string DbPath
        {
            get { return _dbPath; }
            set { }
        }

        public TreasurersAppDbContext(string dbPath)
        {
            _dbPath = dbPath;
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppUserClaim> UserClaims { get; set; }
        public DbSet<AppCashJournal> CashJournals { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<AppContributor> Contributors { get; set; }
        public DbSet<AppAddress> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string CONN = string.Format(@"Server=(localdb)\MSSQLLocalDB;Database=BTA;AttachDbFilename={0};MultipleActiveResultSets=true", DbPath);
            optionsBuilder
                .UseSqlServer(CONN)
                .EnableSensitiveDataLogging(true);
        }
    }
}
