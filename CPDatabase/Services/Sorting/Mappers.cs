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
                TeamSortBy.FixedName => x => x.FixedTeamName,
                TeamSortBy.Club => x => x.Club,
                TeamSortBy.League => x => x.LeagueTeam,
                TeamSortBy.HalfDecade => x => x.HalfDecade,
                TeamSortBy.Season => x => x.Season,
                TeamSortBy.Giggi => x => x.Giggi,
                TeamSortBy.Jbou => x => x.Jbou,
                TeamSortBy.Val => x => x.Val,
                _ => x => x.TeamName
            };
        }
    }

    public class CountriesClubSortByMapper : ISortByMapper<CountryClub, TeamCountryAndNTCountrySortBy>
    {
        public Expression<Func<CountryClub, object>> GetSelector(TeamCountryAndNTCountrySortBy input)
        {
            return input switch
            {
                TeamCountryAndNTCountrySortBy.Name => x => x.CountryClubName,
                TeamCountryAndNTCountrySortBy.HasSub => x => x.HasSub,
                TeamCountryAndNTCountrySortBy.Subcountry => x => x.Subcountry,
                TeamCountryAndNTCountrySortBy.Giggi => x => x.Giggi,
                TeamCountryAndNTCountrySortBy.Jbou => x => x.Jbou,
                TeamCountryAndNTCountrySortBy.Val => x => x.Val,
                _ => x => x.CountryClubName
            };
        }
    }
}