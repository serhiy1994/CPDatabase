using CPDatabase.Models;
using CPDatabase.Services.Sorting;
using Microsoft.Extensions.DependencyInjection;

namespace CPDatabase.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSorting(this IServiceCollection services)
        {
            services.AddSingleton<ISortByMapper<Team, TeamSortBy>, LeagueTeamSortByMapper>();

            return services;
        }
    }
}