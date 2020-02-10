using MMCService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MMCService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IClientService" in both code and config file together.
    [ServiceContract]
    public interface IClientService
    {
        #region Client

        [OperationContract]
        int addClient(DTO.Client _Client);
        [OperationContract]
        int updateClient(DTO.Client _Client);
        [OperationContract]
        void deleteClient(int _ClientId);
        [OperationContract]
        DTO.Client getClientByID(int _ClientId);
        [OperationContract]
        DTO.Paged.PagedClient getClientByName(string SearchText, int _skip, int _take);
        [OperationContract]
        IEnumerable<DTO.Client> getClientAll();
        [OperationContract]
        DTO.Client getClaimAdministratorByClientID(int ClientID);
        [OperationContract]
        IEnumerable<DTO.AdjusterByClientID> getAdjusterByClientID(int ClientID);
        [OperationContract]
        IEnumerable<DTO.ClaimAdministratorAllByClientID> getClaimAdministratorAllByClientID(int _clientid);
        #endregion

        #region Client Insurer

        [OperationContract]
        int addClientInsurer(DTO.ClientInsurer _clientInsurer);
        [OperationContract]
        int updateClientInsurer(DTO.ClientInsurer _clientInsurer);
        [OperationContract]
        void deleteClientInsurer(int _clientInsurerID);
        [OperationContract]
        DTO.Paged.PagedClientInsurer getClientInsurerByClientID(int ClientID, int _skip, int _take);
        [OperationContract]
        IEnumerable<ClientInsurer> getAllClientInsurerByClientID(int ClientID);
        #endregion

        #region Client Insurer Branch
        [OperationContract]
        int addClientInsuranceBranch(DTO.ClientInsuranceBranch _clientInsuranceBranch);
        [OperationContract]
        int updateClientInsuranceBranch(DTO.ClientInsuranceBranch _clientInsuranceBranch);
        [OperationContract]
        void deleteClientInsuranceBranch(int _clientInsuranceBranchID);
        [OperationContract]
        void deleteClientInsuranceBranchByInsurerID(int _insurerID);
        [OperationContract]
        IEnumerable<DTO.ClientInsuranceBranch> getClientInsuranceBranchesAllByInsurerID(int _ClientID, int _insurerID);
        #endregion

        #region Client Employer

        [OperationContract]
        int addClientEmployer(DTO.ClientEmployer _clientEmployer);
        [OperationContract]
        int updateClientEmployer(DTO.ClientEmployer _clientEmployer);
        [OperationContract]
        void deleteClientEmployer(int _clientEmployerID);
        [OperationContract]
        DTO.Paged.PagedClientEmployer getClientEmployerByClientID(int ClientID, int _skip, int _take);
        [OperationContract]
        IEnumerable<ClientEmployer> getAllClientEmployerByClientID(int ClientID);

        #endregion

        #region Client Third Party Administrator

        [OperationContract]
        int addClientThirdPartyAdministrator(DTO.ClientThirdPartyAdministrator _clientThirdPartyAdministrator);
        [OperationContract]
        int updateClientThirdPartyAdministrator(DTO.ClientThirdPartyAdministrator _clientThirdPartyAdministrator);
        [OperationContract]
        void deleteClientThirdPartyAdministrator(int _clientTPAID);
        [OperationContract]
        DTO.Paged.PagedClientThirdPartyAdministrator getClientThirdPartyAdministratorByClientID(int ClientID, int _skip, int _take);
        [OperationContract]
        IEnumerable<ClientThirdPartyAdministrator> getAllClientThirdPartyAdministratorByClientID(int ClientID);
        #endregion

        #region Client Third Party Administrator Branch
        [OperationContract]
        int addClientThirdPartyAdministratorBranch(DTO.ClientThirdPartyAdministratorBranch _clientThirdPartyAdministratorBranch);
        [OperationContract]
        int updateClientThirdPartyAdministratorBranch(DTO.ClientThirdPartyAdministratorBranch _clientThirdPartyAdministratorBranch);
        [OperationContract]
        void deleteClientThirdPartyAdministratorBranch(int _clientTPABranchID);
        [OperationContract]
        void deleteClientThirdPartyAdministratorBranchByTPAID(int _TPAID);
        [OperationContract]
        IEnumerable<DTO.ClientThirdPartyAdministratorBranch> getClientTPABranchesAllByTPAID(int _ClientID, int _tpaID);
        #endregion

        #region Client Managed Care Company

        [OperationContract]
        int addClientManagedCareCompany(DTO.ClientManagedCareCompany _clientManagedCareCompany);
        [OperationContract]
        int updateClientManagedCareCompany(DTO.ClientManagedCareCompany _clientManagedCareCompany);
        [OperationContract]
        void deleteClientManagedCareCompany(int _clientManagedCareCompanyID);
        [OperationContract]
        DTO.Paged.PagedClientManagedCareCompany getClientManagedCareCompanyByClientID(int ClientID, int _skip, int _take);
        [OperationContract]
        IEnumerable<DTO.Client> getAllClaimAdministrator();
        [OperationContract]
        int updateClientManagedCareCompanyByClientID(DTO.ClientManagedCareCompany _clientManagedCareCompany);
        #endregion

        #region Client Billing
        [OperationContract]
        int addClientBilling(ClientBilling _clientBilling);
        [OperationContract]
        int updateClientBilling(ClientBilling _clientBilling);
        [OperationContract]
        void deleteClientBilling(int _id);
        [OperationContract]
        ClientBilling getClientBillingByID(int _id);
        [OperationContract]
        ClientBilling getClientBillingDetailByClientID(int _clientID);
        #endregion

        #region Client Billing Retail Rate
        [OperationContract]
        int addClientBillingRetailRate(ClientBillingRetailRate _clientBillingRetailRate);
        [OperationContract]
        int updateClientBillingRetailRate(ClientBillingRetailRate _clientBillingRetailRate);
        [OperationContract]
        void deleteClientBillingRetailRate(int _id);
        [OperationContract]
        ClientBillingRetailRate getClientBillingRetailRateByID(int _id);
        [OperationContract]
        ClientBillingRetailRate getClientBillingRetailRateByClientBillingID(int _clientBillingID);


        #endregion

        #region Client Billing Wholesale Rate
        [OperationContract]
        int addClientBillingWholesaleRate(ClientBillingWholesaleRate _clientBillingWholesaleRate);
        [OperationContract]
        int updateClientBillingWholesaleRate(ClientBillingWholesaleRate _clientBillingWholesaleRate);
        [OperationContract]
        void deleteClientBillingWholesaleRate(int _id);
        [OperationContract]
        ClientBillingWholesaleRate getClientBillingWholesaleRateByID(int _id);
        [OperationContract]
        ClientBillingWholesaleRate getClientBillingWholesaleRateByClientBillingID(int _clientBillingID);

        #endregion

        #region Client Private Label
        [OperationContract]
        int addClientPrivateLabel(ClientPrivateLabel _clientPrivateLabel);
        [OperationContract]
        int updateClientPrivateLabel(ClientPrivateLabel _clientPrivateLabel);
        [OperationContract]
        void deleteClientPrivateLabel(int _id);
        [OperationContract]
        void deleteClientPrivateLabelByClientID(int _clientID);
        [OperationContract]
        ClientPrivateLabel getClientPrivateLabelByID(int _id);
        [OperationContract]
        ClientPrivateLabel getClientPrivateLabelDetailByClientID(int _clientID);
        
        #endregion

    }
}

