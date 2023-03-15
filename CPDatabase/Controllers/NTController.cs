using CPDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CPDatabase.Controllers
{
    public class NTController : Controller
    {
        CPDBContext cpdbcontext;
        int pageSize = 10;

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

        public async Task<IActionResult> CountryNT(int page = 1, TeamCountryAndNTCountrySortState sortOrder = TeamCountryAndNTCountrySortState.NameAsc)
        {
            IQueryable<CountryNT> countriesNT = cpdbcontext.CountryNT.AsQueryable();

            countriesNT = sortOrder switch
            {
                TeamCountryAndNTCountrySortState.NameDesc => countriesNT.OrderByDescending(s => s.CountryNTName),
                TeamCountryAndNTCountrySortState.HasSubAsc => countriesNT.OrderBy(s => s.HasSub),
                TeamCountryAndNTCountrySortState.HasSubDesc => countriesNT.OrderByDescending(s => s.HasSub),
                TeamCountryAndNTCountrySortState.SubcountryAsc => countriesNT.OrderBy(s => s.Subcountry),
                TeamCountryAndNTCountrySortState.SubcountryDesc => countriesNT.OrderByDescending(s => s.Subcountry),
                TeamCountryAndNTCountrySortState.GiggiAsc => countriesNT.OrderBy(s => s.Giggi),
                TeamCountryAndNTCountrySortState.GiggiDesc => countriesNT.OrderByDescending(s => s.Giggi),
                TeamCountryAndNTCountrySortState.JbouAsc => countriesNT.OrderBy(s => s.Jbou),
                TeamCountryAndNTCountrySortState.JbouDesc => countriesNT.OrderByDescending(s => s.Jbou),
                TeamCountryAndNTCountrySortState.ValAsc => countriesNT.OrderBy(s => s.Val),
                TeamCountryAndNTCountrySortState.ValDesc => countriesNT.OrderByDescending(s => s.Val),
                _ => countriesNT.OrderBy(s => s.CountryNTName),
            };

            var count = await countriesNT.CountAsync();
            var items = await countriesNT.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            CountrySortViewModel sortViewModel = new CountrySortViewModel(sortOrder);
            NTsCountryViewModel ntcViewModel = new NTsCountryViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, CountriesNT = items };

            return View(ntcViewModel);
        }

        public async Task<IActionResult> Year(int page = 1, NTPeriodAndYearSortState sortOrder = NTPeriodAndYearSortState.NameAsc)
        {
            IQueryable<Year> years = cpdbcontext.Year.AsQueryable();

            years = sortOrder switch
            {
                NTPeriodAndYearSortState.NameDesc => years.OrderByDescending(s => s.YearName),
                NTPeriodAndYearSortState.GiggiAsc => years.OrderBy(s => s.Giggi),
                NTPeriodAndYearSortState.GiggiDesc => years.OrderByDescending(s => s.Giggi),
                NTPeriodAndYearSortState.JbouAsc => years.OrderBy(s => s.Jbou),
                NTPeriodAndYearSortState.JbouDesc => years.OrderByDescending(s => s.Jbou),
                NTPeriodAndYearSortState.ValAsc => years.OrderBy(s => s.Val),
                NTPeriodAndYearSortState.ValDesc => years.OrderByDescending(s => s.Val),
                _ => years.OrderBy(s => s.YearName),
            };

            var count = await years.CountAsync();
            var items = await years.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PeriodAndYearSortViewModel sortViewModel = new PeriodAndYearSortViewModel(sortOrder);
            NTsYearViewModel ntyViewModel = new NTsYearViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, Years = items };

            return View(ntyViewModel);
        }

        public async Task<IActionResult> Period(int page = 1, NTPeriodAndYearSortState sortOrder = NTPeriodAndYearSortState.NameAsc)
        {
            IQueryable<Period> periods = cpdbcontext.Period.AsQueryable();

            periods = sortOrder switch
            {
                NTPeriodAndYearSortState.NameDesc => periods.OrderByDescending(s => s.PeriodName),
                NTPeriodAndYearSortState.GiggiAsc => periods.OrderBy(s => s.Giggi),
                NTPeriodAndYearSortState.GiggiDesc => periods.OrderByDescending(s => s.Giggi),
                NTPeriodAndYearSortState.JbouAsc => periods.OrderBy(s => s.Jbou),
                NTPeriodAndYearSortState.JbouDesc => periods.OrderByDescending(s => s.Jbou),
                NTPeriodAndYearSortState.ValAsc => periods.OrderBy(s => s.Val),
                NTPeriodAndYearSortState.ValDesc => periods.OrderByDescending(s => s.Val),
                _ => periods.OrderBy(s => s.PeriodName),
            };

            var count = await periods.CountAsync();
            var items = await periods.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PeriodAndYearSortViewModel sortViewModel = new PeriodAndYearSortViewModel(sortOrder);
            NTsPeriodViewModel ntpViewModel = new NTsPeriodViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, Periods = items };

            return View(ntpViewModel);
        }

        public async Task<IActionResult> LeagueNT(int page = 1, NTLeagueSortState sortOrder = NTLeagueSortState.NameAsc)
        {
            IQueryable<LeagueNT> leaguesNT = cpdbcontext.LeagueNT.AsQueryable();

            leaguesNT = sortOrder switch
            {
                NTLeagueSortState.NameDesc => leaguesNT.OrderByDescending(s => s.LeagueNTName),
                NTLeagueSortState.PeriodAsc => leaguesNT.OrderBy(s => s.PeriodNavigation.PeriodName),
                NTLeagueSortState.PeriodDesc => leaguesNT.OrderByDescending(s => s.PeriodNavigation.PeriodName),
                NTLeagueSortState.YearAsc => leaguesNT.OrderBy(s => s.YearNavigation.YearName),
                NTLeagueSortState.YearDesc => leaguesNT.OrderByDescending(s => s.YearNavigation.YearName),
                NTLeagueSortState.GiggiAsc => leaguesNT.OrderBy(s => s.Giggi),
                NTLeagueSortState.GiggiDesc => leaguesNT.OrderByDescending(s => s.Giggi),
                NTLeagueSortState.JbouAsc => leaguesNT.OrderBy(s => s.Jbou),
                NTLeagueSortState.JbouDesc => leaguesNT.OrderByDescending(s => s.Jbou),
                NTLeagueSortState.ValAsc => leaguesNT.OrderBy(s => s.Val),
                NTLeagueSortState.ValDesc => leaguesNT.OrderByDescending(s => s.Val),
                _ => leaguesNT.OrderBy(s => s.LeagueNTName),
            };

            var count = await leaguesNT.CountAsync();
            var items = await leaguesNT.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            LeagueNTSortViewModel sortViewModel = new LeagueNTSortViewModel(sortOrder);
            NTsLeagueViewModel ntlViewModel = new NTsLeagueViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, LeaguesNT = items };

            return View(ntlViewModel);
        }

        public async Task<IActionResult> All(int? year, int? period, int page = 1, NTSortState sortOrder = NTSortState.NameAsc)
        {
            IQueryable<NationalTeam> nts = cpdbcontext.NationalTeam.AsQueryable();

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