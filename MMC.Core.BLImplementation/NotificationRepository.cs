using MMC.Core.BL;
using MMC.Core.BLImplementation.SPImplementation;
using MMC.Core.Data.SQLServer;
using Omu.ValueInjecter;
using System.Collections.Generic;
using System.Linq;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BLImplementation
{
    public class NotificationRepository : INotificationRepository
    {
        //private readonly BaseRepository<RFAReferral> _rfaReferralRepo;
        //private readonly BaseRepository<RFARequest> _rfaRequestRepo;
        //private readonly BaseRepository<Patient> _patientRepo;
        //private readonly BaseRepository<PatientClaim> _patientclaimRepo;

        //public NotificationRepository()
        //{
        //    _rfaReferralRepo = new BaseRepository<RFAReferral>();
        //    _rfaRequestRepo = new BaseRepository<RFARequest>();
        //    _patientRepo = new BaseRepository<Patient>();
        //    _patientclaimRepo = new BaseRepository<PatientClaim>();
        //}

        public BLModel.Paged.Notification getAllNotifications(int _processlevel, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _notificationsByName = (from rfa in _MMCDbContext.rfaReferrals
                                        join rfareq in _MMCDbContext.rfaRequests
                                            on rfa.RFAReferralID equals rfareq.RFAReferralID
                                        join patclm in _MMCDbContext.patientClaims
                                            on rfa.PatientClaimID equals patclm.PatientClaimID
                                        join pat in _MMCDbContext.patients
                                            on patclm.PatientID equals pat.PatientID
                                        join rfapl in _MMCDbContext.rfaProcessLevels
                                        on rfa.RFAReferralID equals rfapl.RFAReferralID
                                        where rfapl.ProcessLevel == Global.GlobalConst.ProcessLevel.Notifications
                                        select new { rfa.RFAReferralID, rfareq.RFARequestID, pat.PatFirstName, pat.PatLastName, patclm.PatClaimNumber, rfareq.RFARequestedTreatment }
                                        ).OrderByDescending(rfa => rfa.RFAReferralID).ToList();

            return new BLModel.Paged.Notification
            {
                NotificationDetails = _notificationsByName.Select(rfa => new BLModel.Notification().InjectFrom(rfa)).Cast<BLModel.Notification>().Skip(_skip).Take(_take).ToList(),
                TotalCount = _notificationsByName.Count()// _MMCDbContext.rfaProcessLevels.Where(rfapl => rfapl.ProcessLevel == Global.GlobalConst.ProcessLevel.Notifications).Count()
            };
        }

        public IEnumerable<BLModel.NotificationEmailSend> getAdjandPhyEmailByReferralID(int _referralID)
        {
            SPImpl _spImpl = new SPImpl();
           return _spImpl.getAdjandPhyEmailByReferralID(_referralID).ToList(); 
        }
    }
}
