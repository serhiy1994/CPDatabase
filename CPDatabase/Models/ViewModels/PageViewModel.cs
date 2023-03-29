using System;

namespace CPDatabase.Models
{
    public class PageViewModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }

        public PageViewModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage { get { return (PageNumber > 1); } }
        public bool HasPreviousPage2 { get { return (PageNumber > 2); } }
        public bool HasPreviousPage3 { get { return (PageNumber > 3); } }
        public bool HasNextPage { get { return (PageNumber < TotalPages); } }
        public bool HasNextPage2 { get { return (PageNumber < TotalPages - 1); } }
        public bool HasNextPage3 { get { return (PageNumber < TotalPages - 2); } }
        public bool TooFarToFirst { get { return (PageNumber - 1 > 3); } }
        public bool TooFarToLast { get { return (TotalPages - PageNumber > 3); } }
    }
}