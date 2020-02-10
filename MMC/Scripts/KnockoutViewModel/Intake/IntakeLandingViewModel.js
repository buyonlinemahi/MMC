function IntakeLandingViewModel(defaultmodel) {
    var self = this;
    self.TotalItemCount = ko.observable(0);
    self.IncompleteReferralDetailsResults = ko.observableArray();
    
    
    bindGrid(defaultmodel);
    function GrdBinding() {            
        $.post("/Intake/getIncompleteReferralsDetails", {
            _skip: self.Skip()
        }, function (_data) {
            var model = $.parseJSON(_data);
            bindGrid(model);
        });
        hideLoader();
    }

    function bindGrid(model) {
        self.IncompleteReferralDetailsResults.removeAll();
        ko.mapping.fromJS(model.IncompleteReferralsDetails, {}, self.IncompleteReferralDetailsResults);
        self.TotalItemCount(model.TotalCount);
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
}