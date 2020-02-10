using MMC.Core.Data.Model;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL
{
    public interface IThirdPartyAdministratorRepository
    {
        int addThirdPartyAdministrator(ThirdPartyAdministrator _thirdPartyAdministrator);
        int updateThirdPartyAdministrator(ThirdPartyAdministrator _thirdPartyAdministrator);
        void deleteThirdPartyAdministrator(int _thirdPartyAdministratorId);
        IEnumerable<ThirdPartyAdministrator> getThirdPartyAdministratorsAll();
        ThirdPartyAdministrator getThirdPartyAdministratorByID(int _thirdPartyAdministratorId);
        BLModel.Paged.ThirdPartyAdministrator getThirdPartyAdministratorsByName(string SearchText, int _skip, int _take);
    }
}
