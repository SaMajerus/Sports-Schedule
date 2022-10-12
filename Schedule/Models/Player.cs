using System.Collections.Generic;
using System;

namespace Schedule.Models
{
  public class Player
  {
    public Player()
    {
      this.JoinPlrSprt = new HashSet<SportPlayer>();
    }

    public string Name { get; set; }
    public int PlayerId { get; set; }
    public int Grade { get; set; }
    public virtual ICollection<SportPlayer> JoinPlrSprt { get; set; }
  }
}