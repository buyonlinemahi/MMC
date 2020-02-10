function NotificationActionViewModel(model) {
    var self = this;
    self.Patients = ko.observableArray();
    self.RFAReferralID = ko.observable();
    self.RequestDetail = ko.observableArray();
    self.Skip = ko.observable(0);
    self.TotalItemCount = ko.observable();
    self.RFAReferralFileID = ko.observable();
    self.RFAReferralFileName = ko.observable();
    self.ReferralFileNotification = ko.observableArray();
    self.ReferralFileNotification1 = ko.observableArray();    

    self.documentPath = ko.observable();
    self.documentName = ko.observable();
    self.RFASignature = ko.observable();
    self.RFASignatureDescription = ko.observable();
    self.SelectAll = ko.computed({
        read: function () {
            var item = ko.utils.arrayFirst(self.ReferralFileNotification(), function (item) {
                return !item.IsChecked();
            });
            return item == null;
        },
        write: function (value) {
            ko.utils.arrayForEach(self.ReferralFileNotification(), function (person) {
                person.IsChecked(value);
            });
        }
    });
    GrdBinding();
    var mappingOptions = {
        'PatDOI': {
            create: function (options) {
                if (options.data != '/Date(-62135596800000)/')
                    return moment(options.data).format("MM-DD-YYYY ");
            }
        },
        'RFALatestDueDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM-DD-YYYY");
            }
        }
    }
    ko.mapping.fromJS(model, mappingOptions, self);
    ko.mapping.fromJS(model.RequestCount, {}, self.TotalItemCount);
    ko.mapping.fromJS(model.Patients.RFAReferralID, {}, self.RFAReferralID);
    if (model.ReferralFile != null)
    {
        ko.mapping.fromJS(model.ReferralFile.RFAReferralFileID, {}, self.RFAReferralFileID);
        ko.mapping.fromJS(model.ReferralFile.RFAReferralFileName, {}, self.RFAReferralFileName);
    }

    if (model.ReferralsForDeterminationLetter != null) {
        var signatureByDefault = "RN"; //RN for Vicky summogum signature by default in scenario of  case from preperation  to Notification
        for (var i = 0; i < model.ReferralsForDeterminationLetter.length; i++) {
              // this is download and save only certify,modify , deny,Client Authorized case...... by Mahi
            if (model.ReferralsForDeterminationLetter[i].DecisionID == 12) {
                $.post("/Preparation/UploadSignatureAndDescription", {
                    _RFAReferralID: self.RFAReferralID(), SignatureSelect: signatureByDefault
                }, function (model) {
                    var _model = $.parseJSON(model);
                    self.RFASignature(_model.RFASignature);
                    self.RFASignatureDescription(_model.RFASignatureDescription);
                    location.href = "/Notification/GenerateDeterminationLetter/" + model.ReferralsForDeterminationLetter[i].RFAReferralID + "";
                });
            }
            else
                location.href = "/Notification/GenerateDeterminationLetter/" + model.ReferralsForDeterminationLetter[i].RFAReferralID + "";            
        }
    }
   
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

    self.NotificationActionInfoFormBeforeSubmit = function (arr, $form, options) {
       
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    self.NotificationActionSuccess = function (responseText) {       
        if (responseText != null) {
            var e = $.parseJSON(responseText);
            alertify.alert("Uploaded Successfully");
            self.RFAReferralFileName(e.RFAReferralFileName);
            self.RFAReferralFileID(e.RFAReferralFileID);
        };
    };
    $('#frmEmailAction').keypress(function (event) {
         var keycode = (event.keyCode ? event.keyCode : event.which);
        if (keycode == '13') {
            if ($(":focus").attr("id") == "emailSend") {               
                if ($(".multiple_emails-error").length > 0) {
                    return false;
                }
                else
                {
                    return true;
                }              
               
            }
            else {
                 return false;
            }
        }

    });


    $('#OpenEmail').click(function () {
        ClearEmailpopup();
        blockPopupBackground();
    });

    $('#sectPayment').click(function () {
        unblockPopupBackground();
    });
    self.NotificationEmailActionInfoFormBeforeSubmit = function (arr, $form, options) {    
       
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        if ($(".multiple_emails-error").length > 0) {
            return false;
        }
        showLoader();
        return true;
    }

    self.NotificationEmailActionSuccess = function (responseText) {
        if (responseText != null) {
            var e = $.parseJSON(responseText);
            hideLoader();
            alertify.alert(e, function (e) {
                if (e) {
                    unblockPopupBackground();
                    ClearEmailpopup();
                    clearCkhbox();
                }
            });
        };
    };

    //INsuranceBranch..
    
    self.SendEmail = function () {        
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
            alertify.alert("You must check one and only one checkbox!");
            return false;
        }
        else {
            $.post("/Notification/GetAdjAndPhyEmail", { _referralID: self.RFAReferralID() }, function (emailRecord) {
                var e = $.parseJSON(emailRecord);
                $('#RefID').val(self.RFAReferralID());
                $("#EMailTo").val('["' + e[0].NotificationEmail + '","' + e[1].NotificationEmail + '"]');              
                $('#EMailTo').multiple_emails();
                $('#EMailCc').val('["' + 'UR@hcrg.com' + '"]');
                $('#EMailCc').multiple_emails();
                $('#SendEmail').modal('show');
                $("#subject").val(self.Patients().PatFirstName() + " " + self.Patients().PatLastName() + " - " + self.Patients().PatClaimNumber());///+ " " + self.RequestDetail()[0].Decision()
                $("#EmailText").val('Hello, Please see the attached letters for the above patient.');
                
            });
 
            $.ajax({
                url: '/Notification/GetLinkForAttachement',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: ko.toJSON(self.ReferralFileNotification()),
                success: function (responseText) {                    
                    self.documentPath(responseText);
                    self.documentName(self.RFAReferralID() + "_MergeDocuments.pdf");
                    $.post("/Common/AssignRFiInSessionVariable", { FullPath: self.documentPath(), FileName: self.documentName(), referralFileID: 0, sessionVar: "Notification" }, function (model) {
                    });
                }
            });
        }
    };

        $('#additionRecordsBtn').click(function () {
            window.location.href = "/Common/GetAdditionalDocuments?id=" + self.Patients().PatientID() + "&id2=" + self.Patients().PatientClaimID()
              + "&id3=" + self.RFAReferralID() + "&emailPopupName=Notification";
        });

    self.PrintDocument = function () {
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
            alertify.alert("You must check one and only one checkbox!");
            return false;
        }
        else {
            $("#frmPrintDocumnent").submit();
        }
    };

    self.BillingActionProcess = function () {
        $.post("/Notification/BillingActionProcess", { _rFAReferralID: self.RFAReferralID() }, function (responseText) {
            if (responseText != null) {
                alertify.alert(responseText, function (e) {
                    if (e) {
                        showLoader();
                        window.location = '/Notification/Index';
                    }
                });
            };
        });
    }
    
    function ClearEmailpopup() {
        $(".multiple_emails-container").remove();
        $('#EMailTo').val('[]');
        $('#EMailCc').val('[]');
        $('#EmailText').val('');
        $('#subject').val('');
        $('#SendEmail').modal('hide');

    }

    function clearCkhbox() {
        for (var i = 0; i < self.ReferralFileNotification().length; i++) {
            self.ReferralFileNotification()[i].IsChecked(false);
        }
    }

    self.ClearEmailPopUp = function () {
        ClearEmailpopup();
    };
 
    function GrdBinding() {
        $.post("/Notification/GetRequestRecordByReferralID", { _referralID: self.RFAReferralID(), _skip: self.Skip() }, function (medicalRecord) {
            var e = $.parseJSON(medicalRecord);
            ko.mapping.fromJS(e.RequestDetail, mappingOptions, self.RequestDetail);
            self.TotalItemCount(e.RequestCount);
        });
    }
    function GetReferralFileByRFAReferralIDandFileType(ReferralID) {
        $.post("/Notification/GetReferralFileByRFAReferralIDandFileType", { _referralID: ReferralID }, function (data) {
            var _data = $.parseJSON(data);
            ko.mapping.fromJS(_data.ReferralFileNotification, {}, self.ReferralFileNotification);
        });
    }


    self.ProofOfService = function () {
        //location.href = "/Notification/GenerateProofOfService/" + self.RFAReferralID() + "";
        showLoader();
        $.post("/Notification/GenerateProofOfService", { id: self.RFAReferralID() }, function (data) {            

            location.href = "/Notification/GetProofOfServiceFile/" + self.RFAReferralID() + "";
            GetReferralFileByRFAReferralIDandFileType(self.RFAReferralID());
            hideLoader();
        });

        //location.href = "/Notification/GenerateProofOfService/" + self.RFAReferralID() + "";
        
    };

    self.IMRForm = function () {
        var _IsEnable = 0;       
        for (var i = 0; i < self.RequestDetail().length; i++) {
            var _desc = self.RequestDetail()[i].DecisionDesc();
            if(_desc == "Certify" || _desc == "Modify" || _desc == "Deny")
            {
                _IsEnable = 1;
                break;
            }
        }

        if (_IsEnable == 1) {// this is download and save only certify,modify or deny case...... by Mahi
            showLoader();
            $.post("/Notification/GenerateIMRForm", { id: self.RFAReferralID() }, function (data) {
                location.href = "/Notification/GetIMRFile/" + self.RFAReferralID() + "";
                GetReferralFileByRFAReferralIDandFileType(self.RFAReferralID());
                hideLoader();
            });
            //location.href = "/Notification/GenerateIMRForm/" + self.RFAReferralID() + "";
        }
    }
    self.Determination = function () {
        var _desc = "";
        var _IsEnable = 0;        
        for (var i = 0; i < self.RequestDetail().length; i++) {
             _desc = self.RequestDetail()[i].DecisionDesc();
            if (_desc == "Certify" || _desc == "Modify" || _desc == "Deny" || _desc == "Client Authorized")
            {
                _IsEnable = 1;
                break;
            }
        }

        if (_IsEnable == 1) {// this is download and save only certify,modify or deny case...... by Mahi
            var signatureByDefault = "RN"; //RN for Vicky summogum signature by default in scenario of  case from preperation  to Notification
            if (_desc == "Client Authorized") {
                $.post("/Preparation/UploadSignatureAndDescription", {
                    _RFAReferralID: self.RFAReferralID(), SignatureSelect: signatureByDefault
                }, function (model) {
                    var _model = $.parseJSON(model);
                    self.RFASignature(_model.RFASignature);
                    self.RFASignatureDescription(_model.RFASignatureDescription);
                    showLoader();
                    $.post("/Notification/GenerateDeterminationLetter", { id: self.RFAReferralID() }, function (data) {

                        location.href = "/Notification/GetDeterminationFile/" + self.RFAReferralID() + "";
                        GetReferralFileByRFAReferralIDandFileType(self.RFAReferralID());
                        hideLoader();
                    });

                    //location.href = "/Notification/GenerateDeterminationLetter/" + self.RFAReferralID() + "";
                });
            }
            else {
                showLoader();
                $.post("/Notification/GenerateDeterminationLetter", { id: self.RFAReferralID() }, function (data) {
                    location.href = "/Notification/GetDeterminationFile/" + self.RFAReferralID() + "";
                    GetReferralFileByRFAReferralIDandFileType(self.RFAReferralID());
                    hideLoader();
                });
            }
           //GetReferralFileByRFAReferralIDandFileType(self.RFAReferralID());
        }

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

    self.newColor = ko.observable();

    self.changeOrder = function (index) {
        if (index > 0)
        {
            var replaceRow = self.ReferralFileNotification()[index]
            self.ReferralFileNotification.replace(self.ReferralFileNotification()[index], self.ReferralFileNotification()[index - 1]);
            self.ReferralFileNotification.replace(self.ReferralFileNotification()[index - 1], replaceRow);
        }        
    }    
}


function removeAttachment() {
    _notificationActionViewModel.documentPath(null);    
}
