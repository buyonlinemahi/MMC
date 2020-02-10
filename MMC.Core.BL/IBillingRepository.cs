using MMC.Core.Data.Model;

namespace MMC.Core.BL
{
    public interface IBillingRepository
    {
        BL.Model.Paged.Billing getBillingDetails(int _skip, int _take);
        int updateRFARequestBilling(RFARequestBilling _rfaRequestBilling);
        int addRFARequestBilling(RFARequestBilling _rfaRequestBilling);
        BL.Model.Paged.Billing getBillingDetailByClientName(string ClientName, int _skip, int _take);

        #region RFA Referral Invoice
        string addRFAReferralInvoice(RFAReferralInvoice _rRFAReferralInvoice);
        RFAReferralInvoice getInvoiceNumberByClientID(int _ClientID);
        int getInvoiceIDByInvoiceNumber(string InvoiceNumber);
        #endregion

        #region RFA Referral Statement
        int addClientStatement(ClientStatement _clientStatement);
        ClientStatement getStatementNumberByClient(int _clientID);
        string getStatementNumberByStatementID(int StatementID);
        #endregion

        #region AccountReceivable
        BL.Model.Paged.BillingAccountReceivables getAccountReceivablesByClientName(string ClientName, int _skip, int _take);
       #endregion
    }
}
