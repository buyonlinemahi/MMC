function SaveDetailViewModel(model) {
    var self = this;
    self.InsurerID = ko.observable();
    self.InsName = ko.observable();
    self.InsAddress1 = ko.observable();
    self.InsAddress2 = ko.observable();
    self.InsCity = ko.observable();
    self.InsStateId = ko.observable();
    self.InsZip = ko.observable();
    self.InsPhone = ko.observable();
    self.InsFax = ko.observable();
    self.InsEMail = ko.observable();
    self.InsStateName = ko.observable();
    self.InsContactName = ko.observable();
    self.InsPhoneExtension = ko.observable();
    self.State = ko.observable();
    self.States = ko.observableArray();
    self.States = ko.observableArray([self.States(0)]);
    self.selectedItem = ko.observable(0);
    //Branch
    self.TotalItemCount = ko.observable(0);
    self.InsuranceBranchSearchResults = ko.observableArray();
    self.InsuranceBranchID = ko.observable();
    self.InsBranchName = ko.observable();
    self.InsBranchAddress = ko.observable();
    self.InsBranchCity = ko.observable();
    self.InsBranchStateId = ko.observable();
    self.InsBranchStateName = ko.observable();
    self.InsBranchZip = ko.observable();
    self.InsurerID = ko.observable();
    self.BranchState = ko.observable();
    self.BranchStates = ko.observableArray();
    self.BranchStates = ko.observableArray([self.BranchStates(0)]);
    self.BranchselectedItem = ko.observable(0);

    $(document).ready(function () {
        $.getJSON("/Common/getStates", function (data) {
            self.States(data.slice());
            self.BranchStates(data.slice());
            if (model != null)
                self.selectedItem(model.InsStateId);
        });
        GrdBinding();
       
    });

    ko.mapping.fromJS(model, {}, self);

    self.AddInsurerSuccess = function (model) {
        hideLoader();
        if (model != null) {
            var _model = $.parseJSON(model);
            if (_model.AlertMessage == "Saved Successfully" || _model.AlertMessage == "Updated Successfully") {
                alertify.alert(_model.AlertMessage, function (e) {
                    if (e) {                    
                        $("#divinsuranceadd").css("display", "block");
                        self.InsurerID(_model.InsurerID)                        
                    }
                });
            }
            else {
                alertify.alert(_model.AlertMessage);
            }
        };
    };
   
    if (self.InsurerID() != 0) {
        $("#divinsuranceadd").css("display", "block");
    }

    self.InsurerFormBeforeSubmit = function (arr, $form, options) {       
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        showLoader();
        return true;
    }

    //INsuranceBranch...
    self.editBranch = function (model) {
        self.InsuranceBranchID(model.InsuranceBranchID());
        self.InsBranchName(model.InsBranchName());
        self.InsBranchCity(model.InsBranchCity());
        self.BranchselectedItem(model.InsBranchStateId());
        self.InsBranchZip(model.InsBranchZip());
        self.InsBranchAddress(model.InsBranchAddress());
        self.InsurerID(model.InsurerId());
        blockPopupBackground();
    };

    self.deleteBranch = function (_data) {
        alertify.confirm("Are you sure to delete this record?", function (e) {
            if (e) {
                $.post("/Insurer/DeleteInsuranceBranchByID", { _insuranceBranchID: _data.InsuranceBranchID() }, function (_responseText) {
                    if (e) {
                        alertify.alert(_responseText);
                        if (_responseText != "Error") {
                            GrdBinding();
                        }
                        
                        if ((self.TotalItemCount() % $('#hidskip').val()) == 1)
                            self.Pager().CurrentPage(self.Pager().CurrentPage() - 1);
                        else
                            self.Pager().CurrentPage();
                    }
                });
            }
        });
    };

    self.btnopenpop = function () {
        blockPopupBackground();
    };
    self.btnclosepop = function () {
      
        clearBranchForm();
    };

    function clearBranchForm() {
          unblockPopupBackground();
        $("#frmSaveInsuranceBranchDetails")[0].reset();
        self.InsuranceBranchID('');
        self.InsBranchName('');
        self.InsBranchCity('');
        self.BranchselectedItem('');
        self.InsBranchZip('');
        self.InsBranchAddress('');
        unblockPopupBackground();
    }
    
    self.AddInsuranceBranchSuccess = function (model) {         
        if (model != null) {
            var _model = $.parseJSON(model);
            if (_model == "Saved Successfully" || _model == "Updated Successfully") {
                hideLoader();
                alertify.alert(_model, function (e) {
                    if (e) {
                        $('#InsuranceBranch').modal('toggle');
                        GrdBinding();
                        clearBranchForm();
                    }
                });
              
            }
            else {
                alertify.alert(_model);
            }
        };
    };
    self.AddInsuranceBranchFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        showLoader();
        return true;
    }

    function GrdBinding() {
        showLoader();
        var _insurerID = $('#InsurerID').val();
        $.post("/Insurer/GetInsuranceBranchByInsurerID", {
            _insurerID: _insurerID, _skip: $('#hidskip').val()
        }, function (_data) {
            var model = $.parseJSON(_data);
            self.InsuranceBranchSearchResults.removeAll();
            ko.mapping.fromJS(model.InsuranceBranchDetails, {}, self.InsuranceBranchSearchResults);
            self.TotalItemCount(model.TotalCount);
            hideLoader();
        });
      
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