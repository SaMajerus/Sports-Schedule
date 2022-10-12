namespace Schedule.Models
{
  public class SportPlayer
  {       
    public int SportPlayerId { get; set; }
    public int PlayerId { get; set; }
    public int SportId { get; set; }
    public virtual Player Player { get; set; }
    public virtual Sport Sport { get; set; }
  }
}