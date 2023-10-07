using CPDatabase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CPDatabase.Controllers
{
    public class CountryClubController : Controller
    {
        CPDBContext cpdbcontext;

        public CountryClubController(CPDBContext context)
        {
            cpdbcontext = context;
        }

        public async Task<IActionResult> View(int? id, int page = 1, TeamSortState sortOrder = TeamSortState.NameAsc)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            int pageSize = 25;
            if (id == null) return RedirectToAction("CountryClub", "Team");
            IQueryable<Team> teamsByCountry = cpdbcontext.Team.Where(t => t.ClubNavigation.CountryNavigation.Id == id);
            if (teamsByCountry != null)
            {
                ViewBag.CurrentCountryName = cpdbcontext.CountryClub.FirstOrDefault(c => c.Id == id)?.CountryClubName ?? "NOT FOUND";

                teamsByCountry = sortOrder switch
                {
                    TeamSortState.NameDesc => teamsByCountry.OrderByDescending(s => s.TeamName),
                    TeamSortState.FixedNameAsc => teamsByCountry.OrderBy(s => s.FixedTeamName),
                    TeamSortState.FixedNameDesc => teamsByCountry.OrderByDescending(s => s.FixedTeamName),
                    TeamSortState.ClubAsc => teamsByCountry.OrderBy(s => s.ClubNavigation.ClubName),
                    TeamSortState.ClubDesc => teamsByCountry.OrderByDescending(s => s.ClubNavigation.ClubName),
                    TeamSortState.LeagueAsc => teamsByCountry.OrderBy(s => s.LeagueTeamNavigation.Name),
                    TeamSortState.LeagueDesc => teamsByCountry.OrderByDescending(s => s.LeagueTeamNavigation.Name),
                    TeamSortState.HalfDecadeAsc => teamsByCountry.OrderBy(s => s.HalfDecadeNavigation.HalfDecadeName),
                    TeamSortState.HalfDecadeDesc => teamsByCountry.OrderByDescending(s => s.HalfDecadeNavigation.HalfDecadeName),
                    TeamSortState.SeasonAsc => teamsByCountry.OrderBy(s => s.SeasonNavigation.SeasonName),
                    TeamSortState.SeasonDesc => teamsByCountry.OrderByDescending(s => s.SeasonNavigation.SeasonName),
                    TeamSortState.GiggiAsc => teamsByCountry.OrderBy(s => s.Giggi),
                    TeamSortState.GiggiDesc => teamsByCountry.OrderByDescending(s => s.Giggi),
                    TeamSortState.JbouAsc => teamsByCountry.OrderBy(s => s.Jbou),
                    TeamSortState.JbouDesc => teamsByCountry.OrderByDescending(s => s.Jbou),
                    TeamSortState.ValAsc => teamsByCountry.OrderBy(s => s.Val),
                    TeamSortState.ValDesc => teamsByCountry.OrderByDescending(s => s.Val),
                    _ => teamsByCountry.OrderBy(s => s.TeamName),
                };

                var count = await teamsByCountry.CountAsync(cancellationToken);
                var items = await teamsByCountry.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                TeamSortViewModel sortViewModel = new TeamSortViewModel(sortOrder);
                TeamsConcreteViewModel tconViewModel = new TeamsConcreteViewModel { PageViewModel = pageViewModel, SortViewModel = sortViewModel, Teams = items };

                return View(tconViewModel);
            }
            else return NotFound();
        }
    }
}