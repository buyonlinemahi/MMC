using AutoMapper;
using MMC.Core.BL;
using MMCService.CustomServiceBehaviors;
using MMCService.DTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace MMCService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AutoMapperServiceBehavior]
    public class BillingService : IBillingService
    {
        public IBillingRepository _BillingRepository;
        public BillingService(IBillingRepository iBillingRepository)
        {
            _BillingRepository = iBillingRepository;
        }

        public DTO.Paged.PagedBilling getDetailsForBilling(int skip, int take)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.Billing, DTO.Paged.PagedBilling>(_BillingRepository.getBillingDetails(skip,take));
        }

        public int updateRFARequestBilling(RFARequestBilling _RFARequestBilling)
        {
            return _BillingRepository.updateRFARequestBilling(Mapper.Map<MMC.Core.Data.Model.RFARequestBilling>(_RFARequestBilling));
        }

        public int addRFARequestBilling(RFARequestBilling _RFARequestBilling)
        {
            return _BillingRepository.addRFARequestBilling(Mapper.Map<MMC.Core.Data.Model.RFARequestBilling>(_RFARequestBilling));
        }

        public DTO.Paged.PagedBilling getDetailsForBillingByClientName(string ClientName,int skip, int take)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.Billing, DTO.Paged.PagedBilling>(_BillingRepository.getBillingDetailByClientName(ClientName, skip, take));
        }



        #region RFA Referral Invoice

        public string addRFAReferralInvoice(DTO.RFAReferralInvoice _rRFAReferralInvoice)
        {
            return _BillingRepository.addRFAReferralInvoice(Mapper.Map<MMC.Core.Data.Model.RFAReferralInvoice>(_rRFAReferralInvoice));
        }
        public DTO.RFAReferralInvoice getInvoiceNumberByClientID(int _ClientID)
        {
            return Mapper.Map<MMC.Core.Data.Model.RFAReferralInvoice, DTO.RFAReferralInvoice>(_BillingRepository.getInvoiceNumberByClientID(_ClientID));
        }
        public int getInvoiceIDByInvoiceNumber(string InvoiceNumber)
        {
            return _BillingRepository.getInvoiceIDByInvoiceNumber(InvoiceNumber);
        }

        #endregion

        #region Client Statement

        public int addClientStatement(DTO.ClientStatement _clientStatement)
        {
            return _BillingRepository.addClientStatement(Mapper.Map<MMC.Core.Data.Model.ClientStatement>(_clientStatement));
        }

        public DTO.ClientStatement getStatementNumberByClient(int _clientID)
        {
            return Mapper.Map<MMC.Core.Data.Model.ClientStatement, DTO.ClientStatement>(_BillingRepository.getStatementNumberByClient(_clientID));
        }
        public string getStatementNumberByStatementID(int _statementID)
        {
            return (_BillingRepository.getStatementNumberByStatementID(_statementID));
        }
       

        #endregion

        #region Account Receivable
        public DTO.Paged.PagedBillingAccountReceivables getAccountReceivablesByClientName(string ClientName, int skip, int take)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.BillingAccountReceivables, DTO.Paged.PagedBillingAccountReceivables>(_BillingRepository.getAccountReceivablesByClientName(ClientName, skip, take));
        }
        #endregion
    }
}
