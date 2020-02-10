function SearchViewModel() {
    var self = this;
    self.TotalItemCount = ko.observable(0);
    self.SearchTextHidden = ko.observable();
    self.CompanySearchResults = ko.observableArray();

    self.CompanySearchName = function () {
        var _searchtext = $('#_SearchTextByName').val();
        if (_searchtext.trim() != "") {
            showLoader();
            self.SearchTextHidden(_searchtext);
            $.post("/ManagedCareCompany/GetManagedCareCompanyByName", {
                _searchText: _searchtext, _skip: 0
            }, function (_data) {
                var model = $.parseJSON(_data);
                if (model != null) {
                    self.CompanySearchResults.removeAll();
                    ko.mapping.fromJS(model.ManagedCareCompanyDetails, {}, self.CompanySearchResults);
                    self.TotalItemCount(model.TotalCount);

                }
            });
            hideLoader();
        }
        else {
            alertify.alert("Please Enter Company");
        }
        self.Pager().CurrentPage(1);
    };

    $("#_SearchTextByName").keypress(function (e) {//To Search when enter is pressed
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            self.CompanySearchName();
        }
    });

    self.DeletedManagedCareCompanyByID = function (model) {
        var _companyByID = model.CompanyId();
        alertify.confirm("Are you sure you want to delete this record?", function (e) {
            if (e) {
                showLoader();
                $.post("/ManagedCareCompany/DeleteManagedCareCompanyByID", {
                    _companyID: _companyByID
                }, function (_data) {
                    if (_data == 1) {
                        var _searchtext = $('#SearchTextHidden').val();
                        if (_searchtext != "") {
                            $.post("/ManagedCareCompany/GetManagedCareCompanyByName", {
                                _searchText: _searchtext, _skip: $('#hidskip').val()
                            }, function (_data) {
                                var model = $.parseJSON(_data);
                                if (model != null) {
                                    ko.mapping.fromJS(model.ManagedCareCompanyDetails, {}, self.CompanySearchResults);
                                    self.TotalItemCount(model.TotalCount);
                                    var _skipValue = $('#hidskip').val();
                                    var _totalCount = self.TotalItemCount();

                                    if ((_skipValue % _totalCount) == 0)
                                        self.Pager().CurrentPage(1);

                                }
                            });
                            hideLoader();
                        }
                        else {
                            alertify.alert("Please Enter Company");
                        }
                        alertify.alert("Deleted Successfully");
                    }
                    else if (_data == 0) {
                        alertify.alert("Error");
                    }

                });
            }
        });
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
        showLoader();
        var _searchtext = $('#SearchTextHidden').val();
        $.post("/ManagedCareCompany/GetManagedCareCompanyByName", {
            _searchText: _searchtext, _skip: skip
        }, function (_data) {
            var model = $.parseJSON(_data);
            self.CompanySearchResults.removeAll();
            ko.mapping.fromJS(model.ManagedCareCompanyDetails, {}, self.CompanySearchResults);
            self.TotalItemCount(model.TotalCount);

        });
        hideLoader();
    };


    var pagingSettings = {
        pageSize: 20,
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
};