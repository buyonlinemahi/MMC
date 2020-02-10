using MMC.Core.Data.Model;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL
{
    public interface IAdjusterRepository
    {
        int addAdjuster(Adjuster _adjuster);
        int updateAdjuster(Adjuster _adjuster);
        void deleteAdjuster(int _adjusterId);
        IEnumerable<Adjuster> getadjustersAll();
        Adjuster getAdjusterByID(int _adjusterId);
        BLModel.Paged.Adjuster getadjustersByName(string SearchText, int _skip, int _take);
    }
}
