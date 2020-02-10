using MMC.Core.BL;
using MMC.Core.Data.Model;
using MMC.Core.Data.SQLServer;
using Omu.ValueInjecter;
using System.Collections.Generic;
using System.Linq;
using BLModel = MMC.Core.BL.Model;
namespace MMC.Core.BLImplementation
{
    public class InsurerRepository : IInsurerRepository
    {

        private readonly BaseRepository<Insurer> _insurerRepository;
        private readonly BaseRepository<InsuranceBranch> _insuranceBranchRepo;
        
        public InsurerRepository()
        {
            _insurerRepository = new BaseRepository<Insurer>();
            _insuranceBranchRepo = new BaseRepository<InsuranceBranch>();
        }

        #region Insurer

        public int addInsurer(Insurer _Insurer)
        {
            return _insurerRepository.Add(_Insurer).InsurerID;
        }

        public int updateInsurer(Insurer _Insurer)
        {
            return _insurerRepository.Update(_Insurer);
        }

        public void deleteInsurer(int _Insurerid)
        {
            _insurerRepository.Delete(_Insurerid);
        }

        public IEnumerable<Insurer> getInsurersAll()
        {
            return _insurerRepository.GetAll();
        }

        public Insurer getInsurerByID(int _id)
        {
            return _insurerRepository.GetById(_id);
        }

        public BLModel.Paged.Insurer getInsurersByName(string SearchText, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _insurerByName = (from ins in _MMCDbContext.insurers
                                  join st in _MMCDbContext.states
                                  on ins.InsStateId equals st.StateId
                                  where (ins.InsName.StartsWith(SearchText))
                                  select new { ins.InsAddress1, ins.InsAddress2, ins.InsCity, ins.InsEMail, ins.InsFax, ins.InsName, ins.InsPhone, ins.InsStateId, ins.InsurerID, ins.InsZip, InsStateName = st.StateName }).OrderByDescending(ins => ins.InsurerID).Skip(_skip).Take(_take).ToList();

            return new BLModel.Paged.Insurer
            {
                InsurerDetails = _insurerByName.Select(ins => new BLModel.Insurer().InjectFrom(ins)).Cast<BLModel.Insurer>().ToList(),
                TotalCount = _MMCDbContext.insurers.Where(ins => ins.InsName.StartsWith(SearchText)).Count()
            };
        }

        #endregion  

        #region Insurance Branch

        public int addInsuranceBranch(InsuranceBranch _InsuranceBranch)
        {
            return _insuranceBranchRepo.Add(_InsuranceBranch).InsuranceBranchID ;
        }

        public int updateInsuranceBranch(InsuranceBranch _InsuranceBranch)
        {
            return _insuranceBranchRepo.Update(_InsuranceBranch);
        }

        public void deleteInsuranceBranch(int _InsuranceBranchID)
        {
            _insuranceBranchRepo.Delete(_InsuranceBranchID);
        }

        public BLModel.Paged.InsuranceBranch getInsuranceBranchesByInsurerID(int _InsurerID,int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _insurerByName = (from insbrch in _MMCDbContext.insuranceBranches
                                  join ins in _MMCDbContext.insurers
                                  on insbrch.InsurerId equals ins.InsurerID
                                  join st in _MMCDbContext.states
                                  on insbrch.InsBranchStateId equals st.StateId
                                  where (insbrch.InsurerId == _InsurerID)
                                  select new { InsBranchStateName = st.StateName, insbrch.InsBranchAddress, insbrch.InsBranchCity, insbrch.InsBranchName, insbrch.InsBranchStateId, insbrch.InsBranchZip, insbrch.InsuranceBranchID, insbrch.InsurerId }).OrderByDescending(insbrch => insbrch.InsuranceBranchID).ToList();
            return new BLModel.Paged.InsuranceBranch
            {
                InsuranceBranchDetails = _insurerByName.Select(ins => new BLModel.InsuranceBranch().InjectFrom(ins)).Cast<BLModel.InsuranceBranch>().Skip(_skip).Take(_take).ToList(),
                TotalCount = _insurerByName.Count()
            };
        }


        public IEnumerable<InsuranceBranch> getInsuranceBranchesAllByInsurerID(int _InsurerID)
        {
           return _insuranceBranchRepo.GetAll(ins => ins.InsurerId == _InsurerID).Select(ins => new InsuranceBranch().InjectFrom(ins)).Cast<InsuranceBranch>().ToList();
        }

        public InsuranceBranch getInsuranceBranchByID(int _insuranceBranchID)
        {
            return _insuranceBranchRepo.GetById(_insuranceBranchID);
        }
        
        #endregion
    }
}
