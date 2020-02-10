using MMC.Core.Data.Model;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL
{
    public interface IManagedCareCompanyRepository
    {
        int addManagedCareCompany(ManagedCareCompany _company);
        int updateManagedCareCompany(ManagedCareCompany _company);
        void deleteManagedCareCompany(int _companyID);
        IEnumerable<ManagedCareCompany> getManagedCareCompanyAll();
        ManagedCareCompany getManagedCareCompanyByID(int _id);
        BLModel.Paged.ManagedCareCompany getManagedCareCompanyByName(string SearchText, int _skip, int _take);
    }
}
