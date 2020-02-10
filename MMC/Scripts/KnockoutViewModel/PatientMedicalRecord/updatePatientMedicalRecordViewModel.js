
function updatePatientMedicalRecordViewModel() {
    var self = this;
    var mappingOptions = {
        'RFARecDocumentDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        }
    }

    
    self.Init = function (model) {
        ko.mapping.fromJS(model, mappingOptions, self);
        $('#objectPDF').attr('src', model.rfaRecordSpliting.DocumentUrl);
    }

    self.PatientMedicalRecordFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        showLoader();
        return true;
    }
    self.PatientMedicalRecordSuccess = function (response) {
        hideLoader();
        alertify.success("Updated Sucessfully");
    }
}

