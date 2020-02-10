using MMCApp.Domain.Models.MedicalGroup;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.MedicalGroupViewModel
{
    public class MedicalGroupSearchViewModel
    {
        public IEnumerable<MedicalGroup> MedicalGroupDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
