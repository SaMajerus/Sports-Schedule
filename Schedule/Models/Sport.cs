using System.Collections.Generic;
using System;

namespace Schedule.Models
{
  public class Sport
  {
    public Sport()
    {
      this.JoinPlrSprt = new HashSet<SportPlayer>();
      this.JoinSmstrSprt = new HashSet<SemesterSport>();
    }

    public int SportId { get; set; }
    public string Title { get; set; }
    public virtual ICollection<SportPlayer> JoinPlrSprt { get; set; }
    public virtual ICollection<SemesterSport> JoinSmstrSprt { get; set; }
  }
}