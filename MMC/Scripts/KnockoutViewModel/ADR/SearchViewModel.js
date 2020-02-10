function SearchViewModel() {
    var self = this;
    self.TotalItemCount = ko.observable();
    self.ADRSearchResults = ko.observableArray();

    self.ADRSearchName = function () {
        GrdBinding();
        self.Pager().CurrentPage(1);
    };

    $("#_SearchTextByName").keypress(function (e) {//To Search when enter is pressed
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            self.ADRSearchName();
        }
    });

    function GrdBinding() {
        showLoader();
        var _Searchtext = $('#_SearchTextByName').val();
        if (_Searchtext != "") {
            $.post("/ADR/GetADRByName", {
                _searchtext: _Searchtext, skip: $('#hidskip').val()
            }, function (model) {
                if (model.ADRDetails != null) {
                    ko.mapping.fromJS(model.ADRDetails, {}, self.ADRSearchResults);
                    self.TotalItemCount(model.TotalCount);
                }
                else {
                    alertify.alert("Data Not Found");
                }
            });
        }
        else {
            $('#_SearchTextByName').focus();
            alertify.alert("Enter any ADR.");
        }
        hideLoader();
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