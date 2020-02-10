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
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository iClientRepository)
        {
            _clientRepository = iClientRepository;
        }

        #region Client

        public int addClient(Client _Client)
        {
            return _clientRepository.addClient(Mapper.Map<DTO.Client, MMC.Core.Data.Model.Client>(_Client));
        }
        public int updateClient(Client _Client)
        {
            return _clientRepository.updateClient(Mapper.Map<DTO.Client, MMC.Core.Data.Model.Client>(_Client));
        }
        public void deleteClient(int _ClientId)
        {
            _clientRepository.deleteClient(_ClientId);
        }
        public DTO.Client getClientByID(int _ClientId)
        {
            return Mapper.Map<DTO.Client>(_clientRepository.getClientByID(_ClientId));
        }

        public DTO.Paged.PagedClient getClientByName(string SearchText, int _skip, int _take)
        {
            return Mapper.Map<DTO.Paged.PagedClient>(_clientRepository.getClientByName(SearchText, _skip, _take));
        }

        public IEnumerable<DTO.Client> getClientAll()
        {
            return Mapper.Map<IEnumerable<DTO.Client>>(_clientRepository.getClientAll());
        }


        public IEnumerable<DTO.AdjusterByClientID> getAdjusterByClientID(int ClientID)
        {
            return Mapper.Map<IEnumerable<DTO.AdjusterByClientID>>(_clientRepository.getAdjusterByClientID(ClientID));
        }
        public IEnumerable<DTO.ClaimAdministratorAllByClientID> getClaimAdministratorAllByClientID(int ClientID)
        {
            return Mapper.Map<IEnumerable<DTO.ClaimAdministratorAllByClientID>>(_clientRepository.getClaimAdministratorAllByClientID(ClientID));
        }

        #endregion

        #region Client Insurer
        public int addClientInsurer(DTO.ClientInsurer _clientInsurer)
        {
            return _clientRepository.addClientInsurer(Mapper.Map<DTO.ClientInsurer, MMC.Core.Data.Model.ClientInsurer>(_clientInsurer));
        }
        public int updateClientInsurer(DTO.ClientInsurer _clientInsurer)
        {
            return _clientRepository.updateClientInsurer(Mapper.Map<DTO.ClientInsurer, MMC.Core.Data.Model.ClientInsurer>(_clientInsurer));
        }
        public void deleteClientInsurer(int _clientInsurerID)
        {
            _clientRepository.deleteClientInsurer(_clientInsurerID);
        }
        public DTO.Paged.PagedClientInsurer getClientInsurerByClientID(int ClientID, int _skip, int _take)
        {
            return Mapper.Map<DTO.Paged.PagedClientInsurer>(_clientRepository.getClientInsurerByClientID(ClientID, _skip, _take));
        }
        public IEnumerable<ClientInsurer> getAllClientInsurerByClientID(int ClientID)
        {
            return Mapper.Map<IEnumerable<DTO.ClientInsurer>>(_clientRepository.getAllClientInsurerByClientID(ClientID));
        }


        #endregion

        #region Client Insurer Branch

        public int addClientInsuranceBranch(DTO.ClientInsuranceBranch _clientInsuranceBranch)
        {
            return _clientRepository.addClientInsuranceBranch(Mapper.Map<DTO.ClientInsuranceBranch, MMC.Core.Data.Model.ClientInsuranceBranch>(_clientInsuranceBranch));
        }
        public int updateClientInsuranceBranch(DTO.ClientInsuranceBranch _clientInsuranceBranch)
        {
            return _clientRepository.updateClientInsuranceBranch(Mapper.Map<DTO.ClientInsuranceBranch, MMC.Core.Data.Model.ClientInsuranceBranch>(_clientInsuranceBranch));
        }

        public void deleteClientInsuranceBranch(int _clientInsuranceBranchID)
        {
            _clientRepository.deleteClientInsuranceBranch(_clientInsuranceBranchID);
        }
        public void deleteClientInsuranceBranchByInsurerID(int _insurerID)
        {
            _clientRepository.deleteClientInsuranceBranchByInsurerID(_insurerID);
        }

        public IEnumerable<DTO.ClientInsuranceBranch> getClientInsuranceBranchesAllByInsurerID(int _ClientID, int _insurerID)
        {
            return Mapper.Map<IEnumerable<DTO.ClientInsuranceBranch>>(_clientRepository.getClientInsuranceBranchesAllByInsurerID(_ClientID, _insurerID));
        }

        #endregion

        #region Client Employer

        public int addClientEmployer(DTO.ClientEmployer _clientEmployer)
        {
            return _clientRepository.addClientEmployer(Mapper.Map<DTO.ClientEmployer, MMC.Core.Data.Model.ClientEmployer>(_clientEmployer));
        }
        public int updateClientEmployer(DTO.ClientEmployer _clientEmployer)
        {
            return _clientRepository.updateClientEmployer(Mapper.Map<DTO.ClientEmployer, MMC.Core.Data.Model.ClientEmployer>(_clientEmployer));
        }
        public void deleteClientEmployer(int _clientEmployerID)
        {
            _clientRepository.deleteClientEmployer(_clientEmployerID);
        }

        public DTO.Paged.PagedClientEmployer getClientEmployerByClientID(int ClientID, int _skip, int _take)
        {
            return Mapper.Map<DTO.Paged.PagedClientEmployer>(_clientRepository.getClientEmployerByClientID(ClientID, _skip, _take));
        }
        public IEnumerable<ClientEmployer> getAllClientEmployerByClientID(int ClientID)
        {
            return Mapper.Map<IEnumerable<DTO.ClientEmployer>>(_clientRepository.getAllClientEmployerByClientID(ClientID));
        }

        #endregion

        #region Client Third Party Administrator

        public int addClientThirdPartyAdministrator(DTO.ClientThirdPartyAdministrator _clientThirdPartyAdministrator)
        {
            return _clientRepository.addClientThirdPartyAdministrator(Mapper.Map<DTO.ClientThirdPartyAdministrator, MMC.Core.Data.Model.ClientThirdPartyAdministrator>(_clientThirdPartyAdministrator));
        }
        public int updateClientThirdPartyAdministrator(DTO.ClientThirdPartyAdministrator _clientThirdPartyAdministrator)
        {
            return _clientRepository.updateClientThirdPartyAdministrator(Mapper.Map<DTO.ClientThirdPartyAdministrator, MMC.Core.Data.Model.ClientThirdPartyAdministrator>(_clientThirdPartyAdministrator));
        }
        public void deleteClientThirdPartyAdministrator(int _clientTPAID)
        {
            _clientRepository.deleteClientThirdPartyAdministrator(_clientTPAID);
        }

        public DTO.Paged.PagedClientThirdPartyAdministrator getClientThirdPartyAdministratorByClientID(int ClientID, int _skip, int _take)
        {
            return Mapper.Map<DTO.Paged.PagedClientThirdPartyAdministrator>(_clientRepository.getClientThirdPartyAdministratorByClientID(ClientID, _skip, _take));
        }
        public IEnumerable<ClientThirdPartyAdministrator> getAllClientThirdPartyAdministratorByClientID(int ClientID)
        {
            return Mapper.Map<IEnumerable<DTO.ClientThirdPartyAdministrator>>(_clientRepository.getAllClientThirdPartyAdministratorByClientID(ClientID));
        }

        #endregion

        #region Client Third Party Administrator Branch

        public int addClientThirdPartyAdministratorBranch(DTO.ClientThirdPartyAdministratorBranch _clientThirdPartyAdministratorBranch)
        {
            return _clientRepository.addClientThirdPartyAdministratorBranch(Mapper.Map<DTO.ClientThirdPartyAdministratorBranch, MMC.Core.Data.Model.ClientThirdPartyAdministratorBranch>(_clientThirdPartyAdministratorBranch));
        }
        public int updateClientThirdPartyAdministratorBranch(DTO.ClientThirdPartyAdministratorBranch _clientThirdPartyAdministratorBranch)
        {
            return _clientRepository.updateClientThirdPartyAdministratorBranch(Mapper.Map<DTO.ClientThirdPartyAdministratorBranch, MMC.Core.Data.Model.ClientThirdPartyAdministratorBranch>(_clientThirdPartyAdministratorBranch));
        }
        public void deleteClientThirdPartyAdministratorBranch(int _clientTPABranchID)
        {
            _clientRepository.deleteClientThirdPartyAdministratorBranch(_clientTPABranchID);
        }
        public void deleteClientThirdPartyAdministratorBranchByTPAID(int _TPAID)
        {
            _clientRepository.deleteClientThirdPartyAdministratorBranchByTPAID(_TPAID);
        }
        public IEnumerable<DTO.ClientThirdPartyAdministratorBranch> getClientTPABranchesAllByTPAID(int _ClientID, int _tpaID)
        {
            return Mapper.Map<IEnumerable<DTO.ClientThirdPartyAdministratorBranch>>(_clientRepository.getClientTPABranchesAllByTPAID(_ClientID, _tpaID));
        }
        #endregion

        #region Client Managed Care Company
        public int addClientManagedCareCompany(DTO.ClientManagedCareCompany _clientManagedCareCompany)
        {
            return _clientRepository.addClientManagedCareCompany(Mapper.Map<DTO.ClientManagedCareCompany, MMC.Core.Data.Model.ClientManagedCareCompany>(_clientManagedCareCompany));
        }
        public int updateClientManagedCareCompany(DTO.ClientManagedCareCompany _ClientManagedCareCompany)
        {
            return _clientRepository.updateClientManagedCareCompany(Mapper.Map<DTO.ClientManagedCareCompany, MMC.Core.Data.Model.ClientManagedCareCompany>(_ClientManagedCareCompany));
        }
        public void deleteClientManagedCareCompany(int _clientManagedCareCompanyID)
        {
            _clientRepository.deleteClientManagedCareCompany(_clientManagedCareCompanyID);
        }
        public DTO.Client getClaimAdministratorByClientID(int ClientID)
        {
            return Mapper.Map<DTO.Client>(_clientRepository.getClaimAdministratorByClientID(ClientID));
        }
        public IEnumerable<DTO.Client> getAllClaimAdministrator()
        {
            return Mapper.Map<IEnumerable<DTO.Client>>(_clientRepository.getAllClaimAdministrator());
        }
        public int updateClientManagedCareCompanyByClientID(DTO.ClientManagedCareCompany _clientManagedCareCompany)
        {
            return _clientRepository.updateClientManagedCareCompanyByClientID(Mapper.Map<DTO.ClientManagedCareCompany, MMC.Core.Data.Model.ClientManagedCareCompany>(_clientManagedCareCompany));
        }
        public DTO.Paged.PagedClientManagedCareCompany getClientManagedCareCompanyByClientID(int ClientID, int _skip, int _take)
        {
            return Mapper.Map<DTO.Paged.PagedClientManagedCareCompany>(_clientRepository.getClientManagedCareCompanyByClientID(ClientID, _skip, _take));
        }
        #endregion

        #region Client Billing
        public int addClientBilling(ClientBilling _ClientBilling)
        {
            return _clientRepository.addClientBilling(Mapper.Map<DTO.ClientBilling, MMC.Core.Data.Model.ClientBilling>(_ClientBilling));
        }
        public int updateClientBilling(ClientBilling _ClientBilling)
        {
            return _clientRepository.updateClientBilling(Mapper.Map<DTO.ClientBilling, MMC.Core.Data.Model.ClientBilling>(_ClientBilling));
        }
        public void deleteClientBilling(int _id)
        {
            _clientRepository.deleteClientBilling(_id);
        }
        public DTO.ClientBilling getClientBillingByID(int _id)
        {
            return Mapper.Map<DTO.ClientBilling>(_clientRepository.getClientBillingByID(_id));
        }

        public DTO.ClientBilling getClientBillingDetailByClientID(int _clientID)
        {
            return Mapper.Map<DTO.ClientBilling>(_clientRepository.getClientBillingDetailByClientID(_clientID));
        }


        #endregion

        #region  Client Billing Retail Rate

        public int addClientBillingRetailRate(DTO.ClientBillingRetailRate _ClientBillingRetailRate)
        {
            return _clientRepository.addClientBillingRetailRate(Mapper.Map<DTO.ClientBillingRetailRate, MMC.Core.Data.Model.ClientBillingRetailRate>(_ClientBillingRetailRate));
        }
        public int updateClientBillingRetailRate(DTO.ClientBillingRetailRate _ClientBillingRetailRate)
        {
            return _clientRepository.updateClientBillingRetailRate(Mapper.Map<DTO.ClientBillingRetailRate, MMC.Core.Data.Model.ClientBillingRetailRate>(_ClientBillingRetailRate));
        }
        public void deleteClientBillingRetailRate(int _id)
        {
            _clientRepository.deleteClientBillingRetailRate(_id);
        }
        public ClientBillingRetailRate getClientBillingRetailRateByID(int _id)
        {
            return Mapper.Map<DTO.ClientBillingRetailRate>(_clientRepository.getClientBillingRetailRateByID(_id));
        }
        public ClientBillingRetailRate getClientBillingRetailRateByClientBillingID(int _clientBillingID)
        {
            return Mapper.Map<ClientBillingRetailRate>(_clientRepository.getClientBillingRetailRateByClientBillingID(_clientBillingID));
        }



        #endregion

        #region  Client Billing Wholesale Rate

        public int addClientBillingWholesaleRate(DTO.ClientBillingWholesaleRate _ClientBillingWholesaleRate)
        {
            return _clientRepository.addClientBillingWholesaleRate(Mapper.Map<DTO.ClientBillingWholesaleRate, MMC.Core.Data.Model.ClientBillingWholesaleRate>(_ClientBillingWholesaleRate));
        }
        public int updateClientBillingWholesaleRate(DTO.ClientBillingWholesaleRate _ClientBillingWholesaleRate)
        {
            return _clientRepository.updateClientBillingWholesaleRate(Mapper.Map<DTO.ClientBillingWholesaleRate, MMC.Core.Data.Model.ClientBillingWholesaleRate>(_ClientBillingWholesaleRate));
        }
        public void deleteClientBillingWholesaleRate(int _id)
        {
            _clientRepository.deleteClientBillingWholesaleRate(_id);
        }
        public ClientBillingWholesaleRate getClientBillingWholesaleRateByID(int _id)
        {
            return Mapper.Map<DTO.ClientBillingWholesaleRate>(_clientRepository.getClientBillingWholesaleRateByID(_id));
        }
        public ClientBillingWholesaleRate getClientBillingWholesaleRateByClientBillingID(int _clientBillingID)
        {
            return Mapper.Map<ClientBillingWholesaleRate>(_clientRepository.getClientBillingWholesaleRateByClientBillingID(_clientBillingID));
        }



        #endregion


        #region Client Private Label

        public int addClientPrivateLabel(ClientPrivateLabel _clientPrivateLabel)
        {
            return _clientRepository.addClientPrivateLabel(Mapper.Map<DTO.ClientPrivateLabel, MMC.Core.Data.Model.ClientPrivateLabel>(_clientPrivateLabel));
        }
        public int updateClientPrivateLabel(ClientPrivateLabel _clientPrivateLabel)
        {
            return _clientRepository.updateClientPrivateLabel(Mapper.Map<DTO.ClientPrivateLabel, MMC.Core.Data.Model.ClientPrivateLabel>(_clientPrivateLabel));
        }
        public void deleteClientPrivateLabel(int _id)
        {
            _clientRepository.deleteClientPrivateLabel(_id);
        }
        public void deleteClientPrivateLabelByClientID(int _clientID)
        {
            _clientRepository.deleteClientPrivateLabelByClientID(_clientID);
        }
        public DTO.ClientPrivateLabel getClientPrivateLabelByID(int _id)
        {
            return Mapper.Map<DTO.ClientPrivateLabel>(_clientRepository.getClientPrivateLabelByID(_id));
        }
        public DTO.ClientPrivateLabel getClientPrivateLabelDetailByClientID(int _clientID)
        {
            return Mapper.Map<DTO.ClientPrivateLabel>(_clientRepository.getClientPrivateLabelDetailByClientID(_clientID));
        }

        #endregion

    }
}
