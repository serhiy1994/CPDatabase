using CPDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPDatabase.Controllers
{
    public class LeagueNTController : Controller
    {
        CPDBContext cpdbcontext;

        public LeagueNTController(CPDBContext context)
        {
            cpdbcontext = context;
        }

        public async Task<IActionResult> View(int? id, int page = 1, NTSortState sortOrder = NTSortState.NameAsc)
        {
            int pageSize = 10;
            if (id == null) return RedirectToAction("LeagueNT", "NT");
            IQueryable<NationalTeam> NTsByLeague = cpdbcontext.NationalTeam.Where(t => t.LeagueNtNavigation.Id == id);
            if (NTsByLeague != null)
            {
                ViewBag.CurrentLeagueNTName = cpdbcontext.LeagueNT.FirstOrDefault(c => c.Id == id)?.LeagueNTName ?? "NOT FOUND";

                NTsByLeague = sortOrder switch
                {
                    NTSortState.NameDesc => NTsByLeague.OrderByDescending(s => s.NTName),
                    NTSortState.CountryAsc => NTsByLeague.OrderBy(s => s.CountryNavigation.CountryNTName),
                    NTSortState.CountryDesc => NTsByLeague.OrderByDescending(s => s.CountryNavigation.CountryNTName),
                    NTSortState.LeagueAsc => NTsByLeague.OrderBy(s => s.LeagueNtNavigation.LeagueNTName),
                    NTSortState.LeagueDesc => NTsByLeague.OrderByDescending(s => s.LeagueNtNavigation.LeagueNTName),
                    NTSortState.YearAsc => NTsByLeague.OrderBy(s => s.YearNavigation.YearName),
                    NTSortState.YearDesc => NTsByLeague.OrderByDescending(s => s.YearNavigation.YearName),
                    NTSortState.PeriodAsc => NTsByLeague.OrderBy(s => s.PeriodNavigation.PeriodName),
                    NTSortState.PeriodDesc => NTsByLeague.OrderByDescending(s => s.PeriodNavigation.PeriodName),
                    NTSortState.NotQualAsc => NTsByLeague.OrderBy(s => s.NotQual),
                    NTSortState.NotQualDesc => NTsByLeague.OrderByDescending(s => s.NotQual),
                    NTSortState.GiggiAsc => NTsByLeague.OrderBy(s => s.Giggi),
                    NTSortState.GiggiDesc => NTsByLeague.OrderByDescending(s => s.Giggi),
                    NTSortState.JbouAsc => NTsByLeague.OrderBy(s => s.Jbou),
                    NTSortState.JbouDesc => NTsByLeague.OrderByDescending(s => s.Jbou),
                    NTSortState.ValAsc => NTsByLeague.OrderBy(s => s.Val),
                    NTSortState.ValDesc => NTsByLeague.OrderByDescending(s => s.Val),
                    _ => NTsByLeague.OrderBy(s => s.NTName),
                };

                var count = await NTsByLeague.CountAsync();
                var items = await NTsByLeague.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                NTSortViewModel sortViewModel = new NTSortViewModel(sortOrder);
                NTsConcreteViewModel ntconViewModel = new NTsConcreteViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, NTs = items };

                return View(ntconViewModel);
            }
            else return NotFound();
        }
    }
}