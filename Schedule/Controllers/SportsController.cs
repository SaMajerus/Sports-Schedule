using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Schedule.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Schedule.Controllers
{
  public class SportsController : Controller
  {
    private readonly ScheduleContext _db;

    public SportsController(ScheduleContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Sport> model = _db.Sports.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.PlayerId = new SelectList(_db.Players, "PlayerId", "Name");
      ViewBag.SemesterId = new SelectList(_db.Semesters, "SemesterId", "Term");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Sport sport, string title)
    {
      _db.Sports.Add(sport);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisSport = _db.Sports
        .Include(semester => semester.JoinSmstrSprt)
        .ThenInclude(join => join.Semester)  //Connected to:  Details View, Ln 25 
        .Include(sport => sport.JoinPlrSprt)
        .ThenInclude(join => join.Player)  //Connected to:  Details View, Ln 10 
        .FirstOrDefault(sport => sport.SportId == id);
      return View(thisSport);
    }

    public ActionResult Edit(int id)
    {
      var thisSport = _db.Sports.FirstOrDefault(sport => sport.SportId == id);
      return View(thisSport);
    }

    [HttpPost]
    public ActionResult Edit(Sport sport)
    {
      _db.Entry(sport).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisSport = _db.Sports.FirstOrDefault(sport => sport.SportId == id);
      return View(thisSport);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisSport = _db.Sports.FirstOrDefault(sport => sport.SportId == id);
      _db.Sports.Remove(thisSport);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
    public ActionResult AddPlayer(int id)
    {
      var thisSport = _db.Sports.FirstOrDefault(sport => sport.SportId == id);  //Calls the Sport which we'll be adding a Player for. 
      ViewBag.PlayerId = new SelectList(_db.Players, "PlayerId", "Name");  //Connects to:  ../Views/Sports/AddPlayer.cshtml, Ln 16. 
      return View(thisSport); 
    }

    [HttpPost]
    public ActionResult AddPlayer(Sport sport, int PlayerId)
    {
      if (PlayerId != 0)
      {
        _db.SportPlayer.Add(new SportPlayer() { PlayerId = PlayerId, SportId = sport.SportId});
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult AddSemester(int id)
    {
      var thisSport = _db.Sports.FirstOrDefault(sport => sport.SportId == id);  //Calls the Sport which we'll be adding a Semester for. 
      ViewBag.SemesterId = new SelectList(_db.Semesters, "SemesterId", "Term");  //Connects to:  ../Views/Sports/AddSemester.cshtml, Ln 16. 
      return View(thisSport);
    }

    [HttpPost]
    public ActionResult AddSemester(Sport sport, int SemesterId)
    {
      if (SemesterId != 0)
      {
        _db.SemesterSport.Add(new SemesterSport() { SemesterId = SemesterId, SportId = sport.SportId});
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }
  }
}