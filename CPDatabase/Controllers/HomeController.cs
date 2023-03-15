﻿using System.Collections.Generic;
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
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

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

        public async Task<IActionResult> Feedback(int page = 1, FeedbackSortState sortOrder = FeedbackSortState.MessageDateAsc)
        {
            int pageSize = 10;
            IQueryable<FeedbackLog> feedbacks = cpdbcontext.FeedbackLog.AsQueryable();

            feedbacks = sortOrder switch
            {
                FeedbackSortState.MessageDateDesc => feedbacks.OrderByDescending(s => s.DateMessage),
                FeedbackSortState.UsernameAsc => feedbacks.OrderBy(s => s.Username),
                FeedbackSortState.UsernameDesc => feedbacks.OrderByDescending(s => s.Username),
                FeedbackSortState.EmailAsc => feedbacks.OrderBy(s => s.Email),
                FeedbackSortState.EmailDesc => feedbacks.OrderByDescending(s => s.Email),
                FeedbackSortState.MessageAsc => feedbacks.OrderBy(s => s.Message),
                FeedbackSortState.MessageDesc => feedbacks.OrderByDescending(s => s.Message),
                FeedbackSortState.ReplyAsc => feedbacks.OrderBy(s => s.Reply),
                FeedbackSortState.ReplyDesc => feedbacks.OrderByDescending(s => s.Reply),
                FeedbackSortState.ReplyDateAsc => feedbacks.OrderBy(s => s.DateReply),
                FeedbackSortState.ReplyDateDesc => feedbacks.OrderByDescending(s => s.DateReply),
                _ => feedbacks.OrderBy(s => s.DateMessage),
            };
            var count = await feedbacks.CountAsync();
            var items = await feedbacks.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            FeedbackSortViewModel sortViewModel = new FeedbackSortViewModel(sortOrder);
            FeedbackViewModel feedbackViewModel = new FeedbackViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, Feedbacks = items };

            return View(feedbackViewModel);
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

        //todo: make changelog
        //todo: make error page
        //todo: make contacts page
        //todo: make home page
        //todo: make feedback reply page
        //todo: make 2nd level menus
        //todo: check images paths
        //todo: fix bug with both filters selected
        //todo: decide what to do with fixed names
        //todo: check scripts + styles links
        //todo: add validatePartial cshtml
        //todo: make clean iequeryable
        //todo: remove pagesize
        //todo: hide unnecessary columns
        //todo: explore bootstrap
        //todo: show on github
        //todo: make donate button
    }
}