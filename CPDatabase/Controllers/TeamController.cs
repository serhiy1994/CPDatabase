using CPDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CPDatabase.Controllers
{
    public class TeamController : Controller
    {
        CPDBContext cpdbcontext;
        int pageSize = 25;

        public TeamController(CPDBContext context)
        {
            cpdbcontext = context;
        }

        public async Task<IActionResult> View(int? id)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            if (id == null) return RedirectToAction("All");
            Team team = await cpdbcontext.Team.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
            if (team != null) return View(team);
            else return NotFound();
        }

        public async Task<IActionResult> All(int? season, int? halfDecade, int page = 1, TeamSortState sortOrder = TeamSortState.NameAsc)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            IQueryable<Team> teams = cpdbcontext.Team.AsQueryable();

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

            var count = await teams.CountAsync(cancellationToken);
            var items = await teams.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            TeamSortViewModel sortViewModel = new TeamSortViewModel(sortOrder);
            TeamFilterViewModel filterViewModel = new TeamFilterViewModel(cpdbcontext.Season.ToList(), cpdbcontext.HalfDecade.ToList(), season, halfDecade);
            TeamsAllViewModel taViewModel = new TeamsAllViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, FilterViewModel = filterViewModel, Teams = items };
            
            return View(taViewModel);
        }
        
        public async Task<IActionResult> CountryClub(int page = 1, TeamCountryAndNTCountrySortState sortOrder = TeamCountryAndNTCountrySortState.NameAsc)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            IQueryable<CountryClub> countries = cpdbcontext.CountryClub.AsQueryable();

            countries = sortOrder switch
            {
                TeamCountryAndNTCountrySortState.NameDesc => countries.OrderByDescending(s => s.CountryClubName),
                TeamCountryAndNTCountrySortState.HasSubAsc => countries.OrderBy(s => s.HasSub),
                TeamCountryAndNTCountrySortState.HasSubDesc => countries.OrderByDescending(s => s.HasSub),
                TeamCountryAndNTCountrySortState.SubcountryAsc => countries.OrderBy(s => s.Subcountry),
                TeamCountryAndNTCountrySortState.SubcountryDesc => countries.OrderByDescending(s => s.Subcountry),
                TeamCountryAndNTCountrySortState.GiggiAsc => countries.OrderBy(s => s.Giggi),
                TeamCountryAndNTCountrySortState.GiggiDesc => countries.OrderByDescending(s => s.Giggi),
                TeamCountryAndNTCountrySortState.JbouAsc => countries.OrderBy(s => s.Jbou),
                TeamCountryAndNTCountrySortState.JbouDesc => countries.OrderByDescending(s => s.Jbou),
                TeamCountryAndNTCountrySortState.ValAsc => countries.OrderBy(s => s.Val),
                TeamCountryAndNTCountrySortState.ValDesc => countries.OrderByDescending(s => s.Val),
                _ => countries.OrderBy(s => s.CountryClubName),
            };

            var count = await countries.CountAsync(cancellationToken);
            var items = await countries.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            CountrySortViewModel sortViewModel = new CountrySortViewModel(sortOrder);
            TeamsCountryViewModel tcViewModel = new TeamsCountryViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, Countries = items };

            return View(tcViewModel);
        }

        public async Task<IActionResult> Club(int page = 1, TeamClubSortState sortOrder = TeamClubSortState.NameAsc)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            IQueryable<Club> clubs = cpdbcontext.Club.AsQueryable();

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

            var count = await clubs.CountAsync(cancellationToken);
            var items = await clubs.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            ClubSortViewModel sortViewModel = new ClubSortViewModel(sortOrder);
            TeamsClubViewModel tclViewModel = new TeamsClubViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, Clubs = items };

            return View(tclViewModel);
        }

        public async Task<IActionResult> LeagueTeam(int page = 1, TeamLeagueSortState sortOrder = TeamLeagueSortState.NameAsc)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            IQueryable<LeagueTeam> leaguesT = cpdbcontext.LeagueTeam.AsQueryable();

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

            var count = await leaguesT.CountAsync(cancellationToken);
            var items = await leaguesT.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            LeagueTeamSortViewModel sortViewModel = new LeagueTeamSortViewModel(sortOrder);
            TeamsLeagueViewModel tlViewModel = new TeamsLeagueViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, LeaguesT = items };

            return View(tlViewModel);
        }

        public async Task<IActionResult> HalfDecade(int page = 1, TeamHalfDecadeAndSeasonSortState sortOrder = TeamHalfDecadeAndSeasonSortState.NameAsc)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            IQueryable<HalfDecade> halfDecades = cpdbcontext.HalfDecade.AsQueryable();

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

            var count = await halfDecades.CountAsync(cancellationToken);
            var items = await halfDecades.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            HalfDecadeAndSeasonSortViewModel sortViewModel = new HalfDecadeAndSeasonSortViewModel(sortOrder);
            TeamsHalfDecadeViewModel thdViewModel = new TeamsHalfDecadeViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, HalfDecades = items };

            return View(thdViewModel);
        }

        public async Task<IActionResult> Season(int page = 1, TeamHalfDecadeAndSeasonSortState sortOrder = TeamHalfDecadeAndSeasonSortState.NameAsc)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            IQueryable<Season> seasons = cpdbcontext.Season.AsQueryable();

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
            
            var count = await seasons.CountAsync(cancellationToken);
            var items = await seasons.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            HalfDecadeAndSeasonSortViewModel sortViewModel = new HalfDecadeAndSeasonSortViewModel(sortOrder);
            TeamsSeasonViewModel tsViewModel = new TeamsSeasonViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, Seasons = items };

            return View(tsViewModel);
        }

        private string FixTeamName (string teamName)
        {
            teamName.Replace("ä", "a").Replace("Å", "A").Replace("å", "a").Replace("á", "a").Replace("à", "a").Replace("â", "a").Replace("ã", "a");
            teamName.Replace("ç", "c").Replace("ć", "c").Replace("č", "c");
            teamName.Replace("đ", "dj");
            teamName.Replace("ë", "e").Replace("É", "E").Replace("é", "e").Replace("è", "e").Replace("ê", "e").Replace("ę", "e");
            teamName.Replace("ğ", "g");
            teamName.Replace("í", "i").Replace("ì", "i").Replace("ï", "i");
            teamName.Replace("Ł", "L").Replace("ł", "l");
            teamName.Replace("ñ", "n").Replace("ń", "n");
            teamName.Replace("Ö", "O").Replace("ö", "o").Replace("ó", "o").Replace("ő", "o").Replace("ø", "o");
            teamName.Replace("Š", "S").Replace("š", "s").Replace("ş", "s");
            teamName.Replace("ț", "t");
            teamName.Replace("ü", "u").Replace("Ú", "U").Replace("ú", "u");
            teamName.Replace("Ž", "Z").Replace("ž", "z").Replace("ź", "z");
            teamName.Replace("æ", "ae");
            return teamName;
        }
    }
}