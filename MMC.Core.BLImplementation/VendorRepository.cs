using MMC.Core.BL;
using MMC.Core.Data.Model;
using MMC.Core.Data.SQLServer;
using System.Collections.Generic;
using System.Linq;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BLImplementation
{
    public class VendorRepository : IVendorRepository
    {
        MMCDbContext _MMCDbContext;
        private readonly BaseRepository<Vendor> _vendorRepo;
        public VendorRepository()
        {
            _MMCDbContext = new MMCDbContext();
            _vendorRepo = new BaseRepository<Vendor>();
        }
        public int addVendor(Vendor _vendor)
        {
            return _vendorRepo.Add(_vendor).VendorID;
        }
        public int updateVendor(Vendor _vendor)
        {
            return _vendorRepo.Update(_vendor);
        }

        public void deleteVendor(int _id)
        {
            _vendorRepo.Delete(_id);
        }

        public IEnumerable<Vendor> getVendorsAll()
        {
            return _vendorRepo.GetAll();
        }

        public BLModel.Paged.Vendor getVendorsByName(string searchtext, int skip, int take)
        {
            var _vendorDetails = (_MMCDbContext.vendors.Where(p => p.VendorName.StartsWith(searchtext)).OrderByDescending(p => p.VendorID)).Skip(skip).Take(take).ToList();
            return new BLModel.Paged.Vendor
            {
                VendorDetails = _vendorDetails,
                TotalCount = (_MMCDbContext.vendors.Where(p => p.VendorName.StartsWith(searchtext))).Count()
            };
            
        }

        public Vendor getVendorByID(int _id)
        {
            return _vendorRepo.GetById(_id);
        }
    }
}
