using MMC.Core.BL;
using MMC.Core.Data.Model;
using MMC.Core.Data.SQLServer;
using Omu.ValueInjecter;
using System.Collections.Generic;
using System.Linq;
using BLModel = MMC.Core.BL.Model;
namespace MMC.Core.BLImplementation
{
    public class CaseManagerRepository : ICaseManagerRepository
    { 
        private readonly BaseRepository<CaseManager> _caseManagerRepo;
        public CaseManagerRepository()
        {
            _caseManagerRepo = new BaseRepository<CaseManager>();
        }
        public int addCaseManager(CaseManager _caseManager)
        {
            return _caseManagerRepo.Add(_caseManager).CaseManagerID;
        }
        public int updateCaseManager(CaseManager _caseManager)
        {
            return _caseManagerRepo.Update(_caseManager);
        }

        public void deleteCaseManager(int _id)
        {
            _caseManagerRepo.Delete(_id);
        }

        public IEnumerable<CaseManager> getCaseManagerAll()
        {
            return _caseManagerRepo.GetAll();
        }

        public BLModel.Paged.CaseManager getCaseManagerByName(string searchtext, int skip, int take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _CaseManagerDetails = (_MMCDbContext.caseManagers.Join(_MMCDbContext.states, s => s.CMStateId, ss => ss.StateId, (s, ss) => new {s.CaseManagerID,s.CMAddress1,s.CMAddress2,s.CMCity,s.CMEmail,s.CMFax,s.CMFirstName,s.CMLastName,
                                                                                                                                                 s.CMNotes,
                                                                                                                                                 s.CMPhone,
                                                                                                                                                 s.CMStateId,
                                                                                                                                                 s.CMZip,
                                                                                                                                                 ss.StateName
            }).Where(s => s.CMFirstName.StartsWith(searchtext) || s.CMLastName.StartsWith(searchtext) || (s.CMFirstName.Trim() + " " + s.CMLastName.Trim()).StartsWith(searchtext)).OrderByDescending(s => s.CaseManagerID)).Skip(skip).Take(take).ToList();
            return new BLModel.Paged.CaseManager
            {
                CaseManagerDetails = _CaseManagerDetails.Select(cm => new BLModel.CaseManager().InjectFrom(cm)).Cast<BLModel.CaseManager>().ToList(),
                TotalCount = (_MMCDbContext.caseManagers.Where(s => s.CMFirstName.StartsWith(searchtext) || s.CMLastName.StartsWith(searchtext) || (s.CMFirstName.Trim() + " " + s.CMLastName.Trim()).StartsWith(searchtext))).Count()
            };
            
        }

        public CaseManager getCaseManagerByID(int _id)
        {
            return _caseManagerRepo.GetById(_id);
        }
    }
}
