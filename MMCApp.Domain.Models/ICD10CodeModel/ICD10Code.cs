using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMCApp.Domain.Models.ICD10CodeModel
{
    public class ICD10Code
    {
        public int icdICD10NumberID { get; set; }
        public string icdICD10Number { get; set; }
        public string ICD10Description { get; set; }
        public string ICD10Type { get; set; }
    }
}
