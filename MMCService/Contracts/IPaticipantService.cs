using MMCService.DTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace MMCService
{
    [ServiceContract]
    public interface IPaticipantService
    {
        // Adjuster

        #region Adjuster

        [OperationContract]
        int addAdjuster(Adjuster _adjuster);
        [OperationContract]
        int updateAdjuster(Adjuster _adjuster);
        [OperationContract]
        void deleteAdjuster(int _adjusterId);
        [OperationContract]
        IEnumerable<Adjuster> getadjustersAll();
        [OperationContract]
        Adjuster getAdjusterByID(int _adjusterId);
        [OperationContract]
        DTO.Paged.PagedAdjuster getadjustersByName(string SearchText, int _skip, int _take);


        #endregion

        #region Physician

        [OperationContract]
        int addPhysician(Physician _physician);
        [OperationContract]
        int updatePhysician(Physician _physician);
        [OperationContract]
        void deletePhysician(int _physicianID);
        [OperationContract]
        IEnumerable<DTO.Physician> getphysicianAll();
        [OperationContract]
        DTO.Physician getPhysicianByID(int _physicianID);
        [OperationContract]
        DTO.Paged.PagedPhysician GetPhysicianByName(string _searchText, int _skip, int _take);
        [OperationContract]
        DTO.Paged.PagedPhysician GetPhysicianByNPI(string _searchText, int _skip, int _take);
        [OperationContract]
        DTO.Paged.PagedPhysician GetPhysicianBySpeciality(int _searchID, int _skip, int _take);

        #endregion

        #region Vendor

        [OperationContract]
        IEnumerable<Vendor> getVendorsAll();
        [OperationContract]
        Vendor getVendorByID(int _id);
        [OperationContract]
        int addVendor(Vendor _vendor);
        [OperationContract]
        int updateVendor(Vendor _vendor);
        [OperationContract]
        void deleteVendor(int _id);
        [OperationContract]
        DTO.Paged.PagedVendor getVendorsByName(string searchtext, int skip, int take);
        #endregion

        #region Insurer

        [OperationContract]
        int addInsurer(Insurer _Insurer);
        [OperationContract]
        int updateInsurer(Insurer _Insurer);
        [OperationContract]
        void deleteInsurer(int _InsurerId);
        [OperationContract]
        IEnumerable<Insurer> getInsurersAll();
        [OperationContract]
        Insurer getInsurerByID(int _InsurerId);
        [OperationContract]
        DTO.Paged.PagedInsurer getInsurersByName(string SearchText, int _skip, int _take);


        #endregion

        #region Insurance Branch

        [OperationContract]
        int addInsuranceBranch(InsuranceBranch _insuranceBranch);
        [OperationContract]
        int updateInsuranceBranch(InsuranceBranch _insuranceBranch);
        [OperationContract]
        void deleteInsuranceBranch(int _insuranceBranchID);
        [OperationContract]
        DTO.Paged.PagedInsuranceBranch getInsuranceBranchesByInsurerID(int _insurerID, int _skip, int _take);
        [OperationContract]
        IEnumerable<InsuranceBranch> getInsuranceBranchesAllByInsurerID(int _insurerID);
        [OperationContract]
        InsuranceBranch getInsuranceBranchByID(int _insuranceBranchID);

        #endregion

        #region Employer
        [OperationContract]
        int addEmployer(Employer _employer);

        [OperationContract]
        int updateEmployer(Employer _employer);

        [OperationContract]
        void deleteEmployer(int _employerid);

        [OperationContract]
        IEnumerable<DTO.Employer> getEmployersAll();

        [OperationContract]
        DTO.Employer getEmployerByID(int _employerid);

        [OperationContract]
        DTO.Paged.PagedEmployer getEmployersByName(string SearchText, int _skip, int _take);
        #endregion

        #region Employer Subsidiaries
        [OperationContract]
        int addEmployerSubsidiaries(EmployerSubsidiary _employerSubsidiaries);

        [OperationContract]
        int updateEmployerSubsidiaries(EmployerSubsidiary _employerSubsidiaries);

        [OperationContract]
        void deleteEmployerSubsidiaries(int _employerid);

        [OperationContract]
        IEnumerable<DTO.EmployerSubsidiary> getEmployerSubsidiariesAll();

        [OperationContract]
        DTO.EmployerSubsidiary getEmployerSubsidiariesByID(int _employerid);

        [OperationContract]
        DTO.Paged.PagedEmployerSubsidiary getEmployerSubsidiariesByEmployerID(int _employerID, int _skip, int _take);

        [OperationContract]
        IEnumerable<DTO.EmployerSubsidiary> getAllEmployerSubsidiariesByEmployerID(int _employerID);

        #endregion

        #region CaseManager
        [OperationContract]
        int addCaseManager(CaseManager _caseManager);

        [OperationContract]
        int updateCaseManager(CaseManager _caseManager);

        [OperationContract]
        void deleteCaseManager(int _caseManagerId);

        [OperationContract]
        IEnumerable<CaseManager> getCaseManagerAll();

        [OperationContract]
        DTO.CaseManager getCaseManagerByID(int _caseManager);

        [OperationContract]
        DTO.Paged.PagedCaseManager getCaseManagerByName(string SearchText, int _skip, int _take);
        #endregion   

        #region ThirdPartyAdministrator
        [OperationContract]
        int addThirdPartyAdministrator(ThirdPartyAdministrator _thirdPartyAdministrator);

        [OperationContract]
        int updateThirdPartyAdministrator(ThirdPartyAdministrator _thirdPartyAdministrator);

        [OperationContract]
        void deleteThirdPartyAdministrator(int _thirdPartyAdministratorId);

        [OperationContract]
        IEnumerable<ThirdPartyAdministrator> getThirdPartyAdministratorsAll();

        [OperationContract]
        DTO.ThirdPartyAdministrator getThirdPartyAdministratorByID(int _thirdPartyAdministratorId);

        [OperationContract]
        DTO.Paged.PagedThirdPartyAdministrator getThirdPartyAdministratorsByName(string SearchText, int _skip, int _take);
        #endregion   

        #region ThirdPartyAdministratorBranch
        [OperationContract]
        int addThirdPartyAdministratorBranch(ThirdPartyAdministratorBranch _thirdPartyAdministratorBranch);

        [OperationContract]
        int updateThirdPartyAdministratorBranch(ThirdPartyAdministratorBranch _thirdPartyAdministratorBranch);

        [OperationContract]
        void deleteThirdPartyAdministratorBranch(int _thirdPartyAdministratorBranchId);

        [OperationContract]
        IEnumerable<ThirdPartyAdministratorBranch> getThirdPartyAdministratorBranchesAll();

        [OperationContract]
        DTO.ThirdPartyAdministratorBranch getThirdPartyAdministratorBranchByID(int _thirdPartyAdministratorBranchId);

        [OperationContract]
        DTO.Paged.PagedThirdPartyAdministratorBranch getThirdPartyAdministratorBranchesByTPAID(int _thirdPartyAdministratorId, int _skip, int _take);

        [OperationContract]
        IEnumerable<DTO.ThirdPartyAdministratorBranch> getThirdPartyAdministratorBranchesAllByTPAID(int _tpaID);

        #endregion   

        #region ManagedCareCompany
        [OperationContract]
        int addManagedCareCompany(ManagedCareCompany _company);

        [OperationContract]
        int updateManagedCareCompany(ManagedCareCompany _company);

        [OperationContract]
        void deleteManagedCareCompany(int _companyid);

        [OperationContract]
        IEnumerable<DTO.ManagedCareCompany> getManagedCareCompanyAll();

        [OperationContract]
        DTO.ManagedCareCompany getManagedCareCompanyByID(int _companyid);

        [OperationContract]
        DTO.Paged.PagedManagedCareCompany getManagedCareCompanyByName(string SearchText, int _skip, int _take);
        #endregion 

        #region MedicalGroup
          [OperationContract]
          int addMedicalGroup(MedicalGroup _medicalGroup);

          [OperationContract]
          int updateMedicalGroup(MedicalGroup _medicalGroup);

          [OperationContract]
          void deleteMedicalGroup(int _medicalGroupId);

          [OperationContract]
          IEnumerable<MedicalGroup> getMedicalGroupsAll();

          [OperationContract]
          DTO.MedicalGroup getMedicalGroupByID(int _medicalGroupId);

          [OperationContract]
          DTO.Paged.PagedMedicalGroup getMedicalGroupsByName(string SearchText, int _skip, int _take);
          #endregion   

        #region Attorney
           [OperationContract]
           int addAttorney(Attorney _attorney);

          [OperationContract]
          int updateAttorney(Attorney _attorney);

          [OperationContract]
          void deleteAttorney(int _attorneyId);

          [OperationContract]
          IEnumerable<DTO.Attorney> getAttorneyAll();

          [OperationContract]
          DTO.Attorney getAttorneyByID(int _id);

          [OperationContract]
          DTO.Paged.PagedAttorney getAttorneyByAttorneyFirmID(int _attorneyFirmID, int _skip, int _take);

          [OperationContract]
          DTO.Paged.PagedAttorney getAttorneyRecordsByFirmTypeID(int _attorneyFirmTypeID);
        #endregion

        #region AttorneyFirm
          [OperationContract]
          int addAttorneyFirm(AttorneyFirm _attorneyFirm);

          [OperationContract]
          int updateAttorneyFirm(AttorneyFirm _attorneyFirm);

          [OperationContract]
          void deleteAttorneyFirm(int _attorneyFirmID);

          [OperationContract]
          IEnumerable<AttorneyFirm> getAttorneyFirmAll();

          [OperationContract]
          DTO.AttorneyFirm getAttorneyFirmByID(int _id);

          [OperationContract]
          DTO.Paged.PagedAttorneyFirm getAttorneyAndAttorneyFirmByName(string _searchText, int _skip, int _take);
        #endregion

          #region PeerReview

          [OperationContract]
          int addPeerReview(PeerReview _peerReview);
          [OperationContract]
          int updatePeerReview(PeerReview _peerReview);
          [OperationContract]
          void deletePeerReview(int _peerReviewId);
          [OperationContract]
          IEnumerable<PeerReview> getPeerReviewsAll();
          [OperationContract]
          PeerReview getPeerReviewByID(int _peerReviewId);
          [OperationContract]
          DTO.Paged.PagedPeerReview getPeerReviewsByName(string SearchText, int _skip, int _take);
          [OperationContract]
          DTO.PatientAndRequestModel getPatientAndPeerReviewRequestByReferralId(int _referralID);
          #endregion

          #region ADR
          [OperationContract]
          int addADR(ADR _ADR);
          [OperationContract]
          int updateADR(ADR _ADR);
          [OperationContract]
          void deleteADR(int _ADRId);
          [OperationContract]
          IEnumerable<ADR> getADRsAll();
          [OperationContract]
          ADR getADRByID(int _Id);
          [OperationContract]
          DTO.Paged.PagedADR getADRsByName(string SearchText, int _skip, int _take);
          #endregion
    }
}
