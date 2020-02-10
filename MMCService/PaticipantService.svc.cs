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
    public class PaticipantService : IPaticipantService
    {

        private readonly IAdjusterRepository _iAdjusterRepository;
        private readonly IInsurerRepository _iInsurerRepository;
        private readonly IPhysicianRepository _iPhysicianRepository;
        private readonly IVendorRepository _iVendorRepository;
        private readonly IEmployerRepository _iEmployerRepository;
        private readonly ICaseManagerRepository _iCaseManagerRepository;
        private readonly IThirdPartyAdministratorRepository _iThirdPartyAdministratorRepository;
        private readonly IThirdPartyAdministratorBranchRepository _iThirdPartyAdministratorBranchRepository;
        private readonly IManagedCareCompanyRepository _iManagedCareCompanyRepository;
        private readonly IMedicalGroupRepository _iMedicalGroupRepository;
        private readonly IAttorneyRepository _iAttorneyRepository;
        private readonly IPeerReviewRepository _iPeerReviewRepository;
        private readonly IADRRepository _iADRRepository;

        public PaticipantService(IAdjusterRepository iAdjusterRepository,
            IPhysicianRepository iPhysicianRepository,
            IInsurerRepository iInsurerRepository,
            IEmployerRepository iEmployerRepository,
            IVendorRepository iVendorRepository,
            ICaseManagerRepository iCaseManagerRepository,
            IThirdPartyAdministratorRepository iThirdPartyAdministratorRepository,
            IThirdPartyAdministratorBranchRepository iThirdPartyAdministratorBranchRepository,
            IManagedCareCompanyRepository iManagedCareCompanyRepository,
            IMedicalGroupRepository iMedicalGroupRepository,
            IAttorneyRepository iAttorneyRepository,
              IPeerReviewRepository iPeerReviewRepository,
             IADRRepository iADRRepository
            )
        {
            _iAdjusterRepository = iAdjusterRepository;
            _iPhysicianRepository = iPhysicianRepository;
            _iInsurerRepository = iInsurerRepository;
            _iVendorRepository = iVendorRepository;
            _iEmployerRepository = iEmployerRepository;
            _iCaseManagerRepository = iCaseManagerRepository;
            _iThirdPartyAdministratorRepository = iThirdPartyAdministratorRepository;
            _iThirdPartyAdministratorBranchRepository = iThirdPartyAdministratorBranchRepository;
            _iManagedCareCompanyRepository = iManagedCareCompanyRepository;
            _iMedicalGroupRepository = iMedicalGroupRepository;
            _iAttorneyRepository = iAttorneyRepository;
            _iPeerReviewRepository = iPeerReviewRepository;
            _iADRRepository = iADRRepository;
        }

        #region Adjuster
        public int addAdjuster(Adjuster _adjuster)
        {
            return _iAdjusterRepository.addAdjuster(Mapper.Map<DTO.Adjuster, MMC.Core.Data.Model.Adjuster>(_adjuster));
        }

        public int updateAdjuster(Adjuster _adjuster)
        {
            return _iAdjusterRepository.updateAdjuster(Mapper.Map<DTO.Adjuster, MMC.Core.Data.Model.Adjuster>(_adjuster));
        }

        public void deleteAdjuster(int _adjusterid)
        {
            _iAdjusterRepository.deleteAdjuster(_adjusterid);
        }

        public IEnumerable<DTO.Adjuster> getadjustersAll()
        {
            return Mapper.Map<IEnumerable<DTO.Adjuster>>(_iAdjusterRepository.getadjustersAll());
        }

        public DTO.Adjuster getAdjusterByID(int _adjusterid)
        {
            return Mapper.Map<DTO.Adjuster>(_iAdjusterRepository.getAdjusterByID(_adjusterid));
        }

        public DTO.Paged.PagedAdjuster getadjustersByName(string SearchText, int _skip, int _take)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.Adjuster, DTO.Paged.PagedAdjuster>(_iAdjusterRepository.getadjustersByName(SearchText, _skip, _take));
        }
        #endregion

        #region Physician
        public int addPhysician(Physician _physician)
        {
            return _iPhysicianRepository.addPhysician(Mapper.Map<DTO.Physician, MMC.Core.Data.Model.Physician>(_physician));
        }

        public int updatePhysician(Physician _physician)
        {
            return _iPhysicianRepository.updatePhysican(Mapper.Map<DTO.Physician, MMC.Core.Data.Model.Physician>(_physician));
        }

        public void deletePhysician(int _physicianID)
        {
            _iPhysicianRepository.deletePhysican(_physicianID);
        }

        public IEnumerable<DTO.Physician> getphysicianAll()
        {
            return Mapper.Map<IEnumerable<DTO.Physician>>(_iPhysicianRepository.getPhysicianAll());
        }

        public DTO.Physician getPhysicianByID(int _physicianID)
        {
            return Mapper.Map<DTO.Physician>(_iPhysicianRepository.getPhysicianByID(_physicianID));
        }

        public DTO.Paged.PagedPhysician GetPhysicianByName(string _searchText, int _skip, int _take)
        {
            return Mapper.Map<DTO.Paged.PagedPhysician>(_iPhysicianRepository.getPhysicianByName(_searchText, _skip, _take));
        }

        public DTO.Paged.PagedPhysician GetPhysicianByNPI(string _searchText, int _skip, int _take)
        {
            return Mapper.Map<DTO.Paged.PagedPhysician>(_iPhysicianRepository.getPhysicianByNPI(_searchText, _skip, _take));
        }

        public DTO.Paged.PagedPhysician GetPhysicianBySpeciality(int _searchID, int _skip, int _take)
        {
            return Mapper.Map<DTO.Paged.PagedPhysician>(_iPhysicianRepository.getPhysicianBySpeciality(_searchID, _skip, _take));
        }

        #endregion

        #region Vendor
        public int addVendor(Vendor _vendor)
        {
            return _iVendorRepository.addVendor(Mapper.Map<DTO.Vendor, MMC.Core.Data.Model.Vendor>(_vendor));
        }

        public int updateVendor(Vendor _vendor)
        {
            return _iVendorRepository.updateVendor(Mapper.Map<DTO.Vendor, MMC.Core.Data.Model.Vendor>(_vendor));
        }

        public void deleteVendor(int _vendorID)
        {
            _iVendorRepository.deleteVendor(_vendorID);
        }

        public IEnumerable<DTO.Vendor> getVendorsAll()
        {
            return Mapper.Map<IEnumerable<DTO.Vendor>>(_iVendorRepository.getVendorsAll());
        }

        public DTO.Vendor getVendorByID(int _vendorID)
        {
            return Mapper.Map<DTO.Vendor>(_iVendorRepository.getVendorByID(_vendorID));
        }

        public DTO.Paged.PagedVendor getVendorsByName(string _searchText, int _skip, int _take)
        {
            return Mapper.Map<DTO.Paged.PagedVendor>(_iVendorRepository.getVendorsByName(_searchText, _skip, _take));
        }
        #endregion

        #region Insurer
        public int addInsurer(Insurer _insurer)
        {
            return _iInsurerRepository.addInsurer(Mapper.Map<DTO.Insurer, MMC.Core.Data.Model.Insurer>(_insurer));
        }

        public int updateInsurer(Insurer _insurer)
        {
            return _iInsurerRepository.updateInsurer(Mapper.Map<DTO.Insurer, MMC.Core.Data.Model.Insurer>(_insurer));
        }

        public void deleteInsurer(int _insurerid)
        {
            _iInsurerRepository.deleteInsurer(_insurerid);
        }

        public IEnumerable<DTO.Insurer> getInsurersAll()
        {
            return Mapper.Map<IEnumerable<DTO.Insurer>>(_iInsurerRepository.getInsurersAll());
        }

        public DTO.Insurer getInsurerByID(int _insurerid)
        {
            return Mapper.Map<DTO.Insurer>(_iInsurerRepository.getInsurerByID(_insurerid));
        }

        public DTO.Paged.PagedInsurer getInsurersByName(string SearchText, int _skip, int _take)
        {
            return Mapper.Map<DTO.Paged.PagedInsurer>(_iInsurerRepository.getInsurersByName(SearchText, _skip, _take));
        }
        #endregion

        #region Insurance Branch

        public int addInsuranceBranch(InsuranceBranch _insuranceBranch)
        {
            return _iInsurerRepository.addInsuranceBranch(Mapper.Map<DTO.InsuranceBranch, MMC.Core.Data.Model.InsuranceBranch>(_insuranceBranch));
        }

        public int updateInsuranceBranch(InsuranceBranch _insuranceBranch)
        {
            return _iInsurerRepository.updateInsuranceBranch(Mapper.Map<DTO.InsuranceBranch, MMC.Core.Data.Model.InsuranceBranch>(_insuranceBranch));
        }

        public void deleteInsuranceBranch(int _insuranceBranchID)
        {
            _iInsurerRepository.deleteInsuranceBranch(_insuranceBranchID);
        }

        public DTO.Paged.PagedInsuranceBranch getInsuranceBranchesByInsurerID(int _insurerID, int _skip, int _take)
        {
            return Mapper.Map<DTO.Paged.PagedInsuranceBranch>(_iInsurerRepository.getInsuranceBranchesByInsurerID(_insurerID, _skip, _take));
        }

        public IEnumerable<InsuranceBranch> getInsuranceBranchesAllByInsurerID(int _insurerID)
        {
            return Mapper.Map<IEnumerable<DTO.InsuranceBranch>>(_iInsurerRepository.getInsuranceBranchesAllByInsurerID(_insurerID));
        }
        

        public DTO.InsuranceBranch getInsuranceBranchByID(int _insuranceBranchID)
        {
            return Mapper.Map<DTO.InsuranceBranch>(_iInsurerRepository.getInsuranceBranchByID(_insuranceBranchID));
        }


        #endregion

        #region Employer
        public int addEmployer(Employer _employer)
        {
            return _iEmployerRepository.addEmployer(Mapper.Map<DTO.Employer, MMC.Core.Data.Model.Employer>(_employer));
        }

        public int updateEmployer(Employer _employer)
        {
            return _iEmployerRepository.updateEmployer(Mapper.Map<DTO.Employer, MMC.Core.Data.Model.Employer>(_employer));
        }

        public void deleteEmployer(int _employerid)
        {
            _iEmployerRepository.deleteEmployer(_employerid);
        }

        public IEnumerable<DTO.Employer> getEmployersAll()
        {
            return Mapper.Map<IEnumerable<DTO.Employer>>(_iEmployerRepository.getEmployerAll());
        }

        public DTO.Employer getEmployerByID(int _employerid)
        {
            return Mapper.Map<DTO.Employer>(_iEmployerRepository.getEmployerByID(_employerid));
        }

        public DTO.Paged.PagedEmployer getEmployersByName(string SearchText, int _skip, int _take)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.Employer, DTO.Paged.PagedEmployer>(_iEmployerRepository.getEmployerByName(SearchText, _skip, _take));
        }
        #endregion

        #region EmployerSubsidiaries
        public int addEmployerSubsidiaries(EmployerSubsidiary _employerSubsidiaries)
        {
            return _iEmployerRepository.addEmployerSubsidiaries(Mapper.Map<DTO.EmployerSubsidiary, MMC.Core.Data.Model.EmployerSubsidiary>(_employerSubsidiaries));
        }

        public int updateEmployerSubsidiaries(EmployerSubsidiary _employerSubsidiaries)
        {
            return _iEmployerRepository.updateEmployerSubsidiaries(Mapper.Map<DTO.EmployerSubsidiary, MMC.Core.Data.Model.EmployerSubsidiary>(_employerSubsidiaries));
        }

        public void deleteEmployerSubsidiaries(int _employerid)
        {
            _iEmployerRepository.deleteEmployerSubsidiaries(_employerid);
        }

        public IEnumerable<DTO.EmployerSubsidiary> getEmployerSubsidiariesAll()
        {
            return Mapper.Map<IEnumerable<DTO.EmployerSubsidiary>>(_iEmployerRepository.getEmployerSubsidiariesAll());
        }

        public IEnumerable<DTO.EmployerSubsidiary> getAllEmployerSubsidiariesByEmployerID(int _employerID)
        {
            return Mapper.Map<IEnumerable<DTO.EmployerSubsidiary>>(_iEmployerRepository.getAllEmployerSubsidiariesByEmployerID(_employerID));
        }

        public DTO.EmployerSubsidiary getEmployerSubsidiariesByID(int _employerid)
        {
            return Mapper.Map<DTO.EmployerSubsidiary>(_iEmployerRepository.getEmployerSubsidiariesByID(_employerid));
        }

        public DTO.Paged.PagedEmployerSubsidiary getEmployerSubsidiariesByEmployerID(int _employerID, int _skip, int _take)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.EmployerSubsidiary, DTO.Paged.PagedEmployerSubsidiary>(_iEmployerRepository.getEmployerSubsidiariesByEmployerID(_employerID, _skip, _take));
        }
        #endregion

        #region CaseManager
        public int addCaseManager(CaseManager _caseManager)
        {
            return _iCaseManagerRepository.addCaseManager(Mapper.Map<DTO.CaseManager, MMC.Core.Data.Model.CaseManager>(_caseManager));
        }

        public int updateCaseManager(CaseManager _caseManager)
        {
            return _iCaseManagerRepository.updateCaseManager(Mapper.Map<DTO.CaseManager, MMC.Core.Data.Model.CaseManager>(_caseManager));
        }

        public void deleteCaseManager(int _caseManagerId)
        {
            _iCaseManagerRepository.deleteCaseManager(_caseManagerId);
        }

        public IEnumerable<CaseManager> getCaseManagerAll()
        {
            return Mapper.Map<IEnumerable<DTO.CaseManager>>(_iCaseManagerRepository.getCaseManagerAll());
        }

        public DTO.CaseManager getCaseManagerByID(int _caseManager)
        {
            return Mapper.Map<DTO.CaseManager>(_iCaseManagerRepository.getCaseManagerByID(_caseManager));
        }

        public DTO.Paged.PagedCaseManager getCaseManagerByName(string SearchText, int _skip, int _take)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.CaseManager, DTO.Paged.PagedCaseManager>(_iCaseManagerRepository.getCaseManagerByName(SearchText, _skip, _take));
        }
        #endregion

        #region ThirdPartyAdministrator
        public int addThirdPartyAdministrator(ThirdPartyAdministrator _thirdPartyAdministrator)
        {
            return _iThirdPartyAdministratorRepository.addThirdPartyAdministrator(Mapper.Map<DTO.ThirdPartyAdministrator, MMC.Core.Data.Model.ThirdPartyAdministrator>(_thirdPartyAdministrator));
        }

        public int updateThirdPartyAdministrator(ThirdPartyAdministrator _thirdPartyAdministrator)
        {
            return _iThirdPartyAdministratorRepository.updateThirdPartyAdministrator(Mapper.Map<DTO.ThirdPartyAdministrator, MMC.Core.Data.Model.ThirdPartyAdministrator>(_thirdPartyAdministrator));
        }

        public void deleteThirdPartyAdministrator(int _thirdPartyAdministratorId)
        {
            _iThirdPartyAdministratorRepository.deleteThirdPartyAdministrator(_thirdPartyAdministratorId);
        }

        public IEnumerable<ThirdPartyAdministrator> getThirdPartyAdministratorsAll()
        {
            return Mapper.Map<IEnumerable<DTO.ThirdPartyAdministrator>>(_iThirdPartyAdministratorRepository.getThirdPartyAdministratorsAll());
        }

        public DTO.ThirdPartyAdministrator getThirdPartyAdministratorByID(int _thirdPartyAdministratorId)
        {
            return Mapper.Map<DTO.ThirdPartyAdministrator>(_iThirdPartyAdministratorRepository.getThirdPartyAdministratorByID(_thirdPartyAdministratorId));
        }

        public DTO.Paged.PagedThirdPartyAdministrator getThirdPartyAdministratorsByName(string SearchText, int _skip, int _take)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.ThirdPartyAdministrator, DTO.Paged.PagedThirdPartyAdministrator>(_iThirdPartyAdministratorRepository.getThirdPartyAdministratorsByName(SearchText, _skip, _take));
        }
        #endregion

        #region ThirdPartyAdministratorBranch
        public int addThirdPartyAdministratorBranch(ThirdPartyAdministratorBranch _thirdPartyAdministratorBranch)
        {
            return _iThirdPartyAdministratorBranchRepository.addThirdPartyAdministratorBranch(Mapper.Map<DTO.ThirdPartyAdministratorBranch, MMC.Core.Data.Model.ThirdPartyAdministratorBranch>(_thirdPartyAdministratorBranch));
        }

        public int updateThirdPartyAdministratorBranch(ThirdPartyAdministratorBranch _thirdPartyAdministratorBranch)
        {
            return _iThirdPartyAdministratorBranchRepository.updateThirdPartyAdministratorBranch(Mapper.Map<DTO.ThirdPartyAdministratorBranch, MMC.Core.Data.Model.ThirdPartyAdministratorBranch>(_thirdPartyAdministratorBranch));
        }

        public void deleteThirdPartyAdministratorBranch(int _thirdPartyAdministratorBranchId)
        {
            _iThirdPartyAdministratorBranchRepository.deleteThirdPartyAdministratorBranch(_thirdPartyAdministratorBranchId);
        }

        public IEnumerable<ThirdPartyAdministratorBranch> getThirdPartyAdministratorBranchesAll()
        {
            return Mapper.Map<IEnumerable<DTO.ThirdPartyAdministratorBranch>>(_iThirdPartyAdministratorBranchRepository.getThirdPartyAdministratorBranchesAll());
        }

        public DTO.ThirdPartyAdministratorBranch getThirdPartyAdministratorBranchByID(int _thirdPartyAdministratorBranchId)
        {
            return Mapper.Map<DTO.ThirdPartyAdministratorBranch>(_iThirdPartyAdministratorBranchRepository.getThirdPartyAdministratorBranchByID(_thirdPartyAdministratorBranchId));
        }

        public DTO.Paged.PagedThirdPartyAdministratorBranch getThirdPartyAdministratorBranchesByTPAID(int _thirdPartyAdministratorId, int _skip, int _take)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.ThirdPartyAdministratorBranch, DTO.Paged.PagedThirdPartyAdministratorBranch>(_iThirdPartyAdministratorBranchRepository.getThirdPartyAdministratorBranchesByTPAID(_thirdPartyAdministratorId, _skip, _take));
        }

        public IEnumerable<DTO.ThirdPartyAdministratorBranch> getThirdPartyAdministratorBranchesAllByTPAID(int _tpaID)
        {
            return Mapper.Map<IEnumerable<DTO.ThirdPartyAdministratorBranch>>(_iThirdPartyAdministratorBranchRepository.getThirdPartyAdministratorBranchesAllByTPAID(_tpaID));
        }
        #endregion

        #region ManagedCareCompany
        public int addManagedCareCompany(ManagedCareCompany _company)
        {
            return _iManagedCareCompanyRepository.addManagedCareCompany(Mapper.Map<DTO.ManagedCareCompany, MMC.Core.Data.Model.ManagedCareCompany>(_company));
        }

        public int updateManagedCareCompany(ManagedCareCompany _company)
        {
            return _iManagedCareCompanyRepository.updateManagedCareCompany(Mapper.Map<DTO.ManagedCareCompany, MMC.Core.Data.Model.ManagedCareCompany>(_company));
        }

        public void deleteManagedCareCompany(int _companyid)
        {
            _iManagedCareCompanyRepository.deleteManagedCareCompany(_companyid);
        }

        public IEnumerable<DTO.ManagedCareCompany> getManagedCareCompanyAll()
        {
            return Mapper.Map<IEnumerable<DTO.ManagedCareCompany>>(_iManagedCareCompanyRepository.getManagedCareCompanyAll());
        }

        public DTO.ManagedCareCompany getManagedCareCompanyByID(int _companyid)
        {
            return Mapper.Map<DTO.ManagedCareCompany>(_iManagedCareCompanyRepository.getManagedCareCompanyByID(_companyid));
        }

        public DTO.Paged.PagedManagedCareCompany getManagedCareCompanyByName(string SearchText, int _skip, int _take)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.ManagedCareCompany, DTO.Paged.PagedManagedCareCompany>(_iManagedCareCompanyRepository.getManagedCareCompanyByName(SearchText, _skip, _take));
        }
        #endregion

        #region MedicalGroup
        public int addMedicalGroup(MedicalGroup _medicalGroup)
        {
            return _iMedicalGroupRepository.addMedicalGroup(Mapper.Map<DTO.MedicalGroup, MMC.Core.Data.Model.MedicalGroup>(_medicalGroup));
        }

        public int updateMedicalGroup(MedicalGroup _medicalGroup)
        {
            return _iMedicalGroupRepository.updateMedicalGroup(Mapper.Map<DTO.MedicalGroup, MMC.Core.Data.Model.MedicalGroup>(_medicalGroup));
        }

        public void deleteMedicalGroup(int _medicalGroupId)
        {
            _iMedicalGroupRepository.deleteMedicalGroup(_medicalGroupId);
        }

        public IEnumerable<MedicalGroup> getMedicalGroupsAll()
        {
            return Mapper.Map<IEnumerable<DTO.MedicalGroup>>(_iMedicalGroupRepository.getMedicalGroupsAll());
        }

        public DTO.MedicalGroup getMedicalGroupByID(int _medicalGroupId)
        {
            return Mapper.Map<DTO.MedicalGroup>(_iMedicalGroupRepository.getMedicalGroupByID(_medicalGroupId));
        }

        public DTO.Paged.PagedMedicalGroup getMedicalGroupsByName(string SearchText, int _skip, int _take)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.MedicalGroup, DTO.Paged.PagedMedicalGroup>(_iMedicalGroupRepository.getMedicalGroupsByName(SearchText, _skip, _take));
        }
        #endregion

        #region Attorney
        public int addAttorney(Attorney _attorney)
        {
            return _iAttorneyRepository.addAttorney(Mapper.Map<DTO.Attorney, MMC.Core.Data.Model.Attorney>(_attorney));
        }

        public int updateAttorney(Attorney _attorney)
        {
            return _iAttorneyRepository.updateAttorney(Mapper.Map<DTO.Attorney, MMC.Core.Data.Model.Attorney>(_attorney));
        }

        public void deleteAttorney(int _attorneyId)
        {
            _iAttorneyRepository.deleteAttorney(_attorneyId);
        }

        public IEnumerable<DTO.Attorney> getAttorneyAll()
        {
            return Mapper.Map<IEnumerable<DTO.Attorney>>(_iAttorneyRepository.getAttorneyAll());
        }

        public DTO.Attorney getAttorneyByID(int _id)
        {
            return Mapper.Map<DTO.Attorney>(_iAttorneyRepository.getAttorneyByID(_id));
        }

        public DTO.Paged.PagedAttorney getAttorneyByAttorneyFirmID(int _attorneyFirmID, int _skip, int _take)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.Attorney, DTO.Paged.PagedAttorney>(_iAttorneyRepository.getAttorneyByAttorneyFirmID(_attorneyFirmID, _skip, _take));
        }

        public DTO.Paged.PagedAttorney getAttorneyRecordsByFirmTypeID(int _attorneyFirmTypeID)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.Attorney, DTO.Paged.PagedAttorney>(_iAttorneyRepository.getAttorneyRecordsByFirmTypeID(_attorneyFirmTypeID));
        }
        #endregion

        #region AttorneyFirm
        public int addAttorneyFirm(AttorneyFirm _attorneyFirm)
        {
            return _iAttorneyRepository.addAttorneyFirm(Mapper.Map<DTO.AttorneyFirm, MMC.Core.Data.Model.AttorneyFirm>(_attorneyFirm));
        }

        public int updateAttorneyFirm(AttorneyFirm _attorneyFirm)
        {
            return _iAttorneyRepository.updateAttorneyFirm(Mapper.Map<DTO.AttorneyFirm, MMC.Core.Data.Model.AttorneyFirm>(_attorneyFirm));
        }

        public void deleteAttorneyFirm(int _attorneyFirmID)
        {
            _iAttorneyRepository.deleteAttorneyFirm(_attorneyFirmID);
        }

        public IEnumerable<AttorneyFirm> getAttorneyFirmAll()
        {
            return Mapper.Map<IEnumerable<DTO.AttorneyFirm>>(_iAttorneyRepository.getAttorneyFirmAll());
        }

        public DTO.AttorneyFirm getAttorneyFirmByID(int _id)
        {
            return Mapper.Map<DTO.AttorneyFirm>(_iAttorneyRepository.getAttorneyFirmByID(_id));
        }

        public DTO.Paged.PagedAttorneyFirm getAttorneyAndAttorneyFirmByName(string _searchText, int _skip, int _take)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.AttorneyFirm, DTO.Paged.PagedAttorneyFirm>(_iAttorneyRepository.getAttorneyAndAttorneyFirmByName(_searchText, _skip, _take));
        }
        #endregion

        #region PeerReview
        public int addPeerReview(PeerReview _peerReview)
        {
            return _iPeerReviewRepository.addPeerReview(Mapper.Map<DTO.PeerReview, MMC.Core.Data.Model.PeerReview>(_peerReview));
        }

        public int updatePeerReview(PeerReview _peerReview)
        {
            return _iPeerReviewRepository.updatePeerReview(Mapper.Map<DTO.PeerReview, MMC.Core.Data.Model.PeerReview>(_peerReview));
        }

        public void deletePeerReview(int _peerReviewid)
        {
            _iPeerReviewRepository.deletePeerReview(_peerReviewid);
        }

        public IEnumerable<DTO.PeerReview> getPeerReviewsAll()
        {
            return Mapper.Map<IEnumerable<DTO.PeerReview>>(_iPeerReviewRepository.getPeerReviewsAll());
        }

        public DTO.PeerReview getPeerReviewByID(int _peerReviewid)
        {
            return Mapper.Map<DTO.PeerReview>(_iPeerReviewRepository.getPeerReviewByID(_peerReviewid));
        }

        public DTO.Paged.PagedPeerReview getPeerReviewsByName(string SearchText, int _skip, int _take)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.PeerReview, DTO.Paged.PagedPeerReview>(_iPeerReviewRepository.getPeerReviewsByName(SearchText, _skip, _take));
        }

        public DTO.PatientAndRequestModel getPatientAndPeerReviewRequestByReferralId(int _referralID)
        {
            return Mapper.Map<MMC.Core.BL.Model.PatientAndRequestModel, DTO.PatientAndRequestModel>(_iPeerReviewRepository.getPatientAndPeerReviewRequestByReferralId(_referralID));
        }
        #endregion

        #region ADR
        public int addADR(ADR _ADR)
        {
            return _iADRRepository.addADR(Mapper.Map<DTO.ADR, MMC.Core.Data.Model.ADR>(_ADR));
        }

        public int updateADR(ADR _ADR)
        {
            return _iADRRepository.updateADR(Mapper.Map<DTO.ADR, MMC.Core.Data.Model.ADR>(_ADR));
        }

        public void deleteADR(int _ADRId)
        {
            _iADRRepository.deleteADR(_ADRId);
        }

        public IEnumerable<ADR> getADRsAll()
        {
            return Mapper.Map<IEnumerable<DTO.ADR>>(_iADRRepository.getADRsAll());
        }

        public DTO.ADR getADRByID(int _Id)
        {
            return Mapper.Map<DTO.ADR>(_iADRRepository.getADRByID(_Id));
        }

        public DTO.Paged.PagedADR getADRsByName(string SearchText, int _skip, int _take)
        {
            return Mapper.Map<MMC.Core.BL.Model.Paged.ADR, DTO.Paged.PagedADR>(_iADRRepository.getADRsByName(SearchText, _skip, _take));
        }

        #endregion
    }
}