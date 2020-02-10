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
    public class EmployerRepository : IEmployerRepository
    {       
        private readonly BaseRepository<Employer> _employerRepository;
        private readonly BaseRepository<EmployerSubsidiary> _employerSubsidiarieRepository;
        public EmployerRepository()
        {
            _employerRepository = new BaseRepository<Employer>();
            _employerSubsidiarieRepository = new BaseRepository<EmployerSubsidiary>();
        }

        #region Employer
        public int addEmployer(Employer _employer)
        {
            return _employerRepository.Add(_employer).EmployerID;
        }

        public int updateEmployer(Employer _employer)
        {
            return _employerRepository.Update(_employer);
        }

        public void deleteEmployer(int _employerID)
        {
            _employerRepository.Delete(_employerID);
        }

        public IEnumerable<Employer> getEmployerAll()
        {
            return _employerRepository.GetAll();
        }

        public Employer getEmployerByID(int _id)
        {
            return _employerRepository.GetById(_id);
        }

        public BLModel.Paged.Employer getEmployerByName(string SearchText, int _skip, int _take)
        {
            return geEmployerRecords(emp => emp.EmpName.StartsWith(SearchText),_skip,_take);
        }

        public BLModel.Paged.Employer geEmployerRecords(Expression<Func<Employer, bool>> _where, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _employerByName = (from emp in _MMCDbContext.employers.Where(_where)
                                   join st in _MMCDbContext.states
                                   on emp.EmpStateId equals st.StateId
                                   select new
                                   {
                                       emp.EmpAddress1
                                       ,
                                       emp.EmpAddress2
                                       ,
                                       emp.EmpPhoneExtension
                                       ,
                                       emp.EmpCity
                                       ,
                                       emp.EmpContactName
                                       ,
                                       emp.EmpEMail
                                       ,
                                       emp.EmpFax
                                       ,
                                       emp.EmpName
                                       ,
                                       emp.EmployerID
                                       ,
                                       emp.EmpPhone
                                       ,
                                       emp.EmpStateId
                                       ,
                                       emp.EmpZip
                                       ,
                                       EmpStateName = st.StateName
                                   }).OrderBy(emp => emp.EmpName).ToList().OrderByDescending(emp => emp.EmployerID);
          
            return new BLModel.Paged.Employer
            {
                EmployerDetails = _employerByName.Select(emp => new BLModel.Employer().InjectFrom(emp)).Cast<BLModel.Employer>().Skip(_skip).Take(_take).ToList(),
                TotalCount = _employerByName.Count()
            };
        }

        #endregion

        #region Employer Subsidiaries
        public int addEmployerSubsidiaries(EmployerSubsidiary _employerSubsidiaries)
        {
            return _employerSubsidiarieRepository.Add(_employerSubsidiaries).EMPSubsidiaryID;
        }

        public int updateEmployerSubsidiaries(EmployerSubsidiary _employerSubsidiaries)
        {
            return _employerSubsidiarieRepository.Update(_employerSubsidiaries);
        }

        public void deleteEmployerSubsidiaries(int _employerSubsidiariesID)
        {
            _employerSubsidiarieRepository.Delete(_employerSubsidiariesID);
        }

        public IEnumerable<EmployerSubsidiary> getEmployerSubsidiariesAll()
        {
            return _employerSubsidiarieRepository.GetAll();
        }
        public IEnumerable<EmployerSubsidiary> getAllEmployerSubsidiariesByEmployerID(int _empID)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _employerSubsidiaryByName = (from empSubsidiaries in _MMCDbContext.employerSubsidiaries
                                             join emp in _MMCDbContext.employers
                                             on empSubsidiaries.EmployerId equals emp.EmployerID
                                             join st in _MMCDbContext.states
                                             on empSubsidiaries.EMPSubsidiaryStateId equals st.StateId
                                             where emp.EmployerID == _empID
                                             select new
                                             {
                                                 empSubsidiaries.EMPSubsidiaryAddress
                                                 ,
                                                 empSubsidiaries.EMPSubsidiaryAddress2
                                                 ,
                                                 empSubsidiaries.EmployerId
                                                 ,
                                                 empSubsidiaries.EMPSubsidiaryCity
                                                 ,
                                                 empSubsidiaries.EMPSubsidiaryEmail
                                                 ,
                                                 empSubsidiaries.EMPSubsidiaryFax
                                                 ,
                                                 empSubsidiaries.EMPSubsidiaryID
                                                 ,
                                                 empSubsidiaries.EMPSubsidiaryName
                                                 ,
                                                 empSubsidiaries.EMPSubsidiaryPhone
                                                 ,
                                                 empSubsidiaries.EMPSubsidiaryStateId
                                                 ,
                                                 empSubsidiaries.EMPSubsidiaryZip
                                                 ,
                                                 EmpSubsidiaryStateName = st.StateName
                                             }).OrderByDescending(empsub => empsub.EMPSubsidiaryID).ToList().OrderByDescending(emps => emps.EMPSubsidiaryID);

            return _employerSubsidiaryByName.Select(emp => new EmployerSubsidiary().InjectFrom(emp)).Cast<EmployerSubsidiary>().ToList();
        }
        public EmployerSubsidiary getEmployerSubsidiariesByID(int _id)
        {
            return _employerSubsidiarieRepository.GetById(_id);
        }

        public BLModel.Paged.EmployerSubsidiary getEmployerSubsidiariesByEmployerID(int _employerID, int _skip, int _take)
        {

            return getEmployerSubsidiariesRecords(empSubsidiaries => empSubsidiaries.EmployerId == _employerID, _skip, _take);
        }

        public BLModel.Paged.EmployerSubsidiary getEmployerSubsidiariesRecords(Expression<Func<EmployerSubsidiary, bool>> _where, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _employerSubsidiaryByName = (from empSubsidiaries in _MMCDbContext.employerSubsidiaries.Where(_where)
                                             join emp in _MMCDbContext.employers
                                             on empSubsidiaries.EmployerId equals emp.EmployerID
                                             join st in _MMCDbContext.states
                                             on empSubsidiaries.EMPSubsidiaryStateId equals st.StateId
                                             select new
                                             {
                                                 empSubsidiaries.EMPSubsidiaryAddress
                                                 ,
                                                 empSubsidiaries.EMPSubsidiaryAddress2
                                                 ,
                                                 empSubsidiaries.EmployerId
                                                 ,
                                                 empSubsidiaries.EMPSubsidiaryCity
                                                 ,
                                                 empSubsidiaries.EMPSubsidiaryEmail
                                                 ,
                                                 empSubsidiaries.EMPSubsidiaryFax
                                                 ,
                                                 empSubsidiaries.EMPSubsidiaryID
                                                 ,
                                                 empSubsidiaries.EMPSubsidiaryName
                                                 ,
                                                 empSubsidiaries.EMPSubsidiaryPhone
                                                 ,
                                                 empSubsidiaries.EMPSubsidiaryStateId
                                                 ,
                                                 empSubsidiaries.EMPSubsidiaryZip
                                                 ,
                                                 EmpSubsidiaryStateName = st.StateName
                                             }).OrderByDescending(empsub => empsub.EMPSubsidiaryID).ToList().OrderByDescending(emps => emps.EMPSubsidiaryID);

            return new BLModel.Paged.EmployerSubsidiary
            {
                EmployerSubsidiaryDetails = _employerSubsidiaryByName.Select(emp => new BLModel.EmployerSubsidiary().InjectFrom(emp)).Cast<BLModel.EmployerSubsidiary>().Skip(_skip).Take(_take).ToList(),
                TotalCount = _employerSubsidiaryByName.Count()
            };
        }
        #endregion
    }
}
