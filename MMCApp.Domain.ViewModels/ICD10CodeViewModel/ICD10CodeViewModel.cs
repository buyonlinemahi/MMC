using MMCApp.Domain.Models.ICD10CodeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMCApp.Domain.ViewModels.ICD10CodeViewModel
{
    public class ICD10CodeViewModel
    {
        public IEnumerable<ICD10Code> ICD10CodeDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
