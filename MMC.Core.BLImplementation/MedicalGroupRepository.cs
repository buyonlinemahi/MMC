using MMC.Core.BL;
using MMC.Core.Data.Model;
using MMC.Core.Data.SQLServer;
using Omu.ValueInjecter;
using System.Collections.Generic;
using System.Linq;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BLImplementation
{
    public class MedicalGroupRepository : IMedicalGroupRepository
    {
        private readonly BaseRepository<MedicalGroup> _MedicalGroupRepo;
        public MedicalGroupRepository()
        {
            _MedicalGroupRepo = new BaseRepository<MedicalGroup>();
        }
        public int addMedicalGroup(MedicalGroup _medicalGroup)
        {
            return _MedicalGroupRepo.Add(_medicalGroup).MedicalGroupID;
        }
        public int updateMedicalGroup(MedicalGroup _medicalGroup)
        {
            return _MedicalGroupRepo.Update(_medicalGroup);
        }

        public void deleteMedicalGroup(int _medicalGroupId)
        {
            _MedicalGroupRepo.Delete(_medicalGroupId);
        }

        public IEnumerable<MedicalGroup> getMedicalGroupsAll()
        {
            return _MedicalGroupRepo.GetAll();
        }

        public BLModel.Paged.MedicalGroup getMedicalGroupsByName(string SearchText, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _medicalGroupDetails = (_MMCDbContext.medicalGroups.Join(_MMCDbContext.states, s => s.MGStateID, ss => ss.StateId, (s, ss) => new { s.MedicalGroupID, s.MedicalGroupName, s.MGAddress, s.MGAddress2, s.MGCity, s.MGStateID, s.MGZip, s.MGNote, ss.StateName })
                .Where(s => s.MedicalGroupName.Contains(SearchText)).OrderByDescending(s => s.MedicalGroupID)).Skip(_skip).Take(_take).ToList();
            return new BLModel.Paged.MedicalGroup
            {
                MedicalGroupDetails = _medicalGroupDetails.Select(cm => new BLModel.MedicalGroup().InjectFrom(cm)).Cast<BLModel.MedicalGroup>().ToList(),
                TotalCount = _MMCDbContext.medicalGroups.Where(s => s.MedicalGroupName.Contains(SearchText)).Count()
            };
            
        }

        public MedicalGroup getMedicalGroupByID(int _medicalGroupId)
        {
            return _MedicalGroupRepo.GetById(_medicalGroupId);
        }
    }
}
