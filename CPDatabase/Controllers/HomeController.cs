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

        public IActionResult Changelog()
        {
            return View();
        }

        public IActionResult Feedback()
        {
            return View(cpdbcontext.FeedbackLog.ToList());
        }

        [HttpPost]
        public IActionResult MakeFeedback(FeedbackLog feedback)
        {
            cpdbcontext.FeedbackLog.Add(feedback);
            cpdbcontext.SaveChanges();
            return RedirectToAction("Feedback");
        }

        public IActionResult Contacts()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //todo: clean usings etc.
        //todo: check out what EF made
        //todo: make pages for Team/NT entities
        //todo: make changelog
        //todo: make contacts page
        //todo: make feedback page
        //todo: explore bootstrap
        //todo: show on github
        //todo: make 2nd level menus
    }
}