function PreparationActionViewModel(model) {
    var self = this;
    self.Patients = ko.observableArray();
    self.RFAReferralID = ko.observable(model.Patients.RFAReferralID);
    self.RequestDetail = ko.observableArray();
    self.Status = ko.observableArray([{ StatusID: 101, StatusName: "Send To Clinical" }, { StatusID: 1, StatusName: "Certify" }, { StatusID: 8, StatusName: "Unable to Review" }, { StatusID: 12, StatusName: "Client Authorized" }, { StatusID: 9, StatusName: "Deferral" }, { StatusID: 10, StatusName: "Duplicate" }, { StatusID: 102, StatusName: "Peer Review" }, { StatusID: 13, StatusName: "Withdrawn" }]);
    self.AcceptedBodyParts = ko.observable();
    self.DeniedBodyParts = ko.observable();
    self.DelayedBodyParts = ko.observable();
    self.Diagnosis = ko.observable();
    self.PatientID = ko.observable(model.Patients.PatientID);
    self.PatientMedicalRecordByPatientIDDetails = ko.observableArray();
    self.TotalItemCount = ko.observable();
    self.selectedRequest = ko.observable();
    self.selectedStatus = ko.observable();
    self.CombroidCondition = ko.observable();
    self.RFANotes = ko.observable();
    self.ClaimID = ko.observable(model.Patients.PatientClaimID)
    /////------------------------------------AdditionalInfo----------------------////////////////////////////////
    self.RFAAdditionalInfoID = ko.observable();
    self.RFAAdditionalInfoRecord = ko.observable();
    self.RFAAdditionalInfoRecord2 = ko.observable();
    self.RFAAdditionalInfoRecord3 = ko.observable();
    self.RFAAdditionalInfoRecord4 = ko.observable();
    self.RFAAdditionalInfoRecord5 = ko.observable();

    self.RFAAdditionalInfoRecordDate = ko.observable();

    self.RFAAdditionalInfoDetail = ko.observableArray();
    self.ADDTotalItemCount = ko.observable();
    /////------------------------------------end AdditionalInfo----------------------////////////////////////////////


    ///------------------------------------New Feature__________________________________////
    self.IsDuplicate = ko.observable(false);
    self.PreviousDecisionDate = ko.observable();
    self.PreviousDecisionID = ko.observable();
    self.SelectedPreviousRequestID = ko.observable("0");
    self.IsModify = ko.observable(false);
    self.RFARequestModifyID = ko.observable();
    self.PatientClaimStatusID = ko.observable();
    self.IsModify = ko.observable(false);
    self.RFAFrequency = ko.observable();
    self.RFADuration = ko.observable();
    self.RFADurationTypeID = ko.observable();
    self.durationTypes = ko.observableArray();
    self.RequestName = ko.observable();
    self.SelectedDurationTypeID = ko.observable();
    self.RequestForDuplicateDecision = ko.observableArray();
    self.closePopup = ko.observable(false);
    self.DeferralDefault = ko.observable();

    self.closeDeniedRationalePopup = ko.observable(false);
    self.closeDuplicatePopup = ko.observable(false);
    self.closeUnabletoReview = ko.observable(false);
    self.closeWithdrawnPopup = ko.observable(false);
    self.closeWithdrawn = ko.observable(false);
    self.closeClientAuthorizedPopup = ko.observable(false);
    self.closeClientAuthorized = ko.observable(false);
    self.IsDeniedRationale = ko.observable(false);
    self.IsUnableToLetter = ko.observable(false);
    self.SelectedUnabletoReviewLetters = ko.observable(0);
    self.DeniedRationale = ko.observable();
    self.drTotalItemCount = ko.observable(0);
    self.RFARequestAdditionalInfoID = ko.observable(0);

    self.OldRequestName = ko.observable();
    self.OldRFARequestModifyID = ko.observable();
    self.OldRFAFrequency = ko.observable();
    self.OldRFADuration = ko.observable();
    self.OldRFADurationTypeID = ko.observable();
    self.drSkip = ko.observable(0);

    self.URIncompleteRFAForm = ko.observable(false);
    self.URNoRFAForm = ko.observable(false);
    self.URDeclineInternalAppeal = ko.observable(false);
    self.IsBillingDetailFill = ko.observable(false);
    self.RFALatestDueDate = ko.observable();
    self.PatientMedicalAllRecords = ko.observableArray();
    self.maxId = ko.observable(0);
    self.pendingRequest = ko.observable(false);
    self.PagedRecords = ko.observable(0);
    self.mdTotalItemCount = ko.observable(0);
    self.mdCurrentPage = ko.observable(0);
    self.IsSelectedTimeExtension = ko.observable(false);
    self.documentPath = ko.observable();
    self.documentName = ko.observable();
    self.TimeExtensionPNAndProofOfService = ko.observableArray();
    self.CheckProofOfServiceBtnPress = ko.observable(false);
    self.TimeExtensionPNForUrl = ko.observable();
    self.TimeExtensionForUrl = ko.observable();

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
    //---------------------------------------END------------------------------------------//
    var mappingOptions = {
        'RFAHCRGDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
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
                    return moment(options.data).format("MM/DD/YYYY");
            }
        },
        'PatDOI': {
            create: function (options) {
                if (options.data != '/Date(-62135596800000)/')
                    return moment(options.data).format("MM/DD/YYYY ");
            }
        },
        'RFAFileCreationDate': {
            create: function (options) {
                if (options.data != null) {
                    if (options.data != '/Date(-62135596800000)/')
                        return moment(options.data).format("MM/DD/YYYY ");
                }
            }
        },
        'RFAAdditionalInfoRecordDate': {
            create: function (options) {
                if (options.data != null) {
                    if (options.data != '/Date(-62135596800000)/')
                        return moment(options.data).format("MM/DD/YYYY ");
                }
            }
        }

    }


    $.getJSON("/Preparation/GetAcceptedBodyPartByReferralID", { _referralId: self.RFAReferralID() }, function (accepted) {

        ko.mapping.fromJS(accepted, {}, self.AcceptedBodyParts);

    });
    $.getJSON("/Preparation/GetDeniedBodyPartByReferralID", { _referralId: self.RFAReferralID() }, function (denied) {

        ko.mapping.fromJS(denied, {}, self.DeniedBodyParts);

    });
    $.getJSON("/Preparation/GetDelayedBodyPartByReferralID", { _referralId: self.RFAReferralID() }, function (delayed) {

        ko.mapping.fromJS(delayed, {}, self.DelayedBodyParts);

    });
    $.getJSON("/Preparation/GetDiagnosisByReferralID", { _referralId: self.RFAReferralID() }, function (diagnosis) {
        ko.mapping.fromJS(diagnosis, {}, self.Diagnosis);
    });
    $.getJSON("/Common/getPatientComorbidConditionsByPatientID", { PatientID: self.PatientID() }, function (combriod) {
        combriod = combriod.replace(/&nbsp;/, " ");
        ko.mapping.fromJS(combriod, {}, self.CombroidCondition);
    });
    self.Skip = ko.observable(0);
    GrdBinding();

  
    if (model != null) {
        ko.mapping.fromJS(model, mappingOptions, self);
        if (model.ReferralFileTimeExtensionPNAndProofOfService != null) {
            ko.mapping.fromJS(model.ReferralFileTimeExtensionPNAndProofOfService, {}, self.TimeExtensionPNAndProofOfService);
          
        }
    }

   


    $("#RFARequestID").val = "0";
    if (window.location.pathname.split('/').length >= 5) {
        var _v = window.location.pathname.split('/');
        $.each(model.RequestDetail, function (index, value) {
            if (_v[4] == value.RFARequestID) {
                self.selectedRequest(value.RFARequestID);
                if (value.DecisionID > 0) {
                    if (value.DecisionID == 2) {
                        $('#selStatus').hide();
                        $('#selStatus2').show();
                        $('#selStatus2').val('1');
                    }
                    else if (value.DecisionID == 3) {
                        $('#selStatus').hide();
                        $('#selStatus2').show();
                        $('#selStatus2').val('2');
                    }
                    else {
                        $('#selStatus').show();
                        $('#selStatus2').hide();
                        $('#selStatus1').val('1');
                        self.selectedStatus(value.DecisionID);
                    }
                }
                else if (value.RFAStatus != '') {
                    if ((value.RFAStatus == "") || (value.RFAStatus == null)) { // this case is for Peer review , as we are not storing rfs status = peerreview in db
                        self.selectedStatus(102);
                    }
                    else if (value.RFAStatus == "SendToClinical") {
                        self.selectedStatus(101);
                    }
                    $('#selStatus').show();
                    $('#selStatus2').hide();
                }
                else {
                    self.selectedStatus(0);
                    $('#selStatus').show();
                    $('#selStatus2').hide();
                }

                self.RFANotes(value.RFANotes);

                $("#RFARequestID").val(_v[4]);
            }
        });
        $('.disableInput').attr('disabled', 'disabled');
        $('.disableBtn').attr('disabled', 'disabled');
        $('.disableBtn').addClass("btn-info");

        $("td .disableAnchor").hide();
        $("td .spandisable").removeClass("spanHide");

        $('.disableSelect').attr('disabled', 'disabled');
        

        //
        $('#btnUpdateRFAAdditionalInfoRecordDate').hide();
        $('#btnUpdateRFAAdditionalInfoRecordDate2').show();

  

    }
    else {
        $('.disableInput').removeAttr('disabled');
        $('.disableBtn').removeAttr('disabled');
        $('.disableBtn').removeClass("btn-info");
        $("td .spandisable").addClass("spanHide");

        $('.disableSelect').removeAttr('disabled');

        $("td .disableAnchor").show();

        $('#btnUpdateRFAAdditionalInfoRecordDate').show();
        $('#btnUpdateRFAAdditionalInfoRecordDate2').hide();
        $('#selStatus2').hide();
        $('#selStatus').val('1');
    }



    if (model.TimeExtensionPNCount > 0) {
        if (window.location.pathname.split('/').length >= 5)
        {
            $("#btnTimeExtension").removeClass("btn-default").addClass("btn-info");
            $('#btnTimeExtension').prop("disabled", true);

            $("#btnProofOfService").removeClass("btn-default").addClass("btn-info");
            $('#btnProofOfService').prop("disabled", true);
        }
        else
        {
            $("#btnTimeExtension").removeClass("btn-info").addClass("btn-default");
            $('#btnTimeExtension').prop("disabled", false);

            $("#btnProofOfService").removeClass("btn-info").addClass("btn-default");
            $('#btnProofOfService').prop("disabled", false);
        }
    }
    else {
        $("#btnTimeExtension").removeClass("btn-default").addClass("btn-info");
        $('#btnTimeExtension').prop("disabled", true);

        $("#btnProofOfService").removeClass("btn-default").addClass("btn-info");
        $('#btnProofOfService').prop("disabled", true);
    }

    self.PreparationActionInfoFormBeforeSubmit = function (arr, $form, options) {        
        if ($form.jqBootstrapValidation('hasErrors')) {            
            return false;
        }
        if (!(self.IsBillingDetailFill())) {// US#2715 by mahi for Client Authorized(DecisionID = 12) and deferral(DecisionID = 9)
            var numOfPreparationRequest = $.grep(self.RequestDetail(), function (elem) {
                return (elem.DecisionID() == 9);
            }).length;
            if (numOfPreparationRequest == 0) {
                showLoader();
                blockPopupBackground();
                return true;
            }
            else {
                if (self.Patients().ClientBillingRateTypeID() == 2) {
                    self.DeferralDefault('1.0');
                }
                $("#btnRequestBillingPopUp").click();
                //$(".billingrange").mask("9.9");
                self.IsBillingDetailFill(true);
                blockPopupBackground();
                return false;
            }
        }
        else {
            showLoader();
            return true;
        }
    }
    self.PreparationActionSuccess = function (responseText) {
        if (responseText != null) {
            hideLoader();
            if (responseText != "") {               
                $('#closeRequestBillingPopUp').click();
                $('#btnEmailMahiPopUp').click();
                SendEmailMahiPopUp(responseText);
            }
            else {
                alertify.alert("Request process Successfully", function (e) {
                    if (e) {
                        showLoader();
                        window.location = '/Preparation/Index';
                    }
                });
            }
        };
    };
    var fileDownloadCheckTimer;
    function StartDownload() {
        var token = new Date().getTime();
        $('#download_token_value').val(token);
        fileDownloadCheckTimer = window.setInterval(function () {
            var cookieValue = $.cookie('fileDownloadToken');
            if (cookieValue == token)
                finishDownload();
        }, 1000);
    }
    function finishDownload() {
        window.clearInterval(fileDownloadCheckTimer);
        $.cookie('fileDownloadToken', null);
        $.post("/Preparation/GetAllReferralFileByRFAReferralIDAndFileTypeID", {
            _rfarefferalid: self.RFAReferralID(), _filetypeid: 7
        }, function (model) {
            if (model != "" && model != null) {
                var _model = $.parseJSON(model);
                ko.mapping.fromJS(_model, mappingOptions, self.ReferralFileNotification);

            }
        });
    }
    function StartTimeExtentionRequest() {
        var token = new Date().getTime();
        $('#downloadtokenvalue').val(token);
        fileDownloadCheckTimer = window.setInterval(function () {
            var cookieValue = $.cookie('fileDownloadToken');
            var _RFADueDate = $.cookie('RFADueDate');

            for (var i = 0; i < self.RequestDetail().length; i++) {
                self.RequestDetail()[i].RFALatestDueDate(_RFADueDate);
            }

            if (cookieValue == token) {

                finishTimeExtentionRequest();
            }
        }, 1000);
    }
    function StartDownloadTimeExtentionLetter() {
        var token = new Date().getTime();
        $('#downloadTimeExtensionLettertokenvalue').val(token);
        fileDownloadCheckTimer = window.setInterval(function () {
            var cookieValue = $.cookie('TimeExtensionDownloadToken');
            if (cookieValue == token) {
                finishDownloadTimeExtentionLetter();
            }
        }, 1000);
    }
    function finishTimeExtentionRequest() {
        window.clearInterval(fileDownloadCheckTimer);
        var _timeExtensionPNFullFilePath = $.cookie('TimeExtensionPNFullFilePath');
        $("#TimeExtensionPNFullPath").val(_timeExtensionPNFullFilePath);
        $.cookie('fileDownloadToken', null, { path: '/' })
        $.cookie('RFADueDate', null, { path: '/' })
        alertify.alert("Time Extended Successfully", function (e) {
            if (e) {
                {
                    $("#btnTimeExtension").removeClass("btn-info").addClass("btn-default");
                    $('#btnTimeExtension').prop("disabled", false);
                    RebindTimeExtensionPNorProofOfService();
                }
                //OpenEmailPopup(model, 2);
            }
        });
    }
    function finishDownloadTimeExtentionLetter() {
        hideLoader();
        unblockPopupBackground();
        window.clearInterval(fileDownloadCheckTimer);
        var _timeExtensionFullFilePath = $.cookie('TimeExtensionFullFilePath');
        $("#TimeExtensionFullPath").val(_timeExtensionFullFilePath);
        $.cookie('TimeExtensionDownloadToken', null, { path: '/' });
        alertify.alert("Time Extended Letter Generated Successfully", function (e) {
            if (e) {
                ///OpenEmailPopup(model, 2);
                $("#btnProofOfService").removeClass("btn-info").addClass("btn-default");
                $('#btnProofOfService').prop("disabled", false);
                RebindTimeExtensionPNorProofOfService();
            }
        });
    }
    self.GenerateRequest = function () {
        $("#frmFileAction").submit(function () {
            StartDownload();
        });
        $("#frmFileAction").submit();
    }
    self.TimeExtentionRequest = function () {
        $("#frmTimeExtension").submit(function () {
            StartTimeExtentionRequest();
        });
        $("#frmTimeExtension").submit();
    }
    function DownloadTimeExtensionLetter() {
        $("#frmTimeExtensionLetter").submit(function () {
            StartDownloadTimeExtentionLetter();
        });
        $("#frmTimeExtensionLetter").submit();
    }
    self.OpenMedicalRecordReviewInfoPopUp = function () {
        $('#chkSelectAll').attr('checked', false);
        $('table tbody tr td').find('input[type=checkbox]:checked').removeAttr('checked');
        clearCkhbox();
        GetMedicalRecordAll();
        blockPopupBackground();
    };
    function clearCkhbox() {
        for (var i = 0; i < self.PatientMedicalAllRecords().length; i++) {
            self.PatientMedicalAllRecords()[i].IsChecked(false);
        }
    }
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
    self.RFARequestTimeExtentionInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        if (self.IsSelectedTimeExtension() == "2" && $("#txtAdditionalExamination").val() == "") {
            $("#txtAdditionalExamination").addClass("validation-error");
            return false;
        }
        else if (self.IsSelectedTimeExtension() == "3" && $("#txtSpecialtyConsult").val() == "") {
            $("#txtSpecialtyConsult").addClass("validation-error");
            return false;
        }
        return validatecheckBox();
    }
     
    $(".timeextention").click(function () {
        $(".timeextention").removeClass("validation-error");
    });
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
    self.RFARequestTimeExtentionInfoSuccess = function (responseText) {
        var _responseText = $.parseJSON(responseText);
        if (_responseText != null) {
            showLoader();
            $('#myRFAPatientMedicalRecord').modal('hide');
            $('.timeextention').prop('checked', false);
            $('#txtAdditionalExamination').val('');
            $('#txtSpecialtyConsult').val('');
            self.IsSelectedTimeExtension('');
            DownloadTimeExtensionLetter();

             
        };
    };
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
    self.TheCheckboxListSelectedValues = ko.observableArray().extend({
        required: { message: "At least one option is required." }
    });

    ////////////////////////////////
    //----------------For Billing-------------------------------------------//
    // US#2715 by mahi for Client Authorized
    self.getIndexForBilling = function (request) {
        var index = 0;
        for (var i = 0; i < self.RequestDetail().length; i++) {
            if (self.RequestDetail()[i] == request) {
                break;
            }
            if (self.RequestDetail()[i].RFAStatus() != "SendToPreparation" && (self.RequestDetail()[i].DecisionID() ==  9 )) {
                index++;
            }
        }
        return index;
    }
    self.RequestBillingFormBeforeSubmit = function (arr, $form, options) {        
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    self.AddRequestBillingDetailSuccess = function (responseText) {
        $("#btnContinuePrep").submit();
    }
    self.UpdateRFARequestLatestDueDateSuccess = function (responseText) {
        var _model = $.parseJSON(responseText);
        alertify.alert(_model);
    }


    //----------------------------------------------------------------------------//
    self.updateRequestDecision = function (e) {
        showLoader();
        if ($('#selStatus').val() == "") {
            if ($("#selStatus2").val() == "") {
                alertify.alert("Select Decision");
                hideLoader();
                $('#selStatus').focus();
                return false;
            }
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
            RFANotes: $('#txtRFANotes').val(),
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
        if($('#selStatus').val() != "") {
            if (self.selectedStatus() == 101) {
                RFARequest.RFAStatus = "SendToClinical";
            }
            else if (self.selectedStatus() == 102)
                RFARequest.RFAStatus = "Peer Review";
            else
                RFARequest.DecisionID = self.selectedStatus();
        }
        else 
        {
            if (self.selectedStatus() == 101) {
                RFARequest.RFAStatus = "SendToClinical";
            }
            else if (self.selectedStatus() == 102)
                RFARequest.RFAStatus = "Peer Review";
            else
                RFARequest.DecisionID = $('#selStatus2').val() == '1' ? '2' : '3';
        }

        var plainJs = ko.toJS(RFARequest);
        $.ajax({
            url: "/Preparation/updateDecisionByRequestID",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(plainJs),
            success: function (data) {
                if (window.location.pathname.split('/').length >= 5)
                    alertify.alert("Notes Save Successfully.");
                else
                    alertify.alert("Decision Taken Successfully.");
                $.post("/Preparation/PreparationActionDetail", { id: self.RFAReferralID() }, function (modelVal) {
                    ko.mapping.fromJS(modelVal.RemainingDecision, mappingOptions, self.RemainingDecision);
                    ko.mapping.fromJS(modelVal.RequestCount, mappingOptions, self.RequestCount);
                    ko.mapping.fromJS(modelVal.RequestDetail, mappingOptions, self.RequestDetail);
                    ko.mapping.fromJS(modelVal.TimeExtensionPNCount, mappingOptions, self.TimeExtensionPNCount);
                    if (self.TimeExtensionPNCount() > 0) {
                        $("#btnTimeExtension").removeClass("btn-info").addClass("btn-default");
                        $('#btnTimeExtension').prop("disabled", false);
                    }
                    else
                        $("#btnTimeExtension").removeClass("btn-default").addClass("btn-info");

                    hideLoader();
                    self.IsModify(false);
                    self.IsDeniedRationale(false);
                    self.IsUnableToLetter(false);
                });
                for (var i = 0; i < self.RequestDetail().length; i++) {
                    if (self.RequestDetail()[i].RFARequestID() == self.selectedRequest()) {
                        self.RequestDetail()[i].DecisionID(self.selectedStatus());
                        self.RequestDetail()[i].RFARequestedTreatment(self.RequestName());
                        if (self.selectedStatus() == 101)
                            self.RequestDetail()[i].RFAStatus("SendToClinical");
                        if (self.selectedStatus() == 102)
                            self.RequestDetail()[i].RFAStatus("Peer Review");
                        self.RFAFrequency(self.OldRFAFrequency());
                        self.RFADuration(self.OldRFADuration());
                        self.RequestName(self.OldRequestName());
                        self.RFARequestModifyID(self.OldRFARequestModifyID());
                        if (self.durationTypes().length > 0)
                            self.SelectedDurationTypeID(self.OldRFADurationTypeID());
                        break;
                    }
                }
            }
        });
    };

    self.DefaultDeferralValue = function () { return 1.0;}
    self.selectedRequest.subscribe(function (_requestID) {
        var st = ko.utils.arrayFilter(self.RequestDetail(), function (item) {
            return item.RFARequestID() == _requestID;
        });
        self.RFANotes(st[0].RFANotes());
        
        if (st[0].DecisionID() != 0)
            self.selectedStatus(st[0].DecisionID());
        else if (st[0].RFAStatus() == "SendToClinical")
            self.selectedStatus(101);
        else if (st[0].RFAStatus() == "Peer Review")
            self.selectedStatus(102);
        else
            self.selectedStatus("");
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
        if (st[0].RFALatestDueDate() != null)
            self.RFALatestDueDate(moment(st[0].RFALatestDueDate()).format("MM/DD/YYYY"));
        else
            self.RFALatestDueDate(null);
    });

    bindAdditionalInfoGrd(0);
    self.DecisionChanged = function (item) {
        if (item.selectedStatus() == 2) {
            $('#btnOpenRequestDetailPop').click();
            $("#txtRequestName").removeClass("border_r");
            blockPopupBackground();
            if (self.durationTypes().length == 0)
                bindDurationType();
        }
        else if (item.selectedStatus() == 9) {
            $('#btnOpenDeferralPopUp').click();
            $("#txtRationaleDetail").removeClass("border_r");
            bindDeniedRationale();
            blockPopupBackground();
        }
        else if (item.selectedStatus() == 8) {
            $('#btnOpenUnabletoReviewPop').click();
            bindUnabletoReviewLetters();
            blockPopupBackground();
        }
        else if (item.selectedStatus() == 10 || item.selectedStatus() == 11) {
            self.closeDuplicatePopup(false);
            self.SelectedPreviousRequestID("0");
            $('#btnOpenDulicatePopUP').click();
            bindGRequestForDuplicateDecision();
            bindDuplicate();
            blockPopupBackground();
        }
        else if (item.selectedStatus() == 13) {
            $('#btnOpenWithdrawnPopUP').click();           
            blockPopupBackground();
        }
        else if (item.selectedStatus() == 12) {
            $('#btnOpenClientAuthorizedPopUP').click();
            blockPopupBackground();
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
                if (_model.OriginalRFARequestID != null && _model.DecisionID != null) {

                    if (self.selectedStatus() == _model.DecisionID) {
                        self.SelectedPreviousRequestID(_model.OriginalRFARequestID.toString());
                        if (self.selectedStatus() == 10) {
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
        if (self.SelectedPreviousRequestID() != "0" && self.selectedStatus() == 10) {
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
        else if (self.SelectedPreviousRequestID() != "0" && self.selectedStatus() == 11) {
            self.closeDuplicatePopup(true);
            unblockPopupBackground();
            self.IsDuplicate(true);
        }
        else {
            alertify.alert("Please Select One Request");
        }
    });
    $('#btnOpenWithdrawnPopUP').click(function () {
        $('#uploadRfaReferralID').val(self.RFAReferralID());
        $('#uploadRFARequestID').val(self.selectedRequest());
        $('#uploadTypeWithdrawnCheck').val("Withdrawn");
    });
    $('#btnOpenClientAuthorizedPopUP').click(function () {
        $('#uploadClientAuthorizedRfaReferralID').val(self.RFAReferralID());
        $('#uploadClientAuthorizedRFARequestID').val(self.selectedRequest());
        $('#uploadTypeCheck').val("ClientAuthorized");
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
            if (e) {
                self.closeWithdrawn(true);
                $('#myWithdrawnPopUp').modal('hide');
                unblockPopupBackground();
            }
        });
        self.closeWithdrawn(false);
    }
    self.closeClientAuthorizedPopup = function () {
        alertify.confirm("Are you sure you want to close?", function (e) {
            if (e) {
                self.closeClientAuthorized(true);
                $('#myClientAuthorizedPopUp').modal('hide');
                unblockPopupBackground();
            }
        });
        self.closeClientAuthorized(false);
    }
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

    $('#myClientAuthorizedPopUp').on('hide.bs.modal', function (e) {
        if (!self.closeClientAuthorized()) {
            e.preventDefault();
            return false;
        }
    });


    function bindAdditionalInfoGrd(_skip) {
        showLoader();
        $.post("/Preparation/getAllRFAAdditionalInfo", { ReferralID: self.RFAReferralID(), skip: _skip }, function (data) {
            ko.mapping.fromJS(data.RFAAdditionalInfoDetails, mappingOptions, self.RFAAdditionalInfoDetail);
            self.ADDTotalItemCount(data.TotalCount);
            if (_skip == 0)
                self.ADDPager().ADDCurrentPage(1);
            hideLoader();
        });
    }
    self.RFAAdditionalInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }

    $(function () {
       
        $('.RFAAdditionalInfoRecordDate').datepicker({
            autoclose: true
        });
        $.fn.datepicker.defaults.autoclose = true;
    });

    self.RFAAdditionalInfoSuccess = function (responseText) {
        alertify.alert("Save Successully");
        self.RFAAdditionalInfoID(responseText);
        $('#myRFAAdditional').modal("toggle");
        bindAdditionalInfoGrd(0);
        unblockPopupBackground();
        
    };
    self.btnclosepop = function () {
        unblockPopupBackground();
    }

    self.OpenAdditionalInfoByID = function (_data) {
        blockPopupBackground();
        $('#divAddCaseName').hide();
        $('#myRFAAdditional').modal("toggle");
        self.RFAAdditionalInfoID(_data.RFAAdditionalInfoID());
        self.RFAAdditionalInfoRecord(_data.RFAAdditionalInfoRecord());

        self.RFAReferralID(_data.RFAReferralID());
    }

    self.OpenAdditionalInfoPopUp = function () {
        $('#divAddCaseName').show();
        clearAdditionalForm();
        blockPopupBackground();

    }
    self.closePatientMedicalRecord = function () {
        clearCkhbox();
        unblockPopupBackground();
        self.maxId(0);
        self.pendingRequest(false);
        $('.timeextention').prop('checked', false);
        $('#txtAdditionalExamination').val('');
        $('#txtSpecialtyConsult').val('');
        self.IsSelectedTimeExtension('');
    }
    function clearAdditionalForm() {
        self.RFAAdditionalInfoID('');
        self.RFAAdditionalInfoRecord('');
        self.RFAAdditionalInfoRecord2('');
        self.RFAAdditionalInfoRecord3('');
        self.RFAAdditionalInfoRecord4('');
        self.RFAAdditionalInfoRecord5('');
    }

    function GrdBinding() {
        showLoader();
        $.post("/Preparation/GetPatientMedicalRecordByPatientID", { _patientID: self.PatientID(), _skip: self.Skip() }, function (medicalRecord) {
            var e = $.parseJSON(medicalRecord);
            ko.mapping.fromJS(e.PatientMedicalRecordByPatientIDDetails, mappingOptions, self.PatientMedicalRecordByPatientIDDetails);
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


    self.ADDGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.ADDSkip(0);
            self.ADDTake(bppagingSetting.bppageSize);
        }
        else {
            self.ADDSkip(skip);
            self.ADDTake(take);
        }
        bindAdditionalInfoGrd(self.ADDSkip());
    }

    var ADDpagingSetting = {
        ADDpageSize: 10,
        ADDpageSlide: 2
    };

    self.ADDSkip = ko.observable(0);
    self.ADDTake = ko.observable(ADDpagingSetting.ADDpageSize);
    self.ADDPager = ko.ADDpager(self.ADDTotalItemCount);

    self.ADDPager().ADDPageSize(ADDpagingSetting.ADDpageSize);
    self.ADDPager().ADDPageSlide(ADDpagingSetting.ADDpageSlide);
    self.ADDPager().ADDCurrentPage(1);

    self.ADDPager().ADDCurrentPage.subscribe(function () {
        var skip = ADDpagingSetting.ADDpageSize * (self.ADDPager().ADDCurrentPage() - 1);
        var take = ADDpagingSetting.ADDpageSlide;
        self.ADDGetRecordsWithSkipTake(skip, take);
    });

    self.ADDLastPage = ko.computed(function () {
        return Math.floor((self.ADDTotalItemCount() - 1) / ADDpagingSetting.ADDpageSize) + 1;
    });
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

    self.ClearEmailPopUp = function () {
        ClearEmailpopup();
        $('.modal.in').modal('hide');
        unblockPopupBackground();
    }

    function ClearEmailpopup() {
        $(".multiple_emails-container").remove();
        $('#EMailTo').val('[]');
        $('#EMailCc').val('[]');
        $('#EmailText').val('');
        $('#subject').val('');
        $('#SendEmail').modal('hide');

    }


    self.SendRFIReportEmailWithAttachmentBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        showLoader();
        return true;
    }
    self.SendRFIReportEmailWithAttachmentSuccess = function (_data) {
        hideLoader();
        alertify.alert($.parseJSON(_data), function (e) {
            if (e) {
                $('#SendEmail').modal('hide');
                unblockPopupBackground();
            }
        });
    }

    function OpenEmailPopup(model, cnt) {
        //

        ClearEmailpopup();
        blockPopupBackground();       
        $("#EMailTo").val('');
        $('#EMailTo').multiple_emails();
        $('#EMailCc').val('["'+'UR@hcrg.com'+'"]');
        $('#EMailCc').multiple_emails();
        $('#SendEmail').modal('show');
        $("#subject").val(self.Patients().PatFirstName() + " " + self.Patients().PatLastName() + " - " + self.Patients().PatClaimNumber());
        $("#EmailText").val('Hello, Please see the attached letters for the above patient.');

        if (cnt == 2) {
            var TimeExtensionFilePath = $.cookie('TimeExtensionFilePath');
            $.cookie('TimeExtensionFilePath', null, { path: '/' });

            var TimeExtensionFilePathName = $.cookie('TimeExtensionFilePathName');
            $.cookie('TimeExtensionFilePathName', null, { path: '/' });

            self.documentPath(TimeExtensionFilePath);
            self.documentName(TimeExtensionFilePathName);
            $("#popUpID").val(2);
            $("#FileID").val(0);
        }
        else {
            var rfaReferralFileID = model.RFAReferralFileID();
            $("#FileID").val(rfaReferralFileID);
            $("#popUpID").val(1);
            self.documentPath(model.RFAReferralFileFullPath());
            self.documentName(model.RFAReferralFileName());
            $.post("/Common/AssignRFiInSessionVariable", { FullPath: model.RFAReferralFileFullPath(), FileName: model.RFAReferralFileName(), referralFileID: rfaReferralFileID, sessionVar: "RFI" }, function (model) {
            });
        }

    }

    self.SendEmail = function (model) {
        OpenEmailPopup(model, "1");
    };
    

    self.SendEmailWithAttachment = function () {
        showLoader();
        var RFAReport = {
            RFAReportEmailTo: $("#EMailTo").val(),
            RFAReportEmailCC: $("#EMailCc").val(),
            RFAReportSubject: $("#subject").val(),
            RFAReportDocumentPath: $("#hfdocumentPath").val(),
            RFAReportMailContent: $("#EmailText").val()
        }
        var plainJs = ko.toJS(RFAReport);
        $.ajax({
            url: "/Preparation/SendRFIReportEmailWithAttachment",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(plainJs),
            success: function (message) {
                alertify.alert(message, function (e) {
                    if (e) {
                        $('#SendEmail').modal('hide');
                        unblockPopupBackground();
                    }
                });
            }
        });
        hideLoader();
    };
    self.UpdateRFAAdditionalInfoRecordDate = function (model) {
        showLoader();
        if ($("#" + model.RFAAdditionalInfoID()).val() != "") {
            $.post("/Preparation/UpdateRFAAdditionalInfoRecordDateByID", { rFAAdditionalInfoID: model.RFAAdditionalInfoID(), cDate: $("#" + model.RFAAdditionalInfoID()).val(), _rFARequestID: '0' }
            , function (model) {
                alertify.alert($.parseJSON(model), function (e) {
                    if (e) {
                        bindAdditionalInfoGrd(0);
                         
                        unblockPopupBackground();
                    }
                });
                hideLoader();
            });
        }
        else {
            alertify.alert("Enter any date.", function (e) {
                if (e) {
                    $("#" + model.RFAAdditionalInfoID()).focus();
                    unblockPopupBackground();
                }
            });
            hideLoader();
        }

    };

    self.UpdateRFAAdditionalInfoRecordDate2 = function (model) {
        showLoader();
        if ($("#" + model.RFAAdditionalInfoID()).val() != "") {
            $.post("/Preparation/UpdateRFAAdditionalInfoRecordDateByID", { rFAAdditionalInfoID: model.RFAAdditionalInfoID(), cDate: $("#" + model.RFAAdditionalInfoID()).val(), _rFARequestID: $("#RFARequestID").val() }
            , function (model) {
                alertify.alert($.parseJSON(model), function (e) {
                    if (e) {
                        bindAdditionalInfoGrd(0);
                         
                        unblockPopupBackground();
                    }
                });
                hideLoader();
            });
        }
        else {
            alertify.alert("Enter any date.", function (e) {
                if (e) {
                    $("#" + model.RFAAdditionalInfoID()).focus();
                    unblockPopupBackground();
                }
            });
            hideLoader();
        }

    };
    //---------------------------------SEND EMAIL-------------------------------------------------------------//
    self.AttachmentForEmailArray = ko.observableArray([]);
    function SendEmailMahiPopUp(responseText) {
        ClearEmailpopup();  //This popup execute on Continue Button          
        $.post("/Notification/GetAdjAndPhyEmail", { _referralID: self.RFAReferralID() }, function () {           
                $("#SendEMailTo").val('');
                $('#SendEMailTo').multiple_emails();
                $('#SendEMailCc').val('["' + 'UR@hcrg.com' + '"]');
                $('#SendEMailCc').multiple_emails();               
                $("#Sendsubject").val(self.Patients().PatFirstName() + " " + self.Patients().PatLastName() + " - " + self.Patients().PatClaimNumber());
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
    self.PreparationMultipleEmailInfoFormBeforeSubmit = function (arr, $form, options) {

        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        if ($(".multiple_emails-error").length > 0) {
            return false;
        }
        showLoader();
        return true;
    }

    self.PreparationMultipleEmailSuccess = function (responseText) {
        if (responseText != null) {
            var emailmsg = $.parseJSON(responseText);
            hideLoader();
            if (self.CheckProofOfServiceBtnPress() != true) {
                alertify.alert(emailmsg, function (e) {
                    if (e) {
                        showLoader();
                        unblockPopupBackground();
                        ClearEmailpopup();
                        window.location = '/Preparation/Index';
                    }

                });
            }
            else {
                window.location = '/Preparation/PreparationAction/' + self.RFAReferralID();
            }
        };
    };

    $('#closeEmailMahiPopUp').click(function () {
        if (self.CheckProofOfServiceBtnPress() != true) {
            alertify.alert("Request process Successfully", function (e) {
                if (e) {
                    showLoader();
                    window.location = '/Preparation/Index';
                }
            });
        }
        else {
            unblockPopupBackground();
            ClearEmailpopup();
        }
    });

    $('#additionRecordsBtn').click(function () {
        if (self.CheckProofOfServiceBtnPress() == true) {
            window.location.href = "/Common/GetAdditionalDocuments?id=" + self.PatientID() + "&id2=" + self.ClaimID()
              + "&id3=" + self.RFAReferralID() + "&emailPopupName=TimeExtensionPN";
        }
        else {
            window.location.href = "/Common/GetAdditionalDocuments?id=" + self.PatientID() + "&id2=" + self.ClaimID()
              + "&id3=" + self.RFAReferralID() + "&emailPopupName=Preparation";
        }
    });

    $('#additionTimeExtensionRFIBtn').click(function () {
        var _mailPopupName = "";
        var popUPId = $("#popUpID").val();
        if (popUPId == 2)
            _mailPopupName = "TimeExtensionPN"; 
        else
            _mailPopupName = "RFI";

        window.location.href = "/Common/GetAdditionalDocuments?id=" + self.PatientID() + "&id2=" + self.ClaimID()
          + "&id3=" + self.RFAReferralID() + "&emailPopupName=" + _mailPopupName;
    });
    //-----------------------------------SEND EMAIL------------------------------------------------------------//
    //---------------------------For Withdrawn Decision-------------------------------------------------------//
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
    });

    $('#upLoadFileClientAuthorizedCase').change(function () {
        var ext = $('#upLoadFileClientAuthorizedCase').val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['pdf', 'PDF']) == -1) {
            alertify.alert('Upload pdf file only');
            ClearTheUploadClientAuthorized()
        }
    });


    function ClearTheUploadFIle() {
        $('#upLoadFileWithdrawnCase').each(function () {
            $(this).after($(this).clone(true)).remove();
        });

        var control = $('#upLoadFileWithdrawnCase');
        control.replaceWith(control.val('').clone(true));

        control.on({
            change: function () { console.log("Changed") },
            focus: function () { console.log("Focus") }
        });
    }

    function ClearTheUploadClientAuthorized() {
        $('#upLoadFileClientAuthorizedCase').each(function () {
            $(this).after($(this).clone(true)).remove();
        });

        var control = $('#upLoadFileClientAuthorizedCase');
        control.replaceWith(control.val('').clone(true));

        control.on({
            change: function () { console.log("Changed") },
            focus: function () { console.log("Focus") }
        });
    }
    
    self.UploadFileWithdrawnSuccess = function (responseText) {
        if (responseText != "Error") {           
            self.closeWithdrawn(true);
            self.closeClientAuthorized(true);
            ClearTheUploadFIle();
            ClearTheUploadClientAuthorized(true);
            ClearTheUploadClientAuthorized();
            $('#uploadRfaReferralID').val(0);
            $('#uploadRFARequestID').val(0);
            $('#uploadClientAuthorizedRfaReferralID').val(0);
            $('#uploadClientAuthorizedRFARequestID').val(0);
            $('#myWithdrawnPopUp').modal('hide');
            $('#myClientAuthorizedPopUp').modal('hide');
            unblockPopupBackground();
            hideLoader();
            alertify.alert("File uploaded successfully");
        }
        else {
            alertify.alert("Error occured while uploading the file");
            hideLoader();
        }
       
    };
    //-------------------------------End Withdrawn Decision------------------------------------------------------//
    //---For Proof Of Service ----------------------------------------//
    self.ProofOfService = function () {      
        showLoader();
        $.post("/Preparation/GenerateProofOfService", { id: self.RFAReferralID() }, function (data) {
            var _proofService = $.cookie('ProofOfServiceFullFilePath');
            $("#ProofOfServiceFullPath").val(_proofService);

            location.href = "/Preparation/GetProofOfServiceFile/" + self.RFAReferralID() + "";
            RebindTimeExtensionPNorProofOfService();
            hideLoader();
            self.CheckProofOfServiceBtnPress(true); // True means comming from Proof of service                                                                
            $('#btnEmailMahiPopUp').click();        // False means comming from continue buttion 
            SendEmailTimeExtensionorProofOfServicePopUp();
          
        });
    };    

    function RebindTimeExtensionPNorProofOfService() {
        $.post("/Preparation/GetAllRFAReferralFilesAccToReferralIDInTimeExtensionPNAndProofOfService", { _rfaReferralID: self.RFAReferralID() }, function (data) {
            var _data = $.parseJSON(data);
            self.TimeExtensionPNAndProofOfService.removeAll();
            ko.mapping.fromJS(_data, {}, self.TimeExtensionPNAndProofOfService);
        });
    }


    function SendEmailTimeExtensionorProofOfServicePopUp() {
        ClearEmailpopup();  //This Email popup execute on Proof of Service Button   
        blockPopupBackground();
        showLoader();
        $("#rID").val(self.RFAReferralID());
        $.post("/Notification/GetAdjAndPhyEmail", { _referralID: self.RFAReferralID() }, function () {
            $("#SendEMailTo").val('');
            $('#SendEMailTo').multiple_emails();
            $('#SendEMailCc').val('["' + 'UR@hcrg.com' + '"]');
            $('#SendEMailCc').multiple_emails();
            $("#Sendsubject").val(self.Patients().PatFirstName() + " " + self.Patients().PatLastName() + " - " + self.Patients().PatClaimNumber());
            $("#SendEmailText").val('Hello, Please see the attached letters for the above patient.');
        });

        $.post("/Preparation/GetAllLinksFromTimeExtensionPNorProofOfService", { _timeExtensionPN: $("#TimeExtensionPNFullPath").val(), _timeExtension: $("#TimeExtensionFullPath").val(), _proofOfService: $("#ProofOfServiceFullPath").val(), _RfaReferralID: self.RFAReferralID() }, function (responseText) {
            for (var i = 0; i < responseText.length; i++) {
                var _docNamePath = responseText[i].toString().replace('[');
                _docNamePath = responseText[i].toString().replace('(');
                self.AttachmentForEmailArray.push(new AttachementDetails(_docNamePath));
            }
        });
        hideLoader();

    };
    //-------------End Proof of Service--------------------------------//

}
function removeAttachment() {
    _preparationActionViewModel.documentPath(null);
}

function removeMultipleAttachment(index) {
    _preparationActionViewModel.AttachmentForEmailArray.splice(index, 1);
}


(function (ko) {
    var ADDnumericObservable = function (initialValue) {
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

    function ADDPager(totalItemCount) {
        var self = this;
        self.ADDCurrentPage = ADDnumericObservable(1);

        self.ADDTotalItemCount = ko.computed(totalItemCount);

        self.ADDPageSize = ADDnumericObservable(10);
        self.ADDPageSlide = ADDnumericObservable(2);

        self.ADDLastPage = ko.computed(function () {
            return Math.floor((self.ADDTotalItemCount() - 1) / self.ADDPageSize()) + 1;
        });

        self.ADDHasNextPage = ko.computed(function () {
            return self.ADDCurrentPage() < self.ADDLastPage();
        });

        self.ADDHasPrevPage = ko.computed(function () {
            return self.ADDCurrentPage() > 1;
        });

        self.ADDFirstItemIndex = ko.computed(function () {
            return self.ADDPageSize() * (self.ADDCurrentPage() - 1) + 1;
        });

        self.ADDLastItemIndex = ko.computed(function () {
            return Math.min(self.ADDFirstItemIndex() + self.ADDPageSize() - 1, self.ADDTotalItemCount());
        });

        self.ADDPages = ko.computed(function () {
            var ADDpageCount = self.ADDLastPage();
            var ADDpageFrom = Math.max(1, self.ADDCurrentPage() - self.ADDPageSlide());
            var ADDpageTo = Math.min(ADDpageCount, self.ADDCurrentPage() + self.ADDPageSlide());
            ADDpageFrom = Math.max(1, Math.min(ADDpageTo - 2 * self.ADDPageSlide(), ADDpageFrom));
            ADDpageTo = Math.min(ADDpageCount, Math.max(ADDpageFrom + 2 * self.ADDPageSlide(), ADDpageTo));

            var result = [];
            for (var i = ADDpageFrom; i <= ADDpageTo; i++) {
                result.push(i);
            }
            return result;
        });
    }

    ko.ADDpager = function (totalItemCount) {
        var ADDpager = new ADDPager(totalItemCount);
        return ko.observable(ADDpager);
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