using Microsoft.EntityFrameworkCore;

namespace Schedule.Models
{
  public class ScheduleContext : DbContext
  {

    public DbSet<Player> Players { get; set;}
    public DbSet<Sport> Sports { get; set; }
    public DbSet<SemesterSport> SemesterSport { get; set; }
    public DbSet<PlayerSport> PlayerSport { get; set; }

    public ScheduleContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}