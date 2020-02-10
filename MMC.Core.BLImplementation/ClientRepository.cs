using MMC.Core.BL;
using MMC.Core.BLImplementation.SPImplementation;
using MMC.Core.Data.Model;
using MMC.Core.Data.SQLServer;
using Omu.ValueInjecter;
using System.Collections.Generic;
using System.Linq;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BLImplementation
{
    public class ClientRepository : IClientRepository
    {
        private readonly BaseRepository<Client> _clientRepo;
        private readonly BaseRepository<ClientInsurer> _clientInsurerRepo;
        private readonly BaseRepository<ClientInsuranceBranch> _clientInsurerBranchRepo;
        private readonly BaseRepository<ClientEmployer> _clientEmployerRepo;
        private readonly BaseRepository<ClientThirdPartyAdministrator> _clientTPARepo;
        private readonly BaseRepository<ClientThirdPartyAdministratorBranch> _clientTPABranchRepo;
        private readonly BaseRepository<ClientManagedCareCompany> _clientMMCRepo;
        private readonly BaseRepository<ClientBilling> _clientBillingRepo;
        private readonly BaseRepository<ClientBillingRetailRate> _clientBillingRetailRateRepo;
        private readonly BaseRepository<ClientBillingWholesaleRate> _clientBillingWholesaleRateRepo;
        private readonly BaseRepository<ClientPrivateLabel> _clientPrivateLabelRepo;

        public ClientRepository()
        {
            _clientRepo = new BaseRepository<Client>();
            _clientInsurerRepo = new BaseRepository<ClientInsurer>();
            _clientInsurerBranchRepo = new BaseRepository<ClientInsuranceBranch>();
            _clientEmployerRepo = new BaseRepository<ClientEmployer>();
            _clientTPARepo = new BaseRepository<ClientThirdPartyAdministrator>();
            _clientTPABranchRepo = new BaseRepository<ClientThirdPartyAdministratorBranch>();
            _clientMMCRepo = new BaseRepository<ClientManagedCareCompany>();
            _clientBillingRepo = new BaseRepository<ClientBilling>();
            _clientBillingRetailRateRepo = new BaseRepository<ClientBillingRetailRate>();
            _clientBillingWholesaleRateRepo = new BaseRepository<ClientBillingWholesaleRate>();
            _clientPrivateLabelRepo = new BaseRepository<ClientPrivateLabel>();
        }

        #region Client 

        public int addClient(Client _Client)
        {
            if (_clientRepo.GetAll(cl => cl.ClientName == _Client.ClientName).Count() > 0)
                return 0;
            else
                return _clientRepo.Add(_Client).ClientID;
        }

        public int updateClient(Client _Client)
        {
            if (_clientRepo.GetAll(cl => cl.ClientName == _Client.ClientName && cl.ClientID != _Client.ClientID).Count() > 0)
                return 0;
            else
                return _clientRepo.Update(_Client);
        }

        public void deleteClient(int _id)
        {
            _clientRepo.Delete(_id);
        }

        public Client getClientByID(int _id)
        {
            return _clientRepo.GetById(_id);
        }

        public BLModel.Paged.Client getClientByName(string SearchText, int _skip, int _take)
        {
            SPImpl _spImpl = new SPImpl();
            return new BLModel.Paged.Client
            {
                ClientDetails =  _spImpl.getClientDetailsByName(SearchText,_skip,_take).ToList(),
                TotalCount = _clientRepo.GetAll(clnt => clnt.ClientName.StartsWith(SearchText)).Count()
            }; 
        } 

        public IEnumerable<Client> getClientAll()
        {
            return _clientRepo.GetAll();
        }

        public BLModel.Client getClaimAdministratorByClientID(int ClientID)
        {
            SPImpl _spImpl = new SPImpl();
            BLModel.Client _client = new BLModel.Client();
            _client = _spImpl.getClaimAdministratorByClientID(ClientID);
            return _client;
        }

        public IEnumerable<BLModel.Client> getAllClaimAdministrator()
        {
            SPImpl _spImpl = new SPImpl();
            return _spImpl.getAllClaimAdministrator();
        }

        public IEnumerable<BLModel.AdjusterByClientID> getAdjusterByClientID(int _clientid)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _adjusterByClientID = (from adj in _MMCDbContext.adjusters
                                       join clnt in _MMCDbContext.clients
                                       on adj.ClientID equals clnt.ClientID
                                       where clnt.ClientID == _clientid
                                       select new { AdjusterName = adj.AdjFirstName + " " + adj.AdjLastName, AdjusterID = adj.AdjusterID }).OrderBy(adj => adj.AdjusterName).AsEnumerable();

            return _adjusterByClientID.Select(adj => new BLModel.AdjusterByClientID().InjectFrom(adj)).Cast<BLModel.AdjusterByClientID>().ToList();
        }

        public IEnumerable<BLModel.ClaimAdministratorAllByClientID> getClaimAdministratorAllByClientID(int ClientID)
        {
            SPImpl _spImpl = new SPImpl();
            var _claimAdministratorAllByClientID = _spImpl.getClaimAdministratorAllByClientID(ClientID).AsEnumerable();
            return _claimAdministratorAllByClientID;
        }

        #endregion

        #region Client Insurer

        public int addClientInsurer(ClientInsurer _clientInsurer)
        {
            return _clientInsurerRepo.Add(_clientInsurer).ClientInsurerID;
        }

        public int updateClientInsurer(ClientInsurer _clientInsurer)
        {
            return _clientInsurerRepo.Update(_clientInsurer);
        }

        public void deleteClientInsurer(int _id)
        {
            int _clientID = _clientInsurerRepo.GetById(_id).ClientID;
            int cnt = _clientRepo.GetAll(cl => cl.ClientID == _clientID && cl.ClaimAdministratorType.StartsWith(Global.GlobalConst.TableAbbreviation.Ins)).Count();
            if (cnt > 0)
            {
                SPImpl _SPImpl = new SPImpl();
                _SPImpl.UpdateClaimAdministratorResetDetailsByClientID(_clientID);
            }
            _clientInsurerRepo.Delete(_id);
        }

        public BLModel.Paged.ClientInsurer getClientInsurerByClientID(int ClientID, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _ClientInsurer = ((from clntins in _MMCDbContext.clientinsurers
                                   join ins in _MMCDbContext.insurers
                                   on clntins.InsurerID equals ins.InsurerID
                                   join st in _MMCDbContext.states
                                   on ins.InsStateId equals st.StateId
                                   where clntins.ClientID == ClientID
                                   select new { InsType="ins", clntins.ClientInsurerID, ins.InsurerID, ins.InsName, ins.InsAddress1, ins.InsCity, ins.InsZip, InsState = st.StateName }).Union(from clntinsb in _MMCDbContext.clientInsurerBranchs
                                                                                                                                                                                join insb in _MMCDbContext.insuranceBranches
                                                                                                                                                                                on clntinsb.InsuranceBranchID equals insb.InsuranceBranchID
                                                                                                                                                                                join st in _MMCDbContext.states
                                                                                                                                                                                on insb.InsBranchStateId equals st.StateId
                                                                                                                                                                                where clntinsb.ClientID == ClientID
                                                                                                                                                                                select new { InsType="insb", ClientInsurerID = clntinsb.ClientInsuranceBranchID, InsurerID = insb.InsurerId, InsName = insb.InsBranchName, InsAddress1 = insb.InsBranchAddress, InsCity = insb.InsBranchCity, InsZip = insb.InsBranchZip, InsState = st.StateName })).ToList();
            return new BLModel.Paged.ClientInsurer
            {
                ClientInsurerDetails = _ClientInsurer.Select(ins => new BLModel.ClientInsurer().InjectFrom(ins)).Cast<BLModel.ClientInsurer>().Skip(_skip).Take(_take).ToList(),
                TotalCount = _ClientInsurer.Count()
            };

        }

        public IEnumerable<BLModel.ClientInsurer> getAllClientInsurerByClientID(int ClientID)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();          
            var _ClientInsurer = ((from clntins in _MMCDbContext.clientinsurers
                                   join ins in _MMCDbContext.insurers
                                   on clntins.InsurerID equals ins.InsurerID
                                   join st in _MMCDbContext.states
                                   on ins.InsStateId equals st.StateId
                                   where clntins.ClientID == ClientID
                                   select new { InsType = "Ins", clntins.ClientInsurerID, ins.InsurerID, ins.InsName, ins.InsAddress1, ins.InsCity, ins.InsZip, InsState = st.StateName }).Union(from clntinsb in _MMCDbContext.clientInsurerBranchs
                                                                                                                                                                                                 join insb in _MMCDbContext.insuranceBranches
                                                                                                                                                                                                 on clntinsb.InsuranceBranchID equals insb.InsuranceBranchID
                                                                                                                                                                                                 join st in _MMCDbContext.states
                                                                                                                                                                                                 on insb.InsBranchStateId equals st.StateId
                                                                                                                                                                                                 where clntinsb.ClientID == ClientID
                                                                                                                                                                                                 select new { InsType = "InsB", ClientInsurerID = clntinsb.ClientInsuranceBranchID, InsurerID = insb.InsuranceBranchID, InsName = insb.InsBranchName, InsAddress1 = insb.InsBranchAddress, InsCity = insb.InsBranchCity, InsZip = insb.InsBranchZip, InsState = st.StateName })).ToList();
            return _ClientInsurer.Select(ins => new BLModel.ClientInsurer().InjectFrom(ins)).Cast<BLModel.ClientInsurer>().AsEnumerable();
        }

        #endregion

        #region Client Insurer Branch

        public int addClientInsuranceBranch(ClientInsuranceBranch _clientInsuranceBranch)
        {
            return _clientInsurerBranchRepo.Add(_clientInsuranceBranch).ClientInsuranceBranchID;
        }
        public int updateClientInsuranceBranch(ClientInsuranceBranch _clientInsuranceBranch)
        {
            return _clientInsurerBranchRepo.Update(_clientInsuranceBranch);
        }
        public void deleteClientInsuranceBranch(int _clientInsuranceBranchID)
        {
            _clientInsurerBranchRepo.Delete(_clientInsuranceBranchID);
        }
        public void deleteClientInsuranceBranchByInsurerID(int _insurerID)
        {
            _clientInsurerBranchRepo.Delete(ins => ins.InsurerID == _insurerID);
        }

        public IEnumerable<BLModel.ClientInsuranceBranch> getClientInsuranceBranchesAllByInsurerID(int _clientID,int _insurerID)
        {
            //Get_ClientInsuranceBranchesAllByInsurerID
            SPImpl _SPImpl = new SPImpl();
           return  _SPImpl.getClientInsuranceBranchesAllByInsurerID(_clientID, _insurerID).ToList();
        }

        #endregion
        
        #region Client Employer

        public int addClientEmployer(ClientEmployer _clientEmployer)
        {
            return _clientEmployerRepo.Add(_clientEmployer).ClientEmployerID;
        }

        public int updateClientEmployer(ClientEmployer _clientEmployer)
        {
            return _clientEmployerRepo.Update(_clientEmployer);
        }

        public void deleteClientEmployer(int _id)
        {
            int _clientID = _clientEmployerRepo.GetById(_id).ClientID;
            int cnt = _clientRepo.GetAll(cl => cl.ClientID == _clientID && cl.ClaimAdministratorType.StartsWith(Global.GlobalConst.TableAbbreviation.Emp)).Count();
            if (cnt > 0)
            {
                SPImpl _SPImpl = new SPImpl();
                _SPImpl.UpdateClaimAdministratorResetDetailsByClientID(_clientID);
            }
            _clientEmployerRepo.Delete(_id);
        }

        public BLModel.Paged.ClientEmployer getClientEmployerByClientID(int ClientID, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _ClientInsurer = (from clntemp in _MMCDbContext.clientemployers
                                  join emp in _MMCDbContext.employers
                                  on clntemp.EmployerID equals emp.EmployerID
                                  join st in _MMCDbContext.states
                                  on emp.EmpStateId equals st.StateId
                                  where clntemp.ClientID == ClientID
                                  select new { clntemp.ClientEmployerID, emp.EmployerID, emp.EmpName, emp.EmpAddress1, emp.EmpCity, emp.EmpZip, EmpState = st.StateName }).ToList().OrderBy(emp => emp.EmpName);
            return new BLModel.Paged.ClientEmployer
            {
                ClientEmployerDetails = _ClientInsurer.Select(ins => new BLModel.ClientEmployer().InjectFrom(ins)).Cast<BLModel.ClientEmployer>().Skip(_skip).Take(_take).ToList(),
                TotalCount = _ClientInsurer.Count()
            };
        }

        public IEnumerable<BLModel.ClientEmployer> getAllClientEmployerByClientID(int ClientID)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _ClientInsurer = (from clntemp in _MMCDbContext.clientemployers
                                  join emp in _MMCDbContext.employers
                                  on clntemp.EmployerID equals emp.EmployerID
                                  join st in _MMCDbContext.states
                                  on emp.EmpStateId equals st.StateId
                                  where clntemp.ClientID == ClientID
                                  select new { clntemp.ClientEmployerID, emp.EmployerID, emp.EmpName, emp.EmpAddress1, emp.EmpCity, emp.EmpZip, EmpState = st.StateName }).ToList().OrderBy(emp => emp.EmpName);

            return _ClientInsurer.Select(ins => new BLModel.ClientEmployer().InjectFrom(ins)).Cast<BLModel.ClientEmployer>().AsEnumerable();  
        }

        #endregion

        #region Client Third Party Administrator

        public int addClientThirdPartyAdministrator(ClientThirdPartyAdministrator _clientThirdPartyAdministrator)
        {
            return _clientTPARepo.Add(_clientThirdPartyAdministrator).ClientTPAID;
        }

        public int updateClientThirdPartyAdministrator(ClientThirdPartyAdministrator _clientThirdPartyAdministrator)
        {
            return _clientTPARepo.Update(_clientThirdPartyAdministrator);
        }

        public void deleteClientThirdPartyAdministrator(int _id)
        {
            int _clientID = _clientTPARepo.GetById(_id).ClientID;
            int cnt = _clientRepo.GetAll(cl => cl.ClientID == _clientID && cl.ClaimAdministratorType.StartsWith(Global.GlobalConst.TableAbbreviation.Tpa)).Count();
            if (cnt > 0)
            {
                SPImpl _SPImpl = new SPImpl();
                _SPImpl.UpdateClaimAdministratorResetDetailsByClientID(_clientID);
            }
            _clientTPARepo.Delete(_id);
        }

        public BLModel.Paged.ClientThirdPartyAdministrator getClientThirdPartyAdministratorByClientID(int ClientID, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _ClientInsurer = ((from clnttpa in _MMCDbContext.clientthirdpartyadministrators
                                   join tpa in _MMCDbContext.thirdPartyAdministrators
                                   on clnttpa.TPAID equals tpa.TPAID
                                   join st in _MMCDbContext.states
                                   on tpa.TPAStateId equals st.StateId
                                   where clnttpa.ClientID == ClientID
                                   select new { TPAType = "TPA", clnttpa.ClientTPAID, tpa.TPAID, tpa.TPAName, tpa.TPAZip, tpa.TPAAddress, tpa.TPACity, TPAState = st.StateName }).Union(from clnttpab in _MMCDbContext.clientThirdPartyAdministratorBranchs
                                                                                                                                                                                        join tpab in _MMCDbContext.thirdPartyAdministratorBranches
                                                                                                                                                                                        on clnttpab.TPABranchID equals tpab.TPABranchID
                                                                                                                                                                                        join st in _MMCDbContext.states
                                                                                                                                                                                        on tpab.TPABranchStateId equals st.StateId
                                                                                                                                                                                        where clnttpab.ClientID == ClientID
                                                                                                                                                                                        select new { TPAType = "TPAB", ClientTPAID = clnttpab.ClientTPABranchID, TPAID = tpab.TPAID, TPAName = tpab.TPABranchName, TPAZip = tpab.TPABranchZip, TPAAddress = tpab.TPABranchName, TPACity = tpab.TPABranchCity, TPAState = st.StateName })).ToList();
            return new BLModel.Paged.ClientThirdPartyAdministrator
            {
                ClientThirdPartyAdministratorDetails = _ClientInsurer.Select(ins => new BLModel.ClientThirdPartyAdministrator().InjectFrom(ins)).Cast<BLModel.ClientThirdPartyAdministrator>().Skip(_skip).Take(_take).ToList(),
                TotalCount = _ClientInsurer.Count()
            };
        }

        public IEnumerable<BLModel.ClientThirdPartyAdministrator> getAllClientThirdPartyAdministratorByClientID(int ClientID)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _ClientInsurer = ((from clnttpa in _MMCDbContext.clientthirdpartyadministrators
                                   join tpa in _MMCDbContext.thirdPartyAdministrators
                                   on clnttpa.TPAID equals tpa.TPAID
                                   join st in _MMCDbContext.states
                                   on tpa.TPAStateId equals st.StateId
                                   where clnttpa.ClientID == ClientID
                                   select new { TPAType = "tpa", clnttpa.ClientTPAID, tpa.TPAID, tpa.TPAName, tpa.TPAZip, tpa.TPAAddress, tpa.TPACity, TPAState = st.StateName }).Union(from clnttpab in _MMCDbContext.clientThirdPartyAdministratorBranchs
                                                                                                                                                                                        join tpab in _MMCDbContext.thirdPartyAdministratorBranches
                                                                                                                                                                                        on clnttpab.TPABranchID equals tpab.TPABranchID
                                                                                                                                                                                        join st in _MMCDbContext.states
                                                                                                                                                                                        on tpab.TPABranchStateId equals st.StateId
                                                                                                                                                                                        where clnttpab.ClientID == ClientID
                                                                                                                                                                                        select new { TPAType = "tpab", ClientTPAID = clnttpab.ClientTPABranchID, TPAID = tpab.TPABranchID, TPAName = tpab.TPABranchName, TPAZip = tpab.TPABranchZip, TPAAddress = tpab.TPABranchName, TPACity = tpab.TPABranchCity, TPAState = st.StateName })).ToList();
            return _ClientInsurer.Select(ins => new BLModel.ClientThirdPartyAdministrator().InjectFrom(ins)).Cast<BLModel.ClientThirdPartyAdministrator>().AsEnumerable();
        }

        #endregion

        #region Client Third Party Administrator Branch

        public int addClientThirdPartyAdministratorBranch(ClientThirdPartyAdministratorBranch _clientThirdPartyAdministratorBranch)
        {
            return _clientTPABranchRepo.Add(_clientThirdPartyAdministratorBranch).ClientTPABranchID;
        }
        public int updateClientThirdPartyAdministratorBranch(ClientThirdPartyAdministratorBranch _clientThirdPartyAdministratorBranch)
        {
            return _clientTPABranchRepo.Update(_clientThirdPartyAdministratorBranch);
        }
        public void deleteClientThirdPartyAdministratorBranch(int _clientTPABranchID)
        {
            _clientTPABranchRepo.Delete(_clientTPABranchID);
        }

        public void deleteClientThirdPartyAdministratorBranchByTPAID(int _TPAID)
        {
            _clientTPABranchRepo.Delete(tpa => tpa.TPAID == _TPAID);
        }

        public IEnumerable<BLModel.ClientThirdPartyAdministratorBranch> getClientTPABranchesAllByTPAID(int _clientID, int _tPAID)
        {
            //Get_ClientInsuranceBranchesAllByInsurerID
            SPImpl _SPImpl = new SPImpl();
            return _SPImpl.getClientTPABranchesAllByTPAID(_clientID, _tPAID).ToList();
        }
        #endregion

        #region Client Managed Care Company

        public int addClientManagedCareCompany(ClientManagedCareCompany _clientManagedCareCompany)
        {
            if (_clientMMCRepo.GetAll(mmc => mmc.ClientID == _clientManagedCareCompany.ClientID).Count() > 0)
                return 0;
            else
                return _clientMMCRepo.Add(_clientManagedCareCompany).ClientCompanyID;

        }

        public int updateClientManagedCareCompany(ClientManagedCareCompany _clientManagedCareCompany)
        {
            return _clientMMCRepo.Update(_clientManagedCareCompany);
        }

        public void deleteClientManagedCareCompany(int _id)
        {
            _clientMMCRepo.Delete(_id);
        }
        public BLModel.Paged.ClientManagedCareCompany getClientManagedCareCompanyByClientID(int ClientID, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _ClientInsurer = (from clntmmc in _MMCDbContext.clientmanagedcarecompanys
                                  join mmc in _MMCDbContext.managedCareCompanies
                                  on clntmmc.CompanyID equals mmc.CompanyId
                                  join st in _MMCDbContext.states
                                  on mmc.CompStateId equals st.StateId
                                  where clntmmc.ClientID == ClientID
                                  select new { clntmmc.ClientCompanyID, mmc.CompanyId, mmc.CompName, mmc.CompCity, mmc.CompZip, mmc.CompAddress, CompState = st.StateName }).ToList().OrderBy(mmc => mmc.CompName);
            return new BLModel.Paged.ClientManagedCareCompany
            {
                ClientManagedCareCompanyDetails = _ClientInsurer.Select(ins => new BLModel.ClientManagedCareCompany().InjectFrom(ins)).Cast<BLModel.ClientManagedCareCompany>().Skip(_skip).Take(_take).ToList(),
                TotalCount = _ClientInsurer.Count()
            };
        }
        public int updateClientManagedCareCompanyByClientID(ClientManagedCareCompany _clientManagedCareCompany)
        {
            if (_clientMMCRepo.GetAll(mmc => mmc.ClientID == _clientManagedCareCompany.ClientID).Count() == 0)
            {
                return _clientMMCRepo.Add(_clientManagedCareCompany).ClientCompanyID;
            }
            else
            {
                SPImpl _spImpl = new SPImpl();
                _spImpl.UpdateClientManagedCareCompanyByClientID(_clientManagedCareCompany);
                return 2;
            }
        }
        #endregion

        #region Client Billing

        public int addClientBilling(ClientBilling _clientBilling)
        {
            return _clientBillingRepo.Add(_clientBilling).ClientBillingID;
        }
        public int updateClientBilling(ClientBilling _clientBilling)
        {
            return _clientBillingRepo.Update(_clientBilling);
        }
        public void deleteClientBilling(int _id)
        {
            _clientBillingRepo.Delete(_id);
        }

        public ClientBilling getClientBillingByID(int _id)
        {
            return _clientBillingRepo.GetById(_id);
        }

        public ClientBilling getClientBillingDetailByClientID(int _clientID)
        {
            return _clientBillingRepo.GetAll(rk => rk.ClientID == _clientID).SingleOrDefault();
        }
        



        #endregion

        #region Client Billing Retail Rate

        public int addClientBillingRetailRate(ClientBillingRetailRate _clientBillingRetailRate)
        {
            return _clientBillingRetailRateRepo.Add(_clientBillingRetailRate).ClientBillingRetailRateID;
        }
        public int updateClientBillingRetailRate(ClientBillingRetailRate _clientBillingRetailRate)
        {
            if (_clientBillingRetailRateRepo.GetAll(rk => rk.ClientBillingID == _clientBillingRetailRate.ClientBillingID).Count() > 0)
                return _clientBillingRetailRateRepo.Update(_clientBillingRetailRate);
            else
                return _clientBillingRetailRateRepo.Add(_clientBillingRetailRate).ClientBillingRetailRateID;
        }
        public void deleteClientBillingRetailRate(int _id)
        {
            _clientBillingRetailRateRepo.Delete(_id);
        }

        public ClientBillingRetailRate getClientBillingRetailRateByID(int _id)
        {
            return _clientBillingRetailRateRepo.GetById(_id);
        }

        public ClientBillingRetailRate getClientBillingRetailRateByClientBillingID(int _clientBillingID)
        {

            return _clientBillingRetailRateRepo.GetAll(rk => rk.ClientBillingID == _clientBillingID).SingleOrDefault();

            //MMCDbContext _MMCDbContext = new MMCDbContext();
            //var _clientBillingRetailRate = (from clntRetailrates in _MMCDbContext.clientBillingRetailsRates
            //                                join clntBilling in _MMCDbContext.clientBillings
            //                                on clntRetailrates.ClientBillingID equals clntBilling.ClientBillingID
            //                                where clntBilling.ClientBillingID == _clientBillingID
            //                                select clntRetailrates).OrderBy(clntRetailrates => clntRetailrates.CreatedOn).ToList();

            //return _clientBillingRetailRate.Select(ins => new BLModel.ClientBillingRetailRate().InjectFrom(ins)).Cast<BLModel.ClientBillingRetailRate>().SingleOrDefault();
        }

        #endregion

        #region Client Billing Wholesale Rate

        public int addClientBillingWholesaleRate(ClientBillingWholesaleRate _ClientBillingWholesaleRate)
        {
            return _clientBillingWholesaleRateRepo.Add(_ClientBillingWholesaleRate).ClientBillingWholesaleRateID;
        }
        public int updateClientBillingWholesaleRate(ClientBillingWholesaleRate _ClientBillingWholesaleRate)
        {
            if (_clientBillingWholesaleRateRepo.GetAll(rk => rk.ClientBillingID == _ClientBillingWholesaleRate.ClientBillingID).Count() > 0)
                return _clientBillingWholesaleRateRepo.Update(_ClientBillingWholesaleRate);
            else
                return _clientBillingWholesaleRateRepo.Add(_ClientBillingWholesaleRate).ClientBillingWholesaleRateID;
        }
        public void deleteClientBillingWholesaleRate(int _id)
        {
            _clientBillingWholesaleRateRepo.Delete(_id);
        }

        public ClientBillingWholesaleRate getClientBillingWholesaleRateByID(int _id)
        {
            return _clientBillingWholesaleRateRepo.GetById(_id);
        }

        public ClientBillingWholesaleRate getClientBillingWholesaleRateByClientBillingID(int _clientBillingID)
        {
            return _clientBillingWholesaleRateRepo.GetAll(rk => rk.ClientBillingID == _clientBillingID).SingleOrDefault();

            //MMCDbContext _MMCDbContext = new MMCDbContext();
            //var _ClientBillingWholesaleRate = (from clntWholeslaerates in _MMCDbContext.clientBillingWholesaleRates
            //                                   join clntBilling in _MMCDbContext.clientBillings
            //                                   on clntWholeslaerates.ClientBillingID equals clntBilling.ClientBillingID
            //                                   where clntBilling.ClientBillingID == _clientBillingID
            //                                   select clntWholeslaerates).OrderBy(clntWholeslaerates => clntWholeslaerates.CreatedOn).ToList();

            //return new BLModel.Paged.ClientBillingWholesaleRate
            //{
            //    ClientBillingWholesaleRateDetails = _ClientBillingWholesaleRate.Select(ins => new BLModel.ClientBillingWholesaleRate().InjectFrom(ins)).Cast<BLModel.ClientBillingWholesaleRate>().ToList(),
            //    TotalCount = _ClientBillingWholesaleRate.Count()
            //};
        }
     
        #endregion


        #region Client Private Label

        public int addClientPrivateLabel(ClientPrivateLabel _clientPrivateLabel)
        {
            return _clientPrivateLabelRepo.Add(_clientPrivateLabel).ClientPrivateLabelID;
        }
        public int updateClientPrivateLabel(ClientPrivateLabel _clientPrivateLabel)
        {
            return _clientPrivateLabelRepo.Update(_clientPrivateLabel);
        }
        public void deleteClientPrivateLabel(int _id)
        {
            _clientPrivateLabelRepo.Delete(_id);
        }

        public void deleteClientPrivateLabelByClientID(int _clientID)
        {
            _clientPrivateLabelRepo.Delete(rk => rk.ClientID == _clientID);
        }

        public ClientPrivateLabel getClientPrivateLabelByID(int _id)
        {
            return _clientPrivateLabelRepo.GetById(_id);
        }
        public ClientPrivateLabel getClientPrivateLabelDetailByClientID(int _clientID)
        {
            return _clientPrivateLabelRepo.GetAll(rk => rk.ClientID == _clientID).SingleOrDefault();
        }

        #endregion

    }
}
