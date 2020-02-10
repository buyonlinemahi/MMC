using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMCApp.Domain.Models.PatientModel
{
    public class PatientOccupational
    {
        [Key]
        public int PatientOccupationalID { get; set; }
        public int PatientID { get; set; }
        public string PatOptJobTitle { get; set; }
        public string PatOptJobDescription { get; set; }
        public string PatOptInjuryType { get; set; }
        public string PatOptInjuryDescription { get; set; }

        [ForeignKey("PatientID")]
        public Patient patients { get; set; }
    }
}
