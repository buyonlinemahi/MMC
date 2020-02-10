using MMCApp.Domain.Models.EmployerModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.EmployerViewModel
{
    public class EmployerSearchViewModel
    {
        public IEnumerable<Employer> EmployerDetails { get; set; }
        public int TotalCount { get; set; }

    }
}
