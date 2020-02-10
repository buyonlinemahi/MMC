function SearchViewModel() {
    var self = this;
    self.TotalItemCount = ko.observable(0);
    self.RequestIMRRecordSearchResults = ko.observableArray();
    self.RequestIMRRecordResults = ko.observableArray();
    self.cdTotalItemCount = ko.observable(0);

    self.Init = function (model) {
        GrdBinding(0);
        
    };


    function GrdBinding(skip) {
        showLoader();
        $.post("/IMR/GetRequestIMRRecordAll", {
            _skip: skip
        }, function (_data) {
            var model = $.parseJSON(_data);
            self.RequestIMRRecordSearchResults.removeAll();
            ko.mapping.fromJS(model.RequestIMRRecordDetails, {}, self.RequestIMRRecordSearchResults);
            self.TotalItemCount(model.TotalCount);
        });
        hideLoader();
    }
    self.ResetClaimSearch = function () {
        //$('#txtPatietnSearch').val('');
        //self.RequestIMRRecordResults.removeAll();
        //self.cdTotalItemCount(0);
        
        //blockPopupBackground();
        //GrdtRequestIMRRecordsBinding(0);
        //self.Pager().CurrentPage(1);
        window.location = '/IMR/SearchRequestIMRDetail';
        
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
        GrdBinding(skip);
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
   


   