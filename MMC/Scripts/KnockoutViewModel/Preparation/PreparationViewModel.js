function PreparationViewModel(model) {
    var self = this;
    self.ClinicalTriages = ko.observableArray([]);
    self.TotalItemCount = ko.observable(0);
    var mappingOptions = {
        'RFALatestDueDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        },
    }
    ko.mapping.fromJS(model.ClinicalTriages, mappingOptions, self.ClinicalTriages);
    self.TotalItemCount(model.TotalCount);
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
    function GrdBinding() {
        showLoader();
        $.getJSON("/Preparation/Index", {
            skip: self.Skip()
        }, function (model) {
            self.ClinicalTriages.removeAll();
            ko.mapping.fromJS(model.ClinicalTriages, {}, self.ClinicalTriages);
            self.TotalItemCount(model.TotalCount);
            hideLoader();
        });

    }
}
