using MMC.Core.BL;
using MMC.Core.BLImplementation.SPImplementation;
using MMC.Core.Data.Model;
using MMC.Core.Data.SQLServer;
using Omu.ValueInjecter;
using System.Collections.Generic;
using System.Linq;
using BLModel = MMC.Core.BL.Model;
using DLModel = MMC.Core.Data.Model;


namespace MMC.Core.BLImplementation
{
    public class PeerReviewRepository : IPeerReviewRepository
    {
        private readonly BaseRepository<PeerReview> _peerReviewRepo;
        public PeerReviewRepository()
        {
            _peerReviewRepo = new BaseRepository<PeerReview>();
        }

        public int addPeerReview(PeerReview _peerReview)
        {
            return _peerReviewRepo.Add(_peerReview).PeerReviewID;
        }

        public int updatePeerReview(PeerReview _peerReview)
        {
            return _peerReviewRepo.Update(_peerReview);
        }

        public void deletePeerReview(int _peerReviewid)
        {
            _peerReviewRepo.Delete(_peerReviewid);
        }

        public IEnumerable<PeerReview> getPeerReviewsAll()
        {
            return _peerReviewRepo.GetAll();
        }

        public PeerReview getPeerReviewByID(int _id)
        {
            return _peerReviewRepo.GetById(_id);
        }

        public BLModel.Paged.PeerReview getPeerReviewsByName(string SearchText, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _PeerReviewByName = (from peerReview in _MMCDbContext.peerReviews
                                     where (peerReview.PeerReviewName.StartsWith(SearchText))
                                     select new { peerReview.PeerReviewName,peerReview.PeerReviewEmail,peerReview.PeerReviewFax,peerReview.PeerReviewID,peerReview.PeerReviewPhone }).OrderByDescending(pReview => pReview.PeerReviewID).Skip(_skip).Take(_take).ToList();
            return new BLModel.Paged.PeerReview
            {
                PeerReviewDetails = _PeerReviewByName.Select(peerReview => new DLModel.PeerReview().InjectFrom(peerReview)).Cast<DLModel.PeerReview>(),
                TotalCount = (_MMCDbContext.peerReviews.Where(pReview => pReview.PeerReviewName.StartsWith(SearchText)).Count())
                
            };
        }
        public BLModel.PatientAndRequestModel getPatientAndPeerReviewRequestByReferralId(int _referralID)
        {
            SPImpl _spImpl = new SPImpl();
            var varRequestdetails = _spImpl.getRequestByReferralIdForPeerReview(_referralID).ToList();
            return new BLModel.PatientAndRequestModel
            {
                Patients = _spImpl.getPatientByReferralId(_referralID),
                RequestDetail = varRequestdetails
            };
        }

    }
}
