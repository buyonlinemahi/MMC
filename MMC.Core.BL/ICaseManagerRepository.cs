using MMC.Core.Data.Model;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL
{
    public interface ICaseManagerRepository
    {
        int addCaseManager(CaseManager _caseManager);
        int updateCaseManager(CaseManager _caseManager);
        void deleteCaseManager(int _caseManagerId);
        IEnumerable<CaseManager> getCaseManagerAll();
        CaseManager getCaseManagerByID(int _caseManager);
        BLModel.Paged.CaseManager getCaseManagerByName(string SearchText, int _skip, int _take);
    }
}
