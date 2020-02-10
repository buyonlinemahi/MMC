function SaveDetailViewModel(model) {
    var self = this;
    self.PeerReviewID = ko.observable();
    self.PeerReviewName = ko.observable();
    self.PeerReviewEmail = ko.observable();
    self.PeerReviewFax = ko.observable();
    self.PeerReviewPhone = ko.observable();
    
    ko.mapping.fromJS(model, {}, self);

    self.AddPeerReviewSuccess = function (model) {
        if (model != null) {
            var _model = $.parseJSON(model);

            if (_model == "Saved Successfully" || _model == "Updated Successfully") {
                alertify.alert(_model, function (e) {
                    if (e) {
                        showLoader();
                        window.location = '/PeerReview/Index';
                    }

                });
            }
            else {
                alertify.alert(_model);
            }
        };
    };
    self.PeerReviewFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }

    
}