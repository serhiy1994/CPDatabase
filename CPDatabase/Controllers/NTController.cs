using CPDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> All(int? year, int? period, int page = 1, NTSortState sortOrder = NTSortState.NameAsc)
        {
            int pageSize = 10;
            IQueryable<NationalTeam> nts = cpdbcontext.NationalTeam.Include(x => x.YearNavigation);

            if (year != null && year != -1 && year != 0) nts = nts.Where(p => p.YearNavigation.Id == year);
            else if (year == 0) nts = nts.Where(p => p.Year == null);

            if (period != null && period != -1 && period != 0) nts = nts.Where(p => p.PeriodNavigation.Id == period);
            else if (period == 0) nts = nts.Where(p => p.Period == null);

            nts = sortOrder switch
            {
                NTSortState.NameDesc => nts.OrderByDescending(s => s.NTName),
                NTSortState.CountryAsc => nts.OrderBy(s => s.CountryNavigation.CountryNTName),
                NTSortState.CountryDesc => nts.OrderByDescending(s => s.CountryNavigation.CountryNTName),
                NTSortState.LeagueAsc => nts.OrderBy(s => s.LeagueNtNavigation.LeagueNTName),
                NTSortState.LeagueDesc => nts.OrderByDescending(s => s.LeagueNtNavigation.LeagueNTName),
                NTSortState.YearAsc => nts.OrderBy(s => s.YearNavigation.YearName),
                NTSortState.YearDesc => nts.OrderByDescending(s => s.YearNavigation.YearName),
                NTSortState.PeriodAsc => nts.OrderBy(s => s.PeriodNavigation.PeriodName),
                NTSortState.PeriodDesc => nts.OrderByDescending(s => s.PeriodNavigation.PeriodName),
                NTSortState.NotQualAsc => nts.OrderBy(s => s.NotQual),
                NTSortState.NotQualDesc => nts.OrderByDescending(s => s.NotQual),
                NTSortState.GiggiAsc => nts.OrderBy(s => s.Giggi),
                NTSortState.GiggiDesc => nts.OrderByDescending(s => s.Giggi),
                NTSortState.JbouAsc => nts.OrderBy(s => s.Jbou),
                NTSortState.JbouDesc => nts.OrderByDescending(s => s.Jbou),
                NTSortState.ValAsc => nts.OrderBy(s => s.Val),
                NTSortState.ValDesc => nts.OrderByDescending(s => s.Val),
                _ => nts.OrderBy(s => s.NTName),
            };

            var count = await nts.CountAsync();
            var items = await nts.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            NTSortViewModel sortViewModel = new NTSortViewModel(sortOrder);
            NTFilterViewModel filterViewModel = new NTFilterViewModel(cpdbcontext.Year.ToList(), cpdbcontext.Period.ToList(), year, period);
            NTsAllViewModel ntaViewModel = new NTsAllViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, FilterViewModel = filterViewModel, NTs = items };

            return View(ntaViewModel);
        }
    }
}