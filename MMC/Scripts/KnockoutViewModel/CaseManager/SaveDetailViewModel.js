function SaveDetailViewModel(model) {
    var self = this;
    self.CaseManagerID = ko.observable();
    self.CMFirstName = ko.observable();
    self.CMLastName = ko.observable();
    self.CMAddress1 = ko.observable();
    self.CMAddress2 = ko.observable();
    self.CMCity = ko.observable();
    self.CMStateId = ko.observable();
    self.CMZip = ko.observable();
    self.CMPhone = ko.observable();
    self.CMFax = ko.observable();
    self.CMEmail = ko.observable();
    self.CMNotes = ko.observable();
    self.States = ko.observableArray();
    self.selectedItem = ko.observable();

    $(document).ready(function () {
        $.getJSON("/Common/getStates", function (data) {
            self.States(data.slice());
            if (model != null)
                self.selectedItem(model.CMStateId);
        });
    });


    ko.mapping.fromJS(model, {}, self);

    self.AddCaseManagerSuccess = function (model) {
        if (model != null) {
            var _model = $.parseJSON(model);
            if (_model == "Saved Successfully" || _model == "Updated Successfully") {
                alertify.alert(_model, function (e) {
                    if (e) {
                        showLoader();
                        window.location = '/CaseManager/Index';
                    }
                });
            }
            else {
                alertify.alert(_model);
            }
        };
    };
    self.CaseManagerFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }


}