using MMCApp.Domain.Models.EmployerModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.EmployerViewModel
{
    public class EmployerSubsidiarySearchViewModel
    {
        public Employer EmployerResult { get; set; }
        public IEnumerable<EmployerSubsidiary> EmployerSubsidiaryDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
