using MMC.Core.BL;
using MMC.Core.Data.Model;
using MMC.Core.Data.SQLServer;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BLImplementation
{
    public class ManagedCareCompanyRepository : IManagedCareCompanyRepository
    {
        private readonly BaseRepository<ManagedCareCompany> _managedCareCompanyRepository;
        public ManagedCareCompanyRepository()
        {
            _managedCareCompanyRepository = new BaseRepository<ManagedCareCompany>();
        }

        public int addManagedCareCompany(ManagedCareCompany _company)
        {
            return _managedCareCompanyRepository.Add(_company).CompanyId;
        }

        public int updateManagedCareCompany(ManagedCareCompany _company)
        {
            return _managedCareCompanyRepository.Update(_company);
        }

        public void deleteManagedCareCompany(int _companyID)
        {
            _managedCareCompanyRepository.Delete(_companyID);
        }

        public IEnumerable<ManagedCareCompany> getManagedCareCompanyAll()
        {
            return _managedCareCompanyRepository.GetAll();
        }

        public ManagedCareCompany getManagedCareCompanyByID(int _id)
        {
            return _managedCareCompanyRepository.GetById(_id);
        }

        public BLModel.Paged.ManagedCareCompany getManagedCareCompanyByName(string SearchText, int _skip, int _take)
        {
            return getManagedCareCompanyRecords(comp => comp.CompName.StartsWith(SearchText), _skip, _take);
        }

        public BLModel.Paged.ManagedCareCompany getManagedCareCompanyRecords(Expression<Func<ManagedCareCompany, bool>> _where, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _compByName = (from comp in _MMCDbContext.managedCareCompanies.Where(_where)
                               join st in _MMCDbContext.states
                               on comp.CompStateId equals st.StateId
                               select new
                               {
                                   comp.CompanyId
                                  ,
                                   comp.CompName
                                  ,
                                   comp.CompAddress
                                  ,
                                   comp.CompAddress2
                                  ,
                                   comp.CompCity
                                  ,
                                   comp.CompZip
                                  ,
                                   comp.CompStateId
                                  ,
                                   CompStateName = st.StateName
                               }).OrderByDescending(comp => comp.CompanyId).ToList();

            return new BLModel.Paged.ManagedCareCompany
            {
                ManagedCareCompanyDetails = _compByName.Select(comp => new BLModel.ManagedCareCompany().InjectFrom(comp)).Cast<BLModel.ManagedCareCompany>().Skip(_skip).Take(_take).ToList(),
                TotalCount = _compByName.Count()
            };
        }
    }
}
