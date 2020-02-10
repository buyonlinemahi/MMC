function SearchViewModel() {
    var self = this;
    self.TotalItemCount = ko.observable(0);
    self.ClientSearchResults = ko.observableArray();

    self.ClientSetupSearchName = function () {
        if ($('#_SearchTextByName').val() == "") {
            $('#_SearchTextByName').focus();
            alertify.alert("Enter any Client.");
        }
        else {
            GrdBinding();
            self.Pager().CurrentPage(1);
        }
    };

    $("#_SearchTextByName").keypress(function (e) {//To Search when enter is pressed
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            self.ClientSetupSearchName();
        }
    });

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

    function GrdBinding() {
        showLoader();
        var _searchtext = $('#_SearchTextByName').val();
        $.post("/Client/getClientByName", {
            _searchText: _searchtext, _skip: $('#hidskip').val()
        }, function (_data) {
            var model = $.parseJSON(_data);
            self.ClientSearchResults.removeAll();
            ko.mapping.fromJS(model.ClientDetails, {}, self.ClientSearchResults);
            self.TotalItemCount(model.TotalCount);
            hideLoader();
        });

    }


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
}