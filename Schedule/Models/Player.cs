using System.Collections.Generic;
using System;

namespace Schedule.Models
{
  public class Player
  {
    public Player()
    {
      this.JoinPlrSprt = new HashSet<PlayerSport>();
    }

    public string Name { get; set; }
    public int PlayerId { get; set; }
    public int Grade { get; set; }
    public virtual ICollection<PlayerSport> JoinPlrSprt { get; set; }
  }
}