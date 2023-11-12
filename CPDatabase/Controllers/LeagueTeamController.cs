using CPDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CPDatabase.Services.Sorting;

namespace CPDatabase.Controllers
{
    public class LeagueTeamController : Controller
    {
        private const int DefaultPageSize = 25;
        
        private readonly CPDBContext cpdbcontext;
        private readonly ISortByMapper<Team, TeamSortBy> teamSortByMapper;

        public LeagueTeamController(
            CPDBContext context, 
            ISortByMapper<Team, TeamSortBy> teamSortByMapper)
        {
            cpdbcontext = context;
            this.teamSortByMapper = teamSortByMapper;
        }

        public async Task<IActionResult> View(int? id, int page = 1, TeamSortBy sortBy = TeamSortBy.Name, SortOrder sortOrder = SortOrder.Asc)
        {
            var cancellationToken = HttpContext.RequestAborted;
            var query = cpdbcontext.Team.AsQueryable();
            
            if (id == null) 
                return RedirectToAction("LeagueTeam", "Team");
            
            query = QueryExtensions.FilterByLeagueId(query, id.Value, t => t.LeagueTeamNavigation.Id);
            
            var count = await query.CountAsync(cancellationToken);

            if (count is 0)
                return NotFound();
            
            var leagueTeam = await cpdbcontext.LeagueTeam.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
            ViewBag.CurrentLeagueName = leagueTeam?.Name ?? "NOT FOUND";

            query = query.ApplySorting(teamSortByMapper, sortBy, sortOrder);
            query = query.Paginate(new PaginationData
            {
                Page = page, 
                PageSize = DefaultPageSize, 
                TotalCount = count
            });

            var items = await query.ToListAsync(cancellationToken);
                
            var pageViewModel = new PageViewModel(count, page, DefaultPageSize);
            var sortViewModel = new TeamSortViewModel(default); // TODO: fix
            var tconViewModel = new TeamsConcreteViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, Teams = items };

            return View(tconViewModel);
        }
        
        public async Task<IActionResult> View(int? id, int page = 1, TeamSortState sortOrder = TeamSortState.NameAsc)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            int pageSize = 25;
            var query = cpdbcontext.Team.AsQueryable();
            if (id == null) return RedirectToAction("LeagueTeam", "Team");
            query = QueryExtensions.FilterByLeagueId(query, id.Value, t => t.LeagueTeamNavigation.Id);
            var count = await query.CountAsync(cancellationToken);

            if (count != 0)
            {
                ViewBag.CurrentLeagueName = (await cpdbcontext.LeagueTeam.FirstOrDefaultAsync(c => c.Id == id, cancellationToken))?.Name ?? "NOT FOUND";
                query = query.ApplySort(sortOrder, new Dictionary<Enum, Expression<Func<Team, object>>>
                {
                    { TeamSortState.NameDesc, s => s.TeamName },
                    { TeamSortState.FixedNameAsc, s => s.FixedTeamName },
                    { TeamSortState.FixedNameDesc, s => s.FixedTeamName },
                    { TeamSortState.ClubAsc, s => s.ClubNavigation.ClubName },
                    { TeamSortState.ClubDesc, s => s.ClubNavigation.ClubName },
                    { TeamSortState.LeagueAsc, s => s.LeagueTeamNavigation.Name },
                    { TeamSortState.LeagueDesc, s => s.LeagueTeamNavigation.Name },
                    { TeamSortState.HalfDecadeAsc, s => s.HalfDecadeNavigation.HalfDecadeName },
                    { TeamSortState.HalfDecadeDesc, s => s.HalfDecadeNavigation.HalfDecadeName },
                    { TeamSortState.SeasonAsc, s => s.SeasonNavigation.SeasonName },
                    { TeamSortState.SeasonDesc, s => s.SeasonNavigation.SeasonName },
                    { TeamSortState.GiggiAsc, s => s.Giggi },
                    { TeamSortState.GiggiDesc, s => s.Giggi },
                    { TeamSortState.JbouAsc, s => s.Jbou },
                    { TeamSortState.JbouDesc, s => s.Jbou },
                    { TeamSortState.ValAsc, s => s.Val },
                    { TeamSortState.ValDesc, s => s.Val },
                });

                var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                TeamSortViewModel sortViewModel = new TeamSortViewModel(sortOrder);
                TeamsConcreteViewModel tconViewModel = new TeamsConcreteViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, Teams = items };

                return View(tconViewModel);
            }
            else return NotFound();
        }
    }
}