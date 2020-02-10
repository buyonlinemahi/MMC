function SaveDetailViewModel(model) {
    var self = this;
    self.ADRID = ko.observable();
    self.ADRName = ko.observable();
    self.ADRAddress = ko.observable();
    self.ADRAddress2 = ko.observable();
    self.ADRStateID = ko.observable();
    self.ADRCity = ko.observable();
    self.ADRZip = ko.observable();
    self.ContactName = ko.observable();
    self.ContactPhone = ko.observable();

    self.States = ko.observableArray();
    self.selectedItem = ko.observable();

    $.getJSON("/Common/getStates", function (data) {
        self.States(data.slice());
        if (model != null)
            self.selectedItem(model.ADRStateID);
    });

    if (model != null) {
        ko.mapping.fromJS(model, {}, self);
    }
    self.ADRInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }

    self.AddADRSuccess = function (responseText) {
        if (responseText != null) {
            alertify.alert(responseText, function (e) {
                if (e) {
                    showLoader();
                    window.location = '/ADR/Index';
                }
            });
        };
    };
}