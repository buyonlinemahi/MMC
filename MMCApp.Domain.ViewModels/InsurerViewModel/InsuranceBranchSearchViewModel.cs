using MMCApp.Domain.Models.InsurerModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.InsurerViewModel
{
    public class InsuranceBranchSearchViewModel
    {
        public IEnumerable<InsuranceBranch> InsuranceBranchDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
