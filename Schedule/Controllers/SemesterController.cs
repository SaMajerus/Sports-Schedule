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
      ViewBag.PlayerId = new SelectList(_db.Players, "PlayerId", "Name");
      ViewBag.SportId = new SelectList(_db.Sports, "SportId", "Title");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Semester semester, int SportId)
    {
      _db.Semesters.Add(semester);
      _db.SaveChanges();
      if (SportId != 0)
      {
        _db.SemesterSport.Add(new SemesterSport() { SportId = SportId, SemesterId = semester.SemesterId });
        _db.SaveChanges();
      }
    }

    public ActionResult Details(int id)
    {
      var thisSemester = _db.Semesters
        .Include(semester => semester.JoinSmstrSprt)
        .ThenInclude(join => join.Sport)
        .FirstOrDefault(semester => semester.SemesterId == id);
      return View(thisSemester);
    }

    public ActionResult Edit(int id)
    {
      ViewBag.PlayerId = new SelectList(_db.Players, "PlayerId", "Name");
      ViewBag.SportId = new SelectList(_db.Sports, "SportId", "Title");
      Semester thisSemester = _db.Semesters.FirstOrDefault(semester => semester.SemesterId == id);
      return View(thisSemester);
    }

    [HttpPost]
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
  }
}