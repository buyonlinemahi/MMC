using MMC.Core.Data.Model;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL
{
    public interface IClientRepository
    {
        #region Client

        int addClient(Client _client);
        int updateClient(Client _client);
        void deleteClient(int _clientid);
        Client getClientByID(int _clientid);
        BLModel.Paged.Client getClientByName(string SearchText, int _skip, int _take);
        IEnumerable<Client> getClientAll();
        BLModel.Client getClaimAdministratorByClientID(int ClientID);
        IEnumerable<BLModel.Client> getAllClaimAdministrator();
        IEnumerable<BLModel.AdjusterByClientID> getAdjusterByClientID(int _clientid);
        IEnumerable<BLModel.ClaimAdministratorAllByClientID> getClaimAdministratorAllByClientID(int _clientid);

        #endregion

        #region Client Insurer

        int addClientInsurer(ClientInsurer _clientInsurer);
        int updateClientInsurer(ClientInsurer _clientInsurer);
        void deleteClientInsurer(int _clientInsurerID);
        BLModel.Paged.ClientInsurer getClientInsurerByClientID(int ClientID, int _skip, int _take);
        IEnumerable<BLModel.ClientInsurer> getAllClientInsurerByClientID(int ClientID);
        #endregion

        #region Client Insurer Branch

        int addClientInsuranceBranch(ClientInsuranceBranch _clientInsuranceBranch);
        int updateClientInsuranceBranch(ClientInsuranceBranch _clientInsuranceBranch);
        void deleteClientInsuranceBranch(int _clientInsuranceBranchID);
        void deleteClientInsuranceBranchByInsurerID(int _insurerID);
        IEnumerable<BLModel.ClientInsuranceBranch> getClientInsuranceBranchesAllByInsurerID(int _clientID, int _insurerID);
        #endregion

        #region Client Employer

        int addClientEmployer(ClientEmployer _clientEmployer);
        int updateClientEmployer(ClientEmployer _clientEmployer);
        void deleteClientEmployer(int _clientEmployerID);
        BLModel.Paged.ClientEmployer getClientEmployerByClientID(int ClientID, int _skip, int _take);
        IEnumerable<BLModel.ClientEmployer> getAllClientEmployerByClientID(int ClientID);
        #endregion
        
        #region Client Third Party Administrator

        int addClientThirdPartyAdministrator(ClientThirdPartyAdministrator _clientThirdPartyAdministrator);
        int updateClientThirdPartyAdministrator(ClientThirdPartyAdministrator _clientThirdPartyAdministrator);
        void deleteClientThirdPartyAdministrator(int _clientTPAID);
        BLModel.Paged.ClientThirdPartyAdministrator getClientThirdPartyAdministratorByClientID(int ClientID, int _skip, int _take);
        IEnumerable<BLModel.ClientThirdPartyAdministrator> getAllClientThirdPartyAdministratorByClientID(int ClientID);
        
        #endregion

        #region Client Third Party Administrator Branch

        int addClientThirdPartyAdministratorBranch(ClientThirdPartyAdministratorBranch _clientThirdPartyAdministratorBranch);
        int updateClientThirdPartyAdministratorBranch(ClientThirdPartyAdministratorBranch _clientThirdPartyAdministratorBranch);
        void deleteClientThirdPartyAdministratorBranch(int _clientTPABranchID);
        void deleteClientThirdPartyAdministratorBranchByTPAID(int _TPAID);
        IEnumerable<BLModel.ClientThirdPartyAdministratorBranch> getClientTPABranchesAllByTPAID(int _clientID, int _tPAID);
        #endregion
        
        #region Client Managed Care Company
        
        int addClientManagedCareCompany(ClientManagedCareCompany _clientManagedCareCompany);
        int updateClientManagedCareCompany(ClientManagedCareCompany _clientManagedCareCompany);
        void deleteClientManagedCareCompany(int _clientManagedCareCompanyID);
        BLModel.Paged.ClientManagedCareCompany getClientManagedCareCompanyByClientID(int ClientID, int _skip, int _take);
        int updateClientManagedCareCompanyByClientID(ClientManagedCareCompany _clientManagedCareCompany);
        #endregion

        #region Client Billing

        int addClientBilling(ClientBilling _clientBilling);
        int updateClientBilling(ClientBilling _clientBilling);
        void deleteClientBilling(int _id);
        ClientBilling getClientBillingByID(int _id);
        ClientBilling getClientBillingDetailByClientID(int _clientID);

        #endregion

        #region Client Billing Retail Rate

        int addClientBillingRetailRate(ClientBillingRetailRate _clientBillingRetailRate);
        int updateClientBillingRetailRate(ClientBillingRetailRate _clientBillingRetailRate);
        void deleteClientBillingRetailRate(int _id);
        ClientBillingRetailRate getClientBillingRetailRateByID(int _id);
        ClientBillingRetailRate getClientBillingRetailRateByClientBillingID(int _clientBillingID);

        #endregion

        #region Client Billing Wholesale Rate

        int addClientBillingWholesaleRate(ClientBillingWholesaleRate _clientBillingWholesaleRate);
        int updateClientBillingWholesaleRate(ClientBillingWholesaleRate _clientBillingWholesaleRate);
        void deleteClientBillingWholesaleRate(int _id);
        ClientBillingWholesaleRate getClientBillingWholesaleRateByID(int _id);
        ClientBillingWholesaleRate getClientBillingWholesaleRateByClientBillingID(int _clientBillingID);

        #endregion


        #region Client Private Label

        int addClientPrivateLabel(ClientPrivateLabel _clientPrivateLabel);
        int updateClientPrivateLabel(ClientPrivateLabel _clientPrivateLabel);
        void deleteClientPrivateLabel(int _id);
        void deleteClientPrivateLabelByClientID(int _clientID);
        ClientPrivateLabel getClientPrivateLabelByID(int _id);
        ClientPrivateLabel getClientPrivateLabelDetailByClientID(int _clientID);

        #endregion

    }
}
