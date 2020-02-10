function SaveMedicalGroupDetailViewModel(model) {
    var self = this;
    self.MedicalGroupID = ko.observable();
    self.MedicalGroupName = ko.observable();
    self.MGAddress = ko.observable();
    self.MGAddress2 = ko.observable();
    self.MGCity = ko.observable();
    self.MGStateID = ko.observable();
    self.MGZip = ko.observable();
    self.MGNote = ko.observable();
    self.States = ko.observableArray();
    self.selectedItem = ko.observable();

    $.getJSON("/Common/getStates", function (data) {
        self.States(data.slice());
        if (model != null)
            self.selectedItem(model.MGStateID);
    });

    if (model != null) {
        ko.mapping.fromJS(model, {}, self);
    }
    self.MedicalGroupInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }

    self.AddMedicalGroupSuccess = function (responseText) {
        if (responseText != null) {
            alertify.alert(responseText, function (e) {
                if (e) {
                    showLoader();
                    window.location = '/MedicalGroup/Index';
                }
            });
        };
    };
}