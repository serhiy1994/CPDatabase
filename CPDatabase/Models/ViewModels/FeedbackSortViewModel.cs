namespace CPDatabase.Models
{
    public class FeedbackSortViewModel
    {
        public FeedbackSortState MessageDateSort { get; private set; }
        public FeedbackSortState UsernameSort { get; private set; }
        public FeedbackSortState EmailSort { get; private set; }
        public FeedbackSortState MessageSort { get; private set; }
        public FeedbackSortState ReplySort { get; private set; }
        public FeedbackSortState ReplyDateSort { get; private set; }
        public FeedbackSortState Current { get; private set; }

        public FeedbackSortViewModel(FeedbackSortState sortOrder)
        {
            MessageDateSort = sortOrder == FeedbackSortState.MessageDateAsc ? FeedbackSortState.MessageDateDesc : FeedbackSortState.MessageDateAsc;
            UsernameSort = sortOrder == FeedbackSortState.UsernameAsc ? FeedbackSortState.UsernameDesc : FeedbackSortState.UsernameAsc;
            EmailSort = sortOrder == FeedbackSortState.EmailAsc ? FeedbackSortState.EmailDesc : FeedbackSortState.EmailAsc;
            MessageSort = sortOrder == FeedbackSortState.MessageAsc ? FeedbackSortState.MessageDesc : FeedbackSortState.MessageAsc;
            ReplySort = sortOrder == FeedbackSortState.ReplyAsc ? FeedbackSortState.ReplyDesc : FeedbackSortState.ReplyAsc;
            ReplyDateSort = sortOrder == FeedbackSortState.ReplyDateAsc ? FeedbackSortState.ReplyDateDesc : FeedbackSortState.ReplyDateAsc;
            Current = sortOrder;
        }
    }
}