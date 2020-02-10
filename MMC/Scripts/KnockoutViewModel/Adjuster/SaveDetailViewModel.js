function SaveDetailViewModel(model) {
    var self = this;
    self.AdjusterID = ko.observable();
    self.AdjFirstName = ko.observable();
    self.AdjLastName = ko.observable();
    self.AdjAddress1 = ko.observable();
    self.AdjAddress2 = ko.observable();
    self.AdjCity = ko.observable();
    self.AdjStateId = ko.observable();
    self.AdjZip = ko.observable();
    self.AdjPhone = ko.observable();
    self.AdjFax = ko.observable();
    self.AdjEMail = ko.observable();
    self.AdjStateName = ko.observable();
    self.ClientID = ko.observable();
    self.State = ko.observable();
    self.States = ko.observableArray();
    self.States = ko.observableArray([self.States(0)]);
    self.selectedItem = ko.observable(0);
    self.Clients = ko.observableArray();
    self.selectedClientItem = ko.observable();
    $(document).ready(function () {
        $.getJSON("/Common/getStates", function (data) {
            self.States(data.slice());
            if (model != null)
                self.selectedItem(model.AdjStateId);
        });
        $.getJSON("/Client/getAllClaimAdministrator", function (data) {
            self.Clients(data.slice());
            if (model != null)
                self.selectedClientItem(model.ClientID);
        });
    });


    ko.mapping.fromJS(model, {}, self);

    self.AddAdjusterSuccess = function (model) {
        if (model != null) {
            var _model = $.parseJSON(model);

            if (_model == "Saved Successfully" || _model == "Updated Successfully") {
                alertify.alert(_model, function (e) {
                    if (e) {
                        showLoader();
                        window.location = '/Adjuster/Index';
                    }

                });
            }
            else {
                alertify.alert(_model);
            }
        };
    };
    self.AdjusterFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }

    
}