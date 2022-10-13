using Microsoft.AspNetCore.Mvc;
using Schedule.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Schedule.Controllers
{
  public class SemestersController : Controller
  {
    private readonly ScheduleContext _db;

    public SemestersController(ScheduleContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Semester> model = _db.Semesters.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.SportId = new SelectList(_db.Sports, "SportId", "Title");  //Connects to:  ../Views/Semesters/Create.cshtml, Ln 14.
      return View();
    }

    [HttpPost]  //(Destination for Views/Semesters/Create.cshtml, Ln 16) 
    public ActionResult Create(Semester semester, string term, int SemesterId, int SportId)
    {
      if (SportId != 0)
      {
        _db.SemesterSport.Add(new SemesterSport() { SportId = SportId, SemesterId = semester.SemesterId });
        _db.SaveChanges();
      }
      _db.Semesters.Add(semester);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisSemester = _db.Semesters
        .Include(semester => semester.JoinSmstrSprt)  //Details View, Ln 10
        .ThenInclude(join => join.Sport)  //Details View, Ln 20 
        .Include(semester => semester.JoinSmstrPlyr)
        .ThenInclude(join => join.Player)
        .FirstOrDefault(semester => semester.SemesterId == id);
      return View(thisSemester);
    }

    public ActionResult Edit(int id)
    {  
      Semester thisSemester = _db.Semesters.FirstOrDefault(semester => semester.SemesterId == id);
      return View(thisSemester);
    }

    [HttpPost]  //(Destination for Views/Semesters/Edit.cshtml, Ln 21)
    public ActionResult Edit(Semester semester)
    {
      _db.Entry(semester).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Semester thisSemester = _db.Semesters.FirstOrDefault(semester => semester.SemesterId == id);
      return View(thisSemester);
    }

    [HttpPost, ActionName("Delete")] 
    public ActionResult DeleteConfirmed(int id)
    {
      Semester thisSemester = _db.Semesters.FirstOrDefault(semester => semester.SemesterId == id);
      _db.Semesters.Remove(thisSemester);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddSport(int id)
    {
      var thisSemester = _db.Semesters.FirstOrDefault(semester => semester.SemesterId == id);  //Calls the semester which we'll be adding a Sport for. 
      ViewBag.SportId = new SelectList(_db.Sports, "SportId", "Title");
      return View(thisSemester);
    }

    [HttpPost]
    public ActionResult AddSport(Semester semester, int SportId)
    {
      if (SportId != 0)
      {
        _db.SemesterSport.Add(new SemesterSport() { SportId = SportId, SemesterId = semester.SemesterId});
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }
    public ActionResult AddPlayer(int id)
    {
      var thisSemester = _db.Semesters.FirstOrDefault(semester => semester.SemesterId == id); 
      ViewBag.PlayerId = new SelectList(_db.Players, "PlayerId", "Name");
      return View(thisSemester);
    }

    [HttpPost]
    public ActionResult AddPlayer(Semester semester, int PlayerId)
    {
      if (PlayerId != 0)
      {
        _db.SemesterPlayer.Add(new SemesterPlayer() { PlayerId = PlayerId, SemesterId = semester.SemesterId});
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }
  }
}