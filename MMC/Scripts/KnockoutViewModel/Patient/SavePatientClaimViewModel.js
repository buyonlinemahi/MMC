function SavePatientClaimViewModel() {
    var self = this;
    // paging variables      
    var _totalCount = 0;    
    self.TotalItemCount = ko.observable(0);
    self.csTotalItemCount = ko.observable(0);
    self.pbTotalItemCount = ko.observable(0);
    self.bpTotalItemCount = ko.observable(0);
    self.cdTotalItemCount = ko.observable(0);
    self.pTotalItemCount = ko.observable(0);
    // ---------

    self.PatientClaimID = ko.observable();
    self.PatientID = ko.observable();
    self.PatClaimNumber = ko.observable();
    self.PatDOI = ko.observable();
    self.PatPolicyYear = ko.observable();
    self.PatClaimJurisdictionId = ko.observable();
    self.PatAdjudicationStateCaseNumber = ko.observable();
    self.PatEDIExchangeTrackingNumber = ko.observable();
    //self.PatInjuryType = ko.observable();
    //self.PatInjuryDescription = ko.observable();
    self.PatInjuryReportedDate = ko.observable();
    self.PatInsurerID = ko.observable();
    self.PatEmployerID = ko.observable();
    self.PatEmpSubsidiaryID = ko.observable();
    self.PatClaimAdministratorID = ko.observable();

    self.ClaimStatusID = ko.observable();
    self.PatientClaimStatusID = ko.observable();
    self.DeniedRationale = ko.observable();
    self.ClaimStatusIDVal = ko.observable();
    self.DeniedRationaleVal = ko.observable();

    self.ClaimStatusResults = ko.observableArray();
    self.AddOnBodyPartResults = ko.observableArray();
    self.PleadBodyPartResults = ko.observableArray();
    self.icdResults = ko.observableArray();
    self.icdSearchDataResult = ko.observableArray();
    self.ShowUpdatebtn = ko.observable(false);
    self.ICDSearchType = ko.observableArray([{ SearchCriteria: "ICD9", SearchCriteriaName: "ICD9" }, { SearchCriteria: "ICD10", SearchCriteriaName: "ICD10" }]);

    //Case Manager
    self.CaseManagerList = ko.observableArray();
    self.CaseManagerList = ko.observableArray([self.CaseManagerList(0)]);
    self.selectedItemCaseManagerList = ko.observable(0);

    //Attoney
    self.PatInsName = ko.observable();
    self.ApplicantAttorneyList = ko.observableArray();
    self.ApplicantAttorneyList = ko.observableArray([self.ApplicantAttorneyList(0)]);
    self.selectedItemApplicantAttorneyList = ko.observable(0);
    self.DefenseAttorneyList = ko.observableArray();
    self.DefenseAttorneyList = ko.observableArray([self.DefenseAttorneyList(0)]);
    self.selectedItemDefenceAttorneyList = ko.observable(0);
    
    self.AdjusterList = ko.observableArray();
  //  self.AdjusterList = ko.observableArray([self.AdjusterList(0)]);
    self.selectedItemAdjusterList = ko.observable();

    //Client
   

    self.ClientList = ko.observableArray();
    self.ClientList = ko.observableArray([self.ClientList(0)]);
    self.selectedItem = ko.observable(2);

    self.selectedItemEmp = ko.observable(0);
    self.selectedItemIns = ko.observable(0);
    self.selectedItemTpa = ko.observable(0);
    ////Claim Administrator
    //self.PatClaimAdminName = ko.observable();
    //self.ClaimAdministratorList = ko.observableArray();


    //Patient Claim Status
    self.PatientClaimStatusIds = ko.observable();
    var _PatientClaimStatusIds = "";

    self.PatientClaimStatusID = ko.observable();
    self.PatientClaimID = ko.observable();
    self.ClaimStatusID = ko.observable();
    self.PatClaimStatusName = ko.observable();

    self.PatientStatue = ko.observable();
    self.PatientStatues = ko.observableArray();
    self.PatientStatues = ko.observableArray([self.PatientStatues(0)]);
    self.selectedItemPatientStatues = ko.observable(0);

    //Patient Claim AddON Body Parts
    self.PatientClaimAddOnBodyPartIds = ko.observable();
    var _PatientClaimAddOnBodyPartIds = "";

    self.PatientClaimAddOnBodyPartID = ko.observable();
    self.AddOnBodyPartID = ko.observable();
    self.PatAddOnBodyPartName = ko.observable();

    self.PatientAddONBody = ko.observable();
    self.PatientAddONBodys = ko.observableArray();
    self.PatientAddONBodys = ko.observableArray([self.PatientAddONBodys(0)]);
    self.selectedItemPatientAddONBodys = ko.observable(0);

    //Patient Claim Plead Body Parts
    self.PatientClaimPleadBodyPartIds = ko.observable();
    var _PatientClaimPleadBodyPartIds = "";


    self.PatientClaimPleadBodyPartID = ko.observable();
    self.PleadBodyPartID = ko.observable();
    self.BodyPartStatusID = ko.observable();
    self.AcceptedDate = ko.observable();
    self.PleadBodyPartCondition = ko.observable();
    self.PatPleadBodyPartName = ko.observable();
    self.PatBodyPartStatusName = ko.observable();

    self.PatientPleadBody = ko.observable();
    self.PatientPleadBodys = ko.observableArray();
    self.PatientPleadBodys = ko.observableArray([self.PatientPleadBodys(0)]);
    self.selectedItemPatientPleadBodys = ko.observable(0);

    //Patient Claim Diagnosis
    self.PatientClaimDiagnoseIds = ko.observable();
    var _PatientClaimDiagnoseIds = "";

    self.PatientClaimDiagnosisID = ko.observable(0);
    self.icdICDNumber = ko.observable();
    self.ICDDescription = ko.observable();
    self.icdICDNumberID = ko.observable();
    self.ICDType = ko.observable();

    // States
    self.State = ko.observable();
    self.States = ko.observableArray();
    self.States = ko.observableArray([self.States(0)]);
    self.selectedItemState = ko.observable(0);

    // ADRR
    self.PatADRID = ko.observable();
    self.ADRDetails = ko.observableArray();
    self.selectedItemADR = ko.observable(0);

    // BodyPartStatues
    self.BodyPartStatus = ko.observable();
    self.AddOnBodyPartCondition = ko.observable();
    self.BodyPartStatues = ko.observableArray();
    self.BodyPartStatues = ko.observableArray([self.BodyPartStatues(0)]);
    self.selectedItemAddBodyPartStatues = ko.observable(0);
    self.selectedItemPleadBodyPartStatues = ko.observable(0);

    /// Employer Subsidiaries
    self.EmployerSubsidiaries = ko.observableArray();
    self.EmployerSubsidiaries = ko.observableArray([self.EmployerSubsidiaries(0)]);
    self.selectedItemEmployerSubsidiaries = ko.observable(0);

    ///Claim PopUP................
    self.ClientEmployerDetails = ko.observableArray();
    self.ClientTPADetails = ko.observableArray();
    self.ClientMCCDetails = ko.observableArray();
    self.ClientInsurerDetails = ko.observableArray();
    self.EmpTotalItemCount = ko.observable();
    self.InsTotalItemCount = ko.observable();
    self.TPATotalItemCount = ko.observable();
    self.ClientMCCName = ko.observable();
    self.ClaimAdministratorName = ko.observable();

    self.PatPhysicianID = ko.observableArray();

    var adjusterId = 1;
    $('.OpenPop').click(function () {
        $('#divuploadFile').hide();
        blockPopupBackground();
    });

    $('.ClosePop').click(function () {
        unblockPopupBackground();
    });
    //ClosePopUpPatientClaimAddOnBodyPart
    $('#ClosePleadBodyPartPopup').click(function () {
        unblockPopupBackground();
        self.PleadBodyPartCondition("");
    });
    $('#ClosePopUpPatientClaimAddOnBodyPart').click(function () {
        unblockPopupBackground();
        self.AddOnBodyPartCondition("");
    });
    $('#CloseAddOnBodyPartConditionPopup').click(function () {
        unblockPopupBackground();
        self.AddOnBodyPartCondition("");
    });

    $('.icdSearchClosePop').click(function () {
        $('#divuploadFile').show();
        unblockPopupBackground();
        self.icdSearchDataResult.removeAll();
        $("#SearchDiagnosisTitle").val('');
    });
    
    this.Init = function (model) {
    
        if (model == null) {
            ResetPatientClaimDetails();
        }
        else {
            showLoader();

            ko.mapping.fromJS(model.PatientClaimDetails, {}, self);
            $('#ClmInsurance').val(model.PatientClaimDetails.PatInsurerID);
            $('#PatPhysicianID').val(model.PatientClaimDetails.PatPhysicianID);
            $('#ClmEmployer').val(model.PatientClaimDetails.PatEmployerID);            
            ko.mapping.fromJS(model.PatientClaimStatusDetails, {}, self.ClaimStatusResults);
            ko.mapping.fromJS(model.TotalCountClaimStatus, {}, self.csTotalItemCount);
            ko.mapping.fromJS(model.PatientClaimAddOnBodyPartDetails, {}, self.AddOnBodyPartResults);
            ko.mapping.fromJS(model.TotalCountClaimAddOnBodyPart, {}, self.bpTotalItemCount);
            ko.mapping.fromJS(model.PatientClaimPleadBodyPartDetails, {}, self.PleadBodyPartResults);
            ko.mapping.fromJS(model.TotalCountClaimPleadBodyPart, {}, self.pbTotalItemCount);
            ko.mapping.fromJS(model.PatientClaimDiagnoseDetails, {}, self.icdResults);
            ko.mapping.fromJS(model.TotalCountClaimDiagnose, {}, self.cdTotalItemCount);
            $('#PatDOI').datepicker();
            $('#PatInjuryReportedDate').datepicker();
            self.PatDOI(moment(model.PatientClaimDetails.PatDOI).format("MM/DD/YYYY"));
         
            self.ClaimStatusIDVal(model.PatientClaimDetails.ClaimStatusID);
            self.DeniedRationaleVal(model.PatientClaimDetails.DeniedRationale);            
            $('#PatDOI').val(moment(model.PatientClaimDetails.PatDOI).format("MM/DD/YYYY"));
            if (model.PatientClaimDetails.PatInjuryReportedDate != null)
            {   
                self.PatInjuryReportedDate(moment(model.PatientClaimDetails.PatInjuryReportedDate).format("MM/DD/YYYY"));
            }    
            setTimeout(
              function () {

                  hideLoader();
              }, 1300); 
        }
        $.getJSON("/Common/getStates", function (dataStates) {
            self.States(dataStates.slice());
            if (model != null) {
                if (model.PatientClaimDetails != null)
                    self.selectedItemState(model.PatientClaimDetails.PatClaimJurisdictionId);
            }
        });

        $.getJSON("/ADR/getAllADR", function (dataADR) {
            self.ADRDetails(dataADR.slice());
            if (model != null) {
                if (model.PatientClaimDetails != null)
                    self.selectedItemADR(model.PatientClaimDetails.PatADRID);
            }
        });

        $.getJSON("/Patient/getClientAll", function (data) {
            self.ClientList(data.slice());
            if (model != null) {
                if (model.PatientClaimDetails != null)
                    self.selectedItem(model.PatientClaimDetails.PatClientID);
                adjusterId = model.PatientClaimDetails.PatAdjusterID;
                $("ClmClient").change(ClientChange(model));
            }
        });

        $.getJSON("/Patient/getAttorneyByFirmTypeIdAll", { id: 1 }, function (dataAttorney) {
            self.ApplicantAttorneyList(dataAttorney.slice());
            if (model != null) {
                if (model.PatientClaimDetails != null)
                    self.selectedItemApplicantAttorneyList(model.PatientClaimDetails.PatApplicantAttorneyID);
            }
        });
        $.getJSON("/Patient/getAttorneyByFirmTypeIdAll", { id: 2 }, function (dataAttorney) {
            self.DefenseAttorneyList(dataAttorney.slice());
            if (model != null) {
                if (model.PatientClaimDetails != null)
                    self.selectedItemDefenceAttorneyList(model.PatientClaimDetails.PatDefenseAttorneyID);
            }
        });


        $.getJSON("/Common/getPatientClaimStatusesAll", function (dataClaimStatus) {
            self.PatientStatues(dataClaimStatus.slice());
            if (model != null) {
                if (model.PatientClaimDetails != null) {
                    self.selectedItemPatientStatues(model.PatientClaimDetails.ClaimStatusID);
                    self.ClaimStatusIDVal(model.PatientClaimDetails.ClaimStatusID);
                    if (self.selectedItemPatientStatues() == 4) {
                        $("#DeniedRationale").attr("readonly", true)
                    }
                    else {
                        self.DeniedRationale(self.DeniedRationaleVal());
                        $("#DeniedRationale").attr("readonly", false)
                    }
                }
                if (self.ClaimStatusIDVal() == 3) {
                    self.ShowUpdatebtn(true);
                }
                else {
                    self.ShowUpdatebtn(false);
                }
              
            }
        });

        $.getJSON("/CaseManager/getCaseManagerAll", function (dataCaseManager) {
            self.CaseManagerList(dataCaseManager.slice());
            if (model != null) {
                if (model != null) {
                    if (model.PatientClaimDetails != null)
                        self.selectedItemCaseManagerList(model.PatientClaimDetails.PatCaseManagerID);
                }
            }
        });

        $.getJSON("/Common/getPatientClaimAddOnBodyPartsAll", function (dataClaimAddOn) {
            self.PatientAddONBodys(dataClaimAddOn.slice());
            if (model != null) {
                if (model.PatientClaimAddOnBodyPartDetails != null)
                    self.selectedItemPatientAddONBodys(model.PatientClaimAddOnBodyPartDetails.AddOnBodyPartID);
            }
        });
        $.getJSON("/Common/getPatientClaimAddOnBodyPartsAll", function (dataClaimPleadBodyParts) {
            self.PatientPleadBodys(dataClaimPleadBodyParts.slice());
            if (model != null) {
                if (model.PatientClaimPleadBodyPartDetails != null)
                    self.selectedItemPatientPleadBodys(model.PatientClaimPleadBodyPartDetails.PleadBodyPartID);
            }
        });

        $.getJSON("/Common/getPatientClaimBodyPartStatusAll", function (dataClaimAddOnStatues) {
            self.BodyPartStatues(dataClaimAddOnStatues.slice());

        });
        //$(function () {
        //    setTimeout(
        //      function () {
        //          $("ClmClient").change(ClientChange(model));
        //      }, 1200);

        //});
        var _PatientClaimID = $("#PatientClaimID").val();
        if (_PatientClaimID == 0)
            $(".ClaimHideDiv").fadeOut();
        else
            $(".ClaimHideDiv").fadeIn();

        var _patPhysicianID = $("#PatPhysicianID").val();
        if (_patPhysicianID != null)
            self.bindPatientClaimID(_patPhysicianID);
        else {
            $("#PatPhysicianID").val("");
            self.PhysicianId("");
            self.PhysicianName("");
            self.PhysicianAddress("");
            self.PhysCity("");
            self.PhysStateName("");
            self.PhysZip("");
        }

    };

    $("#ClaimStatusID").change(function () {
           setTimeout(
              function () {
                   if (self.selectedItemPatientStatues() == 4) {
            $("#DeniedRationale").attr("readonly", true)
        }
        else {
           self.DeniedRationale(self.DeniedRationaleVal());
            $("#DeniedRationale").attr("readonly", false)
            }
              }, 500);
       
    });
   
    self.selectionChanged = function () {
        ClientChange(null);
    }

    self.selectionChangedByEmployer = function () {
        EmployerChange(self.selectedItemEmp(),null);
    }

    function EmployerChange(_empID,model) {
      
            $.post("/Patient/getAllEmployerSubsidiaryByEmployerID", {
                _employerID: _empID
            }, function (_data) {

                var modelEmp = $.parseJSON(_data);
                if (modelEmp != null) {
                    if (self.EmployerSubsidiaries() != null)
                        self.EmployerSubsidiaries.removeAll();
                    self.EmployerSubsidiaries(modelEmp.EmployerSubsidiaryDetails);
                    if (self.EmployerSubsidiaries().length == 1) {
                        self.selectedItemEmployerSubsidiaries(self.EmployerSubsidiaries()[0].EMPSubsidiaryID);
                    }
                    if (model != null)
                        self.selectedItemEmployerSubsidiaries(model.PatientClaimDetails.PatEmpSubsidiaryID);
                }
            });
        
    }

    function ClientChange(model) {     
        $.post("/Patient/getAdjusterByClientID", {
            clientID: self.selectedItem()
        }, function (_data) {
            var modelAdj = $.parseJSON(_data);
            if (modelAdj != null) {
                //if (self.AdjusterList != null) {
                //    self.AdjusterList.removeAll();
                //}
                self.AdjusterList(modelAdj);
                self.selectedItemAdjusterList(adjusterId);
            }
        });

        $.post("/Patient/getAllEmployerByClientIdAll", {
            clientID: self.selectedItem()
        }, function (_data) {
            var _empID = 0;
            var modelEmp = $.parseJSON(_data);
            if (modelEmp != null) {
                self.ClientEmployerDetails(modelEmp.ClientEmployerDetails);
                if (self.ClientEmployerDetails().length == 1) {
                    self.selectedItemEmp(self.ClientEmployerDetails()[0].EmployerID);
                    _empID = self.ClientEmployerDetails()[0].EmployerID;
                }
                if (model != null) {
                    self.selectedItemEmp(model.PatientClaimDetails.PatEmployerID);
                    _empID = model.PatientClaimDetails.PatEmployerID;
                }
                EmployerChange(_empID,model);
            }

            
        });


        $.post("/Patient/getAllInsurerByClientIdAll", {
            clientID: self.selectedItem()
        }, function (_data) {
            var modelIns = $.parseJSON(_data);
            if (modelIns != null) {
                if (self.ClientInsurerDetails != null) {
                    self.ClientInsurerDetails.removeAll();
                }
                self.ClientInsurerDetails(modelIns.ClientInsurerDetails);
                if (self.ClientInsurerDetails().length == 1) {
                    self.selectedItemIns(self.ClientInsurerDetails()[0].InsValue);
                }
                if (model != null)
                    self.selectedItemIns(model.PatientClaimDetails.PatInsValue);
            }

        });

        $.post("/Patient/getAllThirdPartyAdministratorByClientIdAll", {
            clientID: self.selectedItem()
        }, function (_data) {
            var modelTPA = $.parseJSON(_data);
            if (modelTPA != null) {
                if (self.ClientTPADetails != null) {
                    self.ClientTPADetails.removeAll();
                }
                self.ClientTPADetails(modelTPA.ClientTPADetails);
                if (self.ClientTPADetails().length == 1) {
                    self.selectedItemTpa(self.ClientTPADetails()[0].TPAValue);
                }
                if (model != null)
                    self.selectedItemTpa(model.PatientClaimDetails.PatTPAValue);
                
            }

        });

        $.post("/Patient/getMCCByClientId", {
            clientID: self.selectedItem()
        }, function (_data) {
            var modelMCC = $.parseJSON(_data);
            if (modelMCC != null)
            {
                self.ClientMCCName(modelMCC.CompName);
                $('#PatMCC').val(modelMCC.CompName);              
            }         
        });

        $.post("/Patient/getClaimAdministratorByClientId", {
            clientID: self.selectedItem()
        }, function (_data) {
            var modelCA = $.parseJSON(_data);
            if (modelCA != null)
                self.ClaimAdministratorName(modelCA.ClaimAdministratorName);

        });

        

    }
  
    function ResetPatientClaimDetails() { 
        self.TotalItemCount(0);
        self.csTotalItemCount(0);
        self.pbTotalItemCount(0);
        self.bpTotalItemCount(0);
        self.cdTotalItemCount(0);
        // ---------
        self.PatientClaimID(null);        
        self.PatientID($("#PatientID").val());
        self.PatClaimNumber(null);
        self.PatDOI(null);
        self.PatPolicyYear(null);
       // self.PatClaimJurisdictionId(null);
        self.PatAdjudicationStateCaseNumber(null);
        self.PatEDIExchangeTrackingNumber(null);
        //self.PatInjuryType(null);
        //self.PatInjuryDescription(null);
        self.PatInjuryReportedDate(null);
       // self.PatInsurerID(null);
       // self.PatEmployerID(null);
       // self.PatClaimAdministratorID(null);
        self.selectedItemApplicantAttorneyList('');
        self.selectedItemDefenceAttorneyList('');
        self.selectedItemCaseManagerList('');
        self.selectedItem('');
        self.selectedItemEmp('');
        self.selectedItemIns('');
        self.selectedItemTpa('');
        self.selectedItemAdjusterList('');
        self.ClaimStatusResults(null);
        self.AddOnBodyPartResults(null);
        self.PleadBodyPartResults(null);
        self.icdResults(null);
        self.icdSearchDataResult(null);
     //   self.ClaimStatusID('');
       self.PatientClaimStatusID('');
        self.DeniedRationale('');
       // self.ClaimStatusIDVal('');
        self.DeniedRationaleVal('');
        self.selectedItemPatientStatues('');
        //Physician
        
        $("#PatMCC").val("");
        $("#PatPhysicianID").val("");
        self.PhysicianId("");
        self.PhysicianName("");
        self.PhysicianAddress("");
        self.PhysCity("");
        self.PhysStateName("");
        self.PhysZip("");
    }
    //----------------------------------------------------------------------//
    self.AddPatientClaimSuccess = function (responseText) {
        hideLoader();
        if (responseText[0] != -1) {
            $("#PatientClaimID").val(responseText[0]);
            $("#HDPatientClaimID").val(responseText[0]);
            if (responseText[1]==1)
            self.PatientClaimStatusID(responseText[2]);
            self.ClaimStatusIDVal(self.selectedItemPatientStatues());
            self.DeniedRationaleVal(self.DeniedRationale());
            $.post("/Patient/GetPatientClaimPleadBodyPartByPatientClaimId", {
                _patientClaimID: $("#HDPatientClaimID").val(), _skip: 0 // $('#hidskippb').val()
            }, function (model) {
                //  var model = $.parseJSON(_data);
                if (self.PleadBodyPartResults.length > 0) {
                    self.PleadBodyPartResults.removeAll();
                }
                ko.mapping.fromJS(model.PatientClaimPleadBodyPartDetails, {}, self.PleadBodyPartResults);
                ko.mapping.fromJS(model.TotalCountClaimPleadBodyPart, {}, self.pbTotalItemCount);

            });
          
        }
        
        switch (responseText[1]) {
            case 0:
                alertify.alert("Error Occur");
                break;
            case 1:                
                alertify.alert("Save Successfully");
                $(".ClaimHideDiv").fadeIn();
                break;
            case 2:
                alertify.alert("Update Successfully");
                break;
            case -1:
                alertify.alert("Patient Claim Number Already Exists");                
                $('#PatClaimNumber').focus();
                break;

        };
        if (self.selectedItemPatientStatues() == 3) {           
            self.ShowUpdatebtn(true);
        }
        else {   
            self.ShowUpdatebtn(false);
        }
    }
    self.AddPatientClaimFormBeforeSubmit = function (arr, $form, options) {
        if (self.selectedItemPatientStatues() == 2 && $("#DeniedRationale").val() == '') {          
            $("#DeniedRationale").addClass("border_r");
            return false;
        }
        else {
            $("#DeniedRationale").removeClass("border_r");
        }
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        else {
            showLoader();
        }
        return true;

    };

    self.AddNewPatientClaimStatus = function () {
        if ($("#ClaimStatusID").val() != "") {
            showLoader();            
            var _ClaimStatusName = $("#ClaimStatusID").find("option:selected").text();
            var _ClaimStatusId = $("#ClaimStatusID").val()
           //-----mahi-----------------
            var viewModelClaimStatusDetails = {
                PatientClaimStatusID: 0,
                PatientClaimID: $('#PatientClaimID').val(),
                ClaimStatusID: _ClaimStatusId,
                PatClaimStatusName : _ClaimStatusName

            }
            var plainJs = ko.toJS(viewModelClaimStatusDetails);

            $.ajax({
                url: "/Patient/SavePatientClaimStatus",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify(plainJs),
                success: function (data) {
                    
                    if (data != 0) {   
                        $.post("/Patient/GetPatientClaimStatusByPatientClaimId", {
                            _patientClaimID: $("#HDPatientClaimID").val(), _skip: 0
                        }, function (model) {
                            hideLoader();
                            if (model.PatientClaimStatusDetails != null) {
                                if (self.ClaimStatusResults() != null)
                                    self.ClaimStatusResults.removeAll();
                                ko.mapping.fromJS(model.PatientClaimStatusDetails, {}, self.ClaimStatusResults);
                                ko.mapping.fromJS(model.TotalCountClaimStatus, {}, self.csTotalItemCount);
                                _totalCount = model.TotalCountClaimStatus;
                                alertify.alert('Added Successfully', function () {
                                    $('#addPatientClaimStatus').modal('hide');
                                });
                                self.csPager().csCurrentPage(1);
                                self.csSkip = ko.observable(0);
                                unblockPopupBackground();
                            }
                        });
                    }
                    else {
                        hideLoader();
                        alert("Error Occur");
                    }
                 
                },
                error: function (data) {
                    hideLoader();
                    alert("Error Occur");
                }
            });

          
        }
    };

    self.removeClaimStatus = function (item) {
        alertify.confirm("Are you sure you want to delete this record?", function (e) {
            if (e) {
                if (item.PatientClaimStatusID() != 0) {
                    showLoader();
                    $.post("/Patient/DeletePatientClaimStatus", {
                        _patientClaimStatusID: item.PatientClaimStatusID()
                    }, function (data) {
                        if (data != 0) {
                            $.post("/Patient/GetPatientClaimStatusByPatientClaimId", {
                                _patientClaimID: $("#HDPatientClaimID").val(), _skip: $('#hidskipcs').val()
                            }, function (model) {
                                hideLoader();
                                    self.ClaimStatusResults.removeAll();
                                    ko.mapping.fromJS(model.PatientClaimStatusDetails, {}, self.ClaimStatusResults);
                                    ko.mapping.fromJS(model.TotalCountClaimStatus, {}, self.csTotalItemCount);
                                    _totalCount = model.TotalCountClaimStatus;
                                    var _skipValue = $('#hidskipcs').val();
                                    var _totalCount = self.csTotalItemCount();
                                    if ((self.csTotalItemCount() % $('#hidskipcs').val()) == 0)
                                        self.csPager().csCurrentPage(self.csPager().csCurrentPage() - 1);
                                    else
                                        self.csPager().csCurrentPage();
                               
                            });
                            self.csPager().csCurrentPage(1);
                                self.csSkip = ko.observable(0);
                            alertify.alert("Deleted Successfully");
                        }
                        else {
                            hideLoader();
                            alert("Error Occur")
                        }

                    });
                    hideLoader();
                }
          
            }
        });
    }

    self.addBodyPart = function () {
        $('#AddBodyPartsAcceptedDate').val('');
    };

    self.AddNewPatientClaimAddOnBodyPart = function () {        
        if ($("#AddOnBodyPartID").val() != "") {
            if ($('#AddOnBodyPartStatuesID').val() == 1 && $('#AddBodyPartsAcceptedDate').val() == "") {
                $("#AddBodyPartsAcceptedDate").addClass("border_r");
                return false;
            }
            else {
                $("#AddBodyPartsAcceptedDate").removeClass("border_r");
            }
            showLoader();
            var _PatAddOnBodyPartName = $("#AddOnBodyPartID").find("option:selected").text();
            var _PatAddOnBodyPartId = $("#AddOnBodyPartID").val();
            var _PatientClaimAddOnBodyPartID = $('#PatientClaimAddOnBodyPartID').val();
            //-----mahi-----------------
            var viewModelAddOnBodyPartDetails = {
                PatientClaimAddOnBodyPartID: _PatientClaimAddOnBodyPartID,
                PatientClaimID: $('#PatientClaimID').val(),
                AddOnBodyPartID: _PatAddOnBodyPartId,
                BodyPartStatusID: $('#AddOnBodyPartStatuesID').val(),
                PatAddOnBodyPartName: _PatAddOnBodyPartName,
                AcceptedDate: $('#AddBodyPartsAcceptedDate').val(),
                AddOnBodyPartCondition: self.AddOnBodyPartCondition()
            }
            var plainJs = ko.toJS(viewModelAddOnBodyPartDetails);

            $.ajax({
                url: "/Patient/SavePatientClaimAddOnBodyPart",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify(plainJs),
                success: function (data) {
                    if (data != 0) {
                        $.post("/Patient/GetPatientClaimAddOnBodyPartByPatientClaimId", {
                            _patientClaimID: $('#HDPatientClaimID').val(), _skip: 0
                        }, function (model) {
                            hideLoader();
                            if (model.PatientClaimAddOnBodyPartDetails != null) {
                                if (self.AddOnBodyPartResults()!=null)
                                    self.AddOnBodyPartResults.removeAll();
                                ko.mapping.fromJS(model.PatientClaimAddOnBodyPartDetails, {}, self.AddOnBodyPartResults);
                                ko.mapping.fromJS(model.TotalCountClaimAddOnBodyPart, {}, self.bpTotalItemCount);
                                _totalCount = model.TotalCountClaimAddOnBodyPart;
                                //self.AddOnBodyPartCondition("");
                                if(data=="-1")
                                {
                                    alertify.alert("Updated Successfully", function () {
                                        $('#addAddOnBodyPart').modal('hide');
                                    });
                                }
                                else
                                {
                                    alertify.alert("Added Successfully", function () {
                                        $('#addAddOnBodyPart').modal('hide');
                                    });
                                }
                                self.bpPager().bpCurrentPage(1);
                                self.bpSkip = ko.observable(0);
                                unblockPopupBackground();
                            }
                        });
                    }
                    else {
                        hideLoader();
                        alert("Error Occur");

                    }
                },
                error: function (data) {
                    hideLoader();
                    alert("Error Occur");
                }
            });
         
        }
    };

    self.removeAddOnBodyPart = function (item) {
        alertify.confirm("Are you sure you want to delete this record?", function (e) {
            if (e) {
                if (item.PatientClaimAddOnBodyPartID() != 0) {
                    showLoader();
                    $.post("/Patient/DeletePatientClaimAddOnBodyPart", {
                        _patientAddOnBodyPartID: item.PatientClaimAddOnBodyPartID()
                    }, function (data) {
                        if (data != 0) {
                            $.post("/Patient/GetPatientClaimAddOnBodyPartByPatientClaimId", {
                                _patientClaimID: $('#HDPatientClaimID').val(), _skip: $('#hidskipbp').val()
                            }, function (model) {
                                hideLoader();
                                    self.AddOnBodyPartResults.removeAll();
                                    ko.mapping.fromJS(model.PatientClaimAddOnBodyPartDetails, {}, self.AddOnBodyPartResults);
                                    ko.mapping.fromJS(model.TotalCountClaimAddOnBodyPart, {}, self.bpTotalItemCount);
                                    _totalCount = model.TotalCountClaimAddOnBodyPart;
                                    var _skipValue = $('#hidskipbp').val();
                                    var _totalCount = self.bpTotalItemCount();                                 
                                    if ((self.bpTotalItemCount() % $('#hidskipbp').val()) == 0)
                                        self.bpPager().bpCurrentPage(self.bpPager().bpCurrentPage() - 1);
                                    else
                                        self.bpPager().bpCurrentPage();
                                
                        });
                            self.bpPager().bpCurrentPage(1);
                                self.bpSkip = ko.observable(0);
                            alertify.alert("Deleted Successfully");
                        }
                        else {
                            hideLoader();
                            alert("Error Occur")
                        }

                    });
                    
                    hideLoader();
                }
             
            }
        });
    }


    self.AddNewPatientPleadBodyPart = function () {
        if ($("#PleadBodyPartID").val() != "") {
            if ($('#PleadBodyPartStatuesID').val() == 1 && $('#AcceptedDate').val() == "") {
                $("#AcceptedDate").addClass("border_r");
               // unblockPopupBackground();
              //  hideLoader();
                return false;
            }
            else {
                $("#AcceptedDate").removeClass("border_r");
            }
            showLoader();
            var _PleadBodyPartName = $("#PleadBodyPartID").find("option:selected").text();
            var _PleadBodyPartId = $("#PleadBodyPartID").val()

            //-----mahi-----------------
            var viewModelPleadBodyPartDetails = {
                PatientClaimPleadBodyPartID: $('#PatientClaimPleadBodyPartID').val(),
                PatientClaimID: $('#PatientClaimID').val(),
                PleadBodyPartID: _PleadBodyPartId,
                BodyPartStatusID: $('#PleadBodyPartStatuesID').val(),
                PatPleadBodyPartName: _PleadBodyPartName,
                AcceptedDate: $('#AcceptedDate').val(),
                PleadBodyPartCondition: self.PleadBodyPartCondition()
            }
            var plainJs = ko.toJS(viewModelPleadBodyPartDetails);
        
            $.ajax({
                url: "/Patient/SavePatientClaimPleadBodyPart",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify(plainJs),
                success: function (data) {
                    if (data != 0) {
                        $.post("/Patient/GetPatientClaimPleadBodyPartByPatientClaimId", {
                            _patientClaimID: $('#HDPatientClaimID').val(), _skip: 0
                        }, function (model) {
                            hideLoader();
                            if (model.PatientClaimPleadBodyPartDetails != null) {
                                if(self.PleadBodyPartResults()!=null)
                                    self.PleadBodyPartResults.removeAll();
                                //self.PleadBodyPartCondition("");
                                ko.mapping.fromJS(model.PatientClaimPleadBodyPartDetails, {}, self.PleadBodyPartResults);
                                ko.mapping.fromJS(model.TotalCountClaimPleadBodyPart, { }, self.pbTotalItemCount);
                                _totalCount = model.TotalCountClaimPleadBodyPart;
                                if (data == -1) {
                                    alertify.alert("Updated Successfully", function () {
                                        $('#addPleadBodyPart').modal('hide');
                                    });
                                }
                                else {
                                    alertify.alert("Added Successfully", function () {
                                        $('#addPleadBodyPart').modal('hide');
                                    });

                                }

                                self.pbPager().pbCurrentPage(1);
                                self.pbSkip = ko.observable(0);
                                unblockPopupBackground();
                            }
                        });
                    }
                    //else if (data != 0) {

                    //}
                    else {
                        hideLoader();
                        alert("Error Occur");
                    }
                },
                error: function (data) {
                    hideLoader();
                    alert("Error Occur");

                }
            });
          
        }
    };

    self.removePleadBodyPart = function (item) {
        alertify.confirm("Are you sure you want to delete this record?", function (e) {
            if (e) {
                if (item.PatientClaimPleadBodyPartID() != 0) {
                    showLoader();
                    $.post("/Patient/DeletePatientClaimPleadBodyPart", {
                        _patientPleadBodyPartID: item.PatientClaimPleadBodyPartID()
                    }, function (data) {
                        if (data != 0) {
                            $.post("/Patient/GetPatientClaimPleadBodyPartByPatientClaimId", {
                                _patientClaimID: $('#PatientClaimID').val(), _skip: $('#hidskippb').val()
                            }, function (model) {
                              
                                self.PleadBodyPartResults.removeAll();
                                ko.mapping.fromJS(model.PatientClaimPleadBodyPartDetails, {}, self.PleadBodyPartResults);
                                ko.mapping.fromJS(model.TotalCountClaimPleadBodyPart, {}, self.pbTotalItemCount);
                                _totalCount = model.PatientClaimPleadBodyPartDetails;
                                    var _skipValue = $('#hidskippb').val();
                                    var _totalCount = self.pbTotalItemCount();

                                    if ((self.pbTotalItemCount() % $('#hidskippb').val()) == 0)
                                        self.pbPager().pbCurrentPage(self.pbPager().pbCurrentPage() - 1);
                                    else
                                        self.pbPager().pbCurrentPage();
                               
                    });
                               self.pbPager().pbCurrentPage(1);
                                self.pbSkip = ko.observable(0);
                            alertify.alert("Deleted Successfully");
                        }
                        else {
                            hideLoader();
                            alert("Error Occur")
                        }

                    });
                    if (self.ClaimStatusIDVal() == 3) {
                        self.ShowUpdatebtn(true);
                    }
                    else {
                        self.ShowUpdatebtn(false);
                    }
                   
                    hideLoader();
                }
            
            }
        });
    }

    self.updatePleadBodyPart = function (item) {
        blockPopupBackground();
     
        $('#PleadBodyPartID').prop("disabled", true);
        self.selectedItemPatientPleadBodys(item.PleadBodyPartID())
        $('#PatientClaimPleadBodyPartID').val(item.PatientClaimPleadBodyPartID());
      
        if (self.ClaimStatusIDVal() == 3) {
            self.selectedItemPleadBodyPartStatues(item.BodyPartStatusID());
        
            $('#PleadBodyPartStatuesID').prop("disabled", false);
            
            self.ShowUpdatebtn(true);
        }
        else {
            self.ShowUpdatebtn(false);
        }
        PBPdisableAcceptedDate();
        if (item.AcceptedDate()!=null)
        $('#AcceptedDate').val(moment(item.AcceptedDate()).format("MM/DD/YYYY"));
        // $('#AcceptedDate').val(item.AcceptedDate());

        $('#Condition').val(item.PatPleadBodyPartCondition());

        $('#addPleadBodyPart').modal('show');
    
    }
    $('#PleadBodyPartStatuesID').change(function () {      
        PBPdisableAcceptedDate();
     
    })

   


    function PBPdisableAcceptedDate() {
        if ($('#PleadBodyPartStatuesID').val() == 1) {
            $('#AcceptedDate').prop("disabled", false);
        }
        else {
            $('#AcceptedDate').val('');
            $('#AcceptedDate').prop("disabled", true);
        }
    }

    self.addPleadBodyPart = function (item) {
        $('#AcceptedDate').val('');
      //  $('#PleadBodyPartStatuesID').val(1);
        self.PleadBodyPartCondition("");
        $('#PleadBodyPartID').prop("disabled", false);
        if (self.ClaimStatusIDVal()== 1) {
            self.selectedItemPleadBodyPartStatues(3);
            $('#PleadBodyPartStatuesID').prop("disabled", true);
        }
        else if (self.ClaimStatusIDVal()== 2) {
            self.selectedItemPleadBodyPartStatues(2);
            $('#PleadBodyPartStatuesID').prop("disabled", true);
        }
        else if (self.ClaimStatusIDVal() == 4) {
            self.selectedItemPleadBodyPartStatues(1);
            $('#PleadBodyPartStatuesID').prop("disabled", true);
        }
        else if (self.ClaimStatusIDVal() == 3) {
        
            $('#PleadBodyPartStatuesID').prop("disabled", false);
          
            self.selectedItemPleadBodyPartStatues(1);
          
        }
        PBPdisableAcceptedDate();
        $('#PatientClaimPleadBodyPartID').val(item.PatientClaimPleadBodyPartID());
    }


    self.addBodyPart = function (item) {
        $('#AddBodyPartsAcceptedDate').val('');
       
        self.PleadBodyPartCondition("");
        //$('#AddOnBodyPartID').prop("disabled", false);

        //$('#AddOnBodyPartStatuesID').prop("disabled", false);
        self.selectedItemAddBodyPartStatues(1);

        //ABPdisableAcceptedDate();
        self.selectedItemPatientAddONBodys(19);
        $('#PatientClaimAddOnBodyPartID').val(item.PatientClaimAddOnBodyPartID());
    }

    self.updateAddOnBodyPart = function (item) {
        blockPopupBackground();

       // $('#AddOnBodyPartID').prop("disabled", true);
        self.selectedItemPatientAddONBodys(item.AddOnBodyPartID())
        $('#PatientClaimAddOnBodyPartID').val(item.PatientClaimAddOnBodyPartID());
        
        self.selectedItemAddBodyPartStatues(item.BodyPartStatusID());
        //$('#AddOnBodyPartStatuesID').prop("disabled", false);

       // ABPdisableAcceptedDate();
        if (item.AcceptedDate() != null)
            $('#AddBodyPartsAcceptedDate').val(moment(item.AcceptedDate()).format("MM/DD/YYYY"));
        // $('#AcceptedDate').val(item.AcceptedDate());

        $('#AddOnBodyPartCondition').val(item.PatAddOnBodyPartCondition());
        self.AddOnBodyPartCondition(item.PatAddOnBodyPartCondition());
        $('#addAddOnBodyPart').modal('show');

    }
    //$('#AddOnBodyPartStatuesID').change(function () {
    //    ABPdisableAcceptedDate();

    //})




    function ABPdisableAcceptedDate() {
        if ($('#AddOnBodyPartStatuesID').val() == 1) {
            $('#AddBodyPartsAcceptedDate').prop("disabled", false);
        }
        else {
            $('#AddBodyPartsAcceptedDate').val('');
            $('#AddBodyPartsAcceptedDate').prop("disabled", true);
        }
    }

    

    self.AddNewClaimDiagonsis = function (claimdiag) {
        showLoader();
        //-----mahi-----------------
        var viewModelClaimDiagonsisDetails = {
            PatientClaimDiagnosisID: 0,
            PatientClaimID: $('#PatientClaimID').val(),
            icdICDNumberID : claimdiag.icdICDNumberID(),
            icdICDNumber: claimdiag.icdICDNumber(),
            icdICDTab: claimdiag.icdICDTab(),
            ICDDescription: claimdiag.ICDDescription()

        }
        var plainJs = ko.toJS(viewModelClaimDiagonsisDetails);

        $.ajax({
            url: "/Patient/SavePatientClaimDiagnose",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(plainJs),
            success: function (data) {
                if (data != 0) {
                    $.post("/Patient/GetPatientClaimDiagnoseByPatientClaimId", {
                        _patientClaimID: $('#HDPatientClaimID').val(), _skip: 0
                    }, function (model) {
                        hideLoader();
                        if (model.PatientClaimDiagnoseDetails != null) {
                            if(self.icdResults()!=null)
                               self.icdResults.removeAll();
                            ko.mapping.fromJS(model.PatientClaimDiagnoseDetails, {}, self.icdResults);
                            ko.mapping.fromJS(model.TotalCountClaimDiagnose, { }, self.cdTotalItemCount);
                            _totalCount = model.TotalCountClaimDiagnose;                            
                            alertify.alert('Added Successfully', function () {
                                $("#SearchDiagnosisTitle").val('');
                                $('#addicd9').modal('hide');
                                $('#divuploadFile').show();
                            });
                            
                            self.icdSearchDataResult.removeAll();
                            self.TotalItemCount(0);
                                self.cdPager().cdCurrentPage(1);
                                self.cdSkip = ko.observable(0);
                            unblockPopupBackground();
                        }
                    });
                }
                else {
                    hideLoader();
                    alert("Error Occur");
                }
            },
            error: function (data) {
                hideLoader();
                alert("Error Occur");

            }
        });

    }
    $("#buttonCloseCD").click(function () {
        self.icdSearchDataResult.removeAll();
        self.TotalItemCount(0);
        $("#SearchDiagnosisTitle").val('');
    });
   
  
    self.SearchNewDiagnosis = function () {       
        var searchText = $("#SearchDiagnosisTitle").val();
        if (searchText == "") {
            alert("Diagnosis code required")
        }
        else {
            GetDiagnosisSearchByName(searchText);
        }
        
    }

    function GetDiagnosisSearchByName(searchText)
    {
        $.post("/Common/getPatientClaimDiagnosesAll/", {
            _searchName: searchText, _icdTab: $("#ICDDrp").val(), _skip: 0
        }, function (_data) {
            if (self.icdSearchDataResult() != null)
                self.icdSearchDataResult.removeAll();
            ko.mapping.fromJS(_data.ICDCodeDetails, {}, self.icdSearchDataResult);
            ko.mapping.fromJS(_data.TotalCount, {}, self.TotalItemCount);
            self.Pager().CurrentPage(1);
        });
    }

    $("#SearchDiagnosisTitle").keypress(function (e) {//To Search when enter is pressed
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            var searchText = $("#SearchDiagnosisTitle").val();
            GetDiagnosisSearchByName(searchText);
        }
    });





    self.removeClaimDiagonsis = function (item) {
        alertify.confirm("Are you sure you want to delete this record?", function (e) {
            if (e) {
                if (item.PatientClaimDiagnosisID() != 0) {
                    showLoader();
                    $.post("/Patient/DeletePatientClaimDiagnose", {
                        _patientClaimDiagnoseID: item.PatientClaimDiagnosisID()
                    }, function (data) {
                        if (data != 0) {
                            $.post("/Patient/GetPatientClaimDiagnoseByPatientClaimId", {
                                _patientClaimID: $("#HDPatientClaimID").val(), _skip: $('#hidskipcd').val()
                            }, function (model) {

                                self.icdResults.removeAll();
                                ko.mapping.fromJS(model.PatientClaimDiagnoseDetails, {}, self.icdResults);
                                ko.mapping.fromJS(model.TotalCountClaimDiagnose, {}, self.cdTotalItemCount);
                                _totalCount = model.TotalCountClaimDiagnose;
                                var _skipValue = $('#hidskipcd').val();
                                var _totalCount = self.cdTotalItemCount();

                                if ((self.cdTotalItemCount() % $('#hidskipcd').val()) == 0)
                                    self.cdPager().cdCurrentPage(self.cdPager().cdCurrentPage() - 1);
                                else
                                    self.cdPager().cdCurrentPage();

                            });
                            alertify.alert("Deleted Successfully");
                        }
                        else {
                            alert("Error Occur")
                        }

                });
                    hideLoader();
            }

            }
            });
            }

    self.bindDiagAllPatietMedRec = function () {
        $.post("/Patient/GetPatientClaimDiagnoseByPatientClaimId", {
            _patientClaimID: $('#HDPatientClaimID').val(), _skip: 0
        }, function (model) {
            if (model.PatientClaimDiagnoseDetails != null) {
                if (self.icdResults() != null)
                    self.icdResults.removeAll();
                ko.mapping.fromJS(model.PatientClaimDiagnoseDetails, {}, self.icdResults);
                ko.mapping.fromJS(model.TotalCountClaimDiagnose, {}, self.cdTotalItemCount);
                _totalCount = model.TotalCountClaimDiagnose;
                
                self.icdSearchDataResult.removeAll();
                self.TotalItemCount(0);
                self.cdPager().cdCurrentPage(1);
                self.cdSkip = ko.observable(0);
            }
        });
    }


    //Patient Claim Physician
    //--------------------------------------------------------------------------//
    //--------------------------------------------------------------------------//
    self.PatPhysicianID = ko.observableArray();
    self.PhysicianName = ko.observable();
    self.PhysicianAddress = ko.observable();
    self.PhysicianId = ko.observable();
    self.PhysStateName = ko.observable();
    self.PhysCity = ko.observable();
    self.PhysAddress1 = ko.observable();
    self.PhysAddress2 = ko.observable();
    self.PhysZip = ko.observable();

   
    self.PhysicianSearchResults = ko.observableArray();    


    $('.OpenPopPatientPhysician').click(function () {
        blockPopupBackground();
    });

    $('.patPhysicianSearchClosePop').click(function () {
        unblockPopupBackground();
        if (self.PhysicianSearchResults() != null)
            self.PhysicianSearchResults.removeAll();
        $("#SearchTextByName").val('');
        self.pTotalItemCount(0);
        $("#hidPhysicianskip").val('');       
    });

    self.PhysicianSearchName = function ()    {
        var _searchtext = $("#SearchTextByName").val();
        $.post("/Physician/GetPhysicianByNamePatient", {
            _searchText: _searchtext, _skip: 0
        }, function (_dataResult) {
            var _data = $.parseJSON(_dataResult);
            if (self.PhysicianSearchResults() != null)
                self.PhysicianSearchResults.removeAll();
            ko.mapping.fromJS(_data.PhysicianDetails, {}, self.PhysicianSearchResults);
            self.pTotalItemCount(_data.TotalCount);
            self.pPager().pCurrentPage(1);
        });
    }

  
    self.bindPatientClaimID = function (_patPhysicianID) {
        if (_patPhysicianID != null) {         
            var objphysicianID = _patPhysicianID;
                $.post("/Physician/GetPhysicianByID", {
                    _physicianID: objphysicianID
                }, function (item) {
                    self.PhysicianId(item.PhysicianId);
                    self.PhysicianName(item.PhysFirstName + ' ' + item.PhysLastName);                  
                    self.PhysicianAddress(item.PhysAddress1 + ' ' + item.PhysAddress2);
                    self.PhysCity(item.PhysCity);
                    self.PhysStateName(item.PhysStateName);
                    self.PhysZip(item.PhysZip);
                });
        };
    }

    self.selectPhysician = function (item) {
         showLoader();
        var viewModelClaimPhysicianDetails = {
            PatientClaimID: $('#PatientClaimID').val(),
            PatientID: 0,
            PatClaimNumber: '',
            PatDOI: '',
            PatPolicyYear: '',
            PatClaimJurisdictionId: 0,
            PatAdjudicationStateCaseNumber: '',
            PatEDIExchangeTrackingNumber: '',
            //PatInjuryType: '',
            //PatInjuryDescription: '',
            PatInjuryReportedDate: '',
            PatAdjusterID: 0,
            PatApplicantAttorneyID: 0,
            PatDefenseAttorneyID: 0,
            PatClientID: 0,
            PatCaseManagerID:0,
            PatPhysicianID: item.PhysicianId(),
            PatientName: '',
            ClientName: '',
            ClaimAdministratorName: '',
            ClaimStatusID: 0,
            PatientClaimStatusID: 0,
            DeniedRationale: ''
        }
        var plainJs = ko.toJS(viewModelClaimPhysicianDetails);

        $.ajax({
            url: "/Patient/SavePatientPhysician",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(plainJs),
            success: function (data) {
                if (data != 0) {
                     hideLoader();
                    alertify.alert("Saved Sucessfully");
                }
                else if (data == 0) {
                     hideLoader();
                     alert("Error Occur");
                }
            },
            error: function (data) {
                 hideLoader();
                 alert("Error Occur");
            }
        });



        self.PhysicianId(item.PhysicianId());
        self.PhysicianName(item.PhysFirstName() + ' ' + item.PhysLastName());
        self.PhysicianAddress(item.PhysAddress1() + ' ' + item.PhysAddress2());
        self.PhysCity(item.PhysCity());
        self.PhysStateName(item.PhysStateName());
        self.PhysZip(item.PhysZip());
        $('.patPhysicianSearchClosePop').click();
    };
  
     //--------------------------------------------------------------------------//
    //--------------------------------------------------------------------------//


                /*-------------- Paging for Search Claim Diagnosis -----------*/
    self.GetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }

        var searchText = $("#SearchDiagnosisTitle").val();
        if (searchText == "") {
            alert("Diagnosis code required")
        }
        else {

            $.post("/Common/getPatientClaimDiagnosesAll/", {
                _searchName: searchText, _icdTab: $("#ICDDrp").val(), _skip: skip
            }, function (model) {
            
                self.icdSearchDataResult.removeAll();
                ko.mapping.fromJS(model.ICDCodeDetails, {}, self.icdSearchDataResult);
                ko.mapping.fromJS(model.TotalCount, {}, self.TotalItemCount);

            });
        }
    };
    /*--------------------Search Claim Diagnosis end--------------------------------*/
   // ========= paging code for claim status grid only ===========//
    self.csGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.csSkip(0);
            self.csTake(cspagingSetting.cspageSize);
        }
        else {
            self.csSkip(skip);
            self.csTake(take);
        }

        $.post("/Patient/GetPatientClaimStatusByPatientClaimId", {
                _patientClaimID: $("#HDPatientClaimID").val(), _skip: skip // $('#hidskipcs').val()
        }, function (model) {    
            self.ClaimStatusResults.removeAll();
            ko.mapping.fromJS(model.PatientClaimStatusDetails, {}, self.ClaimStatusResults);
            ko.mapping.fromJS(model.TotalCountClaimStatus, {}, self.csTotalItemCount);
       
        });

    }

    var cspagingSetting = {
        cspageSize: 10,
        cspageSlide: 2
    };

    self.csSkip = ko.observable(0);
    self.csTake = ko.observable(cspagingSetting.cspageSize);    
    self.csPager = ko.cspager(self.csTotalItemCount);
    
    self.csPager().csPageSize(cspagingSetting.cspageSize);
    self.csPager().csPageSlide(cspagingSetting.cspageSlide);
    self.csPager().csCurrentPage(1);

    self.csPager().csCurrentPage.subscribe(function () {        
        var skip = cspagingSetting.cspageSize * (self.csPager().csCurrentPage() - 1);
        var take = cspagingSetting.cspageSlide;
        self.csGetRecordsWithSkipTake(skip, take);
    });

    self.csLastPage = ko.computed(function () {
        return Math.floor((self.csTotalItemCount() - 1) / cspagingSetting.cspageSize) + 1;
    });
    // ============= paging code for claim status ends =============== //

    //========= paging code for addon Body Part grid only ===========//
    self.bpGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.bpSkip(0);
            self.bpTake(bppagingSetting.bppageSize);
        }
        else {
            self.bpSkip(skip);
            self.bpTake(take);
        }
        $.post("/Patient/GetPatientClaimAddOnBodyPartByPatientClaimId", {
                _patientClaimID: $("#HDPatientClaimID").val(), _skip: skip // $('#hidskipbp').val()
        }, function (model) {           
            self.AddOnBodyPartResults.removeAll();
            ko.mapping.fromJS(model.PatientClaimAddOnBodyPartDetails, {}, self.AddOnBodyPartResults);
            ko.mapping.fromJS(model.TotalCountClaimAddOnBodyPart, {}, self.bpTotalItemCount);

        });       
    }

    var bppagingSetting = {
        bppageSize: 10,
        bppageSlide: 2
    };

    self.bpSkip = ko.observable(0);
    self.bpTake = ko.observable(bppagingSetting.bppageSize);
    self.bpPager = ko.bppager(self.bpTotalItemCount);

    self.bpPager().bpPageSize(bppagingSetting.bppageSize);
    self.bpPager().bpPageSlide(bppagingSetting.bppageSlide);
    self.bpPager().bpCurrentPage(1);

    self.bpPager().bpCurrentPage.subscribe(function () {
        var skip = bppagingSetting.bppageSize * (self.bpPager().bpCurrentPage() - 1);
        var take = bppagingSetting.bppageSlide;
        self.bpGetRecordsWithSkipTake(skip, take);
    });

    self.bpLastPage = ko.computed(function () {
        return Math.floor((self.bpTotalItemCount() - 1) / bppagingSetting.bppageSize) + 1;
    });
    // ============= paging code for addon Body Part ends =============== //


    //========= paging code for plead body parts grid only ===========//
    self.pbGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.pbSkip(0);
            self.pbTake(pbpagingSetting.pbpageSize);
        }
        else {
            self.pbSkip(skip);
            self.pbTake(take);
        }      
        $.post("/Patient/GetPatientClaimPleadBodyPartByPatientClaimId", {
            _patientClaimID: $("#HDPatientClaimID").val(), _skip: skip // $('#hidskippb').val()
        }, function (model) {
            //  var model = $.parseJSON(_data);
            self.PleadBodyPartResults.removeAll();
            ko.mapping.fromJS(model.PatientClaimPleadBodyPartDetails, {}, self.PleadBodyPartResults);
            ko.mapping.fromJS(model.TotalCountClaimPleadBodyPart, {}, self.pbTotalItemCount);

        });
      //  GrdBinding();
    }

    var pbpagingSetting = {
        pbpageSize: 10,
        pbpageSlide: 2
    };
    self.pbSkip = ko.observable(0);
    self.pbTake = ko.observable(pbpagingSetting.pbpageSize);
    self.pbPager = ko.pbpager(self.pbTotalItemCount);

    self.pbPager().pbPageSize(pbpagingSetting.pbpageSize);
    self.pbPager().pbPageSlide(pbpagingSetting.pbpageSlide);
    self.pbPager().pbCurrentPage(1);

    self.pbPager().pbCurrentPage.subscribe(function () {
        var skip = pbpagingSetting.pbpageSize * (self.pbPager().pbCurrentPage() - 1);
        var take = pbpagingSetting.pbpageSlide;
        self.pbGetRecordsWithSkipTake(skip, take);
    });
    // ============= paging code for plead body parts ends =============== //

    //========= paging code for ICD grid only ===========//

    self.cdGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.cdSkip(0);
            self.cdTake(cdpagingSetting.cdpageSize);
        }
        else {
            self.cdSkip(skip);
            self.cdTake(take);
        }
        $.post("/Patient/GetPatientClaimDiagnoseByPatientClaimId", {
                _patientClaimID: $("#HDPatientClaimID").val(), _skip: skip // $('#hidskipcd').val()
        }, function (model) {        
            self.icdResults.removeAll();
            ko.mapping.fromJS(model.PatientClaimDiagnoseDetails, {}, self.icdResults);
            ko.mapping.fromJS(model.TotalCountClaimDiagnose, {}, self.cdTotalItemCount);

        });
      
    }

    var cdpagingSetting = {
        cdpageSize: 10,
        cdpageSlide: 2
    };
    self.cdSkip = ko.observable(0);
    self.cdTake = ko.observable(cdpagingSetting.cdpageSize);
    self.cdPager = ko.cdpager(self.cdTotalItemCount);

    self.cdPager().cdPageSize(cdpagingSetting.cdpageSize);
    self.cdPager().cdPageSlide(cdpagingSetting.cdpageSlide);
    self.cdPager().cdCurrentPage(1);

    self.cdPager().cdCurrentPage.subscribe(function () {
        var skip = cdpagingSetting.cdpageSize * (self.cdPager().cdCurrentPage() - 1);
        var take = cdpagingSetting.cdpageSlide;
        self.cdGetRecordsWithSkipTake(skip, take);
    });



    // ============= paging code for ICD ends =============== //

    //========= paging code for Emp grid only ===========//

    self.EmpGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.EmpSkip(0);
            self.EmpTake(EmppagingSetting.EmppageSize);
        }
        else {
            self.EmpSkip(skip);
            self.EmpTake(take);
        }
      
        $.post("/Patient/getEmployerByClientIdAll", {
            clientID: $("#ClmClient").val(), skip: skip
        }, function (_data) {
            var model = $.parseJSON(_data);
            self.ClientEmployerDetails.removeAll();
            self.ClientEmployerDetails(model.ClientEmployerDetails);
            self.EmpTotalItemCount(model.TotalCount);

        });
   
    

    }

    var EmppagingSetting = {
        EmppageSize: 5,
        EmppageSlide: 2
    };
    self.EmpSkip = ko.observable(0);
    self.EmpTake = ko.observable(EmppagingSetting.EmppageSize);
    self.EmpPager = ko.Emppager(self.EmpTotalItemCount);

    self.EmpPager().EmpPageSize(EmppagingSetting.EmppageSize);
    self.EmpPager().EmpPageSlide(EmppagingSetting.EmppageSlide);
    self.EmpPager().EmpCurrentPage(1);

    self.EmpPager().EmpCurrentPage.subscribe(function () {
        var skip = EmppagingSetting.EmppageSize * (self.EmpPager().EmpCurrentPage() - 1);
        var take = EmppagingSetting.EmppageSlide;
        self.EmpGetRecordsWithSkipTake(skip, take);
    });



// ============= paging code for Emp ends =============== //

    //========= paging code for Ins grid only ===========//

    self.InsGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.InsSkip(0);
            self.InsTake(InspagingSetting.InspageSize);
        }
        else {
            self.InsSkip(skip);
            self.InsTake(take);
        }
      
        $.post("/Patient/getInsurerByClientIdAll", {
            clientID: $("#ClmClient").val(), skip: skip
        }, function (_data) {
            var model = $.parseJSON(_data);
            self.ClientInsurerDetails.removeAll();
            self.ClientInsurerDetails(model.ClientInsurerDetails);
            self.InsTotalItemCount(model.TotalCount);

        });
   
    

    }

    var InspagingSetting = {
        InspageSize: 5,
        InspageSlide: 2
    };
    self.InsSkip = ko.observable(0);
    self.InsTake = ko.observable(InspagingSetting.InspageSize);
    self.InsPager = ko.Inspager(self.InsTotalItemCount);

    self.InsPager().InsPageSize(InspagingSetting.InspageSize);
    self.InsPager().InsPageSlide(InspagingSetting.InspageSlide);
    self.InsPager().InsCurrentPage(1);

    self.InsPager().InsCurrentPage.subscribe(function () {
        var skip = InspagingSetting.InspageSize * (self.InsPager().InsCurrentPage() - 1);
        var take = InspagingSetting.InspageSlide;
        self.InsGetRecordsWithSkipTake(skip, take);
    });



// ============= paging code for INs ends =============== //

    //========= paging code for TPA grid only ===========//

    self.TPAGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.TPASkip(0);
            self.TPATake(TPApagingSetting.TPApageSize);
        }
        else {
            self.TPASkip(skip);
            self.TPATake(take);
        }
      
        $.post("/Patient/getThirdPartyAdministratorByClientIdAll", {
            clientID: $("#ClmClient").val(), skip: skip
        }, function (_data) {
            var model = $.parseJSON(_data);
            self.ClientTPADetails.removeAll();
            self.ClientTPADetails(model.ClientTPADetails);
            self.TPATotalItemCount(model.TotalCount);

        });
   
    

    }

    var TPApagingSetting = {
        TPApageSize: 5,
        TPApageSlide: 2
    };
    self.TPASkip = ko.observable(0);
    self.TPATake = ko.observable(TPApagingSetting.TPApageSize);
    self.TPAPager = ko.TPApager(self.TPATotalItemCount);

    self.TPAPager().TPAPageSize(TPApagingSetting.TPApageSize);
    self.TPAPager().TPAPageSlide(TPApagingSetting.TPApageSlide);
    self.TPAPager().TPACurrentPage(1);

    self.TPAPager().TPACurrentPage.subscribe(function () {
        var skip = TPApagingSetting.TPApageSize * (self.TPAPager().TPACurrentPage() - 1);
        var take = TPApagingSetting.TPApageSlide;
        self.TPAGetRecordsWithSkipTake(skip, take);
    });



    // ============= paging code for INs ends =============== //

    var pagingSettings = {
        pageSize: 10,
        pageSlide: 2
    };
    self.Skip = ko.observable(0);
    self.Take = ko.observable(pagingSettings.pageSize);
    self.Pager = ko.pager(self.TotalItemCount);
    self.Pager().PageSize(pagingSettings.pageSize);
    self.Pager().PageSlide(pagingSettings.pageSlide);
    self.Pager().CurrentPage(1);
    self.Pager().CurrentPage.subscribe(function () {
        var skip = pagingSettings.pageSize * (self.Pager().CurrentPage() - 1);
        var take = pagingSettings.pageSize;
        self.GetRecordsWithSkipTake(skip, take);
    });


       // ========= paging code for claim status grid only ===========//
    self.csGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.csSkip(0);
            self.csTake(cspagingSetting.cspageSize);
        }
        else {
            self.csSkip(skip);
            self.csTake(take);
        }

        $.post("/Patient/GetPatientClaimStatusByPatientClaimId", {
                _patientClaimID: $("#HDPatientClaimID").val(), _skip: skip // $('#hidskipcs').val()
        }, function (model) {    
            self.ClaimStatusResults.removeAll();
            ko.mapping.fromJS(model.PatientClaimStatusDetails, {}, self.ClaimStatusResults);
            ko.mapping.fromJS(model.TotalCountClaimStatus, {}, self.csTotalItemCount);
       
        });

    }

    var cspagingSetting = {
        cspageSize: 10,
        cspageSlide: 2
    };

    self.csSkip = ko.observable(0);
    self.csTake = ko.observable(cspagingSetting.cspageSize);    
    self.csPager = ko.cspager(self.csTotalItemCount);
    
    self.csPager().csPageSize(cspagingSetting.cspageSize);
    self.csPager().csPageSlide(cspagingSetting.cspageSlide);
    self.csPager().csCurrentPage(1);

    self.csPager().csCurrentPage.subscribe(function () {        
        var skip = cspagingSetting.cspageSize * (self.csPager().csCurrentPage() - 1);
        var take = cspagingSetting.cspageSlide;
        self.csGetRecordsWithSkipTake(skip, take);
    });

    self.csLastPage = ko.computed(function () {
        return Math.floor((self.csTotalItemCount() - 1) / cspagingSetting.cspageSize) + 1;
    });
    // ============= paging code for claim status ends =============== //


    // ========= paging code for Patient Physician grid only ===========//
    self.pGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.pSkip(0);
            self.pTake(ppagingSetting.ppageSize);
        }
        else {
            self.pSkip(skip);
            self.pTake(take);
        }
        var _phySkip = parseInt($("#hidPhysicianskip").val());
        $.post("/Physician/GetPhysicianByNamePatient", {
            _searchText: $("#SearchTextByName").val(), _skip : _phySkip
        }, function (modelResult) {
            var model = $.parseJSON(modelResult);
            if (self.PhysicianSearchResults() != null)
                self.PhysicianSearchResults.removeAll();
            ko.mapping.fromJS(model.PhysicianDetails, { }, self.PhysicianSearchResults);
            ko.mapping.fromJS(model.TotalCount, { }, self.pTotalItemCount);

        });
    }

    var ppagingSetting = {
        ppageSize: 10,
        ppageSlide: 2
    };

    self.pSkip = ko.observable(0);
    self.pTake = ko.observable(ppagingSetting.ppageSize);
    self.pPager = ko.ppager(self.pTotalItemCount);

    self.pPager().pPageSize(ppagingSetting.ppageSize);
    self.pPager().pPageSlide(ppagingSetting.ppageSlide);
    self.pPager().pCurrentPage(1);

    self.pPager().pCurrentPage.subscribe(function () {
        var skip = ppagingSetting.ppageSize * (self.pPager().pCurrentPage() - 1);
        var take = ppagingSetting.ppageSlide;
        self.pGetRecordsWithSkipTake(skip, take);
    });

    self.pLastPage = ko.computed(function () {
        return Math.floor((self.pTotalItemCount() - 1) / ppagingSetting.ppageSize) + 1;
    });

    // ============= paging code for Patient Physician ends =============== //
}


function PatientClaimStatusInsert(_ClaimStatusName, _ClaimStatusId, _PatientClaimID, _PatientClaimStatusID) {
    var self = this;
    self.PatClaimStatusName = ko.observable(_ClaimStatusName);
    self.ClaimStatusID = ko.observable(_ClaimStatusId);
    self.PatientClaimID = ko.observable(_PatientClaimID)
    self.PatientClaimStatusID = ko.observable(_PatientClaimStatusID);

}

function PatientAddOnBodyPartInsert(_PatAddOnBodyPartName, _PatAddOnBodyPartId, PatientClaimID, PatientClaimAddOnBodyPartID) {
    var self = this;
    self.PatAddOnBodyPartName = ko.observable(_PatAddOnBodyPartName);
    self.AddOnBodyPartID = ko.observable(_PatAddOnBodyPartId);
    self.PatientClaimID = ko.observable(PatientClaimID)
    self.PatientClaimAddOnBodyPartID = ko.observable(PatientClaimAddOnBodyPartID);

}

function PatientPleadOnBodyPartInsert(_PleadBodyPartName, _PleadBodyPartId, PatientClaimID, PatientClaimPleadBodyPartID) {
    var self = this;
    self.PatPleadBodyPartName = ko.observable(_PleadBodyPartName);
    self.PleadBodyPartID = ko.observable(_PleadBodyPartId);
    self.PatientClaimID = ko.observable(PatientClaimID)
    self.PatientClaimPleadBodyPartID = ko.observable(PatientClaimPleadBodyPartID);

}

function PatientClaimDiagnosisInsert(icdICDNumber, ICDDescription, PatientClaimID, PatientClaimDiagnosisID) {
    var self = this;
    self.PatientClaimDiagnosisID = ko.observable(PatientClaimDiagnosisID);
    self.PatientClaimID = ko.observable(PatientClaimID);
    self.icdICDNumber = ko.observable(icdICDNumber)
    self.ICDDescription = ko.observable(ICDDescription);

}

//========== pager js for claim status grid only ========//
(function (ko) {
    var csnumericObservable = function (initialValue) {
        var _actual = ko.observable(initialValue);

        var result = ko.dependentObservable({
            read: function () {
                return _actual();
            },
            write: function (newValue) {
                var parsedValue = parseFloat(newValue);
                _actual(isNaN(parsedValue) ? newValue : parsedValue);
            }
        });

        return result;
    };

    function csPager(totalItemCount) {        
        var self = this;
        self.csCurrentPage = csnumericObservable(1);

        self.csTotalItemCount = ko.computed(totalItemCount);

        self.csPageSize = csnumericObservable(10);
        self.csPageSlide = csnumericObservable(2);

        self.csLastPage = ko.computed(function () {
            return Math.floor((self.csTotalItemCount() - 1) / self.csPageSize()) + 1;
        });

        self.csHasNextPage = ko.computed(function () {
            return self.csCurrentPage() < self.csLastPage();
        });

        self.csHasPrevPage = ko.computed(function () {
            return self.csCurrentPage() > 1;
        });

        self.csFirstItemIndex = ko.computed(function () {
            return self.csPageSize() * (self.csCurrentPage() - 1) + 1;
        });

        self.csLastItemIndex = ko.computed(function () {
            return Math.min(self.csFirstItemIndex() + self.csPageSize() - 1, self.csTotalItemCount());
        });

        self.csPages = ko.computed(function () {
            var cspageCount = self.csLastPage();
            var cspageFrom = Math.max(1, self.csCurrentPage() - self.csPageSlide());
            var cspageTo = Math.min(cspageCount, self.csCurrentPage() + self.csPageSlide());
            cspageFrom = Math.max(1, Math.min(cspageTo - 2 * self.csPageSlide(), cspageFrom));
            cspageTo = Math.min(cspageCount, Math.max(cspageFrom + 2 * self.csPageSlide(), cspageTo));

            var result = [];
            for (var i = cspageFrom; i <= cspageTo; i++) {
                result.push(i);
            }
            return result;
        });
    }

    ko.cspager = function (totalItemCount) {
        var cspager = new csPager(totalItemCount);
        return ko.observable(cspager);
    };
}(ko));
//============= pager js for claim status ends  ============//


//========== pager js for  addon Body Part  grid only ========//
(function (ko) {
    var bpnumericObservable = function (initialValue) {
        var _actual = ko.observable(initialValue);

        var result = ko.dependentObservable({
            read: function () {
                return _actual();
            },
            write: function (newValue) {
                var parsedValue = parseFloat(newValue);
                _actual(isNaN(parsedValue) ? newValue : parsedValue);
            }
        });

        return result;
    };

    function bpPager(totalItemCount) {
        var self = this;
        self.bpCurrentPage = bpnumericObservable(1);

        self.bpTotalItemCount = ko.computed(totalItemCount);

        self.bpPageSize = bpnumericObservable(10);
        self.bpPageSlide = bpnumericObservable(2);

        self.bpLastPage = ko.computed(function () {
            return Math.floor((self.bpTotalItemCount() - 1) / self.bpPageSize()) + 1;
        });

        self.bpHasNextPage = ko.computed(function () {
            return self.bpCurrentPage() < self.bpLastPage();
        });

        self.bpHasPrevPage = ko.computed(function () {
            return self.bpCurrentPage() > 1;
        });

        self.bpFirstItemIndex = ko.computed(function () {
            return self.bpPageSize() * (self.bpCurrentPage() - 1) + 1;
        });

        self.bpLastItemIndex = ko.computed(function () {
            return Math.min(self.bpFirstItemIndex() + self.bpPageSize() - 1, self.bpTotalItemCount());
        });

        self.bpPages = ko.computed(function () {
            var bppageCount = self.bpLastPage();
            var bppageFrom = Math.max(1, self.bpCurrentPage() - self.bpPageSlide());
            var bppageTo = Math.min(bppageCount, self.bpCurrentPage() + self.bpPageSlide());
            bppageFrom = Math.max(1, Math.min(bppageTo - 2 * self.bpPageSlide(), bppageFrom));
            bppageTo = Math.min(bppageCount, Math.max(bppageFrom + 2 * self.bpPageSlide(), bppageTo));

            var result = [];
            for (var i = bppageFrom; i <= bppageTo; i++) {
                result.push(i);
            }
            return result;
        });
    }

    ko.bppager = function (totalItemCount) {
        var bppager = new bpPager(totalItemCount);
        return ko.observable(bppager);
    };
}(ko));
//============= pager js for addon Body Part ends  ============//

//========== pager js for Plead body part grid only ========//
(function (ko) {
    var pbnumericObservable = function (initialValue) {
        var _actual = ko.observable(initialValue);

        var result = ko.dependentObservable({
            read: function () {
                return _actual();
            },
            write: function (newValue) {
                var parsedValue = parseFloat(newValue);
                _actual(isNaN(parsedValue) ? newValue : parsedValue);
            }
        });

        return result;
    };

    function pbPager(totalItemCount) {
        var self = this;
        self.pbCurrentPage = pbnumericObservable(1);

        self.pbTotalItemCount = ko.computed(totalItemCount);

        self.pbPageSize = pbnumericObservable(10);
        self.pbPageSlide = pbnumericObservable(2);

        self.pbLastPage = ko.computed(function () {
            return Math.floor((self.pbTotalItemCount() - 1) / self.pbPageSize()) + 1;
        });

        self.pbHasNextPage = ko.computed(function () {
            return self.pbCurrentPage() < self.pbLastPage();
        });

        self.pbHasPrevPage = ko.computed(function () {
            return self.pbCurrentPage() > 1;
        });

        self.pbFirstItemIndex = ko.computed(function () {
            return self.pbPageSize() * (self.pbCurrentPage() - 1) + 1;
        });

        self.pbLastItemIndex = ko.computed(function () {
            return Math.min(self.pbFirstItemIndex() + self.pbPageSize() - 1, self.pbTotalItemCount());
        });

        self.pbPages = ko.computed(function () {
            var pbpageCount = self.pbLastPage();
            var pbpageFrom = Math.max(1, self.pbCurrentPage() - self.pbPageSlide());
            var pbpageTo = Math.min(pbpageCount, self.pbCurrentPage() + self.pbPageSlide());
            pbpageFrom = Math.max(1, Math.min(pbpageTo - 2 * self.pbPageSlide(), pbpageFrom));
            pbpageTo = Math.min(pbpageCount, Math.max(pbpageFrom + 2 * self.pbPageSlide(), pbpageTo));

            var result = [];
            for (var i = pbpageFrom; i <= pbpageTo; i++) {
                result.push(i);
            }
            return result;
        });
    }

    ko.pbpager = function (totalItemCount) {
        var pbpager = new pbPager(totalItemCount);
        return ko.observable(pbpager);
    };
}(ko));
//============= pager js for Plead body part ends  ============//



//========== pager js for   ICD  grid only ========//
(function (ko) {
    var cdnumericObservable = function (initialValue) {
        var _actual = ko.observable(initialValue);

        var result = ko.dependentObservable({
            read: function () {
                return _actual();
            },
            write: function (newValue) {
                var parsedValue = parseFloat(newValue);
                _actual(isNaN(parsedValue) ? newValue : parsedValue);
            }
        });

        return result;
    };

    function cdPager(totalItemCount) {
        var self = this;
        self.cdCurrentPage = cdnumericObservable(1);

        self.cdTotalItemCount = ko.computed(totalItemCount);

        self.cdPageSize = cdnumericObservable(10);
        self.cdPageSlide = cdnumericObservable(2);

        self.cdLastPage = ko.computed(function () {
            return Math.floor((self.cdTotalItemCount() - 1) / self.cdPageSize()) + 1;
        });

        self.cdHasNextPage = ko.computed(function () {
            return self.cdCurrentPage() < self.cdLastPage();
        });

        self.cdHasPrevPage = ko.computed(function () {
            return self.cdCurrentPage() > 1;
        });

        self.cdFirstItemIndex = ko.computed(function () {
            return self.cdPageSize() * (self.cdCurrentPage() - 1) + 1;
        });

        self.cdLastItemIndex = ko.computed(function () {
            return Math.min(self.cdFirstItemIndex() + self.cdPageSize() - 1, self.cdTotalItemCount());
        });

        self.cdPages = ko.computed(function () {
            var cdpageCount = self.cdLastPage();
            var cdpageFrom = Math.max(1, self.cdCurrentPage() - self.cdPageSlide());
            var cdpageTo = Math.min(cdpageCount, self.cdCurrentPage() + self.cdPageSlide());
            cdpageFrom = Math.max(1, Math.min(cdpageTo - 2 * self.cdPageSlide(), cdpageFrom));
            cdpageTo = Math.min(cdpageCount, Math.max(cdpageFrom + 2 * self.cdPageSlide(), cdpageTo));

            var result = [];
            for (var i = cdpageFrom; i <= cdpageTo; i++) {
                result.push(i);
            }
            return result;
        });
    }

    ko.cdpager = function (totalItemCount) {
        var cdpager = new cdPager(totalItemCount);
        return ko.observable(cdpager);
    };
}(ko));
//============= pager js for ICD ends  ============//


//========== pager js for   Emp  grid only ========//
(function (ko) {
    var EmpnumericObservable = function (initialValue) {
        var _actual = ko.observable(initialValue);

        var result = ko.dependentObservable({
            read: function () {
                return _actual();
            },
            write: function (newValue) {
                var parsedValue = parseFloat(newValue);
                _actual(isNaN(parsedValue) ? newValue : parsedValue);
            }
        });

        return result;
    };

    function EmpPager(totalItemCount) {
        var self = this;
        self.EmpCurrentPage = EmpnumericObservable(1);

        self.EmpTotalItemCount = ko.computed(totalItemCount);

        self.EmpPageSize = EmpnumericObservable(10);
        self.EmpPageSlide = EmpnumericObservable(2);

        self.EmpLastPage = ko.computed(function () {
            return Math.floor((self.EmpTotalItemCount() - 1) / self.EmpPageSize()) + 1;
        });

        self.EmpHasNextPage = ko.computed(function () {
            return self.EmpCurrentPage() < self.EmpLastPage();
        });

        self.EmpHasPrevPage = ko.computed(function () {
            return self.EmpCurrentPage() > 1;
        });

        self.EmpFirstItemIndex = ko.computed(function () {
            return self.EmpPageSize() * (self.EmpCurrentPage() - 1) + 1;
        });

        self.EmpLastItemIndex = ko.computed(function () {
            return Math.min(self.EmpFirstItemIndex() + self.EmpPageSize() - 1, self.EmpTotalItemCount());
        });

        self.EmpPages = ko.computed(function () {
            var EmppageCount = self.EmpLastPage();
            var EmppageFrom = Math.max(1, self.EmpCurrentPage() - self.EmpPageSlide());
            var EmppageTo = Math.min(EmppageCount, self.EmpCurrentPage() + self.EmpPageSlide());
            EmppageFrom = Math.max(1, Math.min(EmppageTo - 2 * self.EmpPageSlide(), EmppageFrom));
            EmppageTo = Math.min(EmppageCount, Math.max(EmppageFrom + 2 * self.EmpPageSlide(), EmppageTo));

            var result = [];
            for (var i = EmppageFrom; i <= EmppageTo; i++) {
                result.push(i);
            }
            return result;
        });
    }

    ko.Emppager = function (totalItemCount) {
        var Emppager = new EmpPager(totalItemCount);
        return ko.observable(Emppager);
    };
}(ko));
//============= pager js for Emp ends  ============//
//========== pager js for   Ins  grid only ========//
(function (ko) {
    var InsnumericObservable = function (initialValue) {
        var _actual = ko.observable(initialValue);

        var result = ko.dependentObservable({
            read: function () {
                return _actual();
            },
            write: function (newValue) {
                var parsedValue = parseFloat(newValue);
                _actual(isNaN(parsedValue) ? newValue : parsedValue);
            }
        });

        return result;
    };

    function InsPager(totalItemCount) {
        var self = this;
        self.InsCurrentPage = InsnumericObservable(1);

        self.InsTotalItemCount = ko.computed(totalItemCount);

        self.InsPageSize = InsnumericObservable(10);
        self.InsPageSlide = InsnumericObservable(2);

        self.InsLastPage = ko.computed(function () {
            return Math.floor((self.InsTotalItemCount() - 1) / self.InsPageSize()) + 1;
        });

        self.InsHasNextPage = ko.computed(function () {
            return self.InsCurrentPage() < self.InsLastPage();
        });

        self.InsHasPrevPage = ko.computed(function () {
            return self.InsCurrentPage() > 1;
        });

        self.InsFirstItemIndex = ko.computed(function () {
            return self.InsPageSize() * (self.InsCurrentPage() - 1) + 1;
        });

        self.InsLastItemIndex = ko.computed(function () {
            return Math.min(self.InsFirstItemIndex() + self.InsPageSize() - 1, self.InsTotalItemCount());
        });

        self.InsPages = ko.computed(function () {
            var InspageCount = self.InsLastPage();
            var InspageFrom = Math.max(1, self.InsCurrentPage() - self.InsPageSlide());
            var InspageTo = Math.min(InspageCount, self.InsCurrentPage() + self.InsPageSlide());
            InspageFrom = Math.max(1, Math.min(InspageTo - 2 * self.InsPageSlide(), InspageFrom));
            InspageTo = Math.min(InspageCount, Math.max(InspageFrom + 2 * self.InsPageSlide(), InspageTo));

            var result = [];
            for (var i = InspageFrom; i <= InspageTo; i++) {
                result.push(i);
            }
            return result;
        });
    }

    ko.Inspager = function (totalItemCount) {
        var Inspager = new InsPager(totalItemCount);
        return ko.observable(Inspager);
    };
}(ko));
//============= pager js for Ins ends  ============//

//========== pager js for   TPA  grid only ========//
(function (ko) {
    var TPAnumericObservable = function (initialValue) {
        var _actual = ko.observable(initialValue);

        var result = ko.dependentObservable({
            read: function () {
                return _actual();
            },
            write: function (newValue) {
                var parsedValue = parseFloat(newValue);
                _actual(isNaN(parsedValue) ? newValue : parsedValue);
            }
        });

        return result;
    };

    function TPAPager(totalItemCount) {
        var self = this;
        self.TPACurrentPage = TPAnumericObservable(1);

        self.TPATotalItemCount = ko.computed(totalItemCount);

        self.TPAPageSize = TPAnumericObservable(10);
        self.TPAPageSlide = TPAnumericObservable(2);

        self.TPALastPage = ko.computed(function () {
            return Math.floor((self.TPATotalItemCount() - 1) / self.TPAPageSize()) + 1;
        });

        self.TPAHasNextPage = ko.computed(function () {
            return self.TPACurrentPage() < self.TPALastPage();
        });

        self.TPAHasPrevPage = ko.computed(function () {
            return self.TPACurrentPage() > 1;
        });

        self.TPAFirstItemIndex = ko.computed(function () {
            return self.TPAPageSize() * (self.TPACurrentPage() - 1) + 1;
        });

        self.TPALastItemIndex = ko.computed(function () {
            return Math.min(self.TPAFirstItemIndex() + self.TPAPageSize() - 1, self.TPATotalItemCount());
        });

        self.TPAPages = ko.computed(function () {
            var TPApageCount = self.TPALastPage();
            var TPApageFrom = Math.max(1, self.TPACurrentPage() - self.TPAPageSlide());
            var TPApageTo = Math.min(TPApageCount, self.TPACurrentPage() + self.TPAPageSlide());
            TPApageFrom = Math.max(1, Math.min(TPApageTo - 2 * self.TPAPageSlide(), TPApageFrom));
            TPApageTo = Math.min(TPApageCount, Math.max(TPApageFrom + 2 * self.TPAPageSlide(), TPApageTo));

            var result = [];
            for (var i = TPApageFrom; i <= TPApageTo; i++) {
                result.push(i);
            }
            return result;
        });
    }

    ko.TPApager = function (totalItemCount) {
        var TPApager = new TPAPager(totalItemCount);
        return ko.observable(TPApager);
    };
}(ko));
//============= pager js for TPA ends  ============//


//========== pager js for  Patient Physician  only ========//
(function (ko) {
    var pnumericObservable = function (initialValue) {
        var _actual = ko.observable(initialValue);

        var result = ko.dependentObservable({
            read: function () {
                return _actual();
            },
            write: function (newValue) {
                var parsedValue = parseFloat(newValue);
                _actual(isNaN(parsedValue) ? newValue : parsedValue);
            }
        });

        return result;
    };

    function pPager(totalItemCount) {
        var self = this;
        self.pCurrentPage = pnumericObservable(1);

        self.pTotalItemCount = ko.computed(totalItemCount);

        self.pPageSize = pnumericObservable(10);
        self.pPageSlide = pnumericObservable(2);

        self.pLastPage = ko.computed(function () {
            return Math.floor((self.pTotalItemCount() - 1) / self.pPageSize()) + 1;
        });
            
        self.pHasNextPage = ko.computed(function () {
            return self.pCurrentPage() < self.pLastPage();
        });

        self.pHasPrevPage = ko.computed(function () {
            return self.pCurrentPage() > 1;
        });

        self.pFirstItemIndex = ko.computed(function () {
            return self.pPageSize() * (self.pCurrentPage() - 1) + 1;
        });

        self.pLastItemIndex = ko.computed(function () {
            return Math.min(self.pFirstItemIndex() + self.pPageSize() - 1, self.pTotalItemCount());
        });

        self.pPages = ko.computed(function () {
            var ppageCount = self.pLastPage();
            var ppageFrom = Math.max(1, self.pCurrentPage() - self.pPageSlide());
            var ppageTo = Math.min(ppageCount, self.pCurrentPage() + self.pPageSlide());
            ppageFrom = Math.max(1, Math.min(ppageTo - 2 * self.pPageSlide(), ppageFrom));
            ppageTo = Math.min(ppageCount, Math.max(ppageFrom + 2 * self.pPageSlide(), ppageTo));

            var result = [];
            for (var i = ppageFrom; i <= ppageTo; i++) {
                result.push(i);
            }
            return result;
        });
    }

    ko.ppager = function (totalItemCount) {
        var ppager = new pPager(totalItemCount);
        return ko.observable(ppager);
    };
}(ko));
//============= pager js for Patient Physician ends  ============//