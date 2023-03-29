using System.Collections.Generic;

namespace CPDatabase.Models
{
    public class FeedbackViewModel : FeedbackInputModel
    {
        public FeedbackViewModel(string returnUrl)
        {
            ReturnUrl = returnUrl;
        }

        public IEnumerable<FeedbackLog> Feedbacks { get; set; }
        
        public PageViewModel PageViewModel { get; set; }
        public FeedbackSortViewModel SortViewModel { get; set; }
    }
}