namespace CPDatabase.Models
{
    public enum SortOrder
    {
        Asc = 0,
        Desc
    }

    public enum TeamSortBy
    {
        Name,
        FixedName,
        Club,
        League,
        HalfDecade,
        Season,
        Giggi,
        Jbou,
        Val
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

    public enum TeamCountryAndNTCountrySortBy
    {
        Name,
        HasSub,
        Subcountry,
        Giggi,
        Jbou,
        Val
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