using MMCApp.Domain.Models.PeerReviewModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.PeerReviewViewModel
{
  public  class PeerReviewSearchViewModel
    {
        public IEnumerable<PeerReview> PeerReviewDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
