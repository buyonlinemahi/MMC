using MMC.Core.Data.Model;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL
{
    public interface IADRRepository
    {
        int addADR(ADR _ADR);
        int updateADR(ADR _ADR);
        void deleteADR(int _ADRId);
        IEnumerable<ADR> getADRsAll();
        ADR getADRByID(int _Id);
        BLModel.Paged.ADR getADRsByName(string SearchText, int _skip, int _take);
    }
}
