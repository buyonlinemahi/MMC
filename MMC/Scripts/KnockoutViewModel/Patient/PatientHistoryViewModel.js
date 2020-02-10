function PatientHistoryViewModel() {
    var self = this;
    self.phSkip = ko.observable(0);
    self.phTotalItemCount = ko.observable(0);
    self.PatientHistoryDetails = ko.observableArray();
    self.RequestHistoryDetails = ko.observableArray();
    self.reqSkip = ko.observable(0);
    self.reqTotalItemCount = ko.observable(0);
    self.RequestId = ko.observable();
    self.NotificationProcessLevelCheck = ko.observable(0);
    self.CliniclAuditProcessLevelCheck = ko.observable(0);
    
    var selectedSortBy = "Request";
    var CurrentSortBy;
    var CurrentSortOrder="DESC";

    var mappingOptions = {
        'DecisionDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM-DD-YYYY");
            }
        },
        'RFARequestDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM-DD-YYYY");
            }
        },
    }

    var mappingOptionsReq = {
        'UploadDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM-DD-YYYY");
            }
        }
    }
    bindPatientHistory();
    var phpagingSetting = {
        phpageSize: 10,
        phpageSlide: 2
    };
    self.phSkip = ko.observable(0);
    self.phTake = ko.observable(phpagingSetting.phpageSize);
    self.phPager = ko.phpager(self.phTotalItemCount);
    self.phPager().phPageSize(phpagingSetting.phpageSize);
    self.phPager().phPageSlide(phpagingSetting.phpageSlide);
    self.phPager().phCurrentPage(1);
    self.phPager().phCurrentPage.subscribe(function () {
        var skip = phpagingSetting.phpageSize * (self.phPager().phCurrentPage() - 1);
        var take = phpagingSetting.phpageSlide;
        self.phGetRecophsWithSkipTake(skip, take);
    });
    self.phGetRecophsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.phSkip(0);
            self.phTake(phpagingSetting.phpageSize);
        }
        else {
            self.phSkip(skip);
            self.phTake(take);
        }
        bindPatientHistory();

    }


    self.SortByHeader = function (_SelectedSortBy) {
        showLoader();
        selectedSortBy = _SelectedSortBy;
        CurrentSortOrder = (CurrentSortBy == selectedSortBy && CurrentSortOrder == 'ASC' ? 'DESC' : 'ASC');
        if (self.phPager().phCurrentPage() != 1) {
            self.phPager().phCurrentPage(1);
        }
        else {
            bindPatientHistory();
        }
        CurrentSortBy = selectedSortBy;
        if (CurrentSortOrder == 'DESC') {
            $(".orderIco").removeClass("glyphicon-chevron-up");
            $(".orderIco").removeClass("glyphicon-chevron-down");
            $("#spanOrderIco" + CurrentSortBy).removeClass("glyphicon-chevron-up");
            $("#spanOrderIco" + CurrentSortBy).addClass("glyphicon-chevron-down");
        }
        else {
            $(".orderIco").removeClass("glyphicon-chevron-up");
            $(".orderIco").removeClass("glyphicon-chevron-down");
            $("#spanOrderIco" + CurrentSortBy).removeClass("glyphicon-chevron-down");
            $("#spanOrderIco" + CurrentSortBy).addClass("glyphicon-chevron-up");
        }
        hideLoader();
    }

    function bindPatientHistory() {
        $.post("/Intake/GetPatientHistoryByPatientID", {
            _patientID: $("#PatientID").val(), _skip: self.phSkip(), _sortBy: selectedSortBy, _order: (CurrentSortOrder == null) ? null : CurrentSortOrder
        }, function (_data) {
            var model = $.parseJSON(_data);
            //self.PatientClaimSearchResults.removeAll();
            ko.mapping.fromJS(model.PatientHistoryDetails, mappingOptions, self.PatientHistoryDetails);
            self.phTotalItemCount(model.TotalCount);
        });
    }

  

    //    --------------
    //var reqpagingSetting = {
    //    reqpageSize: 10,
    //    reqpageSlide: 2
    //};
    //self.reqSkip = ko.observable(0);
    //self.reqTake = ko.observable(reqpagingSetting.reqpageSize);
    //self.reqPager = ko.reqpager(self.reqTotalItemCount);
    //self.reqPager().reqPageSize(reqpagingSetting.reqpageSize);
    //self.reqPager().reqPageSlide(reqpagingSetting.reqpageSlide);
    //self.reqPager().reqCurrentPage(1);
    //self.reqPager().reqCurrentPage.subscribe(function () {
    //    var skip = reqpagingSetting.reqpageSize * (self.reqPager().reqCurrentPage() - 1);
    //    var take = reqpagingSetting.reqpageSlide;
    //    self.reqGetRecoreqsWithSkipTake(skip, take);
    //});
    //self.reqGetRecoreqsWithSkipTake = function (skip, take) {
    //    if (skip == undefined || take == undefined) {
    //        self.reqSkip(0);
    //        self.reqTake(reqpagingSetting.reqpageSize);
    //    }
    //    else {
    //        self.reqSkip(skip);
    //        self.reqTake(take);
    //    }
    //    bindRequestHistory();

    //}

    self.reqCurrentPage = ko.observable(0);
    self.maxId = ko.observable(0);
    self.IsTrue = ko.observable(false);

    function bindRequestHistory() {
        $.post("/Intake/GetRequestHistoryByRequestID", {
            _requestID: self.RequestId(), _skip: self.maxId()
        }, function (_data) {
            var model = $.parseJSON(_data);
            if (model.TotalCount > 0) {
                // self.RequestHistoryDetails.removeAll();
                if (!self.IsTrue()) {
                    showLoader();

                    $(".hide_thead").show();
                    $(".hide_tbody").show();
                    $(".hide_btn").show();
                    self.NotificationProcessLevelCheck(model.NotificationProcessLevelCheck);
                    self.CliniclAuditProcessLevelCheck(model.CliniclAuditProcessLevelCheck);
                    if (self.NotificationProcessLevelCheck() > 0) {
                        $('.urhistoryPerparationbtn').attr('disabled', 'disabled');
                        $('.urhistoryPerparationbtn').addClass("btn-info");
                        $('.urhistoryIntakebtn').attr('disabled', 'disabled');
                        $('.urhistoryIntakebtn').addClass("btn-info");
                    }
                    else if (self.CliniclAuditProcessLevelCheck() > 0) {
                        $('.urhistoryIntakebtn').attr('disabled', 'disabled');
                        $('.urhistoryIntakebtn').addClass("btn-info");
                    }
                    else {
                        $('.urhistoryPerparationbtn').removeAttr('disabled');
                        $('.urhistoryPerparationbtn').removeClass("btn-info");

                        $('.urhistoryIntakebtn').removeAttr('disabled');
                        $('.urhistoryIntakebtn').removeClass("btn-info");
                    }
                    $.each(model.RequestHistoryDetails, function (index, value) {
                        self.RequestHistoryDetails.push(new RequestDetails(value));
                    });
                    self.reqTotalItemCount(model.TotalCount);
                    self.reqCurrentPage(self.maxId());
                    self.maxId(self.maxId() + 10);
                    hideLoader();
                }
              
            }
            else {
                //self.RequestHistoryDetails() = null;
                //self.SelectAll(false);
                $(".hide_thead").show();
                $(".hide_tbody").hide();
                $(".hide_btn").hide();
                $("#selectItemAll").prop('checked', false);
            }

            
            

            blockPopupBackground();
            $('#RequestHistory').modal('show');
        });
    }


    self.patientURHistoryscrolled = function (data, event) {
        if (!self.IsTrue()) {
            if (self.reqTotalItemCount() > self.maxId()) {
                var elem = event.target;
                if (elem.scrollTop > (elem.scrollHeight - elem.offsetHeight - 1)) {
                    bindRequestHistory();
                }
            }
            else {
                self.IsTrue(true);
                
            }
        }
    };

    self.RFAReferralID = ko.observable(0);
    self.RFARequestID = ko.observable(0);

    self.IsChecked = ko.observable(false);
    function RequestDetails(value) {
        var self = this;
        self.RFAReferralID = ko.observable(value.RFAReferralID);
        self.RFARequestID = ko.observable(value.RFARequestID);
        self.FileFullPath = ko.observable(value.FileFullPath);
        self.FileTypeId = ko.observable(value.FileTypeId);
        self.filename = ko.observable(value.filename);
        self.UserName = ko.observable(value.UserName);
        self.UploadDate = ko.observable(moment(value.UploadDate).format("MM/DD/YYYY"));
        self.IsChecked = ko.observable(value.IsChecked);
    };

    self.OpenRequestHistory = function (config) {
        
        self.maxId(0);
        self.IsTrue(false);
        if (self.RequestHistoryDetails() != null) {
            self.RequestHistoryDetails.removeAll();
            self.reqTotalItemCount(0);
        }
        self.RequestId(config.RFARequestID());
        bindRequestHistory();

        $("#RFAReferralID").val(config.RFAReferralID());
        $("#RequestID").val(config.RFARequestID())

        //Preparation, Clinical Triage, Peer Review, Clinical Audit, Notifications

        //if ((config.Status() == "Preparation") || (config.Status() == "Clinical Triage") || (config.Status() == "Peer Review") || (config.Status() == "Clinical Audit") || (config.Status() == "Notifications")) {
        //    $('.urhistorybtn').removeAttr('disabled');
        //    $('.urhistorybtn').removeClass("btn-info");
        //}
        //else {
        //    $('.urhistorybtn').attr('disabled', 'disabled');
        //    $('.urhistorybtn').addClass("btn-info"); 
        //}


       


    }
    //---------------------Email popup---------------------------------------------------------------------------//
    self.AttachmentForEmailArray = ko.observableArray();
    self.OpenEmailMultiplePopUp = function (config) {

        //---- Only in case of Email Pop Up  emailRecordId act as RFARequestID-----------------//
        $.post("/Common/GetEmailRecordAttachmentByEmailRecord", {
            emailRecordId: config.RFARequestID()
        }, function (responseText) {
            $("#btnEmailMahiPopUp").click();
            $("#SendEMailTo").val(responseText.EmailRecordAttachmentDetails[0].EmRecTo);
            $('#SendEMailTo').multiple_emails();
            $('#SendEMailCc').val(responseText.EmailRecordAttachmentDetails[0].EmRecCC);
            $('#SendEMailCc').multiple_emails();
            $("#Sendsubject").val(responseText.EmailRecordAttachmentDetails[0].EmRecSubject);
            $("#SendEmailText").val(responseText.EmailRecordAttachmentDetails[0].EmRecBody);
            for (var i = 0; i < responseText.EmailRecordAttachmentDetails.length; i++) {
                self.AttachmentForEmailArray.push(new AttachementDetails(responseText.EmailRecordAttachmentDetails[i]));
            }
        });
    }

    function AttachementDetails(value) {
        var self = this;       
        if (value.DocumentName.indexOf("_") != -1) {
            var shortText = value.DocumentName.split('_');
            var shortString = shortText[1].substring(0, 15);
            self.AttachmentShortName = ko.observable(shortText[0] + "_" + shortString);
        }
        else {
            self.AttachmentShortName = ko.observable(value.DocumentName);
        }

        var _docPathAndFullPath = value.DocumentPath.split(',');

        self.AttachmentLink = ko.observable(_docPathAndFullPath[0]);
        self.AttachmentName = ko.observable(value.DocumentName);
        self.AttachmentDownload = ko.observable(_docPathAndFullPath[1]);
    };
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
            alertify.alert(e);
            $('#closeEmailMahiPopUp').click();
        }
    };

    $('#closeEmailMahiPopUp').click(function () {       
        ClearEmailpopup();
        self.AttachmentForEmailArray.removeAll();
    });

    function ClearEmailpopup() {
        $(".multiple_emails-container").remove();
        $('#SendEMailTo').val('[]');
        $('#SendEMailCc').val('[]');
        $('#SendEmailText').val('');
        $('#Sendsubject').val('');
    }
    //------------------------------------End Email popup-----------------------------------------------------------//
    self.OpenIntakePage = function () {
        var url = '/Intake/UpdateIntake/' + $("#RFAReferralID").val() + '/p';
        window.open(url, '_blank');
        return false;
    };

    self.OpenPreparationPage = function () {
        var url = '/Preparation/PreparationAction/' + $("#RFAReferralID").val() + '/' + $("#RequestID").val() + '/p';
        window.open(url, '_blank');
        return false;
    }; 

    self.URHistoryDetailRFARequestID = function () {
        var IsChecked = 0;
        $('.selectItem').each(function () {
            if ($('.selectItem:checked').length == 0) {
                IsChecked = 0;
            }
            else
                IsChecked = 2;
        });

        if (IsChecked == 0) {
            alertify.alert("Please select atleast one document to print.");
            return false;
        }
        else {
            return true;
        }
    };



    self.SelectAll = ko.computed({
        read: function () {
            var item = ko.utils.arrayFirst(self.RequestHistoryDetails(), function (item) {
                return !item.IsChecked();
            });
            return item == null;
        },
        write: function (value) {
            ko.utils.arrayForEach(self.RequestHistoryDetails(), function (RequestDetails) {
                RequestDetails.IsChecked(value);
            });
        }
    });

    chekBX = function (name) {
        self.IsChecked = ko.observable(false);
    }

    //----------------For  email Attachment---------------------------//

    //---------------------------------------------------------------//
}

function removeMultipleAttachment(index) {
    _patientHistoryViewModel.AttachmentForEmailArray.splice(index, 1);
}

//========== pager js for   Patient History  grid only ========//
(function (ko) {
    var phnumericObservable = function (initialValue) {
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
    function phPager(totalItemCount) {
        var self = this;
        self.phCurrentPage = phnumericObservable(1);
        self.phTotalItemCount = ko.computed(totalItemCount);
        self.phPageSize = phnumericObservable(10);
        self.phPageSlide = phnumericObservable(2);
        self.phLastPage = ko.computed(function () {
            return Math.floor((self.phTotalItemCount() - 1) / self.phPageSize()) + 1;
        });
        self.phHasNextPage = ko.computed(function () {
            return self.phCurrentPage() < self.phLastPage();
        });
        self.phHasPrevPage = ko.computed(function () {
            return self.phCurrentPage() > 1;
        });
        self.phFirstItemIndex = ko.computed(function () {
            return self.phPageSize() * (self.phCurrentPage() - 1) + 1;
        });
        self.phLastItemIndex = ko.computed(function () {
            return Math.min(self.phFirstItemIndex() + self.phPageSize() - 1, self.phTotalItemCount());
        });
        self.phPages = ko.computed(function () {
            var phpageCount = self.phLastPage();
            var phpageFrom = Math.max(1, self.phCurrentPage() - self.phPageSlide());
            var phpageTo = Math.min(phpageCount, self.phCurrentPage() + self.phPageSlide());
            phpageFrom = Math.max(1, Math.min(phpageTo - 2 * self.phPageSlide(), phpageFrom));
            phpageTo = Math.min(phpageCount, Math.max(phpageFrom + 2 * self.phPageSlide(), phpageTo));
            var result = [];
            for (var i = phpageFrom; i <= phpageTo; i++) {
                result.push(i);
            }
            return result;
        });
    }
    ko.phpager = function (totalItemCount) {
        var phpager = new phPager(totalItemCount);
        return ko.observable(phpager);
    };
}(ko));
//============= pager js for Iph ends  ============//

//========== pager js for   Request History  grid only ========//
//(function (ko) {
//    var reqnumericObservable = function (initialValue) {
//        var _actual = ko.observable(initialValue);
//        var result = ko.dependentObservable({
//            read: function () {
//                return _actual();
//            },
//            write: function (newValue) {
//                var parsedValue = parseFloat(newValue);
//                _actual(isNaN(parsedValue) ? newValue : parsedValue);
//            }
//        });
//        return result;
//    };
//    function reqPager(totalItemCount) {
//        var self = this;
//        self.reqCurrentPage = reqnumericObservable(1);
//        self.reqTotalItemCount = ko.computed(totalItemCount);
//        self.reqPageSize = reqnumericObservable(10);
//        self.reqPageSlide = reqnumericObservable(2);
//        self.reqLastPage = ko.computed(function () {
//            return Math.floor((self.reqTotalItemCount() - 1) / self.reqPageSize()) + 1;
//        });
//        self.reqHasNextPage = ko.computed(function () {
//            return self.reqCurrentPage() < self.reqLastPage();
//        });
//        self.reqHasPrevPage = ko.computed(function () {
//            return self.reqCurrentPage() > 1;
//        });
//        self.reqFirstItemIndex = ko.computed(function () {
//            return self.reqPageSize() * (self.reqCurrentPage() - 1) + 1;
//        });
//        self.reqLastItemIndex = ko.computed(function () {
//            return Math.min(self.reqFirstItemIndex() + self.reqPageSize() - 1, self.reqTotalItemCount());
//        });
//        self.reqPages = ko.computed(function () {
//            var reqpageCount = self.reqLastPage();
//            var reqpageFrom = Math.max(1, self.reqCurrentPage() - self.reqPageSlide());
//            var reqpageTo = Math.min(reqpageCount, self.reqCurrentPage() + self.reqPageSlide());
//            reqpageFrom = Math.max(1, Math.min(reqpageTo - 2 * self.reqPageSlide(), reqpageFrom));
//            reqpageTo = Math.min(reqpageCount, Math.max(reqpageFrom + 2 * self.reqPageSlide(), reqpageTo));
//            var result = [];
//            for (var i = reqpageFrom; i <= reqpageTo; i++) {
//                result.push(i);
//            }
//            return result;
//        });
//    }
//    ko.reqpager = function (totalItemCount) {
//        var reqpager = new reqPager(totalItemCount);
//        return ko.observable(reqpager);
//    };
//}(ko));
//============= pager js for Ireq ends  ============//

function openFileUpload(index)
{    
   $("#fileUploadURReqHistory" + index).click();
}
function fileUploadChange(index) {    
    alertify.confirm("Do you wanna replace the previous upload?", function (ans) {
        if (ans)
        {
            var formdata = new FormData(); //FormData object
            var fileInput = document.getElementById('fileUploadURReqHistory' + index);
            if (fileInput.files.length > 0) {
                //Iterating through each files selected in fileInput
                for (i = 0; i < fileInput.files.length; i++) {
                    //Appending each file to FormData object
                    formdata.append(fileInput.files[i].name, fileInput.files[i]);
                }                
                $.ajax({
                    type: "POST",
                    url: '/Intake/ReplaceRequestHistoryFile?filepath=' + $("#hdnFileFullPath" + index).val(),
                    contentType: false,
                    processData: false,
                    data: formdata,
                    success: function (result) {
                        var path = $("#lnkFile" + index).attr('href');                       
                        $("#lnkFile" + index).attr('href', path.substr(0, path.indexOf('=') + 1) + Math.random());                                                
                        alertify.success(result);
                    },
                    error: function (xhr, status, p3, p4) {
                        var err = "Error " + " " + status + " " + p3 + " " + p4;
                        alertify.alert(err);
                    }
                });
            }
        }   
    });
};