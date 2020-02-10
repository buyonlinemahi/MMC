function IMRActionViewModel(model) {
    var self = this;
    self.Patients = ko.observableArray();
    self.RFAReferralID = ko.observable(model.Patients.RFAReferralID);
    self.RequestDetail = ko.observableArray();
    self.Skip = ko.observable(0);
    self.TotalItemCount = ko.observable();
    self.RFAReferralFileID = ko.observable();
    self.RFAReferralFileName = ko.observable();
    self.ReferralFileNotification = ko.observableArray();
    self.ReferralFileNotification1 = ko.observableArray();
    self.documentPath = ko.observable();
    self.documentFullPath = ko.observable();
    self.documentName = ko.observable();

    self.IMRResponseRFAReferralFileID = ko.observable(0);
    self.IMRResponseDocumentPath = ko.observable();
    self.IMRResponseDocumentFullPath = ko.observable();
    self.IMRResponseDocumentName = ko.observable();

    self.ProofOfServiceRFAReferralFileID = ko.observable(0);
    self.ProofOfServiceDocumentPath = ko.observable();
    self.ProofOfServiceDocumentFullPath = ko.observable();
    self.ProofOfServiceDocumentName = ko.observable();

    self.Requests = ko.observable();
    self.RFAIMRReferenceNumber = ko.observable();
    self.IMRRFAClaimPhysicianID = ko.observable();
    self.IMRApplicationReceivedDate = ko.observable();
    self.IMRNoticeInformationDate = ko.observable();
    self.Physicians = ko.observableArray();
    self.IMRRFAReferralID = ko.observable();

    self.IMRCEReceivedDate = ko.observable();
    self.IMRInternalReceivedDate = ko.observable();
    self.IMRReceivedVia = ko.observable();
    self.IMRResponseDueDate = ko.observable();
    self.IMRPriority = ko.observable();
    self.IMRResponseType = ko.observable();

    var varRequest = '';
    for (var i = 0; i < model.RequestDetail.length; i++) {
        if (i == model.RequestDetail.length - 1) {
            varRequest += model.RequestDetail[i].RFARequestedTreatment;
        }
        else {
            varRequest += model.RequestDetail[i].RFARequestedTreatment + ',';
        }
    }    

    for (var i = 0; i < model.IMRLetters.length; i++) {        
        if (model.IMRLetters[i].RFAFileTypeID == 16) {
            self.IMRResponseRFAReferralFileID(model.IMRLetters[i].RFAReferralFileID);
            self.IMRResponseDocumentName(model.IMRLetters[i].RFAReferralFileName);
            self.IMRResponseDocumentPath(model.IMRLetters[i].RFAReferralFileFullPath);
        }
        if (model.IMRLetters[i].RFAFileTypeID == 14) {
            self.ProofOfServiceRFAReferralFileID(model.IMRLetters[i].RFAReferralFileID);
            self.ProofOfServiceDocumentName(model.IMRLetters[i].RFAReferralFileName);
            self.ProofOfServiceDocumentPath(model.IMRLetters[i].RFAReferralFileFullPath);
        }
    }        

    self.Requests(varRequest);
    var mappingOptions = {
        'RFAFileCreationDate': {
            create: function (options) {
                if (options.data != '/Date(-62135596800000)/')
                    return moment(options.data).format("MM-DD-YYYY");
            }
        }
    };

    $(window).load(function () {
        $.post("/IMR/getIMRRefrenceNumber", { _rfaID: self.RFAReferralID() }, function (_responseText) {
            self.RFAIMRReferenceNumber(_responseText.RFAIMRReferenceNumber);
            setTimeout(
            function () {
                var editor1 = document.getElementById("EditorNote").editor;
                if (editor1 != undefined)
                    editor1.SetText(($("<div>").html(_responseText.RFAIMRHistoryRationale).text()));
            }, 3000);

        });
    });



    ko.mapping.fromJS(model, mappingOptions, self);

    self.IMREmailActionInfoFormBeforeSubmit = function (arr, $form, options) {

        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        if ($(".multiple_emails-error").length > 0) {
            return false;
        }
        showLoader();
        return true;
    }

    self.IMREmailActionSuccess = function (responseText) {
        if (responseText != null) {
            hideLoader();
            alertify.alert(responseText, function (e) {
                if (e) {
                    window.location = "/IMR/Index";
                    unblockPopupBackground();
                    $('#SendEmail').modal('hide');
                    clearCkhbox();
                }
            });
        };
    };

    function clearCkhbox() {
        for (var i = 0; i < self.ReferralFileNotification().length; i++) {
            self.ReferralFileNotification()[i].IsChecked(false);
        }
    }

    getIMRRefrel();
    function getIMRRefrel() {
        $.post("/IMR/GetIMRReferrals", { _rfaID: self.RFAReferralID() }, function (_responseText) {
            if (_responseText != null) {
                self.IMRApplicationReceivedDate(_responseText.IMRApplicationReceivedDate);

                if (self.IMRApplicationReceivedDate() != "")
                    self.IMRApplicationReceivedDate(moment(self.IMRApplicationReceivedDate()).format("MM/DD/YYYY"));

                self.IMRNoticeInformationDate(_responseText.IMRNoticeInformationDate);

                if (self.IMRNoticeInformationDate() != "")
                    self.IMRNoticeInformationDate(moment(self.IMRNoticeInformationDate()).format("MM/DD/YYYY"));

                self.IMRRFAClaimPhysicianID(_responseText.IMRRFAClaimPhysicianID);
                self.IMRRFAReferralID(_responseText.IMRRFAReferralID);

                self.IMRInternalReceivedDate(_responseText.IMRInternalReceivedDate);

                if (self.IMRInternalReceivedDate() != "")
                    self.IMRInternalReceivedDate(moment(self.IMRInternalReceivedDate()).format("MM/DD/YYYY"));

                self.IMRCEReceivedDate(_responseText.IMRCEReceivedDate);
                if (self.IMRCEReceivedDate() != "")
                    self.IMRCEReceivedDate(moment(self.IMRCEReceivedDate()).format("MM/DD/YYYY"));


                self.IMRReceivedVia(_responseText.IMRReceivedVia);

                self.IMRResponseDueDate(_responseText.IMRResponseDueDate);

                if (self.IMRResponseDueDate() != "")
                    self.IMRResponseDueDate(moment(self.IMRResponseDueDate()).format("MM/DD/YYYY"));

                self.IMRPriority(_responseText.IMRPriority);
                self.IMRResponseType(_responseText.IMRResponseType);

                //if ($("#IMRReceivedVia").val() == '1')
                //    $("#IMRNoticeDate").val(add_business_days(15))
                //else
                //    $("#IMRNoticeDate").val(add_business_days(12))


                $('#btnProof').prop('disabled', false);
            }
        });

    }

    $.post("/IMR/GetPhysiciansbyReferralId", { _rfaID: self.RFAReferralID() }, function (data) {
        self.Physicians(data.slice());
    });

    //// ------------------------------------- UpDate RichText ---------------------------------------------------------//
    self.UpdateText = function () {
        if ($("#EditorNote").val() == '') {
            alertify.alert("Enter Text");
            return false;
        }

        showLoader();
        var viewModelText = {
            RFAReferralID: self.RFAReferralID(),
            RFAIMRHistoryRationale: $("<div>").text($("#EditorNote").val()).html()
        }
        var plainJs = ko.toJS(viewModelText);
        $.ajax({
            url: "/IMR/UpdateRFAIMRHistoryRationaleByReferralID",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(plainJs),
            success: function (data) {
                hideLoader();
                if (data != 0) {
                    alertify.alert("Saved Successully", function (e) {
                        if (e) {
                            $('#OpenTextEditor').modal('hide');
                            unblockPopupBackground();
                        }
                    });
                }
                else {
                    alertify.alert("Error Occur");
                    unblockPopupBackground();
                }
            },
            error: function (data) {
                hideLoader();
                alertify.alert("Error Occur");
                unblockPopupBackground();
            }
        });
    };

    //// ----------------------------------End UpDate RichText ---------------------------------------------------------//

    //// ------------------------------------- Upload Document ---------------------------------------------------------//

    self.IMRUploadInfoFormBeforeSubmit = function (arr, $form, options) {

        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        if ($(".multiple_emails-error").length > 0) {
            return false;
        }
        showLoader();
        return true;
    }

    self.IMRUploadActionSuccess = function (responseText) {
        if (responseText != null) {
            hideLoader();
            alertify.alert(responseText, function (e) {
                if (e) {
                    $('#btnProof').prop('disabled', false);
                }
            });
        };
    };
    //// ----------------------------------End Upload Document ---------------------------------------------------------//

    $('#btnOpenText').click(function () {
        blockPopupBackground();
    });

    $('#EditorClose').click(function () {
        unblockPopupBackground();
    });

    $(document).ready(function () {
        $('.selectItemAll').change(function () {
            if (this.checked) {
                $('.selectItem').prop('checked', true);
                $('.hFIsChecked').val(true);
                for (var i = 0; i < self.ReferralFileNotification().length; i++) {
                    self.ReferralFileNotification()[i].IsChecked(true);
                }
            }
            else {
                $('.selectItem').prop('checked', false);
                $('.hFIsChecked').val(false);
                for (var i = 0; i < self.ReferralFileNotification().length; i++) {
                    self.ReferralFileNotification()[i].IsChecked(false);
                }
            }
        });

        $(".selectItem").change(function () {
            if ($('.selectItem:checked').length == $('.selectItem').length) {
                $('.selectItemAll').prop('checked', true);
            } else {
                $('.selectItemAll').prop('checked', false);
            }
        });

    });


    //// ------------------------------------- Generate IMRResponse Letter ---------------------------------------------------------//
    self.GenerateIMRResponse = function () {
        var flag = 0;
        var chk = document.getElementsByName('RFAReferralFile[]')
        var len = chk.length
        if ($('.selectItem:checked').length == 0) {
            alertify.alert("You must check one and only one checkbox!");
        }
        else {
            showLoader();
            self.ReferralFileNotification();
            $.ajax({
                url: '/IMR/GenerateIMRResponse?ReflID=' + self.RFAReferralID() + '&ImrRFAReferralFileID=' + self.IMRResponseRFAReferralFileID(),
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: ko.toJSON(self.ReferralFileNotification()),
                success: function (responseText) {
                    self.IMRResponseDocumentFullPath(responseText.Item1);
                    self.IMRResponseDocumentPath(responseText.Item2);
                    self.IMRResponseRFAReferralFileID(responseText.Item3);
                    self.IMRResponseDocumentName(self.RFAReferralID() + "_IMRResponseLetter.pdf");
                    hideLoader();
                }
            });
            //hideLoader();
        }
    };

    //// ------------------------------------- Send Email popup ---------------------------------------------------------//

    self.SendEmail = function () {
        blockPopupBackground();
        ClearEmailpopup();
        showLoader();
        var flag = 0;
        var chk = document.getElementsByName('RFAReferralFile[]')
        var len = chk.length
        for (i = 0; i < len; i++) {
            if (chk[i].checked) {
                flag++;
                break;
            }
        }
        if (flag == 0) {
            unblockPopupBackground();
            hideLoader();
            alertify.alert("Please check atleast one checkbox!");
            return false;
        }
        else {

            $.post("/IMR/GetIMRProofOfService", { _rfaID: self.RFAReferralID() }, function (_data) {
                if (_data.length > 0) {
                    var s = 0;
                    var viemodelReferral = {
                        RFAReferralID: self.RFAReferralID(),
                        RFAIMRReferenceNumber: self.RFAIMRReferenceNumber(),
                    };

                    $.post("/IMR/getFileSplitedDetail", { _rfaID: self.RFAReferralID() }, function (_responseText) {
                        if (_responseText.length == 0) {
                            unblockPopupBackground();
                            alertify.alert("Upload Notice of Assignment and Request for Information");
                            return false;
                        }
                        else {
                            $('#EMailTo').multiple_emails();
                            $('#EMailCc').val('["' + 'UR@hcrg.com' + '"]');
                            $('#EMailCc').multiple_emails();
                            $('#SendEmail').modal('show');
                            $("#subject").val(self.Patients().PatFirstName() + " " + self.Patients().PatLastName() + " - " + self.Patients().PatClaimNumber());
                            $("#EmailText").val('Hello, Please see the attached letters for the above patient.');
                            $.ajax({
                                url: '/IMR/AttachEmailFile?ReflID=' + self.RFAReferralID(),
                                type: 'POST',
                                contentType: 'application/json; charset=utf-8',
                                data: ko.toJSON(self.ReferralFileNotification()),
                                success: function (responseText) {
                                    self.documentFullPath(responseText.Item1);
                                    self.documentPath(responseText.Item2);
                                    self.documentName(self.RFAReferralID() + "_IMRMergeDocument.pdf");
                                    hideLoader();
                                    blockPopupBackground();
                                    $.post("/Common/AssignRFiInSessionVariable", { FullPath: self.documentPath(), FileName: self.documentName(), referralFileID: 0, sessionVar: "IMR" }, function (model) {
                                    });
                                }
                            });
                        }
                    });

                }
                else {
                    unblockPopupBackground();
                    hideLoader();
                    alertify.alert("Generate Proof Of Service Letter");
                    return false;
                }
            });

            //   }
        }
    };

    $("#sectPayment").click(function () {
        ClearEmailpopup();
    });

    $('#additionRecordsBtn').click(function () {
        window.location.href = "/Common/GetAdditionalDocuments?id=" + self.Patients().PatientID() + "&id2=" + self.Patients().PatientClaimID()
          + "&id3=" + self.RFAReferralID() + "&emailPopupName=IMR";
    });

    function ClearEmailpopup() {
        unblockPopupBackground();
        $(".multiple_emails-container").remove();
        $('#EMailTo').val('[]');
        $('#EMailCc').val('[]');
        $('#EmailText').val('');
        $('#subject').val('');
        $('#SendEmail').modal('hide');


    }

    function Check_business_days(event) {
        if (event.originalEvent) { //user changed
            if ($("#IMRReceivedVia").val() == '1')
                add_business_days(15);
            else if ($("#IMRReceivedVia").val() == '2')
                add_business_days(12);
            else
                $("#IMRResponseDueDate").val($("#HFIMRResponseDueDate").val());
        } else { // program changed
            $("#IMRResponseDueDate").val($("#HFIMRResponseDueDate").val());
        }
    }

    self.AddBusinessDay = function (obj, event) {
        Check_business_days(event);
    };

    $(document).ready(function () {
        $("#IMRNoticeDate").change(function () {
            if ($("#IMRReceivedVia").val() == '1')
                add_business_days(15);
            else if ($("#IMRReceivedVia").val() == '2')
                add_business_days(12);
            else
                $("#IMRResponseDueDate").val($("#HFIMRResponseDueDate").val());
        });
    });

    function add_business_days(days) {
        var startDate = $("#HFIMRNoticeDate").val();
        startDate = new Date(startDate.replace(/-/g, "/"));
        var endDate = "", noOfDaysToAdd = days, count = 0;
        while (count < noOfDaysToAdd) {
            endDate = new Date(startDate.setDate(startDate.getDate() + 1));
            if (endDate.getDay() != 0 && endDate.getDay() != 6) {
                count++;
            }
        };
        $("#IMRResponseDueDate").val(moment(endDate).format("MM/DD/YYYY"));
    }

    self.Changebusinessday = function (obj, event) {
        Check_business_days(event);
    };



    //// ----------------------------------End Send Email popup ---------------------------------------------------------//

    //// ------------------------------------- Proof Of Service ---------------------------------------------------------//
    self.GenerateProofOfService = function () {
        $.post("/IMR/getFileSplitedDetail", { _rfaID: self.RFAReferralID()}, function (_responseText) {
            if (_responseText.length == 0) {
                unblockPopupBackground();
                alertify.alert("Save the above detail");
                return false;
            }
            else {
                showLoader();
                $.post("/IMR/GenerateProofOfService", { _rfaId: self.RFAReferralID(), POSRFAReferralFileID: self.ProofOfServiceRFAReferralFileID() }, function (data) {
                    self.ProofOfServiceDocumentFullPath(data.Item1);
                    self.ProofOfServiceDocumentPath(data.Item2);
                    self.ProofOfServiceRFAReferralFileID(data.Item3);
                    self.ProofOfServiceDocumentName(self.RFAReferralID() + "_IMRProofOfService.pdf");
                    hideLoader();
                });
            }
        });
    };
    //// ----------------------------------End Proof Of Service ---------------------------------------------------------//
}

function removeAttachment() {
    _imrActionViewModel.documentPath(null);
}