using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MMC.Core.Data.Model
{
    public class RFARequestCPTCode
    {
        [Key]
        public int RFACPTCodeID { get; set; }
        public int RFARequestID { get; set; }
        public string CPT_NDCCode { get; set; }

        [ForeignKey("RFARequestID")]
        public RFARequest _rFARequest { get; set; }

    }
}