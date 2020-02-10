using MMCService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MMCService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBillingService" in both code and config file together.
    [ServiceContract]
    public interface IBillingService
    {
        [OperationContract]
        DTO.Paged.PagedBilling getDetailsForBilling(int skip, int take);
        [OperationContract]
        int updateRFARequestBilling(MMCService.DTO.RFARequestBilling _rfaRequestBilling);
        [OperationContract]
        int addRFARequestBilling(MMCService.DTO.RFARequestBilling _rfaRequestBilling);
        [OperationContract]
        DTO.Paged.PagedBilling getDetailsForBillingByClientName(string ClientName, int skip, int take);

        #region RFA Referral Invoice
        [OperationContract]
        string addRFAReferralInvoice(DTO.RFAReferralInvoice _rRFAReferralInvoice);
        [OperationContract]
        RFAReferralInvoice getInvoiceNumberByClientID(int _ClientID);
        [OperationContract]
        int getInvoiceIDByInvoiceNumber(string InvoiceNumber);
        #endregion

        #region Client Statement
        [OperationContract]
        int addClientStatement(ClientStatement _clientStatement);
        [OperationContract]
        ClientStatement getStatementNumberByClient(int _clientID);
         [OperationContract]
        string getStatementNumberByStatementID(int StatementID);
        #endregion

        #region Account Receivable
         [OperationContract]
         DTO.Paged.PagedBillingAccountReceivables getAccountReceivablesByClientName(string ClientName, int skip, int take);
        #endregion

    }
}
