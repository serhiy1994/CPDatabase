using CPDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CPDatabase.Controllers
{
    public class HalfDecadeController : Controller
    {
        CPDBContext cpdbcontext;

        public HalfDecadeController(CPDBContext context)
        {
            cpdbcontext = context;
        }

        public async Task<IActionResult> View(int? id, int page = 1, TeamSortState sortOrder = TeamSortState.NameAsc)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            int pageSize = 25;
            if (id == null) return RedirectToAction("HalfDecade", "Team");
            IQueryable<Team> teamsByHalfDecade = cpdbcontext.Team.Where(t => t.HalfDecadeNavigation.Id == id);
            var count = await teamsByHalfDecade.CountAsync(cancellationToken);
            if (count != 0)
            {
                ViewBag.CurrentHalfDecadeName = (await cpdbcontext.HalfDecade.FirstOrDefaultAsync(c => c.Id == id, cancellationToken))?.HalfDecadeName ?? "NOT FOUND";

                teamsByHalfDecade = sortOrder switch
                {
                    TeamSortState.NameDesc => teamsByHalfDecade.OrderByDescending(s => s.TeamName),
                    TeamSortState.FixedNameAsc => teamsByHalfDecade.OrderBy(s => s.FixedTeamName),
                    TeamSortState.FixedNameDesc => teamsByHalfDecade.OrderByDescending(s => s.FixedTeamName),
                    TeamSortState.ClubAsc => teamsByHalfDecade.OrderBy(s => s.ClubNavigation.ClubName),
                    TeamSortState.ClubDesc => teamsByHalfDecade.OrderByDescending(s => s.ClubNavigation.ClubName),
                    TeamSortState.LeagueAsc => teamsByHalfDecade.OrderBy(s => s.LeagueTeamNavigation.Name),
                    TeamSortState.LeagueDesc => teamsByHalfDecade.OrderByDescending(s => s.LeagueTeamNavigation.Name),
                    TeamSortState.HalfDecadeAsc => teamsByHalfDecade.OrderBy(s => s.HalfDecadeNavigation.HalfDecadeName),
                    TeamSortState.HalfDecadeDesc => teamsByHalfDecade.OrderByDescending(s => s.HalfDecadeNavigation.HalfDecadeName),
                    TeamSortState.SeasonAsc => teamsByHalfDecade.OrderBy(s => s.SeasonNavigation.SeasonName),
                    TeamSortState.SeasonDesc => teamsByHalfDecade.OrderByDescending(s => s.SeasonNavigation.SeasonName),
                    TeamSortState.GiggiAsc => teamsByHalfDecade.OrderBy(s => s.Giggi),
                    TeamSortState.GiggiDesc => teamsByHalfDecade.OrderByDescending(s => s.Giggi),
                    TeamSortState.JbouAsc => teamsByHalfDecade.OrderBy(s => s.Jbou),
                    TeamSortState.JbouDesc => teamsByHalfDecade.OrderByDescending(s => s.Jbou),
                    TeamSortState.ValAsc => teamsByHalfDecade.OrderBy(s => s.Val),
                    TeamSortState.ValDesc => teamsByHalfDecade.OrderByDescending(s => s.Val),
                    _ => teamsByHalfDecade.OrderBy(s => s.TeamName),
                };
                                
                var items = await teamsByHalfDecade.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                TeamSortViewModel sortViewModel = new TeamSortViewModel(sortOrder);
                TeamsConcreteViewModel tconViewModel = new TeamsConcreteViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, Teams = items };

                return View(tconViewModel);
            }
            else return NotFound();
        }
    }
}