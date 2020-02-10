function BillingAccountReceivablesViewModel() {
    var self = this;
    self.TotalItemCount = ko.observable(0);
    self.BillingAccountReceivableDetails = ko.observableArray([]);
   
    self.ClientName = ko.observable();
    self.ClientDetails = ko.observableArray();
    self.ClientDetails = ko.observableArray([self.ClientDetails(0)]);


    var mappingOptions = {
        'CreatedDate': {
            create: function (options) {
                if (options.data != '/Date(-62135596800000)/')
                    return moment(options.data).format("MM-DD-YYYY");
            }
        }
    }

    $(document).ready(function () {
        $.getJSON("/Billing/getClientAll", function (data) {
            self.ClientDetails(data.slice());
        });
    });

    self.SearchBillingAccountReceivablesDetails = function () {
        BindGridByClientName();
        self.Pager().CurrentPage(1);     

    }

    function BindGridByClientName() {
        showLoader();
        $.post("/Billing/GetBillingAccountReceivablesByClientNameResult", {
            ClientName: $("#ClientName").find("option:selected").text(), _skip: $('#hidskip').val()
        }, function (_data) {
            var model = $.parseJSON(_data);
            self.BillingAccountReceivableDetails.removeAll();
            if (model != null) {
                ko.mapping.fromJS(model.AccountReceivablesDetails, mappingOptions, self.BillingAccountReceivableDetails);
                self.TotalItemCount(model.TotalCount);
            }
        });
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
        if ($("#ClientName").find("option:selected").text() == 'Select Any Client') {
            alert("Please select Client");
        }
        else {
            BindGridByClientName();
        }
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