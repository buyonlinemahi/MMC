function IntakePatientClaimViewModel() {
    var self = this;

    self.TotalItemCount = ko.observable(0);
    self.bpTotalItemCount = ko.observable(0);
    self.cdTotalItemCount = ko.observable(0);
    self.cdPopupTotalItemCount = ko.observable(0);
    self.PatientClaimDetails = ko.observableArray();
    self.PatientClaimDiagnoseDetails = ko.observableArray();
    self.TotalItemCount();
    self.SelectedClaim = ko.observable();
    self.SelectedDOI = ko.observable();
    self.SelectedPatientName = ko.observable();
    self.ClaimID = ko.observable();
    self.PatientID = ko.observable();
    self.BodyPartDetails = ko.observableArray();
    self.BodyPartStatusDetails = ko.observableArray();
    self.BodyPartDetailsByClaim = ko.observableArray();
    //------- claim diagnosis ---------//
    self.icdSearchDataResult = ko.observableArray();
    self.ICDSearchType = ko.observableArray([{ SearchCriteria: "ICD9", SearchCriteriaName: "ICD9" }, { SearchCriteria: "ICD10", SearchCriteriaName: "ICD10" }]);
    self.icdResults = ko.observableArray();
    self.checkEnterKeyButton = ko.observable("No");
    //------- end claim diagnosis ---------//

    self.Init = function (model) {
        self.ClaimID(model.PatientClaimID);
        self.PatientID(model.PatientID);
    }

    $("#_SearchTextByName").keypress(function (e) {//To Search when enter is pressed
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            self.checkEnterKeyButton("Yes");
            self.IntakePatientSearchName();
        }
        else {
            self.checkEnterKeyButton("No");
        }
    });

    $('#btnIntakeSave').attr('disabled', 'disabled');
    $.getJSON("/Common/getPatientClaimBodyPartStatusAll", function (data) {
        self.BodyPartStatusDetails(data.slice());
    });
    $.getJSON("/Common/getPatientClaimAddOnBodyPartsAll", function (data) {
        self.BodyPartDetails(data.slice());
    });

    self.SelectClaimNumber = function (data) {
        showLoader();
        $('#divClaimSearch').modal('hide');
        self.SelectedClaim(data.PatClaimNumber());
        $('#hndClaimNo').val(data.PatClaimNumber());        
        self.SelectedPatientName(data.PatientName());
        self.SelectedDOI(data.PatDOI);
        self.ClaimID(data.PatientClaimID());
        self.PatientID(data.PatientID());
        ClaimDiagnoses(self.ClaimID());
        GetAllBodyPartByClaimID(self.ClaimID());
        $("#btnAddClaimDiagnosis").removeClass("disabled");
        $("#btnAddNewBodyParts").removeClass("disabled");
        $('#btnIntakeSave').removeAttr('disabled');
        UdateReferralIntakePatientClaimByID(self.ClaimID());
        $('#_SearchTextByName').val("");
        unblockPopupBackground();
        hideLoader();

    }

    $("#tabIntakeClaim").click(function () {
        showLoader();
        $.ajax({
            url: '/Intake/getPatientDetailsByReferralID',
            cache: false,
            type: "GET",
            datatype: "Json",
            data: { '_rfaReferralID': $("#RFAReferralID").val() },
            success: function (data) {
                if (data.PatientClaimID != 0) {
                    self.SelectedClaim(data.PatClaimNumber);
                    $('#hndClaimNo').val(data.PatClaimNumber);
                    self.SelectedPatientName(data.PatientName);
                    self.SelectedDOI(moment(data.PatDOI).format("MM-DD-YYYY"));
                    self.ClaimID(data.PatientClaimID);
                    self.PatientID(data.PatientID);
                    ClaimDiagnoses(self.ClaimID());
                    GetAllBodyPartByClaimID(self.ClaimID());
                    $("#btnAddClaimDiagnosis").removeClass("disabled");
                    $("#btnAddNewBodyParts").removeClass("disabled");
                }
                if (window.location.pathname.split('/').length == 5)
                    $('.diableInput').attr('disabled', 'disabled');
                else
                    $('#btnIntakeSave').removeAttr('disabled');
                $("#tabIntakeClaim").off("click");
                hideLoader();
            },
            error: function (reponse) {
                alert("error : " + reponse);
                hideLoader();
            }
        });
    });

    function UdateReferralIntakePatientClaimByID(ClaimID)
    {
        $.post("/Intake/UdateReferralIntakePatientClaimByID", {
            _patientClaimID: ClaimID, patientID:self.PatientID(), _rfaReferralId: $("#RFAReferralID").val()
        }, function (_data) {
            alertify.success("Claim assinged Successfully");
            $("#divuploadFile").show();
            showTabsByProcessLevel(30);
        });
    }

    $("#IntakeClaimClose").click(function () {
        unblockPopupBackground();
        $('#_SearchTextByName').val("");
        $("#divuploadFile").show();
    });

    self.AddNewBodyPart = function (e) {        
        var _bodyPartId = $("#BodyPart").val();
        var _bodyPartStatusId = $("#BodyPartStatus").val();
        if (_bodyPartId != "" && _bodyPartStatusId != "") {
            var ClaimID = self.ClaimID();
            var viewModelAddOnBodyPartDetails = {
                AddOnBodyPartID: _bodyPartId,
                BodyPartStatusID: _bodyPartStatusId,
                PatAddOnBodyPartName: '',
                PatBodyPartStatusName: '',
                AcceptedDate: $("#AddBodyPartsAcceptedDate").val(),
                AddOnBodyPartCondition: $("#AddOnBodyPartCondition").val(),
                PatientClaimAddOnBodyPartID: 0,
                PatientClaimID: self.ClaimID()
            }
            var plainJs = ko.toJS(viewModelAddOnBodyPartDetails);
            $.ajax({
                url: "/Patient/SavePatientClaimAddOnBodyPart",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify(plainJs),
                success: function (data) {
                    GetAllBodyPartByClaimID(self.ClaimID());
                    var pValue = parseInt((self.bpTotalItemCount() + 1) / 10);
                    var pRemainder = (self.bpTotalItemCount() + 1) % 10;
                    if (pRemainder > 0)
                        self.bpPager().bpCurrentPage(pValue + 1);
                    $("#divuploadFile").hide();
                    alertify.alert("Save Successfully", function () {
                        $("#divuploadFile").show();
                    });
                }
            });
            $("#AddNewBodyPartsClose").click();
        }
        else {
            $("#divuploadFile").hide();
            alertify.alert("Please select add new body part options");
        }
        
    }

    function ClaimDiagnoses(ClaimID)
    {
        $.post("/Intake/getClaimDiagnosis", {
            _claimID: ClaimID, _skip: self.cdSkip()
        }, function (_data) {
            ko.mapping.fromJS(_data.PatientClaimDiagnoseDetails, {}, self.PatientClaimDiagnoseDetails);
            self.cdTotalItemCount(_data.TotalCount);
            resetClaimID(ClaimID);
        });
    }
    function GetAllBodyPartByClaimID(ClaimID) {
        $.post("/Intake/getAllBodyPartsByClaimID", {
            _claimID: ClaimID, _skip: self.bpSkip()
        }, function (_data) {
            ko.mapping.fromJS(_data.BodyPartDetails, {}, self.BodyPartDetailsByClaim);
            self.bpTotalItemCount(_data.TotalCount);
            resetClaimID(ClaimID);
        });
    }

    function resetClaimID(ClaimID) {
        self.ClaimID(ClaimID);
    }
    self.IntakePatientSearchName = function () {
        if ($('#_SearchTextByName').val().trim() == "") {
            $('#_SearchTextByName').focus();
            $("#divuploadFile").hide();
            alertify.alert("Enter any Claim.", function () {
                $("#divuploadFile").show();
            });
        }
        else {
            GrdBinding();
            self.Pager().CurrentPage(1);
            //
        }
    };

    //---------------- Add Diagnosis popup methods ----------------//
    self.OpenAddNewBodyPartsPopUp = function () {
        $("#divuploadFile").hide();
        blockPopupBackground();        
    };
    //---------------- End Add Diagnosis popup methods ----------------//

    //---------------- Add Diagnosis popup methods ----------------//
    self.OpenClaimDiagnosisPopUp = function () {
        $("#divuploadFile").hide();
        self.cdPopupTotalItemCount(0);
        self.cdPopupSkip = ko.observable(0);
        self.icdSearchDataResult.removeAll();
        blockPopupBackground();        
    };
    var mappingOptions = {
        'PatDOI': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM-DD-YYYY");
            }
        }
    }

    $("#AddNewBodyPartsClose").click(function () {
        $("#divuploadFile").show();
        $('#AddOnBodyPartCondition').val('');
        $('#AddBodyPartsAcceptedDate').val('');
        $('#BodyPart').val('');
        $('#BodyPartStatus').val('');
        unblockPopupBackground();
    });


    $("#buttonCloseCD").click(function () {
        $("#divuploadFile").show();
        unblockPopupBackground();
        self.icdSearchDataResult.removeAll();
        self.cdPopupTotalItemCount(0);
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

    function GetDiagnosisSearchByName(searchText) {
        $.post("/Common/getPatientClaimDiagnosesAll/", {
            _searchName: searchText, _icdTab: $("#ICDDrp").val(), _skip: 0
        }, function (_data) {
            if (self.icdSearchDataResult() != null)
                self.icdSearchDataResult.removeAll();
            ko.mapping.fromJS(_data.ICDCodeDetails, {}, self.icdSearchDataResult);
            ko.mapping.fromJS(_data.TotalCount, {}, self.cdPopupTotalItemCount);
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
    
    self.AddNewClaimDiagonsis = function (claimdiag) {
        showLoader();        
        var viewModelClaimDiagonsisDetails = {
            PatientClaimDiagnosisID: 0,
            PatientClaimID: $('#claimIDHID').val(),
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
                hideLoader();
                unblockPopupBackground();
                self.cdPopupPager().cdPopupCurrentPage(1);                
                ClaimDiagnoses($('#claimIDHID').val());
                $("#buttonCloseCD").click();
            },
            error: function (data) {
                hideLoader();
                alert("Error Occur");

            }
        });        
    };
    //---------------- End of Add Diagnosis popup methods ----------------//

    // ko.mapping.fromJS(model.PatientClaimDetails, mappingOptions, self.PatientClaimDetails);
    function GrdBinding() {
        $("#divuploadFile").hide();
        var skipVal=0;
        var _searchtext = $('#_SearchTextByName').val();
        $.post("/Intake/IntakeClaimDetailByName", {
            _searchText: _searchtext, _skip: self.Skip()
        }, function (_data) {
                self.PatientClaimDetails.removeAll();
                ko.mapping.fromJS(_data.PatientClaimDetails, mappingOptions, self.PatientClaimDetails);
                self.TotalItemCount(_data.TotalCount);
                blockPopupBackground();
               if(self.checkEnterKeyButton()=="Yes")
                     $('#divClaimSearch').modal('show');
        });
    }

    self.GetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSetting.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        GrdBinding();
    }
    var pagingSetting = {
        pageSize: 10,
        pageSlide: 2
    };

    self.Skip = ko.observable(0);
    self.Take = ko.observable(pagingSetting.pageSize);
    self.Pager = ko.pager(self.TotalItemCount);

    self.Pager().PageSize(pagingSetting.pageSize);
    self.Pager().PageSlide(pagingSetting.pageSlide);
    self.Pager().CurrentPage(1);

    self.Pager().CurrentPage.subscribe(function () {
        var skip = pagingSetting.pageSize * (self.Pager().CurrentPage() - 1);
        var take = pagingSetting.pageSlide;
        self.GetRecordsWithSkipTake(skip, take);
    });


    //**-----------------------------Paging Pages------------------------------------**//

    //========= paging code for claim diagnosis grid only ===========//

    self.cdGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.cdSkip(0);
            self.cdTake(cdpagingSetting.cdpageSize);
        }
        else {
            self.cdSkip(skip);
            self.cdTake(take);
        }
        $.post("/Intake/getClaimDiagnosis", {
            _claimID: $('#claimIDHID').val(), _skip: $('#hidskipcd').val()
        }, function (model) {
            if (self.PatientClaimDiagnoseDetails()!=null)
                self.PatientClaimDiagnoseDetails.removeAll();
            ko.mapping.fromJS(model.PatientClaimDiagnoseDetails, {}, self.PatientClaimDiagnoseDetails);
            self.cdTotalItemCount(model.TotalCount);
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

    self.cdLastPage = ko.computed(function () {
        return Math.floor((self.cdTotalItemCount() - 1) / cdpagingSetting.cdpageSize) + 1;
    });

    // ============= paging code for claim diagnosis ends =============== //
    //========= paging code for accepted Body Part grid only ===========//
    self.bpGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.bpSkip(0);
            self.bpTake(bppagingSetting.bppageSize);
        }
        else {
            self.bpSkip(skip);
            self.bpTake(take);
        }
        $.post("/Intake/getAllBodyPartsByClaimID", {
            _claimID: $('#claimIDHID').val(), _skip: $('#hidskipbp').val()
        }, function (_data) {
            if (self.BodyPartDetailsByClaim() != null)
                self.BodyPartDetailsByClaim.removeAll();
            ko.mapping.fromJS(_data.BodyPartDetails, {}, self.BodyPartDetailsByClaim);
            self.bpTotalItemCount(_data.TotalCount);
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
    // ============= paging code for accepted Body Part ends =============== //

    //========= paging code for claim diagnosis PopUp grid only ===========//

    self.cdPopupGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.cdPopupSkip(0);
            self.cdPopupTake(cdPopuppagingSetting.cdPopuppageSize);
        }
        else {
            self.cdPopupSkip(skip);
            self.cdPopupTake(take);
        }

        var searchText = $("#SearchDiagnosisTitle").val();
        if (searchText == "") {
            alertify.alert("Diagnosis code required");
        }
        else {

            $.post("/Common/getPatientClaimDiagnosesAll/", {
                _searchName: searchText, _icdTab: $("#ICDDrp").val(), _skip: skip
            }, function (model) {

                self.icdSearchDataResult.removeAll();
                ko.mapping.fromJS(model.ICDCodeDetails, {}, self.icdSearchDataResult);
                ko.mapping.fromJS(model.TotalCount, {}, self.cdPopupTotalItemCount);

            });
        }

    }

    var cdPopuppagingSetting = {
        cdPopuppageSize: 10,
        cdPopuppageSlide: 2
    };
    self.cdPopupSkip = ko.observable(0);
    self.cdPopupTake = ko.observable(cdPopuppagingSetting.cdPopuppageSize);
    self.cdPopupPager = ko.cdPopuppager(self.cdPopupTotalItemCount);

    self.cdPopupPager().cdPopupPageSize(cdPopuppagingSetting.cdPopuppageSize);
    self.cdPopupPager().cdPopupPageSlide(cdPopuppagingSetting.cdPopuppageSlide);
    self.cdPopupPager().cdPopupCurrentPage(1);

    self.cdPopupPager().cdPopupCurrentPage.subscribe(function () {
        var skip = cdPopuppagingSetting.cdPopuppageSize * (self.cdPopupPager().cdPopupCurrentPage() - 1);
        var take = cdPopuppagingSetting.cdPopuppageSlide;
        self.cdPopupGetRecordsWithSkipTake(skip, take);
    });

    self.cdPopupLastPage = ko.computed(function () {
        return Math.floor((self.cdPopupTotalItemCount() - 1) / cdPopuppagingSetting.cdPopuppageSize) + 1;
    });

    // ============= paging code for claim diagnosis PopUp ends =============== //   
    // ============= paging code for Unknown body parts ends =============== //
}

//========== pager js for   claim diagnosis  grid only ========//
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
//============= pager js for claim diagnosis ends  ============//

//========== pager js for claim diagnosis popup grid only ========//
(function (ko) {
    var cdPopupnumericObservable = function (initialValue) {
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

    function cdPopupPager(totalItemCount) {
        var self = this;
        self.cdPopupCurrentPage = cdPopupnumericObservable(1);

        self.cdPopupTotalItemCount = ko.computed(totalItemCount);

        self.cdPopupPageSize = cdPopupnumericObservable(10);
        self.cdPopupPageSlide = cdPopupnumericObservable(2);

        self.cdPopupLastPage = ko.computed(function () {
            return Math.floor((self.cdPopupTotalItemCount() - 1) / self.cdPopupPageSize()) + 1;
        });

        self.cdPopupHasNextPage = ko.computed(function () {
            return self.cdPopupCurrentPage() < self.cdPopupLastPage();
        });

        self.cdPopupHasPrevPage = ko.computed(function () {
            return self.cdPopupCurrentPage() > 1;
        });

        self.cdPopupFirstItemIndex = ko.computed(function () {
            return self.cdPopupPageSize() * (self.cdPopupCurrentPage() - 1) + 1;
        });

        self.cdPopupLastItemIndex = ko.computed(function () {
            return Math.min(self.cdPopupFirstItemIndex() + self.cdPopupPageSize() - 1, self.cdPopupTotalItemCount());
        });

        self.cdPopupPages = ko.computed(function () {
            var cdPopuppageCount = self.cdPopupLastPage();
            var cdPopuppageFrom = Math.max(1, self.cdPopupCurrentPage() - self.cdPopupPageSlide());
            var cdPopuppageTo = Math.min(cdPopuppageCount, self.cdPopupCurrentPage() + self.cdPopupPageSlide());
            cdPopuppageFrom = Math.max(1, Math.min(cdPopuppageTo - 2 * self.cdPopupPageSlide(), cdPopuppageFrom));
            cdPopuppageTo = Math.min(cdPopuppageCount, Math.max(cdPopuppageFrom + 2 * self.cdPopupPageSlide(), cdPopuppageTo));

            var result = [];
            for (var i = cdPopuppageFrom; i <= cdPopuppageTo; i++) {
                result.push(i);
            }
            return result;
        });
    }

    ko.cdPopuppager = function (totalItemCount) {
        var _cdPopuppager = new cdPopupPager(totalItemCount);
        return ko.observable(_cdPopuppager);
    };
}(ko));
//============= pager js for claim diagnosis ends  ============//

//========== pager js for  Accepted Body Part  grid only ========//
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
//============= pager js for Accepted Body Part ends  ============//


