
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MMC.Core.Data.Model
{
    public class MedicalGroup
    {
        [Key]
        public int MedicalGroupID { get; set; }
        public string MedicalGroupName { get; set; }
        public string MGAddress { get; set; }
        public string MGAddress2 { get; set; }
        public string MGCity { get; set; }
        public int MGStateID { get; set; }
        public string MGZip { get; set; }
        public string MGNote { get; set; }

        [ForeignKey("MGStateID")]
        public State states { get; set; }
    }
}
