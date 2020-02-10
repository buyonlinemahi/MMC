function SearchRequestIMRViewModel() {
    var self = this;
    self.RequestIMRRecordResults = ko.observableArray();
    self.cdTotalItemCount = ko.observable(0);

    self.PatientClaimDetails = ko.observableArray();
    self.TotalItemCount = ko.observable(0);

    self.Init = function (model) {
        $("#btnSendToIMR").hide();
        $('#txtPatietnSearch').val('');
        $('#txtPatietnSearch').focus();
    };

    $("#txtPatietnSearch").keypress(function (e) {//To Search when enter is pressed
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            self.SearchRequestByPatientClaim();
        }
    });

    self.SearchRequestByPatientClaim = function () {
        if ($('#txtPatietnSearch').val().trim() == "") {
            $('#txtPatietnSearch').focus();
            self.PatientClaimDetails.removeAll();
            self.TotalItemCount(0);
            self.RequestIMRRecordResults.removeAll();
            self.cdTotalItemCount(0);
            $('#divClaimSearch').modal('hide');
            alertify.alert("Enter any patient claim.");
        }
        else {
            GrdPatientClaimBinding();
            self.Pager().CurrentPage(1);
            self.Skip = ko.observable(0);
        }
    };
    function GrdPatientClaimBinding() {
        var skipVal = 0;
        var _searchtext = $('#txtPatietnSearch').val();
        $.post("/Intake/IntakeClaimDetailByName", {
            _searchText: _searchtext, _skip: self.Skip()
        }, function (_data) {
            self.PatientClaimDetails.removeAll();
            ko.mapping.fromJS(_data.PatientClaimDetails, mappingOptions, self.PatientClaimDetails);
            self.TotalItemCount(_data.TotalCount);
            if (_data.TotalCount > 0) {
                $('#divClaimSearch').modal('show');
                blockPopupBackground();
            }
            else {
                $('#txtPatietnSearch').focus();
                alertify.alert("Patient Claim not found.");
                
            }
        });
    }

    var mappingOptions = {
        'PatDOI': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM-DD-YYYY");
            }
        }
    }

    
    self.SelectClaimNumber = function (data) {
        $('.modal.in').modal('hide');
        $("#hdPatientClaim").val(data.PatClaimNumber());
        unblockPopupBackground();
        GrdtRequestIMRRecordsBinding(0);
        self.cdPager().cdCurrentPage(1);
        self.cdSkip = ko.observable(0);
    };
    function GrdtRequestIMRRecordsBinding(skip) {
        showLoader();
        var _searchtext = $('#hdPatientClaim').val();
        $.post("/IMR/GetRequestIMRRecordByPatientClaim", {
            _searchText: _searchtext, _skip: skip
        }, function (_data) {
                var model = $.parseJSON(_data);
                if (model.TotalCount != 0) {
                    self.RequestIMRRecordResults.removeAll();
                    ko.mapping.fromJS(model.RequestIMRRecordDetails, {}, self.RequestIMRRecordResults);
                    self.cdTotalItemCount(model.TotalCount);
                }
                else {
                    $('#txtPatietnSearch').val('');
                    self.RequestIMRRecordResults.removeAll();
                    self.cdTotalItemCount(0);
                }
        });
        hideLoader();
    }



    self.SendToIMEProcessLevel = function () {
        var RequestIDs = "";
        $('input:checkbox:checked').each(function () {
            var ref = $(this).val().split('/')
            RequestIDs += ref[0] + "#";
           
        });
        if (RequestIDs != "") {
            $.post("/IMR/SaveRequestIMRRecord", {
                _rFARequestIDs: RequestIDs
            }, function (_data) {
                if (_data != null) {
                    var message = $.parseJSON(_data);
                    alertify.alert(message, function (e) {
                        if (e) {
                            window.location = '/IMR/Index';
                            //GrdtRequestIMRRecordsBinding(0);
                            //self.cdPager().cdCurrentPage(1);
                            //self.cdSkip = ko.observable(0);
                        }
                    });
                }
            });
        }
        else {
            alertify.alert("Please select any Request(s) to proceed.");
        }
    };

    self.ClosePopup = function () {
        $('.modal.in').modal('hide');
        unblockPopupBackground();
    }

    var SelectedRefferalFlags = "";

    var cnt = 0;
     
    self.SelectRefferalGroup = function (data) {
        var groupFlags = data.RFAReferralID()
        if (SelectedRefferalFlags == "")
            SelectedRefferalFlags = groupFlags;

        if (groupFlags == SelectedRefferalFlags) {
            if (!$("#" + data.RFARequestID()).prop('checked')) {
                $("#" + data.RFARequestID()).prop('checked', false);
                cnt--;
                if (cnt == 0)
                    SelectedRefferalFlags = "";
                return true;
            }
            $("#" + data.RFARequestID()).prop("checked", true); 
            cnt++;
        }
        else {
            $('input[data-group="' + groupFlags + '"]:checked').prop('checked', false);
            alertify.alert("you can select only Request(s) for Referral i.e." + SelectedRefferalFlags);
        }
        return true;
    };
   

    self.GetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        GrdPatientClaimBinding(skip);
    };


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
        GrdtRequestIMRRecordsBinding(skip);

    }

    var cdpagingSetting = {
        cdpageSize: 20,
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
};

//////========== pager js for   claim diagnosis  grid only ========//
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
//////============= pager js for claim diagnosis ends  ============//