using CPDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPDatabase.Controllers
{
    public class SeasonController : Controller
    {
        CPDBContext cpdbcontext;

        public SeasonController(CPDBContext context)
        {
            cpdbcontext = context;
        }

        public async Task<IActionResult> View(int? id, int page = 1, TeamSortState sortOrder = TeamSortState.NameAsc)
        {
            int pageSize = 10;
            if (id == null) return RedirectToAction("Season", "Team");
            IQueryable<Team> teamsBySeason = cpdbcontext.Team.Where(t => t.SeasonNavigation.Id == id);
            if (teamsBySeason != null)
            {
                ViewBag.CurrentSeasonName = cpdbcontext.Season.FirstOrDefault(c => c.Id == id)?.SeasonName ?? "NOT FOUND";

                teamsBySeason = sortOrder switch
                {
                    TeamSortState.NameDesc => teamsBySeason.OrderByDescending(s => s.TeamName),
                    TeamSortState.FixedNameAsc => teamsBySeason.OrderBy(s => s.FixedTeamName),
                    TeamSortState.FixedNameDesc => teamsBySeason.OrderByDescending(s => s.FixedTeamName),
                    TeamSortState.ClubAsc => teamsBySeason.OrderBy(s => s.ClubNavigation.ClubName),
                    TeamSortState.ClubDesc => teamsBySeason.OrderByDescending(s => s.ClubNavigation.ClubName),
                    TeamSortState.LeagueAsc => teamsBySeason.OrderBy(s => s.LeagueTeamNavigation.Name),
                    TeamSortState.LeagueDesc => teamsBySeason.OrderByDescending(s => s.LeagueTeamNavigation.Name),
                    TeamSortState.HalfDecadeAsc => teamsBySeason.OrderBy(s => s.HalfDecadeNavigation.HalfDecadeName),
                    TeamSortState.HalfDecadeDesc => teamsBySeason.OrderByDescending(s => s.HalfDecadeNavigation.HalfDecadeName),
                    TeamSortState.SeasonAsc => teamsBySeason.OrderBy(s => s.SeasonNavigation.SeasonName),
                    TeamSortState.SeasonDesc => teamsBySeason.OrderByDescending(s => s.SeasonNavigation.SeasonName),
                    TeamSortState.GiggiAsc => teamsBySeason.OrderBy(s => s.Giggi),
                    TeamSortState.GiggiDesc => teamsBySeason.OrderByDescending(s => s.Giggi),
                    TeamSortState.JbouAsc => teamsBySeason.OrderBy(s => s.Jbou),
                    TeamSortState.JbouDesc => teamsBySeason.OrderByDescending(s => s.Jbou),
                    TeamSortState.ValAsc => teamsBySeason.OrderBy(s => s.Val),
                    TeamSortState.ValDesc => teamsBySeason.OrderByDescending(s => s.Val),
                    _ => teamsBySeason.OrderBy(s => s.TeamName),
                };

                var count = await teamsBySeason.CountAsync();
                var items = await teamsBySeason.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                TeamSortViewModel sortViewModel = new TeamSortViewModel(sortOrder);
                TeamsConcreteViewModel tconViewModel = new TeamsConcreteViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, Teams = items };

                return View(tconViewModel);
            }
            else return NotFound();
        }
    }
}