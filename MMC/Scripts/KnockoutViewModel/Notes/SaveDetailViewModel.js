function SaveDetailViewModel() {
    var self = this;
    self.ClaimDetail = ko.observableArray([]);
    self.ClaimselectedItem = ko.observable(0);
    self.RequestDetail = ko.observableArray([]);
    self.RequestselectedItem = ko.observable(0);
    self.NotePatientID = ko.observable(0);
    self.NoteReferralID = ko.observable(0);
    self.Claim = ko.observable();
    self.PatientID = ko.observable();
    self.RequestID = ko.observable();
    self.NoteID = ko.observable();
    self.Init = function (model, pageName) {
        if (model != null) {
            if (pageName == "Intake") {
                self.PatientID(model.PatientID);
                self.RequestID(model.RFAReferralID);
                self.Claim(model.PatientClaimID);
            }
            else if (pageName == "Triage") {
                self.PatientID(model.patientAndRequestModel.Patients.PatientID);
                self.RequestID(model.patientAndRequestModel.Patients.RFAReferralID);
                self.Claim(model.patientAndRequestModel.Patients.PatientClaimID);
            }
            else if (pageName == "Preparation") {
                self.PatientID(model.Patients.PatientID);
                self.RequestID(model.Patients.RFAReferralID);
                self.Claim(model.Patients.PatientClaimID);
            }
            else if (pageName == "Notification") {
                self.PatientID(model.Patients.PatientID);
                self.RequestID(model.Patients.RFAReferralID);
                self.Claim(model.Patients.PatientClaimID);
            }
            if (pageName == "PeerReview") {
                self.PatientID(model.Patients.PatientID);
                self.RequestID(model.Patients.RFAReferralID);
                self.Claim(model.Patients.PatientClaimID);
            }

            else if (pageName == "Patient") {
                self.PatientID(model.PatientDetails.PatientID);
                //  self.RequestID(model.Patients.RFAReferralID);
                self.Claim('');
            }
            if (self.PatientID() == null) {
                $('#btnAddNote').addClass("disabled")
                $('#selector').css('cursor', 'pointer');
            }
            else {
                $('#btnAddNote').removeClass("disabled")
                $('#selector').css('cursor', 'not-allowed');
            }
            self.NotePatientID(self.PatientID());
            self.NoteReferralID(self.RequestID());

        }
        else {
            $('#btnAddNote').addClass("disabled")
            $('#selector').css('cursor', 'pointer');
        }
    };
    $("#btnAddNote").click(function () {
        $("#divuploadFile").hide();
    });

    self.OpenAddNotes = function (pageName) {
        if (self.ClaimDetail().length == 0) {
            $.post("/Patient/getAllpatientClaimsByPatientID", {
                _patientID: self.NotePatientID()
            }, function (_data) {
                var modelCA = $.parseJSON(_data);
                if (modelCA != null)
                    ko.mapping.fromJS(modelCA, {}, self.ClaimDetail);
                self.ClaimselectedItem(self.Claim());


            });
        }
        if (self.RequestDetail().length == 0) {
            $.post("/Intake/getRFARequestByReferralID", {
                _referralID: self.NoteReferralID()
            }, function (_data) {
                var modelCA = $.parseJSON(_data);
                if (modelCA != null)
                    ko.mapping.fromJS(modelCA, {}, self.RequestDetail);

            });
        }

     
        if (pageName == 'Patient') {
            $('#dllClaim').prop('disabled', false);
            $('#dllRequest').prop('disabled', true);
        }
        else {
            $('#dllClaim').prop('disabled', true);
            $('#dllRequest').prop('disabled', false);
        }

    }



    $("#btnAddNote").click(function () {
        blockPopupBackground();
    });

    function resetAddNotePop() {
        var editor1 = document.getElementById("EditorNote").editor;
        editor1.SetText('');
        self.ClaimselectedItem(self.Claim());
        self.RequestselectedItem("");
        $('#hidNoteID').val('0');
        unblockPopupBackground();

    }

    self.CloseNotes = function () {
        blockPopupBackground();

        alertify.confirm("Are you sure want to close?", function (e) {
            if (e) {
                $('.modal.in').modal('hide');
                //  $('#NoteDiv').modal('hide');
                resetAddNotePop();
                $("#divuploadFile").show();
            }

        });


    }

    self.SaveNotes = function () {

        if ($("#EditorNote").val() == '') {
            alertify.alert("Enter Notes");
            return false;
        }
        if ($("#hidNoteID").val() == '0') {
            if ($('#dllClaim').val() == '') {
                $("#dllClaim").addClass("border_r");
                return false;
            }
            else {
                $("#dllClaim").removeClass("border_r");
            }
        }

        showLoader();
        var viewModelNotesDetails = {
            NoteID: 0,
            Notes: $("<div>").text($("#EditorNote").val()).html(),
            UserID: 0,
            PatientClaimID: $('#dllClaim').val(),
            RFARequestID: $('#dllRequest').val(),
            Date: Date.now()
        }
        var plainJs = ko.toJS(viewModelNotesDetails);
        $.ajax({
            url: "/Intake/SaveNotes",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(plainJs),
            success: function (data) {
                hideLoader();
                if (data != 0) {
                    $('#NoteDiv').modal('hide');
                    alertify.alert("Notes Saved Successfully", function (e) {
                        if (e) {
                            resetAddNotePop();
                        }
                    });
                }
                else {
                    alert("Error Occur");
                }
            },
            error: function (data) {
                hideLoader();
                alert("Error Occur");
            }
        });
    }
}