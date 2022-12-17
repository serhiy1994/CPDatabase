using CPDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPDatabase.Controllers
{
    public class TeamController : Controller
    {
        CPDBContext cpdbcontext;

        public TeamController(CPDBContext context)
        {
            cpdbcontext = context;
        }

        public IActionResult View(int? id)
        {
            if (id == null) return RedirectToAction("All");
            ViewBag.ViewTeamId = id;
            return View();
        }

        public IActionResult All()
{
            return View(cpdbcontext.Team.ToList());
        }
        
        public IActionResult CountryClub()
        {
            return View(cpdbcontext.CountryClub.ToList());
        }

        public IActionResult Club()
        {
            return View(cpdbcontext.Club.ToList());
        }

        public IActionResult LeagueTeam()
        {
            return View(cpdbcontext.LeagueTeam.ToList());
        }

        public IActionResult HalfDecade()
        {
            return View(cpdbcontext.HalfDecade.ToList());
        }

        public IActionResult Season()
        {
            return View(cpdbcontext.Season.ToList());
        }
    }
}