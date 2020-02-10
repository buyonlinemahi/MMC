using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MMC.Core.Data.Model
{
    [Table(Constant.Constant.lookupTables.TreatmentType, Schema = Constant.Constant.Schemas.lookup)]
    public class TreatmentType
    {
        [Key]
        public int TreatmentTypeID { get; set; }
        public int TreatmentCategoryID { get; set; }
        public string TreatmentTypeDesc { get; set; }
        public decimal RFARequestBillingRN { get; set; }
        public decimal RFARequestBillingMD { get; set; }
        public decimal RFARequestBillingPR { get; set; }
        public decimal RFARequestBillingAdmin { get; set; }
        public decimal EstCost { get; set; }
        public bool TreatmentMDRequired { get; set; }
        public string TreatmentDescNumber { get; set; }
    }
}
