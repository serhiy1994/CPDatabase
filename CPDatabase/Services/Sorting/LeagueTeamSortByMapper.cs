using System;
using System.Linq.Expressions;
using CPDatabase.Models;

namespace CPDatabase.Services.Sorting
{
    public class LeagueTeamSortByMapper : ISortByMapper<Team, TeamSortBy>
    {
        public Expression<Func<Team, object>> GetSelector(TeamSortBy input)
        {
            return input switch
            {
                TeamSortBy.Name => x => x.TeamName,

                _ => x => x.TeamName
            };
        }
    }
}