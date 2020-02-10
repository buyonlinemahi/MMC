using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMC.Core.BL.Model
{
   public class PatientOccupationalAddUpdateDetails
    {
       public IEnumerable<MMC.Core.Data.Model.PatientOccupational> PatientOccupationalDetails { get; set; }
       public string TotalPatientOccupational { get; set; }
    }
}
