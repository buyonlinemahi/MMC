function IMRDecisionViewModel(model) {
    var self = this;

    self.IMRReferralDetails = ko.observable();
    self.IMRRequestsDetails = ko.observableArray();
    self.IMRLetters = ko.observableArray();
    self.IMRDecision = ko.observableArray();

    self.documentPath = ko.observable();
    self.documentFullPath = ko.observable();
    self.documentName = ko.observable();

    self.IMRInitialNotificationRFAReferralFileID = ko.observable(0);
    self.IMRInitialNotificationDocumentName = ko.observable();
    self.IMRInitialNotificationDocumentPath = ko.observable();

    self.IMRDecisionLetterRFAReferralFileID = ko.observable(0);
    self.IMRDecisionLetterDocumentName = ko.observable();
    self.IMRDecisionLetterDocumentPath = ko.observable();

    self.IMRDecisionProofOfServiceRFAReferralFileID = ko.observable(0);
    self.IMRDecisionProofOfServiceDocumentName = ko.observable();
    self.IMRDecisionProofOfServiceDocumentPath = ko.observable();

    self.IMRDecisionUploadRFAReferralFileID = ko.observable(0);

    self.EmailAttachments = ko.observableArray();



    var mappingOptions = {
        'DueDate': {
            create: function (options) {
                if (options.data != '/Date(-62135596800000)/')
                    return moment(options.data).format("MM-DD-YYYY");
            }
        },

        'IMRDecisionReceivedDate': {
            create: function (options) {
                if (options.data != null) {
                    if (options.data != '/Date(-62135596800000)/')
                        return moment(options.data).format("MM-DD-YYYY");
                }
            }
        }

    };
    if (model != null) {
        if (model.IMRReferralDetails != null) {
            if (model.IMRReferralDetails.IMRDecisionReceivedDate != null) {
                $("#IMRDecisionReceivedDate").val((moment(model.IMRReferralDetails.IMRDecisionReceivedDate).format("MM-DD-YYYY")));
            }
        }
        else {

            $("#IMRDecisionReceivedDate").val("");
        }
    }
    for (var i = 0; i < model.IMRLetters.length; i++) {
        if (model.IMRLetters[i].RFAFileTypeID == 17) {
            self.IMRInitialNotificationRFAReferralFileID(model.IMRLetters[i].RFAReferralFileID);
            self.IMRInitialNotificationDocumentName(model.IMRLetters[i].RFAReferralFileName);
            self.IMRInitialNotificationDocumentPath(model.IMRLetters[i].RFAReferralFileFullPath);
        }
        if (model.IMRLetters[i].RFAFileTypeID == 18) {
            self.IMRDecisionLetterRFAReferralFileID(model.IMRLetters[i].RFAReferralFileID);
            self.IMRDecisionLetterDocumentName(model.IMRLetters[i].RFAReferralFileName);
            self.IMRDecisionLetterDocumentPath(model.IMRLetters[i].RFAReferralFileFullPath);
        }
        if (model.IMRLetters[i].RFAFileTypeID == 19) {
            self.IMRDecisionProofOfServiceRFAReferralFileID(model.IMRLetters[i].RFAReferralFileID);
            self.IMRDecisionProofOfServiceDocumentName(model.IMRLetters[i].RFAReferralFileName);
            self.IMRDecisionProofOfServiceDocumentPath(model.IMRLetters[i].RFAReferralFileFullPath);
        }
        if (model.IMRLetters[i].RFAFileTypeID == 20) {
            self.IMRDecisionUploadRFAReferralFileID(model.IMRLetters[i].RFAReferralFileID);
        }
    }

    ko.mapping.fromJS(model, mappingOptions, self);


    self.approvedUnitFilter = function (data, event) {
        var approved = $("#App" + data).val().trim();
        var qty = $("#Qty" + data).text().trim();
        var reg = /^\d+$/;
        if (reg.test(approved)) {
            if (parseInt(qty, 10) <= parseInt(approved, 10)) {
                $("#App" + data).val($("#Qty" + data).text() - 1);
            }
        }
        else {
            $("#App" + data).val('');
        }
    };

    self.EmailAttachments($.grep(self.IMRLetters(), function (elem) {
        return (elem.RFAFileTypeID() == 17 || elem.RFAFileTypeID() == 18 || elem.RFAFileTypeID() == 19);
    }));

    self.SaveIMRDecisionRequestDetails = function () {
        var isPartialOverturn = false;
        if (self.IMRReferralDetails().IMRDecisionID() == 3) {
            for (var i = 0; i < self.IMRRequestsDetails().length; i++) {
                if (self.IMRRequestsDetails()[i].Overturn() == 1) {
                    if (self.IMRRequestsDetails()[i].IMRApprovedUnits() < 1) {
                        alertify.alert("Please enter some value greater than zero in Approved units.")
                        return false;
                    }
                    isPartialOverturn = true;
                }
            }
            if (!isPartialOverturn) {
                alertify.alert("Partial Overturn needs atleast one overturn checked.");
                return false;
            }
        }
        blockPopupBackground();
        showLoader();
        $.ajax({
            url: '/IMR/SaveIMRDecisionRecord?DecisionID=' + self.IMRReferralDetails().IMRDecisionID() + '&RFAReferralID=' + self.IMRReferralDetails().RFAReferralID() + '&_IMRDecisionReceivedDate=' + $("#IMRDecisionReceivedDate").val(),
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON(self.IMRRequestsDetails()),
            success: function (_data) {
                ko.mapping.fromJS(_data, {}, self.IMRRequestsDetails);
                alertify.alert("Details Saved Successfully");
                hideLoader();
                unblockPopupBackground();
            },
            error: function (xhr, status, p3, p4) {
                var err = "Error " + " " + status + " " + p3 + " " + p4;
                alertify.alert(err);
                hideLoader();
                unblockPopupBackground();
            }
        });
    };

    self.UploadIMRDecisionFile = function () {
        var formdata = new FormData(); //FormData object
        var fileInput = document.getElementById('upldIMRDecision');
        if (fileInput.files.length > 0) {
            //Iterating through each files selected in fileInput
            for (i = 0; i < fileInput.files.length; i++) {
                //Appending each file to FormData object
                formdata.append(fileInput.files[i].name, fileInput.files[i]);
            }
            $.ajax({
                type: "POST",
                url: '/IMR/UploadIMRDecisionDoc?id=' + self.IMRReferralDetails().RFAReferralID() + '&id2=' + self.IMRDecisionUploadRFAReferralFileID(),
                contentType: false,
                processData: false,
                data: formdata,
                success: function (result) {
                    self.IMRDecisionUploadRFAReferralFileID(result);
                    alertify.alert("File Uploaded Successfully");
                },
                error: function (xhr, status, p3, p4) {
                    var err = "Error " + " " + status + " " + p3 + " " + p4;
                    alertify.alert(err);
                }
            });
        }
        else {
            alertify.alert("Please select a file.");
        }
    };

    self.ClearPopup = function () {
        unblockPopupBackground();
        $(".multiple_emails-container").remove();
        $('#EMailTo').val('');
        $('#EMailCc').val('');
        $('#EmailText').val('');
        $('#subject').val('');
        $('#SendEmail').modal('hide');
    };

    self.SendEmail = function () {
        self.ClearPopup();
        blockPopupBackground();
        showLoader();
        $('#EMailTo').multiple_emails();
        $('#EMailCc').val('["' + 'UR@hcrg.com' + '"]');
        $('#EMailCc').multiple_emails();
        $('#SendEmail').modal('show');
        $("#subject").val(self.IMRReferralDetails().PatFirstName() + " " + self.IMRReferralDetails().PatLastName() + " - " + self.IMRReferralDetails().PatClaimNumber());
        $("#EmailText").val('Hello, Please see the attached letters for the above patient.');
        $.ajax({
            url: '/IMR/GetMergedIMRDecisionDocuments?ReflID=' + self.IMRReferralDetails().RFAReferralID(),
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON(self.EmailAttachments()),
            success: function (responseText) {
                self.documentFullPath(responseText.Item1);
                self.documentPath(responseText.Item2);
                self.documentName(self.IMRReferralDetails().RFAReferralID() + "_IMRDecisionMergeDocument.pdf");
                hideLoader();
                blockPopupBackground();
                $.post("/Common/AssignRFiInSessionVariable", { FullPath: self.documentPath(), FileName: self.documentName(), referralFileID: 0, sessionVar: "IMRDecision" }, function (model) {
                });
            }
        });
        hideLoader();
        blockPopupBackground();
    };

    self.IMRDecisionEmailActionSuccess = function (responseText) {
        hideLoader();
        blockPopupBackground();
        alertify.alert(responseText, function () {
            window.location.href = "/IMR/Index";
        });

    };

    self.IMRDecisionEmailActionInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#EMailTo").val() == "[]") {
            $("#EMailTo").val("");
        }

        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        blockPopupBackground();
        showLoader();
        return true;
    };

    $('#additionRecordsBtn').click(function () {
        window.location.href = "/Common/GetAdditionalDocuments?id=" + self.IMRReferralDetails().PatientID() + "&id2=" + self.IMRReferralDetails().PatientClaimID()
          + "&id3=" + self.IMRReferralDetails().RFAReferralID() + "&emailPopupName=IMRDecision";
    });


    self.DecisionChanged = function (obj, event) {
        if (event.originalEvent) {
            self.DecisionChangedCondition(parseInt($("#ddlIMRDecision").val(), 10));
        }
        else {
            self.DecisionChangedCondition(self.IMRReferralDetails().IMRDecisionID());
            if (self.IMRReferralDetails().IMRDecisionID() == 3) {
                for (var i = 0; i < self.IMRRequestsDetails().length; i++) {
                    self.IMRRequestsDetails()[i].IMRApprovedUnits(model.IMRRequestsDetails[i].IMRApprovedUnits);
                    self.IMRRequestsDetails()[i].Overturn(model.IMRRequestsDetails[i].IMRApprovedUnits);
                }
            }
        }
    };

    self.DecisionChangedCondition = function (decisionID) {
        switch (decisionID) {
            case 1:
                for (var i = 0; i < self.IMRRequestsDetails().length; i++) {
                    self.IMRRequestsDetails()[i].IMRApprovedUnits(null);
                    self.IMRRequestsDetails()[i].Overturn(false);
                }
                $('[name="chkboxOverturn"]').prop('disabled', true);
                $("#btnIMRDecisionProofOfService").addClass("disabled");
                $("#btnIMRDecisionLetter").addClass("disabled");
                $('[name="IMRApprovedUnits"]').attr("readonly", true);
                break;
            case 2:
                for (var i = 0; i < self.IMRRequestsDetails().length; i++) {
                    self.IMRRequestsDetails()[i].IMRApprovedUnits(self.IMRRequestsDetails()[i].RFAQuantity());
                    self.IMRRequestsDetails()[i].Overturn(true);
                }
                $('[name="chkboxOverturn"]').prop('disabled', true);
                $("#btnIMRDecisionProofOfService").removeClass('disabled', false);
                $("#btnIMRDecisionLetter").removeClass('disabled', false);
                $('[name="IMRApprovedUnits"]').prop("disabled", true);
                break;
            case 3:
                for (var i = 0; i < self.IMRRequestsDetails().length; i++) {
                    self.IMRRequestsDetails()[i].IMRApprovedUnits(null);
                    self.IMRRequestsDetails()[i].Overturn(false);
                }
                $('[name="chkboxOverturn"]').prop('disabled', false);
                $("#btnIMRDecisionProofOfService").removeClass('disabled', false);
                $("#btnIMRDecisionLetter").removeClass('disabled', false);
                $('[name="IMRApprovedUnits"]').prop('disabled', false);
                $('[name="IMRApprovedUnits"]').attr("readonly", true);
                break;
        }
    };

    self.overturnChange = function (index) {
        if ($("#Over" + index).is(":checked")) {
            $("#App" + index).removeAttr("readonly", false);
        }
        else {
            $("#App" + index).attr("readonly", true);
            self.IMRRequestsDetails()[index].IMRApprovedUnits("");
        }
        return true;
    };

    self.GenerateIMRInitialNotification = function () {
        $("#btnIMRInitialNotification").prop('disabled', true);
        blockPopupBackground();
        showLoader();
        $.post("/IMR/GenerateIMRInitialNotification/", { referralID: self.IMRReferralDetails().RFAReferralID(), RFAReferralFileID: self.IMRInitialNotificationRFAReferralFileID }, function (data) {
            self.IMRInitialNotificationRFAReferralFileID(data.Item3);
            self.IMRInitialNotificationDocumentName(data.Item1);
            self.IMRInitialNotificationDocumentPath(data.Item2);
            self.IMRLetters(data.Item4);
            self.EmailAttachments($.grep(data.Item4, function (elem) {
                return (elem.RFAFileTypeID == 17 || elem.RFAFileTypeID == 18 || elem.RFAFileTypeID == 19);
            }));
            hideLoader();
            unblockPopupBackground();
            $("#btnIMRInitialNotification").prop('disabled', false);
        });
    };

    self.GenerateIMRDecisionLetter = function () {
        $("#btnIMRDecisionLetter").prop('disabled', true);
        blockPopupBackground();
        showLoader();
        $.post("/IMR/GenerateIMRDecisionLetter/", { referralID: self.IMRReferralDetails().RFAReferralID(), RFAReferralFileID: self.IMRDecisionLetterRFAReferralFileID, DecisionID: self.IMRReferralDetails().IMRDecisionID() }, function (data) {
            self.IMRDecisionLetterRFAReferralFileID(data.Item3);
            self.IMRDecisionLetterDocumentName(data.Item1);
            self.IMRDecisionLetterDocumentPath(data.Item2);
            self.IMRLetters(data.Item4);
            self.EmailAttachments($.grep(data.Item4, function (elem) {
                return (elem.RFAFileTypeID == 17 || elem.RFAFileTypeID == 18 || elem.RFAFileTypeID == 19);
            }));
            hideLoader();
            unblockPopupBackground();
            $("#btnIMRDecisionLetter").prop('disabled', false);
        });
    };

    self.GenerateIMRDecisionProofOfService = function () {
        $("#btnIMRDecisionProofOfService").prop('disabled', true);
        blockPopupBackground();
        showLoader();
        $.post("/IMR/GenerateIMRDecisionProofOfService/", { referralID: self.IMRReferralDetails().RFAReferralID(), RFAReferralFileID: self.IMRDecisionProofOfServiceRFAReferralFileID }, function (data) {
            self.IMRDecisionProofOfServiceRFAReferralFileID(data.Item3);
            self.IMRDecisionProofOfServiceDocumentName(data.Item1);
            self.IMRDecisionProofOfServiceDocumentPath(data.Item2);
            self.IMRLetters(data.Item4);
            self.EmailAttachments($.grep(data.Item4, function (elem) {
                return (elem.RFAFileTypeID == 17 || elem.RFAFileTypeID == 18 || elem.RFAFileTypeID == 19);
            }));
            hideLoader();
            unblockPopupBackground();
            $("#btnIMRDecisionProofOfService").prop('disabled', false);
        });
    };
}

function removeAttachment() {
    _imrDecisionViewModel.documentPath(null);
}