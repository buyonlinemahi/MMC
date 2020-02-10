function SaveDetailViewModel(model) {
    var self = this;
    self.EmployerID = ko.observable();
    self.EmpName = ko.observable();    
    self.EmpAddress1 = ko.observable();
    self.EmpAddress2 = ko.observable();
    self.EmpCity = ko.observable();
    self.EmpStateId = ko.observable();
    self.EmpZip = ko.observable();
    self.EmpPhone = ko.observable();
    self.EmpFax = ko.observable();
    self.EmpEMail = ko.observable();
    self.EmpStateName = ko.observable();
    self.EmpContactName = ko.observable();
    self.EmpPhoneExtension = ko.observable();



    self.State = ko.observable();
    self.States = ko.observableArray();
    self.States = ko.observableArray([self.States(0)]);
    self.selectedItem = ko.observable(2);


    // Employer Subsidiaries
    self.EmployerId = ko.observable();
    self.EMPSubsidiaryID = ko.observable();
    self.EMPSubsidiaryName = ko.observable();
    self.EMPSubsidiaryAddress = ko.observable();
    self.EMPSubsidiaryAddress2 = ko.observable();
    self.EMPSubsidiaryCity = ko.observable();
    self.EMPSubsidiaryStateId = ko.observable();
    self.EMPSubsidiaryZip = ko.observable();
    self.EMPSubsidiaryPhone = ko.observable();
    self.EMPSubsidiaryFax = ko.observable();
    self.EMPSubsidiaryEmail = ko.observable();
    self.EmployerSubsidiarySearchResults = ko.observableArray();
    self.TotalItemCount = ko.observable(0);

    self.EmployerSubsidiarState = ko.observable();
    self.EmployerSubsidiarStates = ko.observableArray();
    self.EmployerSubsidiarStates = ko.observableArray([self.EmployerSubsidiarStates(0)]);
    self.EmployerSubsidiarselectedItem = ko.observable(2);
    var _EmployerID = 0;

    $(document).ready(function () {
        _EmployerID = $("#EmployerID").val();
        self.EmployerID(_EmployerID);
        if (_EmployerID == 0)
            $(".DivHide").fadeOut();
        else
            $(".DivHide").fadeIn();


        $.getJSON("/Common/getStates", function (data) {            
            self.States(data.slice());
            if (model != null)            
                self.selectedItem(model.EmpStateId);            
        });

        $.getJSON("/Common/getStates", function (data) {
            self.EmployerSubsidiarStates(data.slice());
            if (model != null)
                self.EmployerSubsidiarselectedItem(model.EMPSubsidiaryID);
        });
    });

    if (model != null) {
        if (model.EmployerResult !=null)
            ko.mapping.fromJS(model.EmployerResult, {}, self);

        if (model.EmployerSubsidiaryDetails != null) {
            ko.mapping.fromJS(model.EmployerSubsidiaryDetails, {}, self.EmployerSubsidiarySearchResults);
            self.TotalItemCount(model.TotalCount);
        }
    }

    $('.OpenPop').click(function () {
        blockPopupBackground();
    });

    $('.ClosePop').click(function () {
        unblockPopupBackground();
        ClearAll();
    });

    self.AddEmployerSuccess = function (responseText) {
        hideLoader();
        if (responseText[0] != 0) {
            $("#EmployerID").val(responseText[0]);
            self.EmployerID(responseText[0]);
        }
        switch (responseText[1]) {
            case 0:
                alertify.alert("Error Occur");
                break;
            case 1:
                alertify.alert("Save Successfully");
                $(".DivHide").fadeIn();
                break;
            case 2:
                alertify.alert("Update Successfully");
                break;
        };
    };

   
    self.EmployerFormBeforeSubmit = function (arr, $form, options) {        
        if ($form.jqBootstrapValidation('hasErrors')) {            
            return false;
        }
        else {
            showLoader();
        }
        return true;
       
    };


    function GrdBinding() {
        showLoader();
        $.post("/Employer/GetEmployerSubsidiariesByEmployer", {
            id: $('#EmployerID').val(), _skip: $('#hidskip').val()
        }, function (_data) {
            var model = $.parseJSON(_data);
            self.EmployerSubsidiarySearchResults.removeAll();
            ko.mapping.fromJS(model.EmployerSubsidiaryDetails, {}, self.EmployerSubsidiarySearchResults);
            self.TotalItemCount(model.TotalCount);
            var _skipValue = $('#hidskip').val();
            var _totalCount = self.TotalItemCount();

            if ((_skipValue % _totalCount) == 0)
                self.Pager().CurrentPage(1);
        });
        hideLoader();
    }

    self.DeleteEmployerSubsidiary = function (_data) {
        alertify.confirm("Are you sure to delete this record?", function (e) {
            if (e) {
                $.post("/Employer/DeleteEmployerSubsidiaryByID", { _empSubsidiaryID: _data.EMPSubsidiaryID() }, function (_responseText) {
                    if (e) {                        
                        if (_responseText != "Error") {
                            GrdBinding();
                        }
                        alertify.alert(_responseText);
                    }
                });               
            }
        });
    };

    self.AddEmployerSubsidiarySuccess = function (model) {
        hideLoader();
        if (model != null) {
            var _model = $.parseJSON(model);
            if (_model == "Saved Successfully" || _model == "Updated Successfully") {
                GrdBinding();
                $('.ClosePop').click();
                alertify.alert(_model);
            }
            else {
                alertify.alert(_model);
            }
        };
    };

    self.EmployerSubsidiaryFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        else {
            showLoader();
        }
        return true;

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


    var pagingSettings = {
        pageSize: 10,
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

    function ClearAll() {
        $("#frmSaveEmployerSubsidiariesDetails")[0].reset();
        $('#EMPSubsidiaryID').val('');
        $('#EMPSubsidiaryName').val('');
        $('#EMPSubsidiaryAddress').val('');
        $('#EMPSubsidiaryAddress2').val('');
        $('#EMPSubsidiaryCity').val('');
        $('#EMPSubsidiaryZip').val('');
        $('#EMPSubsidiaryPhone').val('');
        $('#EMPSubsidiaryFax').val('');
        $('#EMPSubsidiaryEmail').val('');
    }
}