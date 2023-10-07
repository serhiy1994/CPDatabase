using CPDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CPDatabase.Controllers
{
    public class YearController : Controller
    {
        CPDBContext cpdbcontext;

        public YearController(CPDBContext context)
        {
            cpdbcontext = context;
        }

        public async Task<IActionResult> View(int? id, int page = 1, NTSortState sortOrder = NTSortState.NameAsc)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            int pageSize = 25;
            if (id == null) return RedirectToAction("Year", "NT");
            IQueryable<NationalTeam> NTsByYear = cpdbcontext.NationalTeam.Where(t => t.YearNavigation.Id == id);
            if (NTsByYear.Count() != 0)
            {
                ViewBag.CurrentYearName = cpdbcontext.Year.FirstOrDefault(c => c.Id == id)?.YearName ?? "NOT FOUND";

                NTsByYear = sortOrder switch
                {
                    NTSortState.NameDesc => NTsByYear.OrderByDescending(s => s.NTName),
                    NTSortState.CountryAsc => NTsByYear.OrderBy(s => s.CountryNavigation.CountryNTName),
                    NTSortState.CountryDesc => NTsByYear.OrderByDescending(s => s.CountryNavigation.CountryNTName),
                    NTSortState.LeagueAsc => NTsByYear.OrderBy(s => s.LeagueNtNavigation.LeagueNTName),
                    NTSortState.LeagueDesc => NTsByYear.OrderByDescending(s => s.LeagueNtNavigation.LeagueNTName),
                    NTSortState.YearAsc => NTsByYear.OrderBy(s => s.YearNavigation.YearName),
                    NTSortState.YearDesc => NTsByYear.OrderByDescending(s => s.YearNavigation.YearName),
                    NTSortState.PeriodAsc => NTsByYear.OrderBy(s => s.PeriodNavigation.PeriodName),
                    NTSortState.PeriodDesc => NTsByYear.OrderByDescending(s => s.PeriodNavigation.PeriodName),
                    NTSortState.NotQualAsc => NTsByYear.OrderBy(s => s.NotQual),
                    NTSortState.NotQualDesc => NTsByYear.OrderByDescending(s => s.NotQual),
                    NTSortState.GiggiAsc => NTsByYear.OrderBy(s => s.Giggi),
                    NTSortState.GiggiDesc => NTsByYear.OrderByDescending(s => s.Giggi),
                    NTSortState.JbouAsc => NTsByYear.OrderBy(s => s.Jbou),
                    NTSortState.JbouDesc => NTsByYear.OrderByDescending(s => s.Jbou),
                    NTSortState.ValAsc => NTsByYear.OrderBy(s => s.Val),
                    NTSortState.ValDesc => NTsByYear.OrderByDescending(s => s.Val),
                    _ => NTsByYear.OrderBy(s => s.NTName),
                };

                var count = await NTsByYear.CountAsync(cancellationToken);
                var items = await NTsByYear.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                NTSortViewModel sortViewModel = new NTSortViewModel(sortOrder);
                NTsConcreteViewModel ntconViewModel = new NTsConcreteViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, NTs = items };

                return View(ntconViewModel);
            }
            else return NotFound();
        }
    }
}