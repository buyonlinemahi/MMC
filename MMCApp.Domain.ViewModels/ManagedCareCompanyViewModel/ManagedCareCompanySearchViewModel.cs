using MMCApp.Domain.Models.ManagedCareCompanyModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.ManagedCareCompanyViewModel
{
    public class ManagedCareCompanySearchViewModel
    {
        public IEnumerable<ManagedCareCompany> ManagedCareCompanyDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
