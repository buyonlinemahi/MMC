using MMC.Core.BL;
using MMC.Core.Data.Model;
using MMC.Core.Data.SQLServer;
using Omu.ValueInjecter;
using System.Collections.Generic;
using System.Linq;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BLImplementation
{
    public class ADRRepository :  IADRRepository
    {
        private readonly BaseRepository<ADR> _ADRRepo;
        public ADRRepository()
        {
            _ADRRepo = new BaseRepository<ADR>();
        }

        public int addADR(ADR _ADR)
        {
            return _ADRRepo.Add(_ADR).ADRID;
        }

        public int updateADR(ADR _ADR)
        {
            return _ADRRepo.Update(_ADR);
        }

        public void deleteADR(int _ADRId)
        {
            _ADRRepo.Delete(_ADRId);
        }

        public IEnumerable<ADR> getADRsAll()
        {
            return _ADRRepo.GetAll();
        }

        public ADR getADRByID(int _Id)
        {
            return _ADRRepo.GetById(_Id);
        }

        public BLModel.Paged.ADR getADRsByName(string SearchText, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _ADRByName = (_MMCDbContext.ADRs.Join(_MMCDbContext.states, p => p.ADRStateID, pp => pp.StateId, (p, pp) => new
            {
                p.ADRID, p.ADRName, p.ADRAddress,p.ADRAddress2,p.ADRCity,p.ADRStateID,p.ADRZip,pp.StateName
            }).Where(p => p.ADRName.StartsWith(SearchText)).OrderByDescending(p => p.ADRID)).Skip(_skip).Take(_take).ToList();

            return new BLModel.Paged.ADR
            {
                ADRDetails = _ADRByName.Select(adr => new BLModel.ADR().InjectFrom(adr)).Cast<BLModel.ADR>(),
                TotalCount = (_MMCDbContext.ADRs.Where(adr => adr.ADRName.StartsWith(SearchText))).Count()

            };
        }
    }
}
