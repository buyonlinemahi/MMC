using MMCApp.Domain.Models.CurrentMedicalConditionModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.CurrentMedicalConditionViewModel
{
    public class CurrentMedicalConditionAllViewModel
    {
        public IEnumerable<CurrentMedicalCondition> CurrentMedicalConditionDetails { get; set; }
    }
}
