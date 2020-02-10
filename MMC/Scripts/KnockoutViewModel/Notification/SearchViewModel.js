function SearchViewModel(model) {
    var self = this;
    self.TotalItemCount = ko.observable(0);
    self.ClinicalTriages = ko.observableArray();
    self.TotalItemCount(model.TotalCount);
    var mappingOptions = {
        'RFALatestDueDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        },
    }
    ko.mapping.fromJS(model.ClinicalTriages, mappingOptions, self.ClinicalTriages);
    function GrdBinding() {
        showLoader();
        $.post("/Notification/Index", {
            skip: self.Skip()
        }, function (_data) {
            self.ClinicalTriages.removeAll();
            ko.mapping.fromJS(_data.ClinicalTriages, {}, self.ClinicalTriages);
            self.TotalItemCount(model.TotalCount);
        });
        hideLoader();
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
        pageSize: 20,
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
}


