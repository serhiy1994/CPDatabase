using CPDatabase.Models;
using Microsoft.AspNetCore.Mvc;
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
        
        public IActionResult CountryClub()
        {
            return View(cpdbcontext.CountryClub.ToList());
        }

        public IActionResult Club()
        {
            return View(cpdbcontext.Club.ToList());
        }

        public IActionResult LeagueTeam()
        {
            return View(cpdbcontext.LeagueTeam.ToList());
        }

        public IActionResult HalfDecade()
        {
            return View(cpdbcontext.HalfDecade.ToList());
        }

        public IActionResult Season()
        {
            return View(cpdbcontext.Season.ToList());
        }
    }
}