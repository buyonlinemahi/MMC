using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMCApp.Domain.Models.CurrentWorkloadModel
{
  public  class CurrentWorkloadReportParameter
    {
        public int CurrentWorkloadReportParameterID { get; set; }
        public int? ClientID { get; set; }
        public int CurrentWorkloadReportID { get; set; }
        public int? StatusID { get; set; }
    }
}
