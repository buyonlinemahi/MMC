function BillingLandingViewModel(model) {
    var self = this;
    self.TotalItemCount = ko.observable(0);
    self.BillingLandingDetails = ko.observableArray([]);
    self.BillingDetailsByBillingID = ko.observableArray([]);    
    if (model != null) {
        //ko.mapping.fromJS(model.BillingDetails, {}, self.BillingLandingDetails);
        $.each(model.BillingDetails, function (index, value) {
            self.BillingLandingDetails.push(new BillingGridDetails(value));
        });
        ko.mapping.fromJS(model.TotalCount, {}, self.TotalItemCount);
    }


    self.ClientName = ko.observable();
    self.ClientDetails = ko.observableArray();
    self.ClientDetails = ko.observableArray([self.ClientDetails(0)]);
  

    

    self.bindGrid = function(model) {
        self.BillingLandingDetails.removeAll();
        if (model != null) {
            //ko.mapping.fromJS(model.BillingDetails, {}, self.BillingLandingDetails);
            $.each(model.BillingDetails, function (index, value) {
                self.BillingLandingDetails.push(new BillingGridDetails(value));
            });
            self.TotalItemCount(model.TotalCount);
        }
    }
    self.bindGrid(model);    
    self.GrdBinding = function() {
        showLoader();
        $.post("/Billing/GetPageBillingResult", {
            _skip: $('#hidskip').val()
        }, function (_data) {            
            self.bindGrid(_data);
        });
        hideLoader();        
    };
    $(document).ready(function () {
        $.getJSON("/Billing/getClientAll", function (data) {
            self.ClientDetails(data.slice());
        });
    });

    self.SearchBillingDetailByCLientName = function () {
        BindGridByClientName();
        self.Pager().CurrentPage(1);
        if ($("#ClientName").val() == '') {
            $("#btnCreate").addClass("disabled")
        }
        else {
            $("#btnCreate").removeClass("disabled")
        }
        
                
    }
    self.GridBindByClientName = function () {
        BindGridByClientName();
    }
    function BindGridByClientName() {
        showLoader();
        $.post("/Billing/GetBillingDetailByClientNameResult", {
            ClientName: $("#ClientName").find("option:selected").text(), _skip: $('#hidskip').val()
        }, function (_data) {
            var model = $.parseJSON(_data);
            self.BillingLandingDetails.removeAll();
            if (model != null) {
                ko.mapping.fromJS(model.BillingDetails, {}, self.BillingLandingDetails);
                //$.each(model.BillingDetails, function (index, value) {
                //    self.BillingLandingDetails.push(new BillingGridDetails(value));
                //});
                self.TotalItemCount(model.TotalCount);                
            }
        });
        hideLoader();
    }

    

    self.CreateInvoiceSuccess = function (model) {
     
        alertify.alert(model, function (e) {
                    if (e) {                      
                        window.location = '/Billing/Index';
                    }

                });
          
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
        if ($("#ClientName").find("option:selected").text() == 'Select Any Client') {
            self.GrdBinding();
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
    self.OpenRequestBillingPopUp = function (_RFARequestID, _RFARequestBillingID, _RFARequestBillingRN, _RFARequestBillingMD, _RFARequestBillingSpeciality, _RFARequestBillingAdmin, _RFARequestBillingDeferral, _DecisionID, _ClientBillingRateTypeID) {        
        self.BillingDetailsByBillingID($.grep(self.BillingLandingDetails(), function (elem) {
            return (elem.RFARequestID() == _RFARequestID);
        }));        
        
        //$(".billingrange").mask("9.9");
        blockPopupBackground();
        if (_DecisionID == 9) {
            $('.non-deferral').prop("disabled", true);
            $('.deferral').prop("disabled", false);
            $('.non-deferral').val('');

            $("#txtRFARequestBillingDeferral").val(_RFARequestBillingDeferral.toFixed(1));
        }
        else {
            $('.non-deferral').prop("disabled", false);
            $('.deferral').prop("disabled", true);
            $('.deferral').val('');

            $("#txtRFARequestBillingRN").val(_RFARequestBillingRN.toFixed(1));
            $("#txtRFARequestBillingMD").val(_RFARequestBillingMD.toFixed(1));
            $("#txtRFARequestBillingSpeciality").val(_RFARequestBillingSpeciality.toFixed(1));
            $("#txtRFARequestBillingAdmin").val(_RFARequestBillingAdmin.toFixed(1));
        }
        $("#hfRFARequestID").val(_RFARequestID);
        $("#hfRFARequestBillingID").val(_RFARequestBillingID);
    }

    self.RFARequestBillingsBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        showLoader();
    };
    self.updateRequestBillingsSuccess = function (ab) {
        $('#EditBilling').modal('hide');
        unblockPopupBackground();
        if ($("#ClientName").find("option:selected").text() == 'Select Any Client') {
            _billingLandingViewModel.GrdBinding();
        }
        else {
            _billingLandingViewModel.GridBindByClientName();
        }
        alertify.alert("Updated Successfully");
    };

    function BillingGridDetails(value) {
        var self = this;
        self.ClientName = ko.observable(value.ClientName);
        self.PatientName = ko.observable(value.PatientName);
        self.RFAReferralID = ko.observable(value.RFAReferralID);
        self.RFARequestID = ko.observable(value.RFARequestID);
        self.PatientClaimID = ko.observable(value.PatientClaimID);
        self.PatClientID = ko.observable(value.PatClientID);
        self.RFARequestedTreatment = ko.observable(value.RFARequestedTreatment);
        self.RFARequestBillingID = ko.observable(value.RFARequestBillingID);
        self.RFARequestBillingRN = ko.observable(value.RFARequestBillingRN);
        self.RFARequestBillingMD = ko.observable(value.RFARequestBillingMD);
        self.RFARequestBillingSpeciality = ko.observable(value.RFARequestBillingSpeciality);
        self.RFARequestBillingAdmin = ko.observable(value.RFARequestBillingAdmin);
        self.RFARequestBillingDeferral = ko.observable(value.RFARequestBillingDeferral);
        self.BillingTotal = ko.observable(value.BillingTotal);
        self.DecisionID = ko.observable(value.DecisionID);        
        self.ClientBillingRateTypeID = ko.observable(value.ClientBillingRateTypeID);        
        self.IsChecked = ko.observable(value.IsChecked);
    };
}

function updateRequestBilling()
{
    $.post("/Billing/UpdateRequestBillings", {
        RFARequestBillingID: _RFARequestBillingID, RFARequestBillingRN: _RFARequestBillingRN, RFARequestBillingMD: _RFARequestBillingMD, RFARequestBillingSpeciality: _RFARequestBillingSpeciality
    }, function (_data) {
        var model = $.parseJSON(_data);
        if (self.AttornyFirmSearchResults() != null)
            self.AttornyFirmSearchResults.removeAll();
        ko.mapping.fromJS(model.AttorneyFirmDetails, {}, self.AttornyFirmSearchResults);
        self.TotalItemCount(model.TotalCount);
    });
}


function btnPopUpClose() {    
    unblockPopupBackground();
}