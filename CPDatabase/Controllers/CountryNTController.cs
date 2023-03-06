using CPDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CPDatabase.Controllers
{
    public class CountryNTController : Controller
    {
        CPDBContext cpdbcontext;

        public CountryNTController(CPDBContext context)
        {
            cpdbcontext = context;
        }

        public async Task<IActionResult> View(int? id, int page = 1, NTSortState sortOrder = NTSortState.NameAsc)
        {
            int pageSize = 10;
            if (id == null) return RedirectToAction("CountryNT", "NT");
            IQueryable<NationalTeam> NTsByCountry = cpdbcontext.NationalTeam.Where(t => t.CountryNavigation.Id == id);
            if (NTsByCountry != null)
            {
                ViewBag.CurrentCountryNTName = cpdbcontext.CountryNT.FirstOrDefault(c => c.Id == id)?.CountryNTName ?? "NOT FOUND";

                NTsByCountry = sortOrder switch
                {
                    NTSortState.NameDesc => NTsByCountry.OrderByDescending(s => s.NTName),
                    NTSortState.CountryAsc => NTsByCountry.OrderBy(s => s.CountryNavigation.CountryNTName),
                    NTSortState.CountryDesc => NTsByCountry.OrderByDescending(s => s.CountryNavigation.CountryNTName),
                    NTSortState.LeagueAsc => NTsByCountry.OrderBy(s => s.LeagueNtNavigation.LeagueNTName),
                    NTSortState.LeagueDesc => NTsByCountry.OrderByDescending(s => s.LeagueNtNavigation.LeagueNTName),
                    NTSortState.YearAsc => NTsByCountry.OrderBy(s => s.YearNavigation.YearName),
                    NTSortState.YearDesc => NTsByCountry.OrderByDescending(s => s.YearNavigation.YearName),
                    NTSortState.PeriodAsc => NTsByCountry.OrderBy(s => s.PeriodNavigation.PeriodName),
                    NTSortState.PeriodDesc => NTsByCountry.OrderByDescending(s => s.PeriodNavigation.PeriodName),
                    NTSortState.NotQualAsc => NTsByCountry.OrderBy(s => s.NotQual),
                    NTSortState.NotQualDesc => NTsByCountry.OrderByDescending(s => s.NotQual),
                    NTSortState.GiggiAsc => NTsByCountry.OrderBy(s => s.Giggi),
                    NTSortState.GiggiDesc => NTsByCountry.OrderByDescending(s => s.Giggi),
                    NTSortState.JbouAsc => NTsByCountry.OrderBy(s => s.Jbou),
                    NTSortState.JbouDesc => NTsByCountry.OrderByDescending(s => s.Jbou),
                    NTSortState.ValAsc => NTsByCountry.OrderBy(s => s.Val),
                    NTSortState.ValDesc => NTsByCountry.OrderByDescending(s => s.Val),
                    _ => NTsByCountry.OrderBy(s => s.NTName),
                };

                var count = await NTsByCountry.CountAsync();
                var items = await NTsByCountry.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                NTSortViewModel sortViewModel = new NTSortViewModel(sortOrder);
                NTsConcreteViewModel ntconViewModel = new NTsConcreteViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, NTs = items };

                return View(ntconViewModel);
            }
            else return NotFound();
        }
    }
}