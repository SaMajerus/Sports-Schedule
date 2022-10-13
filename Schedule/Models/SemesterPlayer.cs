namespace Schedule.Models
{
  public class SemesterPlayer
  {
    public int SemesterPlayerId { get; set; }
    public int SemesterId { get; set; }
    public int PlayerId { get; set; }    
    public virtual Semester Semester { get; set; }
    public virtual Player Player { get; set; }
  }
}