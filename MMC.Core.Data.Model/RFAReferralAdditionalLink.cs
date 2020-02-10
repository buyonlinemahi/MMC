using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{ [Table(Constant.Constant.linkTables.RFAReferralAdditionalLink, Schema = Constant.Constant.Schemas.link)]
   public class RFAReferralAdditionalLink
   {
       [Key]
      public int RFAReferralAdditionalLinkID { get; set; }
      public int RFAReferralID { get; set; }
      public int? RFAReferralInvoiceID { get; set; }
      public int? ClientStatementID { get; set; }

      [ForeignKey("RFAReferralID")]
      public RFAReferral _RFAReferral { get; set; }

      [ForeignKey("RFAReferralInvoiceID")]
      public RFAReferralInvoice _RFAReferralInvoice { get; set; }

      [ForeignKey("ClientStatementID")]
      public ClientStatement _ClientStatement { get; set; }

    }
}
