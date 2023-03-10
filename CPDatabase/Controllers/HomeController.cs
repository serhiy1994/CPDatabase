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

        public IActionResult Reply(int? id)
        {
            if (id != null)
            {
                FeedbackLog feedback = cpdbcontext.FeedbackLog.FirstOrDefault(fbl => fbl.MessageId == id);
                if (feedback != null) return PartialView(feedback);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Reply(FeedbackLog feedback)
        {
            FeedbackLog primaryFeedback = cpdbcontext.FeedbackLog.FirstOrDefault(fbl => fbl.MessageId == feedback.MessageId);
            primaryFeedback.Reply = feedback.Reply;
            primaryFeedback.DateReply = feedback.DateReply;
            cpdbcontext.FeedbackLog.Update(primaryFeedback);
            cpdbcontext.SaveChanges();
            return RedirectToAction("Feedback");
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
        //todo: make changelog
        //todo: make error page
        //todo: make contacts page
        //todo: make feedback page
        //todo: make login page
        //todo: make 2nd level menus
        //todo: check images paths
        //todo: fix bug with both filters selected
        //todo: decide what to do with fixed names
        //todo: fix the page titles
        //todo: add validatePartial cshtml
        //todo: make ienumerable
        //todo: explore bootstrap
        //todo: show on github
        //todo: make donate button
    }
}