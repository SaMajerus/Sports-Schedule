namespace Schedule.Models
{
  public class SemesterSport
  {  
    public int SemesterSportId { get; set; }
    public int SemesterId { get; set; }
    public int SportId { get; set; }
    public virtual Semester Semester { get; set; }
    public virtual Sport Sport { get; set; }
  }
}