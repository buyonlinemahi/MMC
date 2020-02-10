using MMC.Core.BL;
using MMC.Core.Data.Model;
using MMC.Core.Data.SQLServer;
using Omu.ValueInjecter;
using System.Collections.Generic;
using System.Linq;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BLImplementation
{
    public class AdjusterRepository : IAdjusterRepository
    {
        private readonly BaseRepository<Adjuster> _adjusterRepo;
        public AdjusterRepository()
        {
            _adjusterRepo = new BaseRepository<Adjuster>();
        }

        public int addAdjuster(Adjuster _adjuster)
        {
            return _adjusterRepo.Add(_adjuster).AdjusterID;
        }

        public int updateAdjuster(Adjuster _adjuster)
        {
            return _adjusterRepo.Update(_adjuster);
        }

        public void deleteAdjuster(int _adjusterid)
        {
            _adjusterRepo.Delete(_adjusterid);
        }

        public IEnumerable<Adjuster> getadjustersAll()
        {
            return _adjusterRepo.GetAll();
        }

        public Adjuster getAdjusterByID(int _id)
        {
            return _adjusterRepo.GetById(_id);
        } 

        public BLModel.Paged.Adjuster getadjustersByName(string SearchText, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _adjusterByName = (from adj in _MMCDbContext.adjusters
                                   join st in _MMCDbContext.states
                                   on adj.AdjStateId equals st.StateId
                                   where (adj.AdjFirstName.StartsWith(SearchText) || adj.AdjLastName.StartsWith(SearchText) || (adj.AdjFirstName +" "+ adj.AdjLastName).StartsWith(SearchText))
                                   select new { adj.AdjAddress1, adj.AdjAddress2, adj.AdjCity, adj.AdjEMail, adj.AdjFax, adj.AdjFirstName, adj.AdjLastName, adj.AdjPhone, adj.AdjStateId, adj.AdjusterID, adj.AdjZip, AdjStateName = st.StateName }).OrderByDescending(adj => adj.AdjusterID).Skip(_skip).Take(_take).ToList();
            return new BLModel.Paged.Adjuster
            {
                AdjusterDetails = _adjusterByName.Select(adj => new BLModel.Adjuster().InjectFrom(adj)).Cast<BLModel.Adjuster>(),
                TotalCount = (_MMCDbContext.adjusters.Where(adj => adj.AdjFirstName.StartsWith(SearchText) || adj.AdjLastName.StartsWith(SearchText) || (adj.AdjFirstName + " " + adj.AdjLastName).StartsWith(SearchText))).Count()

            };
        }
    }
} 