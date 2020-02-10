using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Core.BL;
using MMC.Core.BLImplementation;
using MMC.Core.Data.Model;

namespace MMC.UnitTest
{
    [TestClass]
   public class PeerReviewTest
    {
           IPeerReviewRepository _PeerReviewRepository;

        public PeerReviewTest()
        {
            _PeerReviewRepository = new PeerReviewRepository();
        }
        [TestMethod]
        public void addPeerReview()
        {
            PeerReview _PeerReview = new PeerReview
            {
                PeerReviewName = "dfgfdgd",
                PeerReviewEmail = "g@g.com",
                PeerReviewPhone = "2135649872",
                PeerReviewFax = "64646456"
               
            };
            var _id = _PeerReviewRepository.addPeerReview(_PeerReview);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void updatePeerReview()
        {
            PeerReview _PeerReview = new PeerReview
            {
                PeerReviewID = 1,
                PeerReviewName = "FirstName2",
                PeerReviewEmail = "vLastName2",
                PeerReviewPhone = "2135649872",
                PeerReviewFax = "563535345"
            };
            var _id = _PeerReviewRepository.updatePeerReview(_PeerReview);
            Assert.IsTrue(_id > 0, "failed");
        }
        [TestMethod]
        public void deletePeerReview()
        {
            _PeerReviewRepository.deletePeerReview(1);
        }
        [TestMethod]
        public void getAllPeerReviews()
        {
            var PeerReviews = _PeerReviewRepository.getPeerReviewsAll();
            Assert.IsTrue(PeerReviews != null, "failed");
        }
        [TestMethod]
        public void getPeerReviewByID()
        {
            var PeerReviews = _PeerReviewRepository.getPeerReviewByID(48);
            Assert.IsTrue(PeerReviews != null, "failed");
        }

        [TestMethod]
        public void getAllPeerReviewsName()
        {
            string SearchText = "f";
            int _skip = 0;
            int _take = 10;
            var PeerReviews = _PeerReviewRepository.getPeerReviewsByName(SearchText, _skip, _take);
            Assert.IsTrue(PeerReviews != null, "failed");
        }
    }
}
