function SearchViewModel() {
    var self = this;
    self.TotalItemCount = ko.observable(0);
    self.AttornyFirmSearchResults = ko.observableArray();

    self.AttorneyFirmSearchName = function () {
        if ($('#_SearchTextByName').val() == "") {
            $('#_SearchTextByName').focus();
            alertify.alert("Enter any Attorney or Attorney Firm.");
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
            self.AttorneyFirmSearchName();
        }
    });

    ko.bindingHandlers.formatNumberText = {
        update: function (element, valueAccessor) {
            var phone = ko.utils.unwrapObservable(valueAccessor());
            var formatPhone = function () {
                return phone.replace(/(\d{3})(\d{3})(\d{4})/, "($1)-$2-$3");
            }
            ko.bindingHandlers.text.update(element, formatPhone);
        }
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
        GrdBinding();
    };

  
    function GrdBinding() {
        showLoader();
        var _searchtext = $('#_SearchTextByName').val();
        $.post("/Attorney/GetAttorneyFirmByName", {
            _searchText: _searchtext, _skip: $('#hidskip').val()
        }, function (_data) {
            var model = $.parseJSON(_data);
            if (self.AttornyFirmSearchResults() != null)
                      self.AttornyFirmSearchResults.removeAll();
            ko.mapping.fromJS(model.AttorneyFirmDetails, {}, self.AttornyFirmSearchResults);
            self.TotalItemCount(model.TotalCount);
        });
        hideLoader();
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