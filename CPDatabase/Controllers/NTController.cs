using CPDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPDatabase.Controllers
{
    public class NTController : Controller
    {
        CPDBContext cpdbcontext;

        public NTController(CPDBContext context)
        {
            cpdbcontext = context;
        }

        public IActionResult View(int? id)
        {
            if (id == null) return RedirectToAction("All");
            NationalTeam teamNT = cpdbcontext.NationalTeam.FirstOrDefault(t => t.Id == id);
            if (teamNT != null) return View(teamNT);
            else return NotFound();
        }

        public IActionResult CountryNT()
        {
            return View(cpdbcontext.CountryNT.ToList());
        }

        public IActionResult Year()
        {
            return View(cpdbcontext.Year.ToList());
        }

        public IActionResult Period()
        {
            return View(cpdbcontext.Period.ToList());
        }

        public IActionResult LeagueNT()
        {
            return View(cpdbcontext.LeagueNT.ToList());
        }

        public IActionResult All()
        {
            return View(cpdbcontext.NationalTeam.ToList());
        }
    }
}