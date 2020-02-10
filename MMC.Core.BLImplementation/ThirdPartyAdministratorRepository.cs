using MMC.Core.BL;
using MMC.Core.Data.Model;
using MMC.Core.Data.SQLServer;
using Omu.ValueInjecter;
using System.Collections.Generic;
using System.Linq;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BLImplementation
{
    public class ThirdPartyAdministratorRepository : IThirdPartyAdministratorRepository
    {
        private readonly BaseRepository<ThirdPartyAdministrator> _thirdPartyAdministratorRepo;
        public ThirdPartyAdministratorRepository()
        {
            _thirdPartyAdministratorRepo = new BaseRepository<ThirdPartyAdministrator>();
        }
        public int  addThirdPartyAdministrator(ThirdPartyAdministrator _thirdPartyAdministrator)
        {
            return _thirdPartyAdministratorRepo.Add(_thirdPartyAdministrator).TPAID;
        }
        public  int updateThirdPartyAdministrator(ThirdPartyAdministrator _thirdPartyAdministrator)
        {
            return _thirdPartyAdministratorRepo.Update(_thirdPartyAdministrator);
        }

        public void deleteThirdPartyAdministrator(int _thirdPartyAdministratorId)
        {
            _thirdPartyAdministratorRepo.Delete(_thirdPartyAdministratorId);
        }

        public IEnumerable<ThirdPartyAdministrator> getThirdPartyAdministratorsAll()
        {
            return _thirdPartyAdministratorRepo.GetAll();
        }

        public BLModel.Paged.ThirdPartyAdministrator getThirdPartyAdministratorsByName(string SearchText, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _ThirdPartyAdministratorDetails = (_MMCDbContext.thirdPartyAdministrators.Join(_MMCDbContext.states, s => s.TPAStateId, ss => ss.StateId, (s, ss) => new { s.TPAID,s.TPAName,s.TPAAddress,s.TPAAddress2,s.TPACity,s.TPAStateId,s.TPAZip,ss.StateName })
                .Where(s => s.TPAName.StartsWith(SearchText)).OrderByDescending(s => s.TPAID)).Skip(_skip).Take(_take).ToList();
            return new BLModel.Paged.ThirdPartyAdministrator
            {
                ThirdPartyAdministratorDetails = _ThirdPartyAdministratorDetails.Select(cm => new BLModel.ThirdPartyAdministrator().InjectFrom(cm)).Cast<BLModel.ThirdPartyAdministrator>().ToList(),
                TotalCount = _MMCDbContext.thirdPartyAdministrators.Where(s => s.TPAName.StartsWith(SearchText)).Count()
            };
            
        }

        public  ThirdPartyAdministrator getThirdPartyAdministratorByID(int _thirdPartyAdministratorId)
        {
            return _thirdPartyAdministratorRepo.GetById(_thirdPartyAdministratorId);
        }
    }
}
