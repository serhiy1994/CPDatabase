using CPDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CPDatabase.Controllers
{
    public class LeagueTeamController : Controller
    {
        CPDBContext cpdbcontext;

        public LeagueTeamController(CPDBContext context)
        {
            cpdbcontext = context;
        }

        public async Task<IActionResult> View(int? id, int page = 1, TeamSortState sortOrder = TeamSortState.NameAsc)
        {
            int pageSize = 25;
            if (id == null) return RedirectToAction("LeagueTeam", "Team");
            IQueryable<Team> teamsByLeague = cpdbcontext.Team.Where(t => t.LeagueTeamNavigation.Id == id);
            if (teamsByLeague != null)
            {
                ViewBag.CurrentLeagueName = cpdbcontext.LeagueTeam.FirstOrDefault(c => c.Id == id)?.Name ?? "NOT FOUND";

                teamsByLeague = sortOrder switch
                {
                    TeamSortState.NameDesc => teamsByLeague.OrderByDescending(s => s.TeamName),
                    TeamSortState.FixedNameAsc => teamsByLeague.OrderBy(s => s.FixedTeamName),
                    TeamSortState.FixedNameDesc => teamsByLeague.OrderByDescending(s => s.FixedTeamName),
                    TeamSortState.ClubAsc => teamsByLeague.OrderBy(s => s.ClubNavigation.ClubName),
                    TeamSortState.ClubDesc => teamsByLeague.OrderByDescending(s => s.ClubNavigation.ClubName),
                    TeamSortState.LeagueAsc => teamsByLeague.OrderBy(s => s.LeagueTeamNavigation.Name),
                    TeamSortState.LeagueDesc => teamsByLeague.OrderByDescending(s => s.LeagueTeamNavigation.Name),
                    TeamSortState.HalfDecadeAsc => teamsByLeague.OrderBy(s => s.HalfDecadeNavigation.HalfDecadeName),
                    TeamSortState.HalfDecadeDesc => teamsByLeague.OrderByDescending(s => s.HalfDecadeNavigation.HalfDecadeName),
                    TeamSortState.SeasonAsc => teamsByLeague.OrderBy(s => s.SeasonNavigation.SeasonName),
                    TeamSortState.SeasonDesc => teamsByLeague.OrderByDescending(s => s.SeasonNavigation.SeasonName),
                    TeamSortState.GiggiAsc => teamsByLeague.OrderBy(s => s.Giggi),
                    TeamSortState.GiggiDesc => teamsByLeague.OrderByDescending(s => s.Giggi),
                    TeamSortState.JbouAsc => teamsByLeague.OrderBy(s => s.Jbou),
                    TeamSortState.JbouDesc => teamsByLeague.OrderByDescending(s => s.Jbou),
                    TeamSortState.ValAsc => teamsByLeague.OrderBy(s => s.Val),
                    TeamSortState.ValDesc => teamsByLeague.OrderByDescending(s => s.Val),
                    _ => teamsByLeague.OrderBy(s => s.TeamName),
                };

                var count = await teamsByLeague.CountAsync();
                var items = await teamsByLeague.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                TeamSortViewModel sortViewModel = new TeamSortViewModel(sortOrder);
                TeamsConcreteViewModel tconViewModel = new TeamsConcreteViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, Teams = items };

                return View(tconViewModel);
            }
            else return NotFound();
        }
    }
}