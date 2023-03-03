using CPDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CPDatabase.Controllers
{
    public class TeamController : Controller
    {
        CPDBContext cpdbcontext;

        public TeamController(CPDBContext context)
        {
            cpdbcontext = context;
        }

        public IActionResult View(int? id)
        {
            if (id == null) return RedirectToAction("All");
            Team team = cpdbcontext.Team.FirstOrDefault(t => t.Id == id);
            if (team != null) return View(team);
            else return NotFound();
        }

        public async Task<IActionResult> All(int? season, int? halfDecade, int page = 1, TeamSortState sortOrder = TeamSortState.NameAsc)
        {
            int pageSize = 10;
            IQueryable<Team> teams = cpdbcontext.Team.Include(x => x.SeasonNavigation);

            if (season != null && season != -1 && season != 0) teams = teams.Where(p => p.SeasonNavigation.Id == season);
            else if (season == 0) teams = teams.Where(p => p.Season == null);

            if (halfDecade != null && halfDecade != -1 && halfDecade != 0) teams = teams.Where(p => p.HalfDecadeNavigation.Id == halfDecade);
            else if (halfDecade == 0) teams = teams.Where(p => p.HalfDecade == null);

            teams = sortOrder switch
            {
                TeamSortState.NameDesc => teams.OrderByDescending(s => s.TeamName),
                TeamSortState.FixedNameAsc => teams.OrderBy(s => s.FixedTeamName),
                TeamSortState.FixedNameDesc => teams.OrderByDescending(s => s.FixedTeamName),
                TeamSortState.ClubAsc => teams.OrderBy(s => s.ClubNavigation.ClubName),
                TeamSortState.ClubDesc => teams.OrderByDescending(s => s.ClubNavigation.ClubName),
                TeamSortState.LeagueAsc => teams.OrderBy(s => s.LeagueTeamNavigation.Name),
                TeamSortState.LeagueDesc => teams.OrderByDescending(s => s.LeagueTeamNavigation.Name),
                TeamSortState.HalfDecadeAsc => teams.OrderBy(s => s.HalfDecadeNavigation.HalfDecadeName),
                TeamSortState.HalfDecadeDesc => teams.OrderByDescending(s => s.HalfDecadeNavigation.HalfDecadeName),
                TeamSortState.SeasonAsc => teams.OrderBy(s => s.SeasonNavigation.SeasonName),
                TeamSortState.SeasonDesc => teams.OrderByDescending(s => s.SeasonNavigation.SeasonName),
                TeamSortState.GiggiAsc => teams.OrderBy(s => s.Giggi),
                TeamSortState.GiggiDesc => teams.OrderByDescending(s => s.Giggi),
                TeamSortState.JbouAsc => teams.OrderBy(s => s.Jbou),
                TeamSortState.JbouDesc => teams.OrderByDescending(s => s.Jbou),
                TeamSortState.ValAsc => teams.OrderBy(s => s.Val),
                TeamSortState.ValDesc => teams.OrderByDescending(s => s.Val),
                _ => teams.OrderBy(s => s.TeamName),
            };

            var count = await teams.CountAsync();
            var items = await teams.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            TeamSortViewModel sortViewModel = new TeamSortViewModel(sortOrder);
            TeamFilterViewModel filterViewModel = new TeamFilterViewModel(cpdbcontext.Season.ToList(), cpdbcontext.HalfDecade.ToList(), season, halfDecade);
            TeamsAllViewModel taViewModel = new TeamsAllViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, FilterViewModel = filterViewModel, Teams = items };
            
            return View(taViewModel);
        }
        
        public async Task<IActionResult> CountryClub(int page = 1, TeamCountrySortState sortOrder = TeamCountrySortState.NameAsc)
        {
            int pageSize = 10;
            IQueryable<CountryClub> countries = cpdbcontext.CountryClub.Include(x=>x.Club);

            countries = sortOrder switch
            {
                TeamCountrySortState.NameDesc => countries.OrderByDescending(s => s.CountryClubName),
                TeamCountrySortState.HasSubAsc => countries.OrderBy(s => s.HasSub),
                TeamCountrySortState.HasSubDesc => countries.OrderByDescending(s => s.HasSub),
                TeamCountrySortState.SubcountryAsc => countries.OrderBy(s => s.Subcountry),
                TeamCountrySortState.SubcountryDesc => countries.OrderByDescending(s => s.Subcountry),
                TeamCountrySortState.GiggiAsc => countries.OrderBy(s => s.Giggi),
                TeamCountrySortState.GiggiDesc => countries.OrderByDescending(s => s.Giggi),
                TeamCountrySortState.JbouAsc => countries.OrderBy(s => s.Jbou),
                TeamCountrySortState.JbouDesc => countries.OrderByDescending(s => s.Jbou),
                TeamCountrySortState.ValAsc => countries.OrderBy(s => s.Val),
                TeamCountrySortState.ValDesc => countries.OrderByDescending(s => s.Val),
                _ => countries.OrderBy(s => s.CountryClubName),
            };

            var count = await countries.CountAsync();
            var items = await countries.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            CountrySortViewModel sortViewModel = new CountrySortViewModel(sortOrder);
            TeamsCountryViewModel tcViewModel = new TeamsCountryViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, Countries = items };

            return View(tcViewModel);
        }

        public async Task<IActionResult> Club(int page = 1, TeamClubSortState sortOrder = TeamClubSortState.NameAsc)
        {
            int pageSize = 10;
            IQueryable<Club> clubs = cpdbcontext.Club.Include(x => x.CountryNavigation);

            clubs = sortOrder switch
            {
                TeamClubSortState.NameDesc => clubs.OrderByDescending(s => s.ClubName),
                TeamClubSortState.CountryAsc => clubs.OrderBy(s => s.CountryNavigation.CountryClubName),
                TeamClubSortState.CountryDesc => clubs.OrderByDescending(s => s.CountryNavigation.CountryClubName),
                TeamClubSortState.GiggiAsc => clubs.OrderBy(s => s.Giggi),
                TeamClubSortState.GiggiDesc => clubs.OrderByDescending(s => s.Giggi),
                TeamClubSortState.JbouAsc => clubs.OrderBy(s => s.Jbou),
                TeamClubSortState.JbouDesc => clubs.OrderByDescending(s => s.Jbou),
                TeamClubSortState.ValAsc => clubs.OrderBy(s => s.Val),
                TeamClubSortState.ValDesc => clubs.OrderByDescending(s => s.Val),
                _ => clubs.OrderBy(s => s.ClubName),
            };

            var count = await clubs.CountAsync();
            var items = await clubs.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            ClubSortViewModel sortViewModel = new ClubSortViewModel(sortOrder);
            TeamsClubViewModel tclViewModel = new TeamsClubViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, Clubs = items };

            return View(tclViewModel);
        }

        public async Task<IActionResult> LeagueTeam(int page = 1, TeamLeagueSortState sortOrder = TeamLeagueSortState.NameAsc)
        {
            int pageSize = 10;
            IQueryable<LeagueTeam> leaguesT = cpdbcontext.LeagueTeam.Include(x => x.SeasonNavigation);

            leaguesT = sortOrder switch
            {
                TeamLeagueSortState.NameDesc => leaguesT.OrderByDescending(s => s.Name),
                TeamLeagueSortState.HalfDecadeAsc => leaguesT.OrderBy(s => s.HalfDecadeNavigation.HalfDecadeName),
                TeamLeagueSortState.HalfDecadeDesc => leaguesT.OrderByDescending(s => s.HalfDecadeNavigation.HalfDecadeName),
                TeamLeagueSortState.SeasonAsc => leaguesT.OrderBy(s => s.SeasonNavigation.SeasonName),
                TeamLeagueSortState.SeasonDesc => leaguesT.OrderByDescending(s => s.SeasonNavigation.SeasonName),
                TeamLeagueSortState.GiggiAsc => leaguesT.OrderBy(s => s.Giggi),
                TeamLeagueSortState.GiggiDesc => leaguesT.OrderByDescending(s => s.Giggi),
                TeamLeagueSortState.JbouAsc => leaguesT.OrderBy(s => s.Jbou),
                TeamLeagueSortState.JbouDesc => leaguesT.OrderByDescending(s => s.Jbou),
                TeamLeagueSortState.ValAsc => leaguesT.OrderBy(s => s.Val),
                TeamLeagueSortState.ValDesc => leaguesT.OrderByDescending(s => s.Val),
                _ => leaguesT.OrderBy(s => s.Name),
            };

            var count = await leaguesT.CountAsync();
            var items = await leaguesT.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            LeagueTeamSortViewModel sortViewModel = new LeagueTeamSortViewModel(sortOrder);
            TeamsLeagueViewModel tlViewModel = new TeamsLeagueViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, LeaguesT = items };

            return View(tlViewModel);
        }

        public async Task<IActionResult> HalfDecade(int page = 1, TeamHalfDecadeAndSeasonSortState sortOrder = TeamHalfDecadeAndSeasonSortState.NameAsc)
        {
            int pageSize = 10;
            IQueryable<HalfDecade> halfDecades = cpdbcontext.HalfDecade.Include(x => x.LeagueTeam);

            halfDecades = sortOrder switch
            {
                TeamHalfDecadeAndSeasonSortState.NameDesc => halfDecades.OrderByDescending(s => s.HalfDecadeName),
                TeamHalfDecadeAndSeasonSortState.GiggiAsc => halfDecades.OrderBy(s => s.Giggi),
                TeamHalfDecadeAndSeasonSortState.GiggiDesc => halfDecades.OrderByDescending(s => s.Giggi),
                TeamHalfDecadeAndSeasonSortState.JbouAsc => halfDecades.OrderBy(s => s.Jbou),
                TeamHalfDecadeAndSeasonSortState.JbouDesc => halfDecades.OrderByDescending(s => s.Jbou),
                TeamHalfDecadeAndSeasonSortState.ValAsc => halfDecades.OrderBy(s => s.Val),
                TeamHalfDecadeAndSeasonSortState.ValDesc => halfDecades.OrderByDescending(s => s.Val),
                _ => halfDecades.OrderBy(s => s.HalfDecadeName),
            };

            var count = await halfDecades.CountAsync();
            var items = await halfDecades.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            HalfDecadeAndSeasonSortViewModel sortViewModel = new HalfDecadeAndSeasonSortViewModel(sortOrder);
            TeamsHalfDecadeViewModel thdViewModel = new TeamsHalfDecadeViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, HalfDecades = items };

            return View(thdViewModel);
        }

        public async Task<IActionResult> Season(int page = 1, TeamHalfDecadeAndSeasonSortState sortOrder = TeamHalfDecadeAndSeasonSortState.NameAsc)
        {
            int pageSize = 10;
            IQueryable<Season> seasons = cpdbcontext.Season.Include(x => x.LeagueTeam);

            seasons = sortOrder switch
            {
                TeamHalfDecadeAndSeasonSortState.NameDesc => seasons.OrderByDescending(s => s.SeasonName),
                TeamHalfDecadeAndSeasonSortState.GiggiAsc => seasons.OrderBy(s => s.Giggi),
                TeamHalfDecadeAndSeasonSortState.GiggiDesc => seasons.OrderByDescending(s => s.Giggi),
                TeamHalfDecadeAndSeasonSortState.JbouAsc => seasons.OrderBy(s => s.Jbou),
                TeamHalfDecadeAndSeasonSortState.JbouDesc => seasons.OrderByDescending(s => s.Jbou),
                TeamHalfDecadeAndSeasonSortState.ValAsc => seasons.OrderBy(s => s.Val),
                TeamHalfDecadeAndSeasonSortState.ValDesc => seasons.OrderByDescending(s => s.Val),
                _ => seasons.OrderBy(s => s.SeasonName),
            };
            
            var count = await seasons.CountAsync();
            var items = await seasons.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            HalfDecadeAndSeasonSortViewModel sortViewModel = new HalfDecadeAndSeasonSortViewModel(sortOrder);
            TeamsSeasonViewModel tsViewModel = new TeamsSeasonViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, Seasons = items };

            return View(tsViewModel);
        }
    }
}