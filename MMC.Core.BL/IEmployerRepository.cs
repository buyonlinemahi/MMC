using MMC.Core.Data.Model;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL
{
    public interface IEmployerRepository
    {
        #region Employer
        int addEmployer(Employer _employer);
        int updateEmployer(Employer _employer);
        void deleteEmployer(int _employerID);
        IEnumerable<Employer> getEmployerAll();
        Employer getEmployerByID(int _id);
        BLModel.Paged.Employer getEmployerByName(string SearchText, int _skip, int _take);
        #endregion

        #region EmployerSubsidiaries
        int addEmployerSubsidiaries(EmployerSubsidiary _employerSubsidiaries);
        int updateEmployerSubsidiaries(EmployerSubsidiary _employerSubsidiaries);
        void deleteEmployerSubsidiaries(int _employerSubsidiariesID);
        IEnumerable<EmployerSubsidiary> getEmployerSubsidiariesAll();
        EmployerSubsidiary getEmployerSubsidiariesByID(int _id);
        BLModel.Paged.EmployerSubsidiary getEmployerSubsidiariesByEmployerID(int _employerID, int _skip, int _take);
        IEnumerable<EmployerSubsidiary> getAllEmployerSubsidiariesByEmployerID(int _empID);
        #endregion
    }
}
