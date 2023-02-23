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