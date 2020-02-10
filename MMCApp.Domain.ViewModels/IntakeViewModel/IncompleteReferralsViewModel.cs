using MMCApp.Domain.Models.IntakeModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.IntakeViewModel
{
    public class IncompleteReferralsViewModel
    {
        public IEnumerable<IncompleteReferrals> IncompleteReferralsDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
