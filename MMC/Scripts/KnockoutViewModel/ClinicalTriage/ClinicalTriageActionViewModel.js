function ClinicalTriageActionViewModel(model) {
    var self = this;
    self.RequestDetail = ko.observableArray();
    self.RFARequestAdditionalInfoID = ko.observable(0);
    self.URIncompleteRFAForm = ko.observable(false);
    self.URNoRFAForm = ko.observable(false);
    self.URDeclineInternalAppeal = ko.observable(false);
    self.closeDeniedRationalePopup = ko.observable(false);
    self.closeDuplicatePopup = ko.observable(false);
    self.closeUnabletoReview = ko.observable(false);
    self.IsDeniedRationale = ko.observable(false);
    self.IsUnableToLetter = ko.observable(false);
    self.PatientClaimStatusID = ko.observable();
    self.PatientMedicalAllRecords = ko.observableArray();
    self.SelectedUnabletoReviewLetters = ko.observable(0);
    self.AcceptedBodyParts = ko.observable();
    self.DeniedBodyParts = ko.observable();
    self.DelayedBodyParts = ko.observable();
    self.Diagnosis = ko.observable();
    self.DeniedRationale = ko.observable();
    self.RFAReferralID = ko.observable();
    self.RFARequestID = ko.observable();
    self.RFARequestModifyID = ko.observable();
    self.PatientID = ko.observable();
    self.TotalItemCount = ko.observable();
    self.rdTotalItemCount = ko.observable();
    self.cdTotalItemCount = ko.observable(0);
    self.mrTotalItemCount = ko.observable(0);
    self.mdTotalItemCount = ko.observable(0);
    self.drTotalItemCount = ko.observable(0);
    self.scrolled = ko.observableArray([]);
    self.maxId = ko.observable(0);
    self.mdCurrentPage = ko.observable(0);
    self.pendingRequest = ko.observable(false);
    self.PagedRecords = ko.observable(0);
    self.RFAClinicalReasonforDecision = ko.observable(0);
    self.RFAGuidelinesUtilized = ko.observable(0);
    self.RFARelevantPortionUtilized = ko.observable(0);
    self.NumberOfPendingRequest = ko.observable(0);
    self.selectedRequest = ko.observable();
    self.selectedDecision = ko.observable();
    self.OldRequestName = ko.observable();
    self.OldRFARequestModifyID = ko.observable();
    self.OldRFAFrequency = ko.observable();
    self.OldRFADuration = ko.observable();
    self.OldRFADurationTypeID = ko.observable();
    self.IsModify = ko.observable(false);
    self.RFAFrequency = ko.observable();
    self.RFADuration = ko.observable();
    self.RFADurationTypeID = ko.observable();
    self.durationTypes = ko.observableArray();
    self.RequestName = ko.observable();
    self.RFANotes = ko.observable();
    self.icdSearchDataResult = ko.observableArray();
    self.SelectedDurationTypeID = ko.observable();
    self.RequestForDuplicateDecision = ko.observableArray();
    self.drSkip = ko.observable(0);
    self.ClaimID = ko.observable();
    self.closePopup = ko.observable(false);
    self.IsDuplicate = ko.observable(false);
    self.PreviousDecisionDate = ko.observable();
    self.PreviousDecisionID = ko.observable();
    self.SelectedPreviousRequestID = ko.observable("0");
    self.NotesButtonDisable = ko.observable(true);
    self.PatientName = ko.observable();
    self.PatientName(model.patientAndRequestModel.Patients.PatFirstName + ' ' + model.patientAndRequestModel.Patients.PatLastName);
    self.PatientClaim = ko.observable();
    self.PatientClaim(model.patientAndRequestModel.Patients.PatClaimNumber);
    self.ClientBillingRateTypeID = ko.observable(model.patientAndRequestModel.Patients.ClientBillingRateTypeID);
    self.RFASignature = ko.observable("");
    self.RFASignatureDescription = ko.observable("");
    self.IsBillingDetailFill = ko.observable(false);
    self.RFALatestDueDate = ko.observable();
    self.DeferralDefault = ko.observable();
    self.ICDSearchType = ko.observableArray([{ SearchCriteria: "ICD9", SearchCriteriaName: "ICD9" }, { SearchCriteria: "ICD10", SearchCriteriaName: "ICD10" }]);
    self.closeWithdrawnPopup = ko.observable(false);
    self.closeWithdrawn = ko.observable(false);
    self.SelectAll = ko.computed({
        read: function () {
            var item = ko.utils.arrayFirst(self.PatientMedicalAllRecords(), function (item) {
                return !item.IsChecked();
            });
            return item == null;
        },
        write: function (value) {
            ko.utils.arrayForEach(self.PatientMedicalAllRecords(), function (person) {
                person.IsChecked(value);
            });
        }
    });
    //if (($('#selStatus').val() == '1') || ($('#selStatus').val() == '')) {
    var mappingOptions = {
        'RFAHCRGDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM-DD-YYYY");
            }
        },
        'DecisionDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM-DD-YYYY");
            }
        },
        'PatMRDocumentDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM-DD-YYYY");
            }
        },
        'PatDOI': {
            create: function (options) {
                if (options.data != '/Date(-62135596800000)/')
                    return moment(options.data).format("MM-DD-YYYY ");
            }
        }
    }
    self.PatientDOI = ko.observable();
    self.PatientDOI((moment(model.patientAndRequestModel.Patients.PatDOI).format("MM/DD/YYYY")));
    self.Skip = ko.observable(0);
    self.mdSkip = ko.observable(0);
    if (model.patientAndRequestModel.Patients != null) {
        self.RFAReferralID = ko.observable(model.patientAndRequestModel.Patients.RFAReferralID);
        self.PatientID = ko.observable(model.patientAndRequestModel.Patients.PatientID);
        self.ClaimID = ko.observable(model.patientAndRequestModel.Patients.PatientClaimID)
    }
    if (model.patientAndRequestModel.RequestDetail != null)
        self.RFANotes(model.patientAndRequestModel.RequestDetail[0].RFANotes);
    ko.mapping.fromJS(model, mappingOptions, self);
    if (self.ProcessLevel() == 130) {
        self.Status = ko.observableArray([{ StatusID: 100, StatusName: "Send to Preparation" }, { StatusID: 1, StatusName: "Certify" }, { StatusID: 8, StatusName: "Unable to Review" }, { StatusID: 2, StatusName: "Modify" }, { StatusID: 3, StatusName: "Deny" }, { StatusID: 9, StatusName: "Deferral" }, { StatusID: 10, StatusName: "Duplicate" }, { StatusID: 11, StatusName: "Internal Appeal" }, { StatusID: 13, StatusName: "Withdrawn" }]);
        self.RFASignature(model.RFAReferral.RFASignature);
        self.RFASignatureDescription(model.RFAReferral.RFASignatureDescription);
    }
    else
        self.Status = ko.observableArray([{ StatusID: 100, StatusName: "Send to Preparation" }, { StatusID: 1, StatusName: "Certify" }, { StatusID: 8, StatusName: "Unable to Review" }, { StatusID: 2, StatusName: "Modify" }, { StatusID: 3, StatusName: "Deny" }, { StatusID: 9, StatusName: "Deferral" }, { StatusID: 10, StatusName: "Duplicate" }, { StatusID: 11, StatusName: "Internal Appeal" }, { StatusID: 102, StatusName: "Peer Review" },{ StatusID: 13, StatusName: "Withdrawn" }]);
    if (model.PatientMedicalRecordViewModels != null)
        self.TotalItemCount(model.PatientMedicalRecordViewModels.TotalCount);
    if (model.rfaPatMedicalRecordReviewViewModels != null)
        self.mrTotalItemCount(model.rfaPatMedicalRecordReviewViewModels.TotalCount);
    if (model.rfaDiagnosisViewModel != null)
        self.cdTotalItemCount(model.rfaDiagnosisViewModel.TotalCount);
    for (var i = 0; i < model.patientAndRequestModel.RequestDetail.length; i++) {
        if (model.patientAndRequestModel.RequestDetail[i].DecisionID == 0 && (model.patientAndRequestModel.RequestDetail[i].RFAStatus == "SendToClinical" || model.patientAndRequestModel.RequestDetail[i].RFAStatus == null)) {
            self.NumberOfPendingRequest(self.NumberOfPendingRequest() + 1);
        }
    }
    //if ((model.patientAndRequestModel.RequestDetail[0].DecisionID == '1') || (model.patientAndRequestModel.RequestDetail[0].DecisionID == '')) {
    //    //$(".ClinicalNoteDiv").css("display", "none");
    //    self.NotesButtonDisable(false);
    //}
    //else {
    //    //$(".ClinicalNoteDiv").css("display", "block");
    //    self.NotesButtonDisable(true);
    //}
    function ClearRichtextEditor() {
        var editor1 = document.getElementById("Editor1").editor;
        editor1.SetText('');
        var editor2 = document.getElementById("Editor2").editor;
        editor2.SetText('');
        var editor3 = document.getElementById("Editor3").editor;
        editor3.SetText('');
        self.RFAClinicalReasonforDecision(null);
        self.RFAGuidelinesUtilized(null);
        self.RFARelevantPortionUtilized(null);
    }
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
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

    function GetDiagnosisSearchByName(searchText) {
        $.post("/Common/getPatientClaimDiagnosesAll/", {
            _searchName: searchText, _icdTab: $("#ICDDrp").val(), _skip: 0
        }, function (_data) {
            if (self.icdSearchDataResult() != null)
                self.icdSearchDataResult.removeAll();
            ko.mapping.fromJS(_data.ICDCodeDetails, {}, self.icdSearchDataResult);
            ko.mapping.fromJS(_data.TotalCount, {}, self.rdTotalItemCount);
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
                            $.post("/Preparation/getRFADiagnosisByReferralID", {
                                _rfaReferralId: self.RFAReferralID(), _skip: self.cdSkip() // $('#hidskipcd').val()
                            }, function (model) {
                                var _model = $.parseJSON(model);
                                ko.mapping.fromJS(_model.RFADiagnosisDetails, mappingOptions, self.rfaDiagnosisViewModel.RFADiagnosisDetails);
                                self.cdTotalItemCount(_model.TotalCount);
                                if ((self.cdTotalItemCount() % self.cdSkip()) == 0)
                                    self.cdPager().cdCurrentPage(self.cdPager().cdCurrentPage() - 1);
                                else
                                    self.cdPager().cdCurrentPage();
                                hideLoader();
                            });
                            alertify.alert("Deleted Successfully");
                        }
                        else {
                            alert("Error Occur")
                        }
                    });
                }
            }
        });
    }
    self.openClaimDiagonsisPopUp = function () {
        blockPopupBackground();
    }
    $("#buttonCloseCD").click(function () {
        self.icdSearchDataResult.removeAll();
        self.rdTotalItemCount(0);
        $("#SearchDiagnosisTitle").val('');
        unblockPopupBackground();
    });
    self.AddNewClaimDiagonsis = function (claimdiag) {
        showLoader();
        //-----mahi-----------------
        var viewModelClaimDiagonsisDetails = {
            PatientClaimDiagnosisID: 0,
            PatientClaimID: self.ClaimID(),
            icdICDNumberID: claimdiag.icdICDNumberID(),
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
                    $.post("/Preparation/getRFADiagnosisByReferralID", {
                        _rfaReferralId: self.RFAReferralID(), _skip: 0 // $('#hidskipcd').val()
                    }, function (model) {
                        var _model = $.parseJSON(model);
                        ko.mapping.fromJS(_model.RFADiagnosisDetails, mappingOptions, self.rfaDiagnosisViewModel.RFADiagnosisDetails);
                        self.cdTotalItemCount(_model.TotalCount);
                        self.cdPager().cdCurrentPage(1);
                        $('#addicd9').modal('toggle');
                        unblockPopupBackground();
                        self.icdSearchDataResult.removeAll();
                        self.rdTotalItemCount(0);
                        $("#SearchDiagnosisTitle").val("");
                        alertify.alert("Added Successfully");
                        $('#selStatus').focus();
                        hideLoader();
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
    self.ClinicalTriageActionInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }

        if (self.ProcessLevel() == 120 && !(self.IsBillingDetailFill())) {          
            showLoader();
            for (var i = 0; i < self.patientAndRequestModel.RequestDetail().length; i++) {
                if (self.patientAndRequestModel.RequestDetail()[i].DecisionID() == 11) {
                    alertify.alert("Please Change Internal Appeal Decision.You can't take this Decision");
                    hideLoader();
                    return false;
                }
            }

            var numOfPreparationRequest = $.grep(self.patientAndRequestModel.RequestDetail(), function (elem) {
                return (elem.DecisionID() == 9);
            }).length;
            if (numOfPreparationRequest == 0) {
                showLoader();
                blockPopupBackground();
                return true;
            }
            else {
                if (self.ClientBillingRateTypeID() == 2) {
                    self.DeferralDefault('1.0');
                }
                $("#btnRequestBillingPopUp").click();
                //$(".billingrange").mask("9.9");
                self.IsBillingDetailFill(true);
                hideLoader();
                return false;
            }
        }

        if (self.ProcessLevel() == 120) {
            
        }
        if (self.PreviousProcessLevel() == 125 && self.ProcessLevel() == 130) {
            if (self.RFASignature() == "" || self.RFASignature() == null) {
                alertify.alert("Please Upload Signature");
                return false;
            }

            if ((self.RFASignatureDescription() == "" || self.RFASignatureDescription() == null) && self.PreviousProcessLevel() != 125) {
                alertify.alert("Please enter Signature Description");
                return false;
            }
        }
        else {
            if (self.RFASignature() == null && self.RFASignatureDescription() == null) {
                if ($("#Sign_ID").val() != "") {
                    $.post("/Preparation/UploadSignatureAndDescription", {
                        _RFAReferralID: self.RFAReferralID(), SignatureSelect: $("#Sign_ID").val()
                    }, function (_model) {
                        var model = $.parseJSON(_model);
                        self.RFASignature(model.RFASignature);
                        self.RFASignatureDescription(model.RFASignatureDescription);
                    });
                }
                else {
                    alertify.alert("Please select Signature Selection");
                }
            }
        }
        if (self.ProcessLevel() == 130 && !(self.IsBillingDetailFill())) {
            var numOfPreparationRequest = $.grep(self.patientAndRequestModel.RequestDetail(), function (elem) {
                return elem.RFAStatus() === "SendToPreparation";
            }).length;
            if (self.patientAndRequestModel.RequestDetail().length == numOfPreparationRequest)
                return true;
            else {

                var numOfWithdrawnRequest = $.grep(self.patientAndRequestModel.RequestDetail(), function (elem) {
                    return elem.DecisionID() === 13;
                }).length;

                if (self.patientAndRequestModel.RequestDetail().length == numOfWithdrawnRequest) {
                    return true;
                }
                else {
                    var checkCheckBOx = $("#Sign_ID").val();
                    if (checkCheckBOx == "") {
                        alertify.alert("Please select Signature Selection");
                        return true;
                    }
                    else {
                        if (self.RFASignature() == null && self.RFASignatureDescription() == null) {
                            SignatureUpload();
                        }
                        $("#btnRequestBillingPopUp").click();
                        //$(".billingrange").mask("9.9");
                        self.IsBillingDetailFill(true);
                        return false;
                    }
                    
                }    
            }
        }
        else {
            showLoader();
            return true;
        }
    }
    self.UploadSignatureBeforeSubmit = function (arr, $form, options) {
        if ($("#RFAReferralSignature").val() != "") {
            var sFileName = $("#RFAReferralSignature").val();
            var sFileExtension = sFileName.split('.')[sFileName.split('.').length - 1].toLowerCase();
            if (!(sFileExtension === "ico" || sFileExtension === "png" || sFileExtension === "jpg")) {
                $("#lblFileValidation").html("Please make sure your file is in jpg or png or bitmap format");
                return false;
            }
            else {
                $("#lblFileValidation").html("");
            }
            if ($("#RFASignature").val() != '') {
                if (confirm("Do you wanna replace the old signature?"))
                    return true;
                else
                    return false;
            }
        }
        return true;
    }
    self.UploadSignatureSuccess = function (responseText) {
        var model = $.parseJSON(responseText);
        unblockPopupBackground();
        $('#mySignatureUpload').modal('hide');
        alertify.success("Saved Successfully");
        self.RFASignature(model);
    }
    self.UploadSignatureDescriptionBeforeSubmit = function (arr, $form, options) {
        if (self.RFASignatureDescription() == "") {
            alertify.alert("Signature Description required");
            return false;
        }
        return true;
    }
    self.saveSignatureDescription = function () {
        $("#hdfRFASignatureDescription").val($("<div>").text($("#Editor4").val()).html());
        $("#btnSignatureDescription").submit();
    }
    self.UploadSignatureDescriptionSuccess = function (responseText) {
        //  var model = $.parseJSON(responseText);
        unblockPopupBackground();
        self.RFASignatureDescription($("<div>").text($("#Editor4").val()).html());
        $('#mySignatureDescriptionPopUp').modal('hide');
        alertify.success("Saved Successfully");
    }
    function resetUploadSignaturecontrol() {
        $('.form-group').removeClass('has-error has-feedback');
        $('.form-group').find('.help-block').hide();
        $('.form-group').find('i.form-control-feedback').hide();
        $("#RFAReferralSignature").val("");
        $("#RFAReferralSignature").replaceWith($("#RFAReferralSignature").clone(true));
    }
    self.ClinicalTriageActionSuccess = function (responseText) {
        if (responseText != null) {                  
            hideLoader();
            if (responseText != "") {
                $('#closeRequestBillingPopUp').click();
                $('#btnEmailMahiPopUp').click();
                SendEmailMahiPopUp(responseText);
            }
            else {
                if (self.ProcessLevel() == 120) {
                    alertify.alert("Request process Successfully", function (e) {
                        showLoader();
                        if (e) {
                            window.location = '/Preparation/ClinicalTriage';
                        }
                    });
                }
                else {
                 
                    alertify.alert("Request process Successfully", function (e) {
                        showLoader();
                        if (e) {
                            if (self.RFASignature() == null && self.RFASignatureDescription() == null) {
                                $.post("/Preparation/UploadSignatureAndDescription", {
                                    _RFAReferralID: self.RFAReferralID(), SignatureSelect: $("#Sign_ID").val()
                                }, function (model) {
                                    var _model = $.parseJSON(model);
                                    self.RFASignature(_model.RFASignature);
                                    self.RFASignatureDescription(_model.RFASignatureDescription);
                                    window.location = '/Preparation/ClinicalAudit';
                                });
                            }
                            else
                                window.location = '/Preparation/ClinicalAudit';
                        }
                    });
                }
            }
        };
    };
    //var fileDownloadCheckTimer;
    //function StartDownload() {
    //    var token = new Date().getTime(); //use the current timestamp as the token value
    //    $('#download_token_value').val(token);
    //    fileDownloadCheckTimer = window.setInterval(function () {
    //        var cookieValue = $.cookie('fileDownloadToken');
    //        if (cookieValue == token)
    //            finishDownload();
    //    }, 1000);
    //}
    //function finishDownload() {
    //    window.clearInterval(fileDownloadCheckTimer);
    //    $.cookie('fileDownloadToken', null); //clears this cookie value
    //    hideLoader();
    //    alertify.alert("Request process Successfully", function (e) {
    //        showLoader();
    //        if (e) {
    //            window.location = '/Preparation/ClinicalAudit';
    //        }
    //    });
    //}
    self.RFAPatientMedicalRecordInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return validatecheckBox();
    }
    self.RequestBillingFormBeforeSubmit = function (arr, $form, options) {        
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }               
       return true;
    }
    self.AddRequestBillingDetailSuccess = function (responseText) {
        $("#btnContinue").submit();
    }
    function validatecheckBox() {
        var flag = 0;
        var chk = document.getElementsByName('RFAPatMedicalRecordReview[]')
        var len = chk.length
        for (i = 0; i < len; i++) {
            if (chk[i].checked) {
                flag++;
                break;
            }
        }
        if (flag == 0) {
            alertify.alert("You must check one and only one checkbox!");
            return false;
        }
        return true;
    }
    self.btnRFARelevantPortionUtilized = function () {
        self.RFARelevantPortionUtilized($("<div>").text($("#Editor3").val()).html());//encoding
        if (self.RFARelevantPortionUtilized() == "")
            self.RFARelevantPortionUtilized(null);
        unblockPopupBackground();
    }
    self.btnSignatureDescriptionPopup = function () {
        self.RFASignatureDescription($("<div>").text($("#Editor4").val()).html());//encoding
        if (self.RFASignatureDescription() == "")
            self.RFASignatureDescription(null);
        unblockPopupBackground();
    }
    self.btnRFAGuidelinesUtilized = function () {
        self.RFAGuidelinesUtilized($("<div>").text($("#Editor2").val()).html());//encoding
        if (self.RFAGuidelinesUtilized() == "")
            self.RFAGuidelinesUtilized(null);
        unblockPopupBackground();
    }
    self.btnRFAClinicalReasonforDecision = function () {
        self.RFAClinicalReasonforDecision($("<div>").text($("#Editor1").val()).html());//encoding
        if (self.RFAClinicalReasonforDecision() == "")
            self.RFAClinicalReasonforDecision(null);
        unblockPopupBackground();
    }
    self.RFAPatientMedicalRecordInfoSuccess = function (responseText) {
        var _responseText = $.parseJSON(responseText);
        if (_responseText != null) {
            alertify.alert(_responseText, function (e) {
                if (e) {
                    $('#myRFAPatientMedicalRecord').modal('hide');
                    showLoader();
                    GetMedicalRecordReview(0);
                    self.mrPager().mrCurrentPage(1);
                    hideLoader();
                    unblockPopupBackground();
                }
            });
        };
    };
    $('.myCheckbox').click(function () {
        $(this).siblings('input:checkbox').prop('checked', false);
        $("#Sign_ID").val("");
        if ($("#RN_ID").prop('checked') == true) {
            $("#Sign_ID").val("RN");
        }
        else {
            $("#Sign_ID").val("MD");
        }
    });
    self.updateRequestDecision = function (e) {
        showLoader();
        if ($('#selStatus').val() == "") {
            alertify.alert("Select Decision");
            $('#selStatus').focus();
            hideLoader();
            return false;
        }
        if ($('#selStatus').val() == "11" && (self.ProcessLevel() == 120)) {
            hideLoader();
            alertify.alert("Please Change Decision.You can't take this Decision");
            return false;
        }
        if (($('#selStatus').val() == "10" || $('#selStatus').val() == "11") && (!self.IsDuplicate())) {
            hideLoader();
            alertify.alert("Please Select One Previous Request");
            return false;
        }
        var RFARequest = {
            RFARequestID: self.selectedRequest(),
            RFAReferralID: self.RFAReferralID(),
            RFAExpediteReferral: null,
            RFARequestedTreatment: self.RequestName(),
            RFADurationTypeID: self.SelectedDurationTypeID(),
            RFAFrequency: self.RFAFrequency(),
            RequestTypeID: null,
            RFADuration: self.RFADuration(),
            RFAQuantity: null,
            TreatmentCategoryID: null,
            TreatmentTypeID: null,
            RFAStrenght: null,
            RFACPT_NDC: null,
            DecisionID: null,
            RFAStatus: null,
            RFAClinicalReasonforDecision: self.RFAClinicalReasonforDecision(),
            RFAGuidelinesUtilized: self.RFAGuidelinesUtilized(),
            RFARelevantPortionUtilized: self.RFARelevantPortionUtilized(),
            RFANotes: $('#txtRFANotes').val(),
            ProcessLevel: self.ProcessLevel(),
            RFARequestModifyID: self.RFARequestModifyID(),
            IsModify: self.IsModify(),
            IsDeniedRationale: self.IsDeniedRationale(),
            DeniedRationale: self.DeniedRationale(),
            PatientClaimStatusID: self.PatientClaimStatusID(),
            RFARequestAdditionalInfoID: self.RFARequestAdditionalInfoID(),
            URIncompleteRFAForm: self.URIncompleteRFAForm(),
            URNoRFAForm: self.URNoRFAForm(),
            URDeclineInternalAppeal: self.URDeclineInternalAppeal(),
            IsUnableToLetter: self.IsUnableToLetter(),
            PatientClaimID: self.ClaimID(),
            OriginalRFARequestID: self.SelectedPreviousRequestID(),
            IsDuplicate: self.IsDuplicate()
        }
        if (self.selectedDecision() == 100) {
            RFARequest.RFAStatus = "SendToPreparation";
        }
        else if (self.selectedDecision() == 102) {
            RFARequest.RFAStatus = "Peer Review";
        }
        else
            RFARequest.DecisionID = self.selectedDecision();
        var plainJs = ko.toJS(RFARequest);
        $.ajax({
            url: "/Preparation/updateDecisionByRequestID",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(plainJs),
            success: function (data) {
                AfterUpdateRequestDetail();
                alertify.alert("Decision Taken Successfully");
                for (var i = 0; i < self.patientAndRequestModel.RequestDetail().length; i++) {
                    if (self.patientAndRequestModel.RequestDetail()[i].RFARequestID() == self.selectedRequest()) {
                        self.patientAndRequestModel.RequestDetail()[i].DecisionID(self.selectedDecision());
                        self.patientAndRequestModel.RequestDetail()[i].RFARequestedTreatment(self.RequestName());
                        self.patientAndRequestModel.RequestDetail()[i].RFAClinicalReasonforDecision(self.RFAClinicalReasonforDecision());
                        self.patientAndRequestModel.RequestDetail()[i].RFARelevantPortionUtilized(self.RFARelevantPortionUtilized());
                        self.patientAndRequestModel.RequestDetail()[i].RFAGuidelinesUtilized(self.RFAGuidelinesUtilized());
                        if (self.selectedDecision() == 100)
                            self.patientAndRequestModel.RequestDetail()[i].RFAStatus("SendToPreparation");
                        else if (self.selectedDecision() == 102)
                            self.patientAndRequestModel.RequestDetail()[i].RFAStatus("Peer Review");
                        else
                            self.patientAndRequestModel.RequestDetail()[i].RFAStatus(null);
                        self.RFAFrequency(self.OldRFAFrequency());
                        self.RFADuration(self.OldRFADuration());
                        self.RequestName(self.OldRequestName());
                        self.RFARequestModifyID(self.OldRFARequestModifyID());
                        if (self.durationTypes().length > 0)
                            self.SelectedDurationTypeID(self.OldRFADurationTypeID());
                        break;
                    }
                }
                $.post("/Preparation/getRemainingRequest", {
                    _rfaReferralId: self.RFAReferralID()
                }, function (model) {
                    var _model = $.parseJSON(model);
                    self.NumberOfPendingRequest(_model);
                    self.IsModify(false);
                    self.IsDeniedRationale(false);
                    self.IsUnableToLetter(false);
                    hideLoader();
                });
                if (self.selectedDecision() == "1" || self.selectedDecision() == "") {
                    ClearRichtextEditor();
                }
            }
        });
    };

    function AfterUpdateRequestDetail() {
        $.ajax({
            url: "/Preparation/ClinicalTriageActionAfterUpdateRequest",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({id :self.RFAReferralID(), ProcessLevel : self.ProcessLevel()}),
            success: function (model) {
                ko.mapping.fromJS(model.patientAndRequestModel, {}, self.patientAndRequestModel);
              
            }
        });
    }

    self.getIndexForBilling = function (request) {       
        var index = 0;
        for (var i = 0; i < self.patientAndRequestModel.RequestDetail().length; i++) {
            if (self.patientAndRequestModel.RequestDetail()[i] == request) {
                break;
            }
            if (self.ProcessLevel() == 120) {
                if (self.patientAndRequestModel.RequestDetail()[i].RFAStatus() != "SendToPreparation" && self.patientAndRequestModel.RequestDetail()[i].DecisionID() == 9) {
                    index++;
                }
            }
            else {
                if (self.patientAndRequestModel.RequestDetail()[i].RFAStatus() != "SendToPreparation" && self.patientAndRequestModel.RequestDetail()[i].DecisionID() != 13) {
                    index++;
                }
            }

        }
        return index;
    }
    self.selectedRequest.subscribe(function (_requestID) {
        var st = ko.utils.arrayFilter(self.patientAndRequestModel.RequestDetail(), function (item) {
            return item.RFARequestID() == _requestID;
        });
        self.RFANotes(st[0].RFANotes());
        self.RFAClinicalReasonforDecision(st[0].RFAClinicalReasonforDecision());
        self.RFAGuidelinesUtilized(st[0].RFAGuidelinesUtilized());
        self.RFARelevantPortionUtilized(st[0].RFARelevantPortionUtilized());
        self.RFARequestModifyID(st[0].RFARequestModifyID());
        self.RequestName(st[0].RFARequestedTreatment());
        self.OldRequestName(st[0].RFARequestedTreatment());
        self.OldRFAFrequency(st[0].RFAFrequency());
        self.OldRFADuration(st[0].RFADuration());
        self.OldRFADurationTypeID(st[0].RFADurationTypeID());
        self.OldRFARequestModifyID(st[0].RFARequestModifyID());
        self.RFAFrequency(st[0].RFAFrequency());
        self.RFADuration(st[0].RFADuration());
        self.RFADurationTypeID(st[0].RFADurationTypeID());
        if(st[0].RFALatestDueDate()!=null)
            self.RFALatestDueDate(moment(st[0].RFALatestDueDate()).format("MM-DD-YYYY"));
        else
            self.RFALatestDueDate(null);
        if (st[0].DecisionID() != 0) {
            self.selectedDecision(st[0].DecisionID());
            if (st[0].DecisionID() == 1) {
                self.NotesButtonDisable(false);
            }
            else {
                self.NotesButtonDisable(true);
            }
        }
        else if (st[0].RFAStatus() == "SendToPreparation") {
            self.selectedDecision(100);
            self.NotesButtonDisable(true);
        }
        else if (st[0].RFAStatus() == "Peer Review") {
            self.selectedDecision(102);
            self.NotesButtonDisable(true);
        }
        else {
            self.selectedDecision("");
            self.NotesButtonDisable(false);
        }
    });
    self.DecisionChanged = function (item) {
        if (item.selectedDecision() == 2) {
            $('#btnOpenRequestDetailPop').click();
            $("#txtRequestName").removeClass("border_r");
            blockPopupBackground();
            if (self.durationTypes().length == 0)
                bindDurationType();
        }
        else if (item.selectedDecision() == 9) {
            $('#btnOpenDeferralPopUp').click();
            $("#txtRationaleDetail").removeClass("border_r");
            bindDeniedRationale();
            blockPopupBackground();
        }
        else if (item.selectedDecision() == 8) {
            $('#btnOpenUnabletoReviewPop').click();
            bindUnabletoReviewLetters();
            blockPopupBackground();
        }
        else if (item.selectedDecision() == 10) {
            self.closeDuplicatePopup(false);
            self.SelectedPreviousRequestID("0");
            $('#btnOpenDulicatePopUP').click();
            bindGRequestForDuplicateDecision();
            bindDuplicate();
            blockPopupBackground();
        }
        else if (item.selectedDecision() == 11) {
            self.closeDuplicatePopup(false);
            self.SelectedPreviousRequestID("0");
            $('#btnOpenDulicatePopUP').click();
            bindGRequestForDuplicateDecision();
            bindDuplicate();
            blockPopupBackground();
        }
        else if (item.selectedDecision() == 13) {
            $('#btnOpenWithdrawnPopUP').click();
            blockPopupBackground();
            }

        if (item.selectedDecision() == 1) {
            self.NotesButtonDisable(false);
        }
        else {
            self.NotesButtonDisable(true);
        }

    };
    function bindDurationType() {
        $.post("/Preparation/getDurationType", function (model) {
            var _model = $.parseJSON(model);
            ko.mapping.fromJS(_model, mappingOptions, self.durationTypes);
            self.SelectedDurationTypeID(self.RFADurationTypeID());
        });
    }
    function bindGRequestForDuplicateDecision() {
        $.post("/Preparation/GetRequestForDuplicateDecision", {
            _patientClaimID: self.ClaimID(), _skip: self.drSkip()
        }, function (model) {
            if (model != "" && model != null) {
                var _model = $.parseJSON(model);
                ko.mapping.fromJS(_model.RequestDetail, mappingOptions, self.RequestForDuplicateDecision);
                self.drTotalItemCount(_model.RequestCount);
            }
        });
    }
    function bindDeniedRationale() {
        showLoader();
        $.post("/Preparation/getDeniedRationaleDetail", {
            ReferralID: self.RFAReferralID()
        }, function (model) {
            if (model != null && model != "") {
                var _model = $.parseJSON(model);
                self.PatientClaimStatusID(_model.PatientClaimStatusID);
                self.DeniedRationale(_model.DeniedRationale);
            }
            hideLoader();
        });
    }
    function bindUnabletoReviewLetters() {
        showLoader();
        $.post("/Preparation/getUnableToReviewLettersDetail", {
            RequestID: self.selectedRequest()
        }, function (model) {
            if (model != null && model != "") {
                var _model = $.parseJSON(model);
                self.URIncompleteRFAForm(_model.URIncompleteRFAForm);
                self.URNoRFAForm(_model.URNoRFAForm);
                self.URDeclineInternalAppeal(_model.URDeclineInternalAppeal);
                self.RFARequestAdditionalInfoID(_model.RFARequestAdditionalInfoID);
                if (_model.URIncompleteRFAForm)
                    self.SelectedUnabletoReviewLetters("1");
                else if (_model.URNoRFAForm)
                    self.SelectedUnabletoReviewLetters("2");
                else if (_model.URDeclineInternalAppeal)
                    self.SelectedUnabletoReviewLetters("3");
                else
                    self.SelectedUnabletoReviewLetters("0");
            }
            hideLoader();
        });
    }
    function bindDuplicate() {
        showLoader();
        $.post("/Preparation/getUnableToReviewLettersDetail", {
            RequestID: self.selectedRequest()
        }, function (model) {
            if (model != null && model != "") {
                var _model = $.parseJSON(model);
                if (_model.OriginalRFARequestID != null) {
                    if (self.selectedDecision() == _model.DecisionID) {
                        self.SelectedPreviousRequestID(_model.OriginalRFARequestID.toString());
                        if (self.selectedDecision() == 10) {
                            for (var i = 0; i < self.RequestForDuplicateDecision().length; i++) {
                                if (self.RequestForDuplicateDecision()[i].RFARequestID() == _model.OriginalRFARequestID) {
                                    self.PreviousDecisionDate(self.RequestForDuplicateDecision()[i].DecisionDate);
                                    self.PreviousDecisionID(self.RequestForDuplicateDecision()[i].DecisionID());
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            hideLoader();
        });
    }
    self.changePreviousRequest = function (item) {
        //var s = item;
        //self.SelectedPreviousRequestID(s.RFARequestID().toString());
        self.PreviousDecisionDate(item.DecisionDate);
        self.PreviousDecisionID(item.DecisionID());
        return true;
    }
    $('#btnRequestDetailPopup').click(function () {
        if (self.RequestName() != "") {
            self.closePopup(true);
            unblockPopupBackground();
            self.IsModify(true);
            self.OldRFAFrequency(self.RFAFrequency());
            self.OldRFADuration(self.RFADuration());
            self.OldRequestName(self.RequestName());
            self.OldRFADurationTypeID(self.SelectedDurationTypeID());
            self.OldRFARequestModifyID(self.RFARequestModifyID());
        }
        else {
            $("#txtRequestName").addClass("border_r");
        }
    });
    $('#btnDeniedRationale').click(function () {
        if (self.DeniedRationale() != "") {
            self.closeDeniedRationalePopup(true);
            self.IsDeniedRationale(true);
            unblockPopupBackground();
        }
        else {
            $("#txtRationaleDetail").addClass("border_r");
        }
    });
    $('#btnUnabletoReview').click(function () {
        if (self.SelectedUnabletoReviewLetters() != 0) {
            self.closeUnabletoReview(true);
            unblockPopupBackground();
            self.IsUnableToLetter(true);
            if (self.SelectedUnabletoReviewLetters() == "1") {
                self.URIncompleteRFAForm(true);
                self.URNoRFAForm(null);
                self.URDeclineInternalAppeal(null);
            }
            else if (self.SelectedUnabletoReviewLetters() == "2") {
                self.URIncompleteRFAForm(null);
                self.URNoRFAForm(true);
                self.URDeclineInternalAppeal(null);
            }
            else if (self.SelectedUnabletoReviewLetters() == "3") {
                self.URIncompleteRFAForm(null);
                self.URNoRFAForm(null);
                self.URDeclineInternalAppeal(true);
            }
        }
        else {
            $('input:radio').addClass("border_r");
        }
    });
    $('#btnDuplicate').click(function () {
        if (self.SelectedPreviousRequestID() != "0" && self.selectedDecision() == 10) {
            if (self.PreviousDecisionID() == "2" || self.PreviousDecisionID() == "3") {
                var selectedDate = new Date(self.PreviousDecisionDate().replace(/-/g, '/'));
                selectedDate.setFullYear(selectedDate.getFullYear() + 1);
                if (selectedDate < new Date()) {
                    alertify.alert("It has been over a year since this request has been reviewed, please change Decision");
                    self.SelectedPreviousRequestID("0");
                }
                else {
                    self.closeDuplicatePopup(true);
                    unblockPopupBackground();
                    self.IsDuplicate(true);
                }
            }
            else {
                self.closeDuplicatePopup(true);
                unblockPopupBackground();
                self.IsDuplicate(true);
            }
        }
        else if (self.SelectedPreviousRequestID() != "0" && self.selectedDecision() == 11) {
            self.closeDuplicatePopup(true);
            unblockPopupBackground();
            self.IsDuplicate(true);
        }
        else {
            alertify.alert("Please Select One Request");
        }
    });
    $("#txtRequestName").click(function () { $("#txtRequestName").removeClass("border_r"); });
    $('input:radio').click(function () { $('input:radio').removeClass("border_r"); });
    $("#txtRationaleDetail").click(function () { $("#txtRationaleDetail").removeClass("border_r"); });
    self.closeRequestDetailPopup = function () {
        alertify.confirm("Are you sure you want to close?", function (e) {
            if (e) {
                unblockPopupBackground();
                self.closePopup(true);
                $('#myRequestDetail').modal('hide');
                self.RequestName(self.OldRequestName());
                self.RFAFrequency(self.OldRFAFrequency());
                self.RFADuration(self.OldRFADuration());
                self.RFARequestModifyID(self.OldRFARequestModifyID());
                self.SelectedDurationTypeID(self.OldRFADurationTypeID());
            }
        });
        self.closePopup(false);
    }
    self.closeRationaleDetailPopup = function () {
        alertify.confirm("Are you sure you want to close?", function (e) {
            if (e) {
                unblockPopupBackground();
                self.closeDeniedRationalePopup(true);
                $('#myDeferralPopUp').modal('hide');
            }
        });
        self.closeDeniedRationalePopup(false);
    }
    self.closeDuplicateDetailPopup = function () {
        alertify.confirm("Are you sure you want to close?", function (e) {
            if (e) {
                unblockPopupBackground();
                self.closeDuplicatePopup(true);
                $('#myDuplicatePopUp').modal('hide');
            }
        });
        self.closeDuplicatePopup(false);
    }
    self.closeUnabletoReviewPopup = function () {
        alertify.confirm("Are you sure you want to close?", function (e) {
            if (e) {
                unblockPopupBackground();
                self.closeUnabletoReview(true);
                $('#myUnabletoReview').modal('hide');
                self.SelectedUnabletoReviewLetters(0);
            }
        });
        self.closeUnabletoReview(false);
        }

     self.closeWithdrawnPopup = function () {
        alertify.confirm("Are you sure you want to close?", function (e) {
            if(e) {
                self.closeWithdrawn(true);
                $('#myWithdrawnPopUp').modal('hide');
                unblockPopupBackground();
                }
        });
        self.closeWithdrawn(false);
        }

    $('#btnOpenWithdrawnPopUP').click(function () {
        $('#uploadRfaReferralID').val(self.RFAReferralID());
        $('#uploadRFARequestID').val(self.selectedRequest());
        $('#uploadTypeWithdrawnCheck').val("Withdrawn");
        });


    $('#myRequestDetail').on('hide.bs.modal', function (e) {
        if (!self.closePopup()) {
            e.preventDefault();
            return false;
        }
    });
    $('#myDeferralPopUp').on('hide.bs.modal', function (e) {
        if (!self.closeDeniedRationalePopup()) {
            e.preventDefault();
            return false;
        }
    });
    $('#myUnabletoReview').on('hide.bs.modal', function (e) {
        if (!self.closeUnabletoReview()) {
            e.preventDefault();
            return false;
        }
    });
    $('#myDuplicatePopUp').on('hide.bs.modal', function (e) {
        if (!self.closeDuplicatePopup()) {
            e.preventDefault();
            return false;
        }
    });
     $('#myWithdrawnPopUp').on('hide.bs.modal', function (e) {
        if (!self.closeWithdrawn()) {
            e.preventDefault();
            return false;
            }
            });
    self.OpenMedicalRecordReviewInfoPopUp = function () {
        $('#chkSelectAll').attr('checked', false);
        $('table tbody tr td').find('input[type=checkbox]:checked').removeAttr('checked');
        clearCkhbox();
        GetMedicalRecordAll();
        blockPopupBackground();
    };
    function GetMedicalRecordAll() {
        showLoader();
        $.post("/Preparation/GetPatientMedicalRecordByPatientID", {
            _patientid: self.PatientID(), _skip: self.maxId()
        }, function (model) {
            var _model = $.parseJSON(model);
            if (_model.PatientMedicalRecordByPatientIDDetails != null) {
                if (!self.pendingRequest()) {
                    showLoader();
                    $.each(_model.PatientMedicalRecordByPatientIDDetails, function (index, value) {
                        self.PatientMedicalAllRecords.push(new PatientMedical(value));
                    });
                    self.mdTotalItemCount(_model.TotalCount);
                    self.mdCurrentPage(self.maxId());
                    self.maxId(self.maxId() + _model.PagedRecords);
                    hideLoader();
                }
            }
        });
    }
    self.scrolled = function (data, event) {
        if (!self.pendingRequest()) {
            if (self.mdTotalItemCount() > self.maxId()) {
                var elem = event.target;
                if (elem.scrollTop > (elem.scrollHeight - elem.offsetHeight - 1)) {
                    GetMedicalRecordAll();
                }
            }
            else {
                self.pendingRequest(true);
            }
        }
    }
    //---------------added for see the preview of Determination letter by Mahi
    self.DeterminationLetterClinicalTriage = function () {
        var IsEnableButton = 0;
        for (var i = 0; i < model.patientAndRequestModel.RequestDetail.length; i++) {
            if (model.patientAndRequestModel.RequestDetail[i].DecisionID == 1 || model.patientAndRequestModel.RequestDetail[i].DecisionID == 2 || model.patientAndRequestModel.RequestDetail[i].DecisionID == 3) {
                IsEnableButton = 1;
                break;
            }
        }
        var _signature = $("#Sign_ID").val();
        if (_signature == "") {
            alertify.alert("Please select checkbox");
        }
        if (self.NumberOfPendingRequest() == 0 && _signature != "" && (self.selectedDecision() == 1 || self.selectedDecision() == 2 || self.selectedDecision() == 3 || IsEnableButton == 1)) {// this is download and save only certify,modify or deny case...... by Mahi
            if (self.PreviousProcessLevel() != 125) {
                $.post("/Preparation/UploadSignatureAndDescription", {
                    _RFAReferralID: self.RFAReferralID(), SignatureSelect: $("#Sign_ID").val()
                }, function (model) {
                    var _model = $.parseJSON(model);
                    self.RFASignature(_model.RFASignature);
                    self.RFASignatureDescription(_model.RFASignatureDescription);
                    //location.href = "/Notification/GenerateDeterminationLetter/" + self.RFAReferralID() + "";
                    $.post("/Notification/GenerateDeterminationLetter", { id: self.RFAReferralID() }, function (data) {
                        location.href = "/Notification/GetDeterminationFile/" + self.RFAReferralID() + "";                       
                        hideLoader();
                    });
                });
            }
            else {
                //location.href = "/Notification/GenerateDeterminationLetter/" + self.RFAReferralID() + "";
                $.post("/Notification/GenerateDeterminationLetter", { id: self.RFAReferralID() }, function (data) {
                    location.href = "/Notification/GetDeterminationFile/" + self.RFAReferralID() + "";
                    hideLoader();
                });
            }
        }
    }
    //------------------------------End-----------------------------------------------------//
    self.OpenrichtextPopUp1 = function () {
        blockPopupBackground();
        var editor1 = document.getElementById("Editor1").editor;
        editor1.SetText(($("<div>").html(self.RFAClinicalReasonforDecision()).text()));
    }
    self.OpenSignaturePopUp = function () {
        blockPopupBackground();
        resetUploadSignaturecontrol();
        $('[data-toggle="popover"]').popover("destroy");
        $('[data-toggle="popover"]').popover({
            placement: 'top',
            trigger: 'hover',
            html: true,
            content: '<div class="media"><img src="' + $('#RFASignature').val() + '" style="width:150px; height:100px;"/></div>'
        });
    }
    self.OpenSignatureDescriptionPopUp = function () {
        blockPopupBackground();
        var editor4 = document.getElementById("Editor4").editor;
        editor4.SetText(($("<div>").html(self.RFASignatureDescription()).text()));
    }
    self.OpenrichtextPopUp2 = function () {
        blockPopupBackground();
        var editor2 = document.getElementById("Editor2").editor;
        editor2.SetText(($("<div>").html(self.RFAGuidelinesUtilized()).text()));
    }
    self.OpenrichtextPopUp3 = function () {
        blockPopupBackground();
        var editor3 = document.getElementById("Editor3").editor;
        editor3.SetText(($("<div>").html(self.RFARelevantPortionUtilized()).text()));
    }
    self.closePatientMedicalRecord = function () {
        clearCkhbox();
        unblockPopupBackground();
        self.maxId(0);
        self.pendingRequest(false);
    }
    self.closeClinicalReasonforDecision = function () {
        unblockPopupBackground();
    }
    self.closeSignatureUploadPopup = function () {
        unblockPopupBackground();
    }
    self.closeSignatureDescriptionPopUp = function () {
        unblockPopupBackground();
    }
    self.closeGuidelinesUtilized = function () {
        unblockPopupBackground();
    }
    self.closeRelevantPortionUtilized = function () {
        unblockPopupBackground();
    }
    function clearCkhbox() {
        for (var i = 0; i < self.PatientMedicalAllRecords().length; i++) {
            self.PatientMedicalAllRecords()[i].IsChecked(false);
        }
    }
    function PatientMedical(value) {
        var self = this;
        self.PatientMedicalRecordID = ko.observable(value.PatientMedicalRecordID);
        self.PatMRDocumentName = ko.observable(value.PatMRDocumentName);
        self.PatMRDocumentDate = ko.observable(moment(value.PatMRDocumentDate).format("MM-DD-YYYY"));
        self.PhysicianName = ko.observable(value.PhysicianName);
        self.RFAReferralID = ko.observable(value.RFAReferralID);
        if ($("#chkSelectAll").is(':checked'))
            self.IsChecked = ko.observable(true);
        else
            self.IsChecked = ko.observable(value.IsChecked);
    }
    function GrdBinding() {
        showLoader();
        $.post("/Preparation/GetPatientMedicalRecordByPatientID", { _patientID: self.PatientID(), _skip: self.Skip() }, function (medicalRecord) {
            var e = $.parseJSON(medicalRecord);
            ko.mapping.fromJS(e.PatientMedicalRecordByPatientIDDetails, mappingOptions, self.PatientMedicalRecordViewModels.PatientMedicalRecordByPatientIDDetails);
            self.TotalItemCount(e.TotalCount);
            $('[data-toggle="tooltip"]').tooltip();
            hideLoader();
        });
    }
    self.GetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        GrdBinding();
    };
    var pagingSettings = {
        pageSize: 5,
        pageSlide: 2
    };
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
    //========= paging code for ICD grid only ===========//
    self.cdGetRecordsWithSkipTake = function (skip, take) {
        showLoader();
        if (skip == undefined || take == undefined) {
            self.cdSkip(0);
            self.cdTake(cdpagingSetting.cdpageSize);
        }
        else {
            self.cdSkip(skip);
            self.cdTake(take);
        }
        $.post("/Preparation/getRFADiagnosisByReferralID", {
            _rfaReferralId: self.RFAReferralID(), _skip: skip // $('#hidskipcd').val()
        }, function (model) {
            var _model = $.parseJSON(model);
            ko.mapping.fromJS(_model.RFADiagnosisDetails, mappingOptions, self.rfaDiagnosisViewModel.RFADiagnosisDetails);
            self.cdTotalItemCount(_model.TotalCount);
            hideLoader();
        });
    }
    var cdpagingSetting = {
        cdpageSize: 5,
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
    //========== pager js for   Medical record Review  ========//
    self.mrGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.mrSkip(0);
            self.mrTake(mrpagingSetting.mrpageSize);
        }
        else {
            self.mrSkip(skip);
            self.mrTake(take);
        }
        GetMedicalRecordReview(skip);
    }
    function GetMedicalRecordReview(skip) {
        showLoader();
        $.post("/Preparation/getPatMedicalRecordReview", {
            _patientID: self.PatientID(), _rfaReferralId: self.RFAReferralID(), _skip: skip
        }, function (model) {
            var _model = $.parseJSON(model);
            ko.mapping.fromJS(_model.RFAPatMedicalRecordReviewDetails, mappingOptions, self.rfaPatMedicalRecordReviewViewModels.RFAPatMedicalRecordReviewDetails);
            self.mrTotalItemCount(_model.TotalCount);
            hideLoader();
        });
    }
    var mrpagingSetting = {
        mrpageSize: 5,
        mrpageSlide: 2
    };
    self.mrSkip = ko.observable(0);
    self.mrTake = ko.observable(mrpagingSetting.mrpageSize);
    self.mrPager = ko.mrpager(self.mrTotalItemCount);
    self.mrPager().mrPageSize(mrpagingSetting.mrpageSize);
    self.mrPager().mrPageSlide(mrpagingSetting.mrpageSlide);
    self.mrPager().mrCurrentPage(1);
    self.mrPager().mrCurrentPage.subscribe(function () {
        var skip = mrpagingSetting.mrpageSize * (self.mrPager().mrCurrentPage() - 1);
        var take = mrpagingSetting.mrpageSlide;
        self.mrGetRecordsWithSkipTake(skip, take);
    });
    var rdpagingSetting = {
        rdpageSize: 5,
        rdpageSlide: 2
    };
    self.rdSkip = ko.observable(0);
    self.rdTake = ko.observable(rdpagingSetting.rdpageSize);
    self.rdPager = ko.rdpager(self.rdTotalItemCount);
    self.rdPager().rdPageSize(rdpagingSetting.rdpageSize);
    self.rdPager().rdPageSlide(rdpagingSetting.rdpageSlide);
    self.rdPager().rdCurrentPage(1);
    self.rdPager().rdCurrentPage.subscribe(function () {
        var skip = rdpagingSetting.rdpageSize * (self.rdPager().rdCurrentPage() - 1);
        var take = rdpagingSetting.rdpageSlide;
        self.rdGetRecordsWithSkipTake(skip, take);
    });
    self.rdGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.rdSkip(0);
            self.rdTake(rdpagingSetting.rdpageSize);
        }
        else {
            self.rdSkip(skip);
            self.rdTake(take);
        }
        var searchText = $("#SearchDiagnosisTitle").val();
        if (searchText == "") {
            alert("Diagnosis code required")
        }
        else {
            $.post("/Common/getPatientClaimDiagnosesAll/", {
                _searchName: searchText, _icdTab: $("#ICDDrp").val(), _skip: self.rdSkip()
            }, function (model) {
                self.icdSearchDataResult.removeAll();
                ko.mapping.fromJS(model.ICDCodeDetails, {}, self.icdSearchDataResult);
                ko.mapping.fromJS(model.TotalCount, {}, self.rdTotalItemCount);
            });
        }
    }
    var drpagingSetting = {
        drpageSize: 5,
        drpageSlide: 2
    };
    self.drTake = ko.observable(drpagingSetting.drpageSize);
    self.drPager = ko.drpager(self.drTotalItemCount);
    self.drPager().drPageSize(drpagingSetting.drpageSize);
    self.drPager().drPageSlide(drpagingSetting.drpageSlide);
    self.drPager().drCurrentPage(1);
    self.drPager().drCurrentPage.subscribe(function () {
        var skip = drpagingSetting.drpageSize * (self.drPager().drCurrentPage() - 1);
        var take = drpagingSetting.drpageSlide;
        self.drGetRecodrsWithSkipTake(skip, take);
    });
    self.drGetRecodrsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.drSkip(0);
            self.drTake(drpagingSetting.drpageSize);
        }
        else {
            self.drSkip(skip);
            self.drTake(take);
        }
        bindGRequestForDuplicateDecision();
    }

    //-----------------SEND EMAIL-------------------------------------------//
  
    self.AttachmentForEmailArray = ko.observableArray([]);
    function SendEmailMahiPopUp(responseText) {     
        ClearEmailpopup();
        $.post("/Notification/GetAdjAndPhyEmail", { _referralID: self.RFAReferralID() }, function () {
            $("#SendEMailTo").val('');
            $('#SendEMailTo').multiple_emails();
            $('#SendEMailCc').val('["' + 'UR@hcrg.com' + '"]');
            $('#SendEMailCc').multiple_emails();
            $("#Sendsubject").val(self.PatientName() + " - " + self.PatientClaim());
            $("#SendEmailText").val('Hello, Please see the attached letters for the above patient.');
        });

        for (var i = 0; i < responseText.length; i++) {
            var _docNamePath = responseText[i].toString().replace('[');
            _docNamePath = responseText[i].toString().replace('(');
            self.AttachmentForEmailArray.push(new AttachementDetails(_docNamePath));
        }
 
    };
    function AttachementDetails(value) {
        var self = this;
        var _docNamePath = value.split(',');
        if (_docNamePath[1].indexOf("_") != -1) {
            var shortText = _docNamePath[1].split('_');
            var shortString = shortText[1].substring(0, 15);
            self.AttachmentShortName = ko.observable(shortText[0] + "_" + shortString);
        }
        else {
            self.AttachmentShortName = ko.observable(_docNamePath[1]);
        }


        self.AttachmentLink = ko.observable(_docNamePath[0]);
        self.AttachmentName = ko.observable(_docNamePath[1]);
        self.AttachmentDownload = ko.observable(_docNamePath[2]);
    };

    function ClearEmailpopup() {
        $(".multiple_emails-container").remove();
        $('#SendEMailTo').val('[]');
        $('#SendEMailCc').val('[]');
        $('#SendEmailText').val('');
        $('#Sendsubject').val('');
        self.AttachmentForEmailArray.removeAll();
    }
   

    $('#frmMulitpleEmails').keypress(function (event) {
        var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            if ($(":focus").attr("id") == "emailSendMultiple") {
                if ($(".multiple_emails-error").length > 0) {
                    return false;
                }
                else {
                    return true;
                }

            }
            else {
                return false;
            }
        }

    });
    self.ClinicalTriageMultipleEmailInfoFormBeforeSubmit = function (arr, $form, options) {

        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        if ($(".multiple_emails-error").length > 0) {
            return false;
        }
        showLoader();
        return true;
    }

    self.ClinicalTriageMultipleEmailSuccess = function (responseText) {
        if (responseText != null) {
            var emailmsg = $.parseJSON(responseText);
            hideLoader();
            alertify.alert(emailmsg, function (e) {
                if (e) {
                    unblockPopupBackground();
                    ClearEmailpopup();
                    if (self.ProcessLevel() == 120) {
                        unblockPopupBackground();
                        ClearEmailpopup();
                        window.location = '/Preparation/ClinicalTriage';
                    }
                    else {
                           unblockPopupBackground();
                           ClearEmailpopup();
                            if (self.RFASignature() == null && self.RFASignatureDescription() == null) {
                                $.post("/Preparation/UploadSignatureAndDescription", {
                                    _RFAReferralID: self.RFAReferralID(), SignatureSelect: $("#Sign_ID").val()
                                }, function (model) {
                                    var _model = $.parseJSON(model);
                                    self.RFASignature(_model.RFASignature);
                                    self.RFASignatureDescription(_model.RFASignatureDescription);
                                    window.location = '/Preparation/ClinicalAudit';
                                });
                            }
                            else
                                window.location = '/Preparation/ClinicalAudit';                         
                    }
                }

            });

        };
    };

    $('#closeEmailMahiPopUp').click(function () {
        if (self.ProcessLevel() == 120) {
            alertify.alert("Request process Successfully", function (e) {
                showLoader();
                if (e) {
                    window.location = '/Preparation/ClinicalTriage';
                }
            });
        }
        else {
            alertify.alert("Request process Successfully", function (e) {
                showLoader();
                if (e) {
                    if (self.RFASignature() == null && self.RFASignatureDescription() == null) {
                        $.post("/Preparation/UploadSignatureAndDescription", {
                            _RFAReferralID: self.RFAReferralID(), SignatureSelect: $("#Sign_ID").val()
                        }, function (model) {
                            var _model = $.parseJSON(model);
                            self.RFASignature(_model.RFASignature);
                            self.RFASignatureDescription(_model.RFASignatureDescription);
                            window.location = '/Preparation/ClinicalAudit';
                        });
                    }
                    else
                        window.location = '/Preparation/ClinicalAudit';
                }
            });
        }
    });

    $('#additionRecordsBtn').click(function () {
        if (self.ProcessLevel() == 120) {
            window.location.href = "/Common/GetAdditionalDocuments?id=" + self.PatientID() + "&id2=" + self.ClaimID()
            + "&id3=" + self.RFAReferralID() + "&emailPopupName=ClinicalTriage";
        }
        else {
            window.location.href = "/Common/GetAdditionalDocuments?id=" + self.PatientID() + "&id2=" + self.ClaimID()
             + "&id3=" + self.RFAReferralID() + "&emailPopupName=ClinicalAudit";
        }
    });
    //-----------------SEND EMAIL-------------------------------------------//

    self.UploadFileWithdrawnInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }

        showLoader();
        return true;
    }

    $('#upLoadFileWithdrawnCase').change(function () {
        var ext = $('#upLoadFileWithdrawnCase').val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['pdf', 'PDF']) == -1) {
            alertify.alert('Upload pdf file only');
            ClearTheUploadFIle()
        }
      })


   function ClearTheUploadFIle() {
        $('#upLoadFileWithdrawnCase').each(function () {
            $(this).after($(this).clone(true)).remove();
            });

              var control = $('#upLoadFileWithdrawnCase');
              control.replaceWith(control.val('').clone(true));

        control.on({
                change: function () {console.log("Changed") },
                    focus: function () { console.log("Focus")
            }
          });
    }

    self.UploadFileWithdrawnSuccess = function (responseText) {
        if(responseText != "Error") {
            self.closeWithdrawn(true);
            ClearTheUploadFIle();

            $('#uploadRfaReferralID').val(0);
            $('#uploadRFARequestID').val(0);
            $('#myWithdrawnPopUp').modal('hide');
                  unblockPopupBackground();
            hideLoader();
            alertify.alert("File uploaded successfully");
            }
            else {
            alertify.alert("Error occured while uploading the file");
            hideLoader();
            }

    }

    function SignatureUpload() {
        if (self.RFASignature() == null && self.RFASignatureDescription() == null) {
            $.post("/Preparation/UploadSignatureAndDescription", {
                _RFAReferralID: self.RFAReferralID(), SignatureSelect: $("#Sign_ID").val()
            }, function (model) {
                var _model = $.parseJSON(model);
                self.RFASignature(_model.RFASignature);
                self.RFASignatureDescription(_model.RFASignatureDescription);
            });
        }
    };

}
//------- remove email attachment methods -------//
function removeMultipleAttachment(index) {
    _clinicalTriageActionViewModel.AttachmentForEmailArray.splice(index, 1);
}

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
//========== pager js for   Medical record Review  grid only ========//
(function (ko) {
    var mrnumericObservable = function (initialValue) {
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
    function mrPager(totalItemCount) {
        var self = this;
        self.mrCurrentPage = mrnumericObservable(1);
        self.mrTotalItemCount = ko.computed(totalItemCount);
        self.mrPageSize = mrnumericObservable(10);
        self.mrPageSlide = mrnumericObservable(2);
        self.mrLastPage = ko.computed(function () {
            return Math.floor((self.mrTotalItemCount() - 1) / self.mrPageSize()) + 1;
        });
        self.mrHasNextPage = ko.computed(function () {
            return self.mrCurrentPage() < self.mrLastPage();
        });
        self.mrHasPrevPage = ko.computed(function () {
            return self.mrCurrentPage() > 1;
        });
        self.mrFirstItemIndex = ko.computed(function () {
            return self.mrPageSize() * (self.mrCurrentPage() - 1) + 1;
        });
        self.mrLastItemIndex = ko.computed(function () {
            return Math.min(self.mrFirstItemIndex() + self.mrPageSize() - 1, self.mrTotalItemCount());
        });
        self.mrPages = ko.computed(function () {
            var mrpageCount = self.mrLastPage();
            var mrpageFrom = Math.max(1, self.mrCurrentPage() - self.mrPageSlide());
            var mrpageTo = Math.min(mrpageCount, self.mrCurrentPage() + self.mrPageSlide());
            mrpageFrom = Math.max(1, Math.min(mrpageTo - 2 * self.mrPageSlide(), mrpageFrom));
            mrpageTo = Math.min(mrpageCount, Math.max(mrpageFrom + 2 * self.mrPageSlide(), mrpageTo));
            var result = [];
            for (var i = mrpageFrom; i <= mrpageTo; i++) {
                result.push(i);
            }
            return result;
        });
    }
    ko.mrpager = function (totalItemCount) {
        var mrpager = new mrPager(totalItemCount);
        return ko.observable(mrpager);
    };
}(ko));
(function (ko) {
    var rdnumericObservable = function (initialValue) {
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
    function rdPager(totalItemCount) {
        var self = this;
        self.rdCurrentPage = rdnumericObservable(1);
        self.rdTotalItemCount = ko.computed(totalItemCount);
        self.rdPageSize = rdnumericObservable(10);
        self.rdPageSlide = rdnumericObservable(2);
        self.rdLastPage = ko.computed(function () {
            return Math.floor((self.rdTotalItemCount() - 1) / self.rdPageSize()) + 1;
        });
        self.rdHasNextPage = ko.computed(function () {
            return self.rdCurrentPage() < self.rdLastPage();
        });
        self.rdHasPrevPage = ko.computed(function () {
            return self.rdCurrentPage() > 1;
        });
        self.rdFirstItemIndex = ko.computed(function () {
            return self.rdPageSize() * (self.rdCurrentPage() - 1) + 1;
        });
        self.rdLastItemIndex = ko.computed(function () {
            return Math.min(self.rdFirstItemIndex() + self.rdPageSize() - 1, self.rdTotalItemCount());
        });
        self.rdPages = ko.computed(function () {
            var rdpageCount = self.rdLastPage();
            var rdpageFrom = Math.max(1, self.rdCurrentPage() - self.rdPageSlide());
            var rdpageTo = Math.min(rdpageCount, self.rdCurrentPage() + self.rdPageSlide());
            rdpageFrom = Math.max(1, Math.min(rdpageTo - 2 * self.rdPageSlide(), rdpageFrom));
            rdpageTo = Math.min(rdpageCount, Math.max(rdpageFrom + 2 * self.rdPageSlide(), rdpageTo));
            var result = [];
            for (var i = rdpageFrom; i <= rdpageTo; i++) {
                result.push(i);
            }
            return result;
        });
    }
    ko.rdpager = function (totalItemCount) {
        var rdpager = new rdPager(totalItemCount);
        return ko.observable(rdpager);
    };
}(ko));
(function (ko) {
    var drnumericObservable = function (initialValue) {
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
    function drPager(totalItemCount) {
        var self = this;
        self.drCurrentPage = drnumericObservable(1);
        self.drTotalItemCount = ko.computed(totalItemCount);
        self.drPageSize = drnumericObservable(10);
        self.drPageSlide = drnumericObservable(2);
        self.drLastPage = ko.computed(function () {
            return Math.floor((self.drTotalItemCount() - 1) / self.drPageSize()) + 1;
        });
        self.drHasNextPage = ko.computed(function () {
            return self.drCurrentPage() < self.drLastPage();
        });
        self.drHasPrevPage = ko.computed(function () {
            return self.drCurrentPage() > 1;
        });
        self.drFirstItemIndex = ko.computed(function () {
            return self.drPageSize() * (self.drCurrentPage() - 1) + 1;
        });
        self.drLastItemIndex = ko.computed(function () {
            return Math.min(self.drFirstItemIndex() + self.drPageSize() - 1, self.drTotalItemCount());
        });
        self.drPages = ko.computed(function () {
            var drpageCount = self.drLastPage();
            var drpageFrom = Math.max(1, self.drCurrentPage() - self.drPageSlide());
            var drpageTo = Math.min(drpageCount, self.drCurrentPage() + self.drPageSlide());
            drpageFrom = Math.max(1, Math.min(drpageTo - 2 * self.drPageSlide(), drpageFrom));
            drpageTo = Math.min(drpageCount, Math.max(drpageFrom + 2 * self.drPageSlide(), drpageTo));
            var result = [];
            for (var i = drpageFrom; i <= drpageTo; i++) {
                result.push(i);
            }
            return result;
        });
    }
    ko.drpager = function (totalItemCount) {
        var drpager = new drPager(totalItemCount);
        return ko.observable(drpager);
    };
}(ko));
$('[data-toggle="popover"]').popover({
    placement: 'top',
    trigger: 'hover',
    html: true,
    content: '<div class="media"><img src="' + $('#RFASignature').val() + '" style="width:150px; height:100px;"/></div>'
});