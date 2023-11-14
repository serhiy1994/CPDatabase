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
        private const int pageSize = 25;
        
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
            
            query = QueryExtensions.Filter(query, id.Value, t => t.LeagueTeamNavigation.Id);
            
            var count = await query.CountAsync(cancellationToken);

            if (count is 0)
                return NotFound();
            
            var leagueTeam = await cpdbcontext.LeagueTeam.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
            ViewBag.CurrentLeagueName = leagueTeam?.Name ?? "NOT FOUND";

            query = query.ApplySorting(teamSortByMapper, sortBy, sortOrder);
            query = query.Paginate(new PaginationData
            {
                Page = page, 
                PageSize = pageSize, 
                TotalCount = count
            });

            var items = await query.ToListAsync(cancellationToken);
                
            var pageViewModel = new PageViewModel(count, page, pageSize);
            var sortViewModel = new TeamSortViewModel(default); // TODO: fix
            var tconViewModel = new TeamsConcreteViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, Teams = items };

            return View(tconViewModel);
        }      
    }
}