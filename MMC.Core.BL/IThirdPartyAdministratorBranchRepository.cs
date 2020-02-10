using MMC.Core.Data.Model;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL
{
    public interface IThirdPartyAdministratorBranchRepository
    {
        int addThirdPartyAdministratorBranch(ThirdPartyAdministratorBranch _thirdPartyAdministratorBranch);
        int updateThirdPartyAdministratorBranch(ThirdPartyAdministratorBranch _thirdPartyAdministratorBranch);
        void deleteThirdPartyAdministratorBranch(int _thirdPartyAdministratorBranchId);
        IEnumerable<ThirdPartyAdministratorBranch> getThirdPartyAdministratorBranchesAll();
        ThirdPartyAdministratorBranch getThirdPartyAdministratorBranchByID(int _thirdPartyAdministratorBranchId);
        BLModel.Paged.ThirdPartyAdministratorBranch getThirdPartyAdministratorBranchesByName(string SearchText, int _skip, int _take);
        BLModel.Paged.ThirdPartyAdministratorBranch getThirdPartyAdministratorBranchesByTPAID(int _thirdPartyAdministratorId, int _skip, int _take);
        IEnumerable<ThirdPartyAdministratorBranch> getThirdPartyAdministratorBranchesAllByTPAID(int _tpaID);
    }
}
