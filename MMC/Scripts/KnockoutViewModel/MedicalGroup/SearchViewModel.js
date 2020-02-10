function SearchViewModel() {
    var self = this;
    self.TotalItemCount = ko.observable();
    self.MedicalGroupSearchResults = ko.observableArray();

    self.MedicalGroupSearchName = function () {
        if ($.trim($('#_SearchTextByName').val()) == "") {
            $('#_SearchTextByName').focus();
            alertify.alert("Enter any Medical Group.");
            return false;
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
            self.MedicalGroupSearchName();
        }
    });

    self.DeleteMedicalGroupByID = function (_data) {
        alertify.confirm("Are you sure  you want to delete the Medical Group?", function (e) {
            if (e) {
                $.post("/MedicalGroup/DeleteMedicalGroupById", { id: _data.MedicalGroupID() }, function (_responseText) {
                    alertify.alert(_responseText);
                    if (_responseText != "Error") {
                        GrdBinding();
                        if ((self.TotalItemCount() % $('#hidskip').val()) == 1)
                            self.Pager().CurrentPage(self.Pager().CurrentPage() - 1);
                    }
                });
            }
        });
    }


    function GrdBinding() {
        showLoader();
        var _Searchtext = $('#_SearchTextByName').val();
        if (_Searchtext != "") {
            $.post("/MedicalGroup/GetMedicalGroupByName", {
                _searchtext: _Searchtext, skip: $('#hidskip').val()
            }, function (model) {
                ko.mapping.fromJS(model.MedicalGroupDetails, {}, self.MedicalGroupSearchResults);
                self.TotalItemCount(model.TotalCount);
                hideLoader();
            });
        }
        else {
            hideLoader();
            $('#_SearchTextByName').focus();
            alertify.alert("Enter any Medical Group.");
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