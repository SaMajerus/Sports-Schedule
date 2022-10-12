using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Schedule.Models;
using System.Collections.Generic;
using System.Linq;

namespace Schedule.Controllers
{
  public class PlayersController : Controller
  {
    private readonly ScheduleContext _db;

    public PlayersController(ScheduleContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Players.ToList());
    }

    public ActionResult Create()
    {
      ViewBag.SportId = new SelectList(_db.Sports, "SportId", "Title");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Player player, int SportId)
    {
      _db.Players.Add(player);
      _db.SaveChanges();
      if (SportId != 0)
      {
        _db.SportPlayer.Add(new SportPlayer() { SportId = SportId, PlayerId = player.PlayerId});
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisPlayer = _db.Players
        .Include(player => player.JoinPlrSprt)
        .ThenInclude(join => join.Sport)
        .FirstOrDefault(player => player.PlayerId == id);
      return View(thisPlayer);
    }

    public ActionResult Edit(int id)
    {
      var thisPlayer = _db.Players.FirstOrDefault(player => player.PlayerId == id);
      ViewBag.SportId = new SelectList(_db.Sports, "SportId", "Title");
      return View(thisPlayer);
    }

    [HttpPost]
    public ActionResult Edit(Player player, int SportId)
    {
      if (SportId != 0)
      {
        _db.SportPlayer.Add(new SportPlayer() { SportId = SportId, PlayerId = player.PlayerId});
      }
      _db.Entry(player).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddSport(int id)
    {
      var thisPlayer = _db.Players.FirstOrDefault(player => player.PlayerId == id);
      ViewBag.SportId = new SelectList(_db.Sports, "SportId", "Title");
      return View(thisPlayer);
    }

    [HttpPost]
    public ActionResult AddSport(Player player, int SportId)
    {
      if (SportId != 0)
      {
        _db.SportPlayer.Add(new SportPlayer() { SportId = SportId, PlayerId = player.PlayerId});
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisPlayer = _db.Players.FirstOrDefault(player => player.PlayerId == id);
      return View(thisPlayer);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisPlayer = _db.Players.FirstOrDefault(player => player.PlayerId == id);
      _db.Players.Remove(thisPlayer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteSport(int joinId)
    {
      var joinEntry = _db.SportPlayer.FirstOrDefault(entry => entry.SportPlayerId == joinId);
      _db.SportPlayer.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}