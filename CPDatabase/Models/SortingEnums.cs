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

    public enum TeamCountryAndNTCountrySortState
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

    public enum NTLeagueSortState
    {
        NameAsc,
        NameDesc,
        PeriodAsc,
        PeriodDesc,
        YearAsc,
        YearDesc,
        GiggiAsc,
        GiggiDesc,
        JbouAsc,
        JbouDesc,
        ValAsc,
        ValDesc
    }

    public enum NTPeriodAndYearSortState
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

    public enum FeedbackSortState
    {
        MessageDateAsc,
        MessageDateDesc,
        UsernameAsc,
        UsernameDesc,
        EmailAsc,
        EmailDesc,
        MessageAsc,
        MessageDesc,
        ReplyAsc,
        ReplyDesc,
        ReplyDateAsc,
        ReplyDateDesc
    }
}