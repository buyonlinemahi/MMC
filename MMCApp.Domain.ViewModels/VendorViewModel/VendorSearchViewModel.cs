using MMCApp.Domain.Models.VendorModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.VendorViewModel
{
    public class VendorSearchViewModel
    {
        public IEnumerable<Vendor> VendorDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
