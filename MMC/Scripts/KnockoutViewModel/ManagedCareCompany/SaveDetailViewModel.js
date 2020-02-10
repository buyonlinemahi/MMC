function SaveDetailViewModel(model) {
    var self = this;
    self.CompanyId = ko.observable();
    self.CompName = ko.observable();
    self.CompAddress = ko.observable();
    self.CompAddress2 = ko.observable();
    self.CompCity = ko.observable();
    self.CompStateId = ko.observable();
    self.CompZip = ko.observable();    

    //State
    self.State = ko.observable();
    self.States = ko.observableArray();
    self.States = ko.observableArray([self.States(0)]);
    self.selectedItem = ko.observable(0);

   

    $(document).ready(function () {
        $.getJSON("/Common/getStates", function (data) {
            self.States(data.slice());
            if (model != null)
                self.selectedItem(model.CompStateId);
        });       
    });

    if (model != null) {
        ko.mapping.fromJS(model, {}, self);
    }


    self.AddManagedCareCompanySuccess = function (responseText) {
        alertify.confirm(responseText, function (e) {
            if (e) {
                showLoader();
                window.location = '/ManagedCareCompany/Index';
            }
        });
    };
};