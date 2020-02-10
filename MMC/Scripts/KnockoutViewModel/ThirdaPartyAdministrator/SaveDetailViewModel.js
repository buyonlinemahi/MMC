function SaveTPADetailViewModel(model) {
    var self = this;
    self.TPAID = ko.observable();
    self.TPAName = ko.observable();
    self.TPAAddress = ko.observable();
    self.TPAAddress2 = ko.observable();
    self.TPACity = ko.observable();
    self.TPAStateId = ko.observable();
    self.TPAZip = ko.observable();
    self.TPAPhone = ko.observable();
    self.TPAFax = ko.observable();
   // self.StateName = ko.observable();
    self.States = ko.observableArray();
    self.selectedItem = ko.observable();
    self.selectedItem1 = ko.observable();
    // TPA Branch //
    self.TPABranchID = ko.observable();
    self.TPABranchName = ko.observable();
    self.TPABranchAddress = ko.observable();
    self.TPABranchAddress2 = ko.observable();
    self.TPABranchCity = ko.observable();
    self.TPABranchStateId = ko.observable();
    self.TPABranchZip = ko.observable();
    self.TPABranchPhone = ko.observable();
    self.TPABranchFax = ko.observable();
    self.TPABranchResults = ko.observableArray();
    self.TotalItemCount = ko.observable();

    $.getJSON("/Common/getStates", function (data) {
        self.States(data.slice());      
    });

    if (model != null) {
        self.selectedItem(model.TPAStateId);
    }


    function GetTPABranchByTPAID() {
        showLoader();
        var _skipValue;
        if ($('#hidskip').val() != null && $('#hidskip').val() != "")
        {
            _skipValue = $('#hidskip').val();
        }
        else {
            _skipValue = 0;
        }
        $.post("/ThirdPartyAdministrator/GetTPABranchByTPAID", {
            _TPAID: self.TPAID(), skip: _skipValue
        }, function (model) {
            ko.mapping.fromJS(model.ThirdPartyAdministratorBranchDetails, {}, self.TPABranchResults);
            self.TotalItemCount(model.TotalCount);
            hideLoader();
        })
       
    }

    if (model != null) {
        ko.mapping.fromJS(model, {}, self);
        if (self.TPAID() != null)
            GetTPABranchByTPAID();
    }
    self.TPAInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        showLoader();
        return true;
    }

    self.DeleteTPABranchByID = function (_data) {
        var _TPAID = _data.TPAID();
        alertify.confirm("Are you sure to delete this record?", function (e) {
            if (e) {
                $.post("/ThirdPartyAdministrator/DeleteTPABranchByID", { id: _data.TPABranchID() }, function (_responseText) {
                    alertify.alert(_responseText);
                    if (_responseText != "Error") {                    
                        GetTPABranchByTPAID();
                        if ((self.TotalItemCount() % $('#hidskip').val()) == 1)
                            self.Pager().CurrentPage(self.Pager().CurrentPage() - 1);
                        self.TPAID(_TPAID);
                    }
                });
            }
        });
    }

    self.GetTPABranchByID = function (_data) {
      //  showLoader();
        blockPopupBackground();
        $('#myDivTPABranchPartial').modal('toggle');
        $.post("/ThirdPartyAdministrator/GetTPABranchByID", { id: _data.TPABranchID() }, function (_branchModel) {
            if (_branchModel != "Error") {
                ko.mapping.fromJS(_branchModel, {}, self);
                self.selectedItem1(_branchModel.TPABranchStateId);
            //    hideLoader();
            }
        });
       
    }

    self.btnclosepop = function () {
        unblockPopupBackground();
    }
    self.OpenBranchPartialPopup = function () {
        clearBranchForm();
        blockPopupBackground();
    }
    function clearBranchForm() {
        $("#frmTPABranchPartial")[0].reset();
        self.TPABranchID('');
        self.TPABranchName('');
        self.TPABranchAddress('');
        self.TPABranchAddress2('');
        self.TPABranchCity('');
        self.TPABranchStateId('');
        self.TPABranchZip('');
    }

    self.AddTPASuccess = function (responseText) {
        hideLoader();
        if (responseText[0] != null) {
            alertify.alert(responseText[0], function (e) {
                if (e) {
                    self.TPAID(responseText[1]);
                    unblockPopupBackground();                    
                }
            });
        };
    };

    self.TPABranchInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        showLoader();
        return true;
    }

    self.AddTPABranchSuccess = function (responseText) {
        hideLoader();        
        if (responseText != null) {
            alertify.alert(responseText, function (e) {
                if (e) {
                    $('#myDivTPABranchPartial').modal('toggle');
                    GetTPABranchByTPAID();
                    self.Pager().CurrentPage(1);
                    unblockPopupBackground();
                }
            });
        };
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
        GetTPABranchByTPAID();
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