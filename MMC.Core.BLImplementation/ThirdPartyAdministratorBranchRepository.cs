using MMC.Core.BL;
using MMC.Core.Data.Model;
using MMC.Core.Data.SQLServer;
using Omu.ValueInjecter;
using System.Collections.Generic;
using System.Linq;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BLImplementation
{
    public class ThirdPartyAdministratorBranchRepository : IThirdPartyAdministratorBranchRepository
    {
        private readonly BaseRepository<ThirdPartyAdministratorBranch> _thirdPartyAdministratorBranchRepo;
        public ThirdPartyAdministratorBranchRepository()
        {
            _thirdPartyAdministratorBranchRepo = new BaseRepository<ThirdPartyAdministratorBranch>();
        }
        public int addThirdPartyAdministratorBranch(ThirdPartyAdministratorBranch _thirdPartyAdministratorBranch)
        {
            return _thirdPartyAdministratorBranchRepo.Add(_thirdPartyAdministratorBranch).TPABranchID;
        }
        public int updateThirdPartyAdministratorBranch(ThirdPartyAdministratorBranch _thirdPartyAdministratorBranch)
        {
            return _thirdPartyAdministratorBranchRepo.Update(_thirdPartyAdministratorBranch);
        }

        public void deleteThirdPartyAdministratorBranch(int _thirdPartyAdministratorBranchId)
        {
            _thirdPartyAdministratorBranchRepo.Delete(_thirdPartyAdministratorBranchId);
        }

        public IEnumerable<ThirdPartyAdministratorBranch> getThirdPartyAdministratorBranchesAll()
        {
            return _thirdPartyAdministratorBranchRepo.GetAll();
        }

        public BLModel.Paged.ThirdPartyAdministratorBranch getThirdPartyAdministratorBranchesByName(string SearchText, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _ThirdPartyAdministratorBranchDetails = (_MMCDbContext.thirdPartyAdministratorBranches.Join(_MMCDbContext.states, s => s.TPABranchStateId, ss => ss.StateId, (s, ss) => new { s.TPABranchName, s.TPABranchID, s.TPABranchCity, s.TPABranchAddress, s.TPABranchAddress2, s.TPABranchStateId, s.TPABranchZip, s.states.StateName, s.TPAID })
                .Where(s => s.TPABranchName.StartsWith(SearchText)).OrderByDescending(s => s.TPABranchID)).Skip(_skip).Take(_take).ToList();
            return new BLModel.Paged.ThirdPartyAdministratorBranch
            {
                ThirdPartyAdministratorBranchDetails = _ThirdPartyAdministratorBranchDetails.Select(cm => new BLModel.ThirdPartyAdministratorBranch().InjectFrom(cm)).Cast<BLModel.ThirdPartyAdministratorBranch>().ToList(),
                TotalCount = _MMCDbContext.thirdPartyAdministratorBranches.Where(s => s.TPABranchName.StartsWith(SearchText)).Count()
            };
        }

        public ThirdPartyAdministratorBranch getThirdPartyAdministratorBranchByID(int _thirdPartyAdministratorBranchId)
        {
            return _thirdPartyAdministratorBranchRepo.GetById(_thirdPartyAdministratorBranchId);
        }
        public BLModel.Paged.ThirdPartyAdministratorBranch getThirdPartyAdministratorBranchesByTPAID(int _thirdPartyAdministratorId, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _ThirdPartyAdministratorBranchDetails = (_MMCDbContext.thirdPartyAdministratorBranches.Join(_MMCDbContext.states, s => s.TPABranchStateId, ss => ss.StateId, (s, ss) => new { s.TPABranchName, s.TPABranchID, s.TPABranchCity, s.TPABranchAddress, s.TPABranchAddress2, s.TPABranchStateId, s.TPABranchZip, s.states.StateName, s.TPAID })
                .Where(s => s.TPAID == _thirdPartyAdministratorId).OrderByDescending(s => s.TPABranchID)).Skip(_skip).Take(_take).ToList();
            return new BLModel.Paged.ThirdPartyAdministratorBranch
            {
                ThirdPartyAdministratorBranchDetails = _ThirdPartyAdministratorBranchDetails.Select(cm => new BLModel.ThirdPartyAdministratorBranch().InjectFrom(cm)).Cast<BLModel.ThirdPartyAdministratorBranch>().ToList(),
                TotalCount = _MMCDbContext.thirdPartyAdministratorBranches.Where(s => s.TPAID == _thirdPartyAdministratorId).Count()
            };
        }

        public IEnumerable<ThirdPartyAdministratorBranch> getThirdPartyAdministratorBranchesAllByTPAID(int _tpaID)
        {
            return _thirdPartyAdministratorBranchRepo.GetAll(tpab => tpab.TPAID == _tpaID).Select(ins => new ThirdPartyAdministratorBranch().InjectFrom(ins)).Cast<ThirdPartyAdministratorBranch>().ToList();
        }

    }
}
