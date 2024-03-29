﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Linq;
using System.Diagnostics;
using System;
using System.Text;
using CPDatabase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System.Threading;

namespace CPDatabase.Controllers
{
    public class HomeController : Controller
    {
        private CPDBContext cpdbcontext;
        StringBuilder sb;

        public HomeController(CPDBContext context)
        {
            cpdbcontext = context;
            sb = new StringBuilder();
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
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            int pageSize = 25;
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
            var count = await feedbacks.CountAsync(cancellationToken);
            var items = await feedbacks.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            FeedbackSortViewModel sortViewModel = new FeedbackSortViewModel(sortOrder);
            FeedbackViewModel feedbackViewModel = new FeedbackViewModel(null) { PageViewModel = pageViewModel, SortViewModel = sortViewModel, Feedbacks = items };

            return View(feedbackViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> MakeFeedback(FeedbackInputModel feedback)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            FeedbackViewModel vm = await BuildFeedbackViewModelAsync(feedback.ReturnUrl!, cancellationToken);
            if (ModelState.IsValid)
            {
                if (feedback != null)
                {
                    FeedbackLog fb = new FeedbackLog() { Message = feedback.Message, Email = feedback.Email, Username = feedback.Username, DateMessage = DateTime.Now };
                    cpdbcontext.FeedbackLog.Add(fb);
                    cpdbcontext.SaveChanges();
                    return RedirectToAction("Feedback");
                }
                foreach (var error in ModelState.Values.SelectMany(x => x.Errors))
                    sb.Append(error.ErrorMessage + "; ");
                ModelState.AddModelError("Feedbacking process error(s)", sb.ToString());
                sb.Clear();
            }
            return RedirectToAction("Feedback", vm);
        }

        private async Task<FeedbackViewModel> BuildFeedbackViewModelAsync(string returnUrl, CancellationToken cancellationToken)
        {
            var vm = new FeedbackViewModel(returnUrl)
            {                
                ReturnUrl = returnUrl
            };
            return vm;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Reply(FeedbackLog feedback)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            if (feedback != null)
            {
                FeedbackLog primaryFeedback = await cpdbcontext.FeedbackLog.FirstOrDefaultAsync(fbl => fbl.MessageId == feedback.MessageId, cancellationToken);
                if (primaryFeedback != null)
                {
                    primaryFeedback.Reply = feedback.Reply;
                    primaryFeedback.DateReply = DateTime.Now;
                    cpdbcontext.FeedbackLog.Update(primaryFeedback);
                    cpdbcontext.SaveChanges();
                    return RedirectToAction("Feedback");
                }
                foreach (var error in ModelState.Values.SelectMany(x => x.Errors))
                    sb.Append(error.ErrorMessage + "; ");
                ModelState.AddModelError("Replying process error(s)", sb.ToString());
                sb.Clear();
            }
            return NotFound();
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
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            if (ModelState.IsValid)
            {
                User user = await cpdbcontext.User.FirstOrDefaultAsync(u => u.Username == model.Name && u.Password == model.Password, cancellationToken);
                if (user != null)
                {
                    await Authenticate(model.Name);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in ModelState.Values.SelectMany(x => x.Errors))
                    sb.Append(error.ErrorMessage + "; ");
                ModelState.AddModelError("Logging in process error(s)", sb.ToString());
                sb.Clear();
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

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, 
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = null, IsEssential = true}
                );
            return LocalRedirect(returnUrl);
        }

        //todo: check DB Val
        //todo: make error page
        //todo: make privacy policy page
        //todo: complete feedback reply page (show validation and localization results to an user)
        //todo: complete localizations for other languages
        //todo: check images paths (place them in the wwwroot folder + make accessible)
        //todo: check English grammar
        //---
        //todo: team[nt]/all
        //---
        //todo: implement the actual DBs
        //todo: add SEO stuff
        //todo: complete design
        //todo: make donate button
        //todo: publish on github
    }
}