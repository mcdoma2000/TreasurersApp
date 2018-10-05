using Microsoft.EntityFrameworkCore;
using TreasurersApp.Model;

namespace TreasurersApp.Database
{
    public class BtaDbContext : DbContext
    {
        private string _dbPath;
        public string DbPath
        {
            get { return _dbPath; }
            set { }
        }

        public BtaDbContext(string dbPath)
        {
            _dbPath = dbPath;
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppUserClaim> UserClaims { get; set; }
        public DbSet<AppCashJournal> CashJournals { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string CONN = string.Format(@"Server=(localdb)\MSSQLLocalDB;Database=BTA;AttachDbFilename={0};MultipleActiveResultSets=true", DbPath);
            optionsBuilder.UseSqlServer(CONN);
        }
    }
}
