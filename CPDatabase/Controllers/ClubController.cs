using CPDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CPDatabase.Controllers
{
    public class ClubController : Controller
    {
        CPDBContext cpdbcontext;

        public ClubController(CPDBContext context)
        {
            cpdbcontext = context;
        }

        public async Task<IActionResult> View(int? id, int page = 1, TeamSortState sortOrder = TeamSortState.NameAsc)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            int pageSize = 25;
            if (id == null) return RedirectToAction("Club", "Team");
            IQueryable<Team> teamsByClub = cpdbcontext.Team.Where(t => t.ClubNavigation.Id == id);
            if (teamsByClub != null)
            {
                ViewBag.CurrentClubName = cpdbcontext.Club.FirstOrDefault(c => c.Id == id)?.ClubName ?? "NOT FOUND";

                teamsByClub = sortOrder switch
                {
                    TeamSortState.NameDesc => teamsByClub.OrderByDescending(s => s.TeamName),
                    TeamSortState.FixedNameAsc => teamsByClub.OrderBy(s => s.FixedTeamName),
                    TeamSortState.FixedNameDesc => teamsByClub.OrderByDescending(s => s.FixedTeamName),
                    TeamSortState.ClubAsc => teamsByClub.OrderBy(s => s.ClubNavigation.ClubName),
                    TeamSortState.ClubDesc => teamsByClub.OrderByDescending(s => s.ClubNavigation.ClubName),
                    TeamSortState.LeagueAsc => teamsByClub.OrderBy(s => s.LeagueTeamNavigation.Name),
                    TeamSortState.LeagueDesc => teamsByClub.OrderByDescending(s => s.LeagueTeamNavigation.Name),
                    TeamSortState.HalfDecadeAsc => teamsByClub.OrderBy(s => s.HalfDecadeNavigation.HalfDecadeName),
                    TeamSortState.HalfDecadeDesc => teamsByClub.OrderByDescending(s => s.HalfDecadeNavigation.HalfDecadeName),
                    TeamSortState.SeasonAsc => teamsByClub.OrderBy(s => s.SeasonNavigation.SeasonName),
                    TeamSortState.SeasonDesc => teamsByClub.OrderByDescending(s => s.SeasonNavigation.SeasonName),
                    TeamSortState.GiggiAsc => teamsByClub.OrderBy(s => s.Giggi),
                    TeamSortState.GiggiDesc => teamsByClub.OrderByDescending(s => s.Giggi),
                    TeamSortState.JbouAsc => teamsByClub.OrderBy(s => s.Jbou),
                    TeamSortState.JbouDesc => teamsByClub.OrderByDescending(s => s.Jbou),
                    TeamSortState.ValAsc => teamsByClub.OrderBy(s => s.Val),
                    TeamSortState.ValDesc => teamsByClub.OrderByDescending(s => s.Val),
                    _ => teamsByClub.OrderBy(s => s.TeamName),
                };

                var count = await teamsByClub.CountAsync(cancellationToken);
                var items = await teamsByClub.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                TeamSortViewModel sortViewModel = new TeamSortViewModel(sortOrder);
                TeamsConcreteViewModel tconViewModel = new TeamsConcreteViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, Teams = items };

                return View(tconViewModel);
            }
            else return NotFound();
        }
    }
}