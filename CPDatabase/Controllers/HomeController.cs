using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using CPDatabase.Models;

namespace CPDatabase.Controllers
{
    public class HomeController : Controller
    {
        private CPDBContext cpdbcontext;

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
            if (ModelState.IsValid)
            {
                cpdbcontext.FeedbackLog.Add(feedback);
                cpdbcontext.SaveChanges();
                return RedirectToAction("Feedback");
            }
            else return Content("An error occured during adding feedback. Please go back and try again.");
        }

        public IActionResult Contacts()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            return Content($"Login: {login}, Password: {password}");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //todo: clean usings etc.
        //todo: check out what EF made
        //todo: make changelog
        //todo: make contacts page
        //todo: make feedback page
        //todo: make login page
        //todo: make 2nd level menus
        //todo: write views using helpers
        //todo: make page titles
        //todo: add validatePartial cshtml
        //todo: make one style pages
        //todo: explore bootstrap
        //todo: show on github
    }
}