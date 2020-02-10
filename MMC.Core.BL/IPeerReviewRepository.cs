using MMC.Core.Data.Model;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL
{
   public interface IPeerReviewRepository
    {
        int addPeerReview(PeerReview _peerReview);
        int updatePeerReview(PeerReview _peerReview);
        void deletePeerReview(int _peerReviewId);
        IEnumerable<PeerReview> getPeerReviewsAll();
        PeerReview getPeerReviewByID(int _peerReviewId);
        BLModel.Paged.PeerReview getPeerReviewsByName(string SearchText, int _skip, int _take);
        BLModel.PatientAndRequestModel getPatientAndPeerReviewRequestByReferralId(int _referralID);
    }
}
