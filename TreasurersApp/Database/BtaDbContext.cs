using Microsoft.EntityFrameworkCore;
using TreasurersApp.Model;

namespace TreasurersApp.Database
{
  public class BtaDbContext : DbContext
  {
    public DbSet<AppUser> Users { get; set; }
    public DbSet<AppUserClaim> UserClaims { get; set; }
    public DbSet<AppCashJournal> CashJournals { get; set; }
    public DbSet<Report> Reports { get; set; }

    // private const string CONN =
    //               @"Server=Localhost;
    //                 Database=BTA;
    //                 Trusted_Connection=True;
    //                 MultipleActiveResultSets=true";

    private const string CONN = @"Server=(localdb)\MSSQLLocalDB;
    Database=BTA;
    AttachDbFilename=F:\R_D\bta\SQLData\BTA.mdf;
    MultipleActiveResultSets=true";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer(CONN);
    }
  }
}
