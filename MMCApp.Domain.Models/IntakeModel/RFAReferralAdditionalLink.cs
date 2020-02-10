
namespace MMCApp.Domain.Models.IntakeModel
{
   public  class RFAReferralAdditionalLink
    {
        public int RFAReferralAdditionalLinkID { get; set; }
        public int RFAReferralID { get; set; }
        public int? RFAReferralInvoiceID { get; set; }
        public int? ClientStatementID { get; set; }
    }
}
