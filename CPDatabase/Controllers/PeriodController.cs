using CPDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CPDatabase.Controllers
{
    public class PeriodController : Controller
    {
        CPDBContext cpdbcontext;

        public PeriodController(CPDBContext context)
        {
            cpdbcontext = context;
        }

        public async Task<IActionResult> View(int? id, int page = 1, NTSortState sortOrder = NTSortState.NameAsc)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            int pageSize = 25;
            if (id == null) return RedirectToAction("Period", "NT");
            IQueryable<NationalTeam> NTsByPeriod = cpdbcontext.NationalTeam.Where(t => t.PeriodNavigation.Id == id);
            var count = await NTsByPeriod.CountAsync(cancellationToken);
            if (count != 0)
            {
                ViewBag.CurrentPeriodName = (await cpdbcontext.Period.FirstOrDefaultAsync(c => c.Id == id, cancellationToken))?.PeriodName ?? "NOT FOUND";

                NTsByPeriod = QueryExtensions.ApplySort(NTsByPeriod, sortOrder, new Dictionary<Enum, Expression<Func<NationalTeam, object>>>
                {
                    { NTSortState.NameDesc, s => s.NTName },
                    { NTSortState.CountryAsc, s => s.CountryNavigation.CountryNTName },
                    { NTSortState.CountryDesc, s => s.CountryNavigation.CountryNTName },
                    { NTSortState.LeagueAsc, s => s.LeagueNtNavigation.LeagueNTName },
                    { NTSortState.LeagueDesc, s => s.LeagueNtNavigation.LeagueNTName },
                    { NTSortState.YearAsc, s => s.YearNavigation.YearName },
                    { NTSortState.YearDesc, s => s.YearNavigation.YearName },
                    { NTSortState.PeriodAsc, s => s.PeriodNavigation.PeriodName },
                    { NTSortState.PeriodDesc, s => s.PeriodNavigation.PeriodName },
                    { NTSortState.NotQualAsc, s => s.NotQual },
                    { NTSortState.NotQualDesc, s => s.NotQual },
                    { NTSortState.GiggiAsc, s => s.Giggi },
                    { NTSortState.GiggiDesc, s => s.Giggi },
                    { NTSortState.JbouAsc, s => s.Jbou },
                    { NTSortState.JbouDesc, s => s.Jbou },
                    { NTSortState.ValAsc, s => s.Val },
                    { NTSortState.ValDesc, s => s.Val },
                });

                var items = await NTsByPeriod.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                NTSortViewModel sortViewModel = new NTSortViewModel(sortOrder);
                NTsConcreteViewModel ntconViewModel = new NTsConcreteViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, NTs = items };

                return View(ntconViewModel);
            }
            else return NotFound();
        }
    }
}