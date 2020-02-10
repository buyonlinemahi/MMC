
namespace MMCApp.Domain.Models.ClientBillingModel
{
    public class ClientBilling
    {
        public int ClientBillingID { get; set; }
        public int ClientID { get; set; }
        public int ClientBillingRateTypeID { get; set; }
        public bool ClientIsPrivateLabel { get; set; }
        public string ClientInvoiceNumber { get; set; }
        public int ClientAttentionToID { get; set; }
        public string ClientAttentionToFreeText { get; set; }
    }
}   
