using System.Collections.Generic;
using System;

namespace Schedule.Models
{
  public class Semester
  {
    public Semester()
    {
      this.JoinSmstrSprt = new HashSet<SemesterSport>();
      this.JoinSmstrPlyr = new HashSet<SemesterPlayer>();
    }
    public string Term { get; set; }
    public int SemesterId { get; set; }
    public virtual ICollection<SemesterSport> JoinSmstrSprt { get; set; }
    public virtual ICollection<SemesterPlayer> JoinSmstrPlyr { get; set; }
  }
}