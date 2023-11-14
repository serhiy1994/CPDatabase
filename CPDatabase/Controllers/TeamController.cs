using CPDatabase.Models;
using CPDatabase.Services.Sorting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CPDatabase.Controllers
{
    public class TeamController : Controller
    {
        private const int pageSize = 25;

        private readonly CPDBContext cpdbcontext;
        private readonly ISortByMapper<Team, TeamSortBy> teamSortByMapper;
        private readonly ISortByMapper<CountryClub, TeamCountryAndNTCountrySortBy> countrySortByMapper;

        public TeamController(
            CPDBContext context,
            ISortByMapper<Team, TeamSortBy> teamSortByMapper,
            ISortByMapper<CountryClub, TeamCountryAndNTCountrySortBy> countrySortByMapper)
        {
            cpdbcontext = context;
            this.teamSortByMapper = teamSortByMapper;
            this.countrySortByMapper = countrySortByMapper;
        }

        public async Task<IActionResult> View(int? id)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            if (id == null) return RedirectToAction("All");
            Team team = await cpdbcontext.Team.FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
            if (team != null) return View(team);
            else return NotFound();
        }

        public async Task<IActionResult> All(int? season, int? halfDecade, int page = 1, TeamSortBy sortBy = TeamSortBy.Name, SortOrder sortOrder = SortOrder.Asc)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            var query = cpdbcontext.Team.AsQueryable();

            if (season != null && season != -1 && season != 0) query = QueryExtensions.Filter(query, season.Value, t => t.SeasonNavigation.Id);
            else if (season == 0) query = query.Where(p => p.Season == null);

            if (halfDecade != null && halfDecade != -1 && halfDecade != 0) query = QueryExtensions.Filter(query, halfDecade.Value, t => t.HalfDecadeNavigation.Id);
            else if (halfDecade == 0) query = query.Where(p => p.HalfDecade == null);

            var count = await query.CountAsync(cancellationToken);

            query = query.ApplySorting(teamSortByMapper, sortBy, sortOrder);
            query = query.Paginate(new PaginationData
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = count
            });

            var items = await query.ToListAsync(cancellationToken);

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            TeamSortViewModel sortViewModel = new TeamSortViewModel(default); //TODO: fix
            TeamFilterViewModel filterViewModel = new TeamFilterViewModel(cpdbcontext.Season.ToList(), cpdbcontext.HalfDecade.ToList(), season, halfDecade);
            TeamsAllViewModel taViewModel = new TeamsAllViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, FilterViewModel = filterViewModel, Teams = items };
            
            return View(taViewModel);
        }
        
        public async Task<IActionResult> CountryClub(int page = 1, TeamCountryAndNTCountrySortBy sortBy = TeamCountryAndNTCountrySortBy.Name, SortOrder sortOrder=SortOrder.Asc)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            IQueryable<CountryClub> countries = cpdbcontext.CountryClub.AsQueryable();

            var count = await countries.CountAsync(cancellationToken);

            countries = countries.ApplySorting(teamSortByMapper, sortBy, sortOrder);
            countries = countries.Paginate(new PaginationData
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = count
            });
            var items = await countries.ToListAsync(cancellationToken);

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            CountrySortViewModel sortViewModel = new CountrySortViewModel(default); //TODO: fix
            TeamsCountryViewModel tcViewModel = new TeamsCountryViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, Countries = items };

            return View(tcViewModel);
        }
        //TODO: refactor other controllers below
        public async Task<IActionResult> Club(int page = 1, TeamClubSortState sortOrder = TeamClubSortState.NameAsc)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            IQueryable<Club> clubs = cpdbcontext.Club.AsQueryable();

            clubs = QueryExtensions.ApplySort(clubs, sortOrder, new Dictionary<Enum, Expression<Func<Club, object>>>
            {
                { TeamClubSortState.NameDesc, s => s.ClubName },
                { TeamClubSortState.CountryAsc, s => s.CountryNavigation.CountryClubName },
                { TeamClubSortState.CountryDesc, s => s.CountryNavigation.CountryClubName },
                { TeamClubSortState.GiggiAsc, s => s.Giggi },
                { TeamClubSortState.GiggiDesc, s => s.Giggi },
                { TeamClubSortState.JbouAsc, s => s.Jbou },
                { TeamClubSortState.JbouDesc, s => s.Jbou },
                { TeamClubSortState.ValAsc, s => s.Val },
                { TeamClubSortState.ValDesc, s => s.Val },
            });

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

            leaguesT = QueryExtensions.ApplySort(leaguesT, sortOrder, new Dictionary<Enum, Expression<Func<LeagueTeam, object>>>
            {
                { TeamLeagueSortState.NameDesc, s => s.Name },
                { TeamLeagueSortState.HalfDecadeAsc, s => s.HalfDecadeNavigation.HalfDecadeName },
                { TeamLeagueSortState.HalfDecadeDesc, s => s.HalfDecadeNavigation.HalfDecadeName },
                { TeamLeagueSortState.SeasonAsc, s => s.SeasonNavigation.SeasonName },
                { TeamLeagueSortState.SeasonDesc, s => s.SeasonNavigation.SeasonName },
                { TeamLeagueSortState.GiggiAsc, s => s.Giggi },
                { TeamLeagueSortState.GiggiDesc, s => s.Giggi },
                { TeamLeagueSortState.JbouAsc, s => s.Jbou },
                { TeamLeagueSortState.JbouDesc, s => s.Jbou },
                { TeamLeagueSortState.ValAsc, s => s.Val },
                { TeamLeagueSortState.ValDesc, s => s.Val },
            });


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

            halfDecades = QueryExtensions.ApplySort(halfDecades, sortOrder, new Dictionary<Enum, Expression<Func<HalfDecade, object>>>
            {
                { TeamHalfDecadeAndSeasonSortState.NameDesc, s => s.HalfDecadeName },
                { TeamHalfDecadeAndSeasonSortState.GiggiAsc, s => s.Giggi },
                { TeamHalfDecadeAndSeasonSortState.GiggiDesc, s => s.Giggi },
                { TeamHalfDecadeAndSeasonSortState.JbouAsc, s => s.Jbou },
                { TeamHalfDecadeAndSeasonSortState.JbouDesc, s => s.Jbou },
                { TeamHalfDecadeAndSeasonSortState.ValAsc, s => s.Val },
                { TeamHalfDecadeAndSeasonSortState.ValDesc, s => s.Val },
            });

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

            seasons = QueryExtensions.ApplySort(seasons, sortOrder, new Dictionary<Enum, Expression<Func<Season, object>>>
            {
                { TeamHalfDecadeAndSeasonSortState.NameDesc, s => s.SeasonName },
                { TeamHalfDecadeAndSeasonSortState.GiggiAsc, s => s.Giggi },
                { TeamHalfDecadeAndSeasonSortState.GiggiDesc, s => s.Giggi },
                { TeamHalfDecadeAndSeasonSortState.JbouAsc, s => s.Jbou },
                { TeamHalfDecadeAndSeasonSortState.JbouDesc, s => s.Jbou },
                { TeamHalfDecadeAndSeasonSortState.ValAsc, s => s.Val },
                { TeamHalfDecadeAndSeasonSortState.ValDesc, s => s.Val },
            });

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