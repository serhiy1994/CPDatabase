using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPDatabase.Models
{
    public enum TeamSortState
    {
        NameAsc,
        NameDesc,
        FixedNameAsc,
        FixedNameDesc,
        ClubAsc,
        ClubDesc,
        LeagueAsc,
        LeagueDesc,
        HalfDecadeAsc,
        HalfDecadeDesc,
        SeasonAsc,
        SeasonDesc,
        GiggiAsc,
        GiggiDesc,
        JbouAsc,
        JbouDesc,
        ValAsc,
        ValDesc
    }

    public enum TeamClubSortState
    {
        NameAsc,
        NameDesc,
        CountryAsc,
        CountryDesc,        
        GiggiAsc,
        GiggiDesc,
        JbouAsc,
        JbouDesc,
        ValAsc,
        ValDesc
    }

    public enum TeamCountrySortState
    {
        NameAsc,
        NameDesc,
        HasSubAsc,
        HasSubDesc,
        SubcountryAsc,
        SubcountryDesc,
        GiggiAsc,
        GiggiDesc,
        JbouAsc,
        JbouDesc,
        ValAsc,
        ValDesc
    }

    public enum TeamLeagueSortState
    {
        NameAsc,
        NameDesc,
        HalfDecadeAsc,
        HalfDecadeDesc,
        SeasonAsc,
        SeasonDesc,
        GiggiAsc,
        GiggiDesc,
        JbouAsc,
        JbouDesc,
        ValAsc,
        ValDesc
    }

    public enum TeamHalfDecadeAndSeasonSortState
    {
        NameAsc,
        NameDesc,
        GiggiAsc,
        GiggiDesc,
        JbouAsc,
        JbouDesc,
        ValAsc,
        ValDesc
    }

    public enum NTSortState
    {
        NameAsc,
        NameDesc,
        CountryAsc,
        CountryDesc,
        LeagueAsc,
        LeagueDesc,
        YearAsc,
        YearDesc,
        PeriodAsc,
        PeriodDesc,
        NotQualAsc,
        NotQualDesc,
        GiggiAsc,
        GiggiDesc,
        JbouAsc,
        JbouDesc,
        ValAsc,
        ValDesc
    }
}