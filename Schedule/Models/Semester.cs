using System.Collections.Generic;
using System;

namespace Schedule.Models
{
  public class Semester
  {
    public Semester()
    {
      this.JoinSmstrSprt = new HashSet<SemesterSport>();
    }
    public string Term { get; set; }
    public int SemesterId { get; set; }
    public int SportId { get; set; }
    public virtual Sport Sport { get; set; }
    public virtual ICollection<SemesterSport> JoinSmstrSprt { get; set; }
  }
}