using Microsoft.EntityFrameworkCore;

namespace Schedule.Models
{
  public class ScheduleContext : DbContext
  {

    public DbSet<Player> Players { get; set;}
    public DbSet<Sport> Sports { get; set; }
    public DbSet<Semester> Semesters { get; set; }
    public DbSet<SemesterSport> SemesterSport { get; set; }
    public DbSet<SportPlayer> SportPlayer { get; set; }
    public DbSet<SemesterPlayer> SemesterPlayer { get; set; }

    public ScheduleContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}