function PatientMedicalRecordSplitInsert(_RFARecSpltID, _PatientID, _PatientClaimID, _DocumentCategoryID, _DocumentTypeID, _RFARecDocumentName, _RFARecDocumentDate, _RFARecPageStart, _RFARecPageEnd, _DocumentCategoryName, _DocumentTypeDesc, _PatientClaimNumber, _RFAReferralFileName, _AuthorName, _RFARequestID) {
    var self = this;
    self.RFARecSpltID = ko.observable(0);
    self.PatientID = ko.observable(_PatientID);
    self.PatientClaimID = ko.observable(_PatientClaimID);
    self.DocumentCategoryID = ko.observable(_DocumentCategoryID);
    self.DocumentTypeID = ko.observable(_DocumentTypeID);
    self.RFARecDocumentName = ko.observable(_RFARecDocumentName);
    self.RFARecDocumentDate = ko.observable(moment(_RFARecDocumentDate).format("MM/DD/YYYY"));
    self.RFARecPageStart = ko.observable(_RFARecPageStart);
    self.RFARecPageEnd = ko.observable(_RFARecPageEnd);
    self.DocumentCategoryName = ko.observable(_DocumentCategoryName);
    self.DocumentTypeDesc = ko.observable(_DocumentTypeDesc);
    self.PatientClaimNumber = ko.observable(_PatientClaimNumber);
    self.RFAReferralFileName = ko.observable(_RFAReferralFileName);
    self.AuthorName = ko.observable(_AuthorName);
    self.RFARequestID = ko.observable(_RFARequestID);

}
function PatientMedicalRecordSplitUpdate(_RFARecSpltID, _PatientID, _PatientClaimID, _DocumentCategoryID, _DocumentTypeID, _RFARecDocumentName, _RFARecDocumentDate, _RFARecPageStart, _RFARecPageEnd, _DocumentCategoryName, _DocumentTypeDesc, _PatientClaimNumber, _RFAReferralFileName, _AuthorName, _RFARequestID) {
    var self = this;
    self.RFARecSpltID = ko.observable(0);
    self.PatientID = ko.observable(_PatientID);
    self.PatientClaimID = ko.observable(_PatientClaimID);
    self.DocumentCategoryID = ko.observable(_DocumentCategoryID);
    self.DocumentTypeID = ko.observable(_DocumentTypeID);
    self.RFARecDocumentName = ko.observable(_RFARecDocumentName);
    self.RFARecDocumentDate = ko.observable(moment(_RFARecDocumentDate).format("MM/DD/YYYY"));
    self.RFARecPageStart = ko.observable(_RFARecPageStart);
    self.RFARecPageEnd = ko.observable(_RFARecPageEnd);
    self.DocumentCategoryName = ko.observable(_DocumentCategoryName);
    self.DocumentTypeDesc = ko.observable(_DocumentTypeDesc);
    self.PatientClaimNumber = ko.observable(_PatientClaimNumber);
    self.RFAReferralFileName = ko.observable(_RFAReferralFileName);
    self.AuthorName = ko.observable(_AuthorName);
    self.RFARequestID = ko.observable(_RFARequestID);

}
function AdditionalViewModel(model) {
    var self = this;    
    self.reqSkip = ko.observable(0);
    self.PatientID = ko.observable(0);
    self.PatientClaimID = ko.observable(0);
    self.PatientFullName = ko.observable();
    self.PatClaimNumber = ko.observable();
    self.RFAReferralID = ko.observable(0);
    self.emailPopupName = ko.observable();
    self.reqTotalItemCount = ko.observable(0);
    self.AdditionalDocumentResults = ko.observableArray();
    self.PreviousAttachmentArrayLinks = ko.observableArray();
    self.reqCurrentPage = ko.observable(0);
    self.maxId = ko.observable(0);
    self.IsTrue = ko.observable(false);


    self.additionalscrolled = function (data, event) {
        if (!self.IsTrue()) {
            if (self.reqTotalItemCount() > self.maxId()) {
                var elem = event.target;
                if (elem.scrollTop > (elem.scrollHeight - elem.offsetHeight - 1)) {
                    bindAdditionalDocumentGrid();
                }
            }
            else {
                self.IsTrue(true);

            }
        }
    };

    var mappingOptions = {
        'DocumentDate': {
            create: function (options) {
                if (options.data != '/Date(-62135596800000)/')
                    return moment(options.data).format("MM/DD/YYYY");
            }
        }
    }


    if (model != null) {
        $.each(model.AdditionalDocumentDetails, function (index, value) {
            self.AdditionalDocumentResults.push(new AdditionalDetails(value));
        });
        $.each(model.PreviousAttachmentLinks, function (index, value) {
            self.PreviousAttachmentArrayLinks.push(new AddPreviousDetails(value));
        });
        self.PatientID(model.PatientID);
        self.PatientClaimID(model.PatientClaimID);
        self.PatientFullName(model.PatientFullName);
        self.PatClaimNumber(model.PatClaimNumber);
        self.RFAReferralID(model.RFAReferralID);
        self.emailPopupName(model.emailPopupName);
        self.maxId(20);
        self.IsTrue(false);
        self.reqTotalItemCount(model.TotalCount);    
    }

    function AddPreviousDetails(value) {
        var self = this;
        self.AttachmentLink = ko.observable(value.AttachmentLink);
    };
    

    function bindAdditionalDocumentGrid() {        
        $.post("/Common/GetAdditionalDocumentByPatientID", {
            _patientID: self.PatientID(), _patientClaimID: self.PatientClaimID(), _skip: self.maxId()
        }, function (_data) {
            var model = $.parseJSON(_data);
            if (model.TotalCount > 0) {
                if (!self.IsTrue()) {
                    showLoader();
                    $(".hide_thead").show();
                    $(".hide_tbody").show();
                    $(".hide_btn").show();
                    $.each(model.AdditionalDocumentDetails, function (index, value) {
                        self.AdditionalDocumentResults.push(new AdditionalDetails(value));
                    });
                    self.reqTotalItemCount(model.TotalCount);
                    self.reqCurrentPage(self.maxId());
                    self.maxId(self.maxId() + 20);
                    hideLoader();
                }
            }
            else {
                $(".hide_thead").show();
                $(".hide_tbody").hide();
                $(".hide_btn").hide();
            }
            //blockPopupBackground();
        });
    };
    function bindAdditionalDocumentGridByUploadDocument() {
        $.post("/Common/GetAdditionalDocumentByMedicalUploadPatientID", {
            _patientID: self.PatientID(), _patientClaimID: self.PatientClaimID(), _take: self.maxId()
        }, function (_data) {
            var model = $.parseJSON(_data);
            if (model.TotalCount > 0) {
                if (!self.IsTrue()) {
                    showLoader();
                    $(".hide_thead").show();
                    $(".hide_tbody").show();
                    $(".hide_btn").show();
                    self.AdditionalDocumentResults.removeAll();
                    $.each(model.AdditionalDocumentDetails, function (index, value) {
                        self.AdditionalDocumentResults.push(new AdditionalDetails(value));
                    });
                    self.reqTotalItemCount(model.TotalCount);
                    self.reqCurrentPage(self.maxId());
                    self.maxId(self.maxId() + 20);
                    hideLoader();
                }
            }
            else {
                $(".hide_thead").show();
                $(".hide_tbody").hide();
                $(".hide_btn").hide();
            }
            //blockPopupBackground();
        });
    };
    function AdditionalDetails(value) {
        var self = this;
        self.TypeName = ko.observable(value.TypeName);
        self.FileID = ko.observable(value.FileID);
        self.RFAReferralID = ko.observable(value.RFAReferralID);
        self.PatientClaimID = ko.observable(value.PatientClaimID);
        self.DocumentName = ko.observable(value.DocumentName);
        self.RFAFileName = ko.observable(value.RFAFileName);
        if (value.DocumentDate != null)
            self.DocumentDate = ko.observable(moment(value.DocumentDate).format("MM-DD-YYYY"));
        else
            self.DocumentDate = ko.observable();
        self.IsChecked = ko.observable(false);
    };

    //---------------Upload Patient Medical Record Spilliting..............MS
    //------------------------------------------------------------------------//
    // Claim Acc to Patient ID
    self.ClaimCategorie = ko.observable();
    self.ClaimCategories = ko.observableArray();
    self.ClaimCategories = ko.observableArray([self.ClaimCategories(0)]);
    self.selectedItemClaimCategories = ko.observable(0);

    // Request list's
    self.RequestCategories = ko.observable();
    self.RequestCategories = ko.observableArray();
    self.RequestCategories = ko.observableArray([self.RequestCategories(0)]);
    self.selectedItemRequestCategories = ko.observable(0);

    // Document Categories
    self.DocumentCategorie = ko.observable();
    self.DocumentCategories = ko.observableArray();
    self.DocumentCategories = ko.observableArray([self.DocumentCategories(0)]);
    self.selectedItemDocumentCategories = ko.observable(0);

    // DocumentTypes
    self.DocumentType = ko.observable();
    self.DocumentTypes = ko.observableArray();
    self.DocumentTypes = ko.observableArray([self.DocumentTypes(0)]);
    self.selectedItemDocumentTypes = ko.observable(0);

    self.RFARecSpltID = ko.observable();
    self.DocumentCategoryID = ko.observable();
    self.DocumentTypeID = ko.observable();
    self.RFARecDocumentName = ko.observable();
    self.RFARecDocumentDate = ko.observable();
    self.RFARecPageStart = ko.observable();
    self.RFARecPageEnd = ko.observable();
    self.PageTotal = ko.observable();
    self.PageRemaining = ko.observable();
    self.RFAReferralFileName = ko.observable();
    self.DocumentCategoryName = ko.observable();
    self.DocumentTypeDesc = ko.observable();
    self.PatientClaimNumber = ko.observable();
    self.AuthorName = ko.observable();
    self.PatientMedicalRecordSpilt = ko.observableArray([]);
    self.RFARequestID = ko.observable();
    self.AttachmentForEmailArray = ko.observableArray([]);

    var index = 0;

    self.GreaterFromThisNum = ko.observable(0);
    self.EditMode = ko.observable(0);

    $('.OpenPopMedical').click(function () {
        blockPopupBackground();

    });
    $('#OpenPopMedicalButton').click(function () {
        $.post("/PatientMedicalRecord/GetPatientClaimsByPatientIDOnDrop", {
            _patientID: $("#PatientID").val()
        }, function (dataTypeClaimsAll) {
            if (dataTypeClaimsAll != null) {
                self.ClaimCategories(dataTypeClaimsAll.slice());
            }
        });

        $.getJSON("/PatientMedicalRecord/getRFARequestRecordsByPatientClaim", {
            _patiantClaim: self.PatClaimNumber()
        }, function (Requests) {
            self.RequestCategories(Requests.slice());
        });
    });


    $('#buttonClosePatientSplitting').click(function () {
        unblockPopupBackground();
        ResetAllRecordSplit();
        $('#divMedicalSpilliting').hide();
        $('#DivMedicalSpillitingGrid').hide();

        if (self.PatientMedicalRecordSpilt() != null) {
            self.PatientMedicalRecordSpilt.removeAll();
        }
    });

    jQuery(function () {     
        $.getJSON("/Common/getDocumentCategoriesAll", function (dataCategories) {
            self.DocumentCategories(dataCategories.slice());

        });
    });

   

    function ResetAllInput() {
        $('#RFARecPageStart').attr('readonly', false);
        $('#RFARecPageEnd').attr('readonly', false);
        $('#RFARecPageStart').val('');
        $('#RFARecPageEnd').val('');
        $('#RFARecDocumentName').val('');
        $('#RFARecDocumentDate').val('');
        $('#DocumentCategoryID').val('');
        $('#RFARequestID').val('');
        $('#AuthorName').val('');
        self.DocumentTypes(null);
    }

    function ResetAllRecordSplit() {
        $('#RFARecPageStart').attr('readonly', false);
        $('#RFARecPageEnd').attr('readonly', false);
        $('#RFARecPageStart').val('');
        $('#RFARecPageEnd').val('');
        $('#RFARecDocumentName').val('');
        $('#RFARecDocumentDate').val('');
        $('#AuthorName').val('');
        $("#GreaterFromThisNum").val('');
        $("#PageRemaining").val('');
        $('#upMedicalRecordFile').each(function () {
            $(this).after($(this).clone(true)).remove();
        });

        var control = $('#upMedicalRecordFile');
        control.replaceWith(control.val('').clone(true));

        control.on({
            change: function () { console.log("Changed") },
            focus: function () { console.log("Focus") }
        });
    }

    function BindSplitGrid() {
        var _patientID = $("#PatientID").val();
        $.post("/PatientMedicalRecord/getPatientMedicalRecordSplit", {
            _patientID: _patientID
        }, function (_data) {
            var model = $.parseJSON(_data);
            if (self.PatientMecdicalRecordSpilt() != null)
                self.PatientMecdicalRecordSpilt.removeAll();
            if (model.patientMedicalRecordSplitingDetails != null) {
                $('#DivMedicalSpillitingGrid').show();
                ko.mapping.fromJS(model.patientMedicalRecordSplitingDetails, {}, self.PatientMecdicalRecordSpilt);
            }
        })
    }

    self.PatientMedicalRecordFileInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        showLoader();
        return true;
    }
    self.PatientMedicalRecordUploadSuccess = function (_data) {
        var responseText = $.parseJSON(_data);
        hideLoader();
        var _patientID = $("#PatientID").val();
        self.PatientID(_patientID);
        self.PageTotal(responseText.TotalPages);
        self.PageRemaining(responseText.TotalPages);
        self.EditMode(0);
        self.GreaterFromThisNum(0);
        self.RFAReferralFileName(responseText.RFAReferralFileName);
        alertify.success("Uploaded Sucessfully");
        $('#divMedicalSpilliting').show();
    }

    self.PatientMedicalRecordSplitSuccess = function () {
        var _allvalid = 1;
        var _checked = 1;

        var _PageTotal = parseInt($("#PageTotal").val());
        var _PageRemaining = parseInt($("#PageRemaining").val());
        var _EditMode = parseInt($('#EditMode').val());

        if (_PageRemaining == 0 && _EditMode == 0) {
            alertify.alert("Zero page remaining for split");
        }
        else {

            var _PatientID = $("#PatientID ").val();
            var _PatientClaimID = parseInt($("#PatientClaimIDtt").val());
            var _DocumentCategoryID = parseInt($("#DocumentCategoryID").val());
            var _DocumentTypeID = parseInt($("#DocumentTypeID").val());
            var _RFARecDocumentName = $("#RFARecDocumentName").val();
            var _RFARecDocumentDate = $("#RFARecDocumentDate").val();
            var _RFARecPageStart = parseInt($("#RFARecPageStart").val());
            var _RFARecPageEnd = parseInt($("#RFARecPageEnd").val());
            var _DocumentCategoryName = $("#DocumentCategoryID").find("option:selected").text();
            var _DocumentTypeDesc = $("#DocumentTypeID").find("option:selected").text();
            var _PatientClaimNumber = $("#PatientClaimIDtt").find("option:selected").text();
            var _RFAReferralFileName = $("#RFAReferralFileName").val();
            var _GreaterFromThisNum = parseInt($("#GreaterFromThisNum").val());
            var _AuthorName = $('#AuthorName').val();
            var _RFARequestID = parseInt($("#RFARequestID").val());

            if (_RFARecPageStart == '' && _checked == 1) {
                if (_EditMode == 0) {
                    _allvalid = 0;
                    _checked = 0;
                    alertify.alert("Page Start required");
                }
            }
            else if (_RFARecPageEnd == '' && _checked == 1) {
                if (_EditMode == 0) {
                    _allvalid = 0;
                    _checked = 0;
                    alertify.alert("Page End required");
                }
            }
            else if (_RFARecDocumentName == '' && _checked == 1) {
                _allvalid = 0;
                _checked = 0;
                alertify.alert("Document Name required");
            }
            else if (_RFARecDocumentDate == '' && _checked == 1) {
                _allvalid = 0;
                _checked = 0;
                alertify.alert("Document Date required");
            }
            else if (_RFARecPageStart <= _GreaterFromThisNum && _checked == 1) {
                if (_EditMode == 0) {
                    _allvalid = 0;
                    _checked = 0;
                    alertify.alert("Start Page is already split");
                    $('#RFARecPageStart').val('');
                }
            }
            else if (_RFARecPageEnd <= _GreaterFromThisNum && _checked == 1) {
                if (_EditMode == 0) {
                    _allvalid = 0;
                    _checked = 0;
                    alertify.alert("End Page is already split");
                    $('#RFARecPageEnd').val('');
                }
            }
            else if (_PageTotal < _RFARecPageStart && _checked == 1) {
                if (_EditMode == 0) {
                    _allvalid = 0;
                    _checked = 0;
                    alertify.alert("Start Page is not exists");
                    $('#RFARecPageStart').val('');
                }
            }
            else if (_PageTotal < _RFARecPageEnd && _checked == 1) {
                if (_EditMode == 0) {
                    _allvalid = 0;
                    _checked = 0;
                    alertify.alert("End Page is not exists");
                    $('#RFARecPageEnd').val('');
                }
            }
            else if (_RFARecPageEnd < _RFARecPageStart && _checked == 1) {
                if (_EditMode == 0) {
                    _allvalid = 0;
                    _checked = 0;
                    alertify.alert("Start Page is not greater than end page");
                    $('#RFARecPageStart').val('');
                }
            }


            if (_allvalid == 1) {

                //---calculation by---------------MS
                if (_EditMode == 0) {
                    var _splitPageTotal = _RFARecPageEnd - _RFARecPageStart + 1;
                    var _totalPageRemaining = _PageRemaining - _splitPageTotal;

                    $('#DivMedicalSpillitingGrid').show();
                    $("#GreaterFromThisNum").val(_RFARecPageEnd);
                    $("#PageRemaining").val(_totalPageRemaining);
                    self.PageRemaining(_totalPageRemaining);
                    self.PatientMedicalRecordSpilt.push(new PatientMedicalRecordSplitInsert(0, _PatientID, _PatientClaimID, _DocumentCategoryID, _DocumentTypeID, _RFARecDocumentName, _RFARecDocumentDate, _RFARecPageStart, _RFARecPageEnd, _DocumentCategoryName, _DocumentTypeDesc, _PatientClaimNumber, _RFAReferralFileName, _AuthorName, _RFARequestID));
                    ResetAllInput();
                    alertify.alert("Add Successfully");
                }
                else {
                    self.PatientMedicalRecordSpilt.splice(index, 1);
                    $('#RFARecPageStart').attr('readonly', false);
                    $('#RFARecPageEnd').attr('readonly', false);
                    self.EditMode(0);
                    self.PatientMedicalRecordSpilt.splice(index, 0, new PatientMedicalRecordSplitUpdate(0, _PatientID, _PatientClaimID, _DocumentCategoryID, _DocumentTypeID, _RFARecDocumentName, _RFARecDocumentDate, _RFARecPageStart, _RFARecPageEnd, _DocumentCategoryName, _DocumentTypeDesc, _PatientClaimNumber, _RFAReferralFileName, _AuthorName, _RFARequestID));
                    ResetAllInput();
                    alertify.alert("Updated Successfully");
                }
            }
        }

    }

    // validation for upload medical record for pdf format only
    $('#upMedicalRecordFile').change(function () {
        if ($("#upMedicalRecordFile").val() != "") {
            var fileExtension = ['pdf', 'PDF'];
            if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                alert("Only pdf format are allowed.");
                var control = $('#upMedicalRecordFile');
                control.replaceWith(control.val('').clone(true));

                control.on({
                    change: function () { console.log("Changed") },
                    focus: function () { console.log("Focus") }
                });
            }
        }
    })

    self.EditPatientMedicalRecordSplit = function (data) {

        index = self.PatientMedicalRecordSpilt.indexOf(data);
        self.EditMode(1);
        self.RFARecSpltID(0);
        $('#PatientID').val(data.PatientID());
        $("#PatientClaimIDtt").val(data.PatientClaimID());
        $("#RFARecDocumentName").val(data.RFARecDocumentName());
        $("#DocumentCategoryID").val(data.DocumentCategoryID());
        $("#DocumentTypeID").val(data.DocumentTypeID());
        $("#RFARecDocumentDate").val(data.RFARecDocumentDate());
        $("#RFARecPageStart").val(data.RFARecPageStart());
        $("#RFARecPageEnd").val(data.RFARecPageEnd());
        $("#RFAReferralFileName").val(data.RFAReferralFileName());
        $('#AuthorName').val(data.AuthorName());
        $('#RFARecPageStart').attr('readonly', true);
        $('#RFARecPageEnd').attr('readonly', true);
        $('#RFARequestID').val(data.RFARequestID());
    };

    self.PatientMedicalRecordGridSuccess = function (response) {
        var result = $.parseJSON(response);
        if (result == 1) {
            ResetAllRecordSplit();
            bindAdditionalDocumentGridByUploadDocument();
            $('#buttonClosePatientSplitting').click();
            alertify.success("Split Successfully");
            
        }
        else {
            alertify.alert("Error Occured");
        }
    };

    $("#DocumentCategoryID").change(function () {
        showLoader();
        if ($("#DocumentCategoryID").val() != "") {
            $.getJSON("/common/GetDocumentTypeByDocumentCategoryID", {
                _documentCategoryID: $("#DocumentCategoryID").val()
            }, function (DocumentType) {
                self.DocumentTypes(DocumentType.slice());
            });
        }
        else {
            self.DocumentTypes(null);
        }
        hideLoader();
    });

    self.PatientClaimChanged = function () {
        $.getJSON("/PatientMedicalRecord/getRFARequestRecordsByPatientClaim", {
            _patiantClaim: $('#PatientClaimIDtt :selected').text()
        }, function (Requests) {
            self.RequestCategories(Requests.slice());
        });
    };
  

    self.AddLinkForEmailAttachment = function (data) {
        var responseText = $.parseJSON(data);
        hideLoader();
               
        if (responseText == "Exceed Limit") {
            alertify.alert("Attachment Limit is exceed from 20 MB");
        } else {
            blockPopupBackground();
            $('#btnEmailMahiPopUp').click();
            SendEmailMahiPopUp(responseText);
        }
    }

    self.AddLinkForEmailFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        showLoader();
        return true;
    }
    //-------------------------------------End--------------------------------//

    //------------Email Send-----------------------------------------------------------//
    function SendEmailMahiPopUp(responseText) {
        ClearEmailpopup();
        var _popName = self.emailPopupName();
            $("#popName").val(_popName);
            $("#refflID").val(self.RFAReferralID());
            $("#SendEMailTo").val('');
            $('#SendEMailTo').multiple_emails();
            $('#SendEMailCc').val('["' + 'UR@hcrg.com' + '"]');
            $('#SendEMailCc').multiple_emails();
            $("#Sendsubject").val(self.PatientFullName() + " " + self.PatClaimNumber());
            $("#SendEmailText").val('Hello, Please see the attached letters for the above patient.');
         
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
    self.CommonMultipleEmailInfoFormBeforeSubmit = function (arr, $form, options) {

        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        if ($(".multiple_emails-error").length > 0) {
            return false;
        }
        showLoader();
        return true;
    }

    self.CommonMultipleEmailSuccess = function (responseText) {
        if (responseText != null) {
            var e = $.parseJSON(responseText);
            hideLoader();
            
                var _popName = self.emailPopupName();
                if (_popName == "Preparation") {
                    alertify.alert("Request process Successfully", function (e) {
                        if (e) {
                            showLoader();
                            window.location = '/Preparation/Index';
                        }
                    });
                    
                }
                else if (_popName == "ClinicalTriage")
                {
                    alertify.alert("Request process Successfully", function (e) {
                        if (e) {
                            showLoader();
                            window.location = '/Preparation/ClinicalTriage';
                        }
                    });
                   
                }
                else if (_popName == "ClinicalAudit") {
                    alertify.alert("Request process Successfully", function (e) {
                        if (e) {
                            showLoader();
                            window.location = '/Preparation/ClinicalAudit';
                        }
                    });                   
                }
                else if (_popName == "TimeExtensionPN") {
                    alertify.alert("Email send Successfully", function (e) {
                        if (e) {
                            showLoader();
                            window.location = '/Preparation/PreparationAction/' + self.RFAReferralID();
                        }
                    });                    
                }
                else if (_popName == "RFI") {
                    alertify.alert("Email send Successfully", function (e) {
                        if (e) {
                            showLoader();
                            window.location = '/Preparation/PreparationAction/' + self.RFAReferralID();
                        }
                    });
                }
                else if (_popName == "Notification"){
                    alertify.alert("Email send Successfully", function (e) {
                        if (e) {
                            showLoader();
                            window.location = '/Notification/NotificationAction/' + self.RFAReferralID();
                        }
                    });                    
                }
                else if (_popName == "IMR" || _popName == "IMRDecision") {
                    alertify.alert("Email send Successfully", function (e) {
                        if (e) {
                            showLoader();
                            window.location = '/IMR/Index';
                        }
                    });                    
                }            
        }
    };

    $('#closeEmailMahiPopUp').click(function () {
        unblockPopupBackground();
        ClearEmailpopup();
        self.AttachmentForEmailArray.removeAll();
    });
 
    //---------------Email Send End---------------------------------------------------//
}

function removeMultipleAttachment(index) {
    _additionalViewModel.AttachmentForEmailArray.splice(index, 1);
}
