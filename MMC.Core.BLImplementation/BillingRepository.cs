using MMC.Core.BL;
using MMC.Core.BLImplementation.SPImplementation;
using MMC.Core.Data.Model;
using MMC.Core.Data.SQLServer;
using System.Linq;
using BLModel = MMC.Core.BL.Model;
using Omu.ValueInjecter;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using MMC.Core.BLImplementation.Global;


namespace MMC.Core.BLImplementation
{
    public class BillingRepository : IBillingRepository
    {
        private readonly BaseRepository<RFARequestBilling> _rfaRequestBillingRepo;
        private readonly BaseRepository<RFAReferralInvoice> _rFAReferralInvoiceRepo;
        private readonly BaseRepository<ClientStatement> _clientStatementRepo;

        public BillingRepository()
        {
            _rfaRequestBillingRepo = new BaseRepository<RFARequestBilling>();
            _rFAReferralInvoiceRepo = new BaseRepository<RFAReferralInvoice>();
            _clientStatementRepo = new BaseRepository<ClientStatement>();
        }

        public BL.Model.Paged.Billing getBillingDetails(int _skip, int _take)
        {
            SPImpl _spImpl = new SPImpl();
            return new BLModel.Paged.Billing
            {
                BillingDetails = _spImpl.GetBillingDetails(_skip, _take).ToList(),
                TotalCount = _spImpl.GetBillingDetailsCount()
            };
        }
        public BL.Model.Paged.Billing getBillingDetailByClientName(string ClientName, int _skip, int _take)
        {
            SPImpl _spImpl = new SPImpl();
            return new BLModel.Paged.Billing
            {
                BillingDetails = _spImpl.GetBillingDetailsByClientName(ClientName, _skip, _take).ToList(),
                TotalCount = _spImpl.GetBillingDetailsByClientNameCount(ClientName)
            };
        }
        public int updateRFARequestBilling(RFARequestBilling _rfaRequestBilling)
        {
            try
            {
                return _rfaRequestBillingRepo.Update(_rfaRequestBilling, rb => rb.RFARequestBillingRN, rb => rb.RFARequestBillingMD, rb => rb.RFARequestBillingSpeciality, rb => rb.RFARequestBillingID, rb => rb.RFARequestBillingAdmin, rb => rb.RFARequestBillingDeferral);
            }
            catch
            {
                return 0;
            }
        }

        public int addRFARequestBilling(RFARequestBilling _rfaRequestBilling)
        {
            return _rfaRequestBillingRepo.Add(_rfaRequestBilling).RFARequestBillingID;
        }       

        #region RFA Referral Invoice       

        public RFAReferralInvoice getInvoiceNumberByClientID(int ClientID)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _getInvoiceNumber = (from _invData in _MMCDbContext.rFAReferralInvoices
                                     where _invData.ClientID == ClientID
                                     orderby _invData.RFAReferralInvoiceID descending
                                     select new
                                     {
                                         _invData.RFAReferralInvoiceID,
                                         _invData.PatientClaimID,
                                         _invData.InvoiceNumber,
                                         _invData.ClientID
                                     }).ToList();
            return _getInvoiceNumber.Select(inv => new RFAReferralInvoice().InjectFrom(inv)).Cast<RFAReferralInvoice>().FirstOrDefault();
        }
        public int getInvoiceIDByInvoiceNumber(string InvoiceNumber)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _getInvoiceID = (from _invData in _MMCDbContext.rFAReferralInvoices
                                     where _invData.InvoiceNumber == InvoiceNumber
                                     select new
                                     {
                                       _invData.RFAReferralInvoiceID                                        
                                     }).Single();
            return Convert.ToInt32(_getInvoiceID.RFAReferralInvoiceID);
        }
        
        public string addRFAReferralInvoice(RFAReferralInvoice _rRFAReferralInvoice)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            string getIncrementInvoiceNumber = "";
            var billingDetails = getInvoiceNumberByClientID(_rRFAReferralInvoice.ClientID.Value);

            if (billingDetails == null)
            {
                var _getInvoiceNumber = (from _clientBill in _MMCDbContext.clientBillings
                                         join _client in _MMCDbContext.clients
                                         on _clientBill.ClientID equals _client.ClientID
                                         join _patClaim in _MMCDbContext.patientClaims
                                         on _client.ClientID equals _patClaim.PatClientID
                                         where _patClaim.PatientClaimID == _rRFAReferralInvoice.PatientClaimID
                                         select new
                                         {
                                             _clientBill.ClientInvoiceNumber
                                         }).FirstOrDefault();
                getIncrementInvoiceNumber = _getInvoiceNumber.ClientInvoiceNumber.ToString();
            }
            else
            {
                getIncrementInvoiceNumber = IncrementInvoice(billingDetails.InvoiceNumber);
            }

            _rRFAReferralInvoice.InvoiceNumber = getIncrementInvoiceNumber;
            return _rFAReferralInvoiceRepo.Add(_rRFAReferralInvoice).InvoiceNumber;             
        }


       
        #endregion

        #region RFA Client Statement

        public ClientStatement getStatementNumberByClient(int _clientID)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _getStatementNumber = (from _stmtData in _MMCDbContext.ClientStatements
                                       where _stmtData.ClientID == _clientID
                                       orderby _stmtData.ClientStatementID descending
                                       select
                                       (
                                           _stmtData
                                       )).ToList();
            return _getStatementNumber.Select(inv => new ClientStatement().InjectFrom(inv)).Cast<ClientStatement>().FirstOrDefault();
        }


        public int addClientStatement(ClientStatement _clientStatement)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            string getIncrementStatementNumber = "";
            var statementDetails = getStatementNumberByClient(_clientStatement.ClientID);

            if (statementDetails == null)
            {
                var _getStatementNumber = (from _clientPrivateLabel in _MMCDbContext.clientPrivateLabelss
                                           where _clientStatement.ClientID == _clientPrivateLabel.ClientID
                                         select new
                                         {
                                             _clientPrivateLabel.ClientStatementStart
                                         }).FirstOrDefault();
                getIncrementStatementNumber = _getStatementNumber.ClientStatementStart.ToString();
            }
            else
            {
                getIncrementStatementNumber = IncrementInvoice(statementDetails.ClientStatementNumber);
            }

            _clientStatement.ClientStatementNumber = getIncrementStatementNumber;
           _clientStatement.ClientStatementFileName = _clientStatement.ClientStatementNumber + "_" + GlobalConst.FileName.InvoiceStatement + GlobalConst.Extension.pdf;
            return _clientStatementRepo.Add(_clientStatement).ClientStatementID;
        }

        public string getStatementNumberByStatementID(int StatementID)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _getStatementID = (from _stmtData in _MMCDbContext.ClientStatements
                                   where _stmtData.ClientStatementID == StatementID
                                 select new
                                 {
                                     _stmtData.ClientStatementNumber
                                 }).Single();
            return (_getStatementID.ClientStatementNumber).ToString();
        }
        #endregion

        #region IncrementNumericForInvoice
        //-----worked by mahinder on Invoice Increment method    
        public static string IncrementInvoice(string invoiceNumber)
        {   // ---Made by Mahinder Singh---
            List<string> alphaNumeric = new List<string>();
            List<string> digits = new List<string>();
            string invoiceNumIncrement = "";

            string invoiceNumberFinal = "";
            string foundZeros = "";
            long value;
            string invoicIncrementWithZeroFormat ="";

            for (int i = 0; i < invoiceNumber.Length; i++)
            {
                alphaNumeric.Add(invoiceNumber[i].ToString());
            }    

            string onlyNumbers = Regex.Replace(invoiceNumber, "[^0-9]", "");

            for (int i = 0; i < onlyNumbers.Length; i++)
            {
                foundZeros += "0";
            }

            if (onlyNumbers != "" && onlyNumbers != "00")
            {
                invoiceNumIncrement = (Convert.ToInt64(onlyNumbers) + 1).ToString();
                value = Convert.ToInt64(invoiceNumIncrement);
                invoicIncrementWithZeroFormat = value.ToString(foundZeros);

                for (int i = 0; i < invoicIncrementWithZeroFormat.Length; i++)
                {
                    digits.Add(invoicIncrementWithZeroFormat[i].ToString());
                }
            }
            else if (onlyNumbers == "00")
            {
                for (int i = 0; i < 1; i++)
                {
                    digits.Add("0");
                    alphaNumeric.Add("0");
                }
                digits.Add("1");
                alphaNumeric.Add("1");
            }
            else if (onlyNumbers == "")
            {
                for (int i = 0; i < 2; i++)
                {
                    digits.Add("0");
                    alphaNumeric.Add("0");
                }
            }

            foreach (var _numstring in alphaNumeric)
            {
                char forDigitCheck = Convert.ToChar(_numstring);
                bool digit = char.IsDigit(forDigitCheck);
                if (digit == true)
                {
                    foreach (var _digitval in digits)
                    {
                        int position = digits.IndexOf(_digitval);
                        invoiceNumberFinal += _digitval;
                        digits.RemoveAt(position);
                        break;
                    }
                }
                else
                {
                    invoiceNumberFinal += _numstring.ToString();
                }
            }

            foreach (var _digitval in digits)
            {
                int position = digits.IndexOf(_digitval);
                invoiceNumberFinal += _digitval;
                digits.RemoveAt(position);
                break;
            }
            return invoiceNumberFinal;
        }
        #endregion

        #region Account Recieveable
        public BL.Model.Paged.BillingAccountReceivables getAccountReceivablesByClientName(string ClientName, int _skip, int _take)
        {
            SPImpl _spImpl = new SPImpl();
            return new BLModel.Paged.BillingAccountReceivables
            {
                AccountReceivableDetails = _spImpl.GetAccountReceivablesByClientName(ClientName, _skip, _take).ToList(),
                TotalCount = _spImpl.GetAccountReceivablesByClientNameCount(ClientName)
            };
        }
        #endregion
    }
}
