using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;
using System.Diagnostics;
using CPDatabase.Models;
using CPDatabase.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize]
        public IActionResult Reply(int? id)
        {
            if (id != null)
            {
                FeedbackLog feedback = cpdbcontext.FeedbackLog.FirstOrDefault(fbl => fbl.MessageId == id);
                if (feedback != null) return PartialView(feedback);
            }
            return NotFound();
        }

        [Authorize]
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await cpdbcontext.User.FirstOrDefaultAsync(u => u.Username == model.Name && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(model.Name);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Incorrect name and/or password");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }

        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        User user = await cpdbcontext.User.FirstOrDefaultAsync(u => u.Username == model.Name);
        //        if (user == null)
        //        {
        //            cpdbcontext.User.Add(new User { Username = model.Name, Password = model.Password });
        //            await cpdbcontext.SaveChangesAsync();

        //            await Authenticate(model.Name);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //            ModelState.AddModelError("", "Incorrect name and/or password");
        //    }
        //    return View(model);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        //todo: clean usings etc.
        //todo: make changelog
        //todo: make error page
        //todo: make contacts page
        //todo: make feedback page
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