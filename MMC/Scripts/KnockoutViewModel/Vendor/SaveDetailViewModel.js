function SaveVendorDetailViewModel(model) {
    var self = this;
    self.VendorID = ko.observable();
    self.VendorName = ko.observable();
    self.VendorTAX= ko.observable();
    self.VendorAddress1 = ko.observable();
    self.VendorAddress2 = ko.observable();
    self.VendorCity = ko.observable();
    self.VendorStateId = ko.observable();
    self.VendorZip = ko.observable();
    self.VendorPhone = ko.observable();
    self.VendorFax = ko.observable();
    self.States = ko.observableArray();
    self.selectedItem = ko.observable();

    $.getJSON("/Common/getStates", function (data) {
        self.States(data.slice());
        if(model != null)
        self.selectedItem(model.VendorStateId);
    });

    if (model != null) {
        ko.mapping.fromJS(model, {}, self);
    }
    self.VendorInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }

    self.AddVendorSuccess = function (responseText) {
        if (responseText != null) {
            alertify.alert(responseText, function (e) {
                if (e) {
                    showLoader();
                    window.location = '/Vendor/Index';
                }
            });
        };
    };
}