using CPDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            int pageSize = 25;
            if (id == null) return RedirectToAction("Period", "NT");
            IQueryable<NationalTeam> NTsByPeriod = cpdbcontext.NationalTeam.Where(t => t.PeriodNavigation.Id == id);
            if (NTsByPeriod != null)
            {
                ViewBag.CurrentPeriodName = cpdbcontext.Period.FirstOrDefault(c => c.Id == id)?.PeriodName ?? "NOT FOUND";

                NTsByPeriod = sortOrder switch
                {
                    NTSortState.NameDesc => NTsByPeriod.OrderByDescending(s => s.NTName),
                    NTSortState.CountryAsc => NTsByPeriod.OrderBy(s => s.CountryNavigation.CountryNTName),
                    NTSortState.CountryDesc => NTsByPeriod.OrderByDescending(s => s.CountryNavigation.CountryNTName),
                    NTSortState.LeagueAsc => NTsByPeriod.OrderBy(s => s.LeagueNtNavigation.LeagueNTName),
                    NTSortState.LeagueDesc => NTsByPeriod.OrderByDescending(s => s.LeagueNtNavigation.LeagueNTName),
                    NTSortState.YearAsc => NTsByPeriod.OrderBy(s => s.YearNavigation.YearName),
                    NTSortState.YearDesc => NTsByPeriod.OrderByDescending(s => s.YearNavigation.YearName),
                    NTSortState.PeriodAsc => NTsByPeriod.OrderBy(s => s.PeriodNavigation.PeriodName),
                    NTSortState.PeriodDesc => NTsByPeriod.OrderByDescending(s => s.PeriodNavigation.PeriodName),
                    NTSortState.NotQualAsc => NTsByPeriod.OrderBy(s => s.NotQual),
                    NTSortState.NotQualDesc => NTsByPeriod.OrderByDescending(s => s.NotQual),
                    NTSortState.GiggiAsc => NTsByPeriod.OrderBy(s => s.Giggi),
                    NTSortState.GiggiDesc => NTsByPeriod.OrderByDescending(s => s.Giggi),
                    NTSortState.JbouAsc => NTsByPeriod.OrderBy(s => s.Jbou),
                    NTSortState.JbouDesc => NTsByPeriod.OrderByDescending(s => s.Jbou),
                    NTSortState.ValAsc => NTsByPeriod.OrderBy(s => s.Val),
                    NTSortState.ValDesc => NTsByPeriod.OrderByDescending(s => s.Val),
                    _ => NTsByPeriod.OrderBy(s => s.NTName),
                };

                var count = await NTsByPeriod.CountAsync();
                var items = await NTsByPeriod.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                NTSortViewModel sortViewModel = new NTSortViewModel(sortOrder);
                NTsConcreteViewModel ntconViewModel = new NTsConcreteViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, NTs = items };

                return View(ntconViewModel);
            }
            else return NotFound();
        }
    }
}