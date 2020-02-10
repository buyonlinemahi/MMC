using MMC.Core.Data.Model;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL
{
    public interface IVendorRepository
    {
        IEnumerable<Vendor> getVendorsAll();
        Vendor getVendorByID(int _id);
        int addVendor(Vendor _vendor);
        int updateVendor(Vendor _vendor);
        void deleteVendor(int _id);
        BLModel.Paged.Vendor getVendorsByName(string searchtext, int skip, int take);
    }
}
