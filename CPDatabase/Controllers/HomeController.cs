using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using CPDatabase.Models;

namespace CPDatabase.Controllers
{
    public class HomeController : Controller
    {
        CPDBContext cpdbcontext;

        public HomeController(CPDBContext context)
        {
            cpdbcontext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CountryClub()
        {
            return View(cpdbcontext.CountryClub.ToList());
        }

        public IActionResult CountryNT()
        {
            return View(cpdbcontext.CountryNT.ToList());
        }

        public IActionResult HalfDecade()
        {
            return View(cpdbcontext.HalfDecade.ToList());
        }

        public IActionResult Season()
        {
            return View(cpdbcontext.Season.ToList());
        }

        public IActionResult Year()
        {
            return View(cpdbcontext.Year.ToList());
        }

        public IActionResult Period()
        {
            return View(cpdbcontext.Period.ToList());
        }

        public IActionResult LeagueTeam()
        {
            return View(cpdbcontext.LeagueTeam.ToList());
        }

        public IActionResult LeagueNT()
        {
            return View(cpdbcontext.LeagueNT.ToList());
        }

        public IActionResult Club()
        {
            return View(cpdbcontext.Club.ToList());
        }
        public IActionResult Team()
        {
            return View(cpdbcontext.Team.ToList());
        }

        public IActionResult NT()
        {
            return View(cpdbcontext.NationalTeam.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}