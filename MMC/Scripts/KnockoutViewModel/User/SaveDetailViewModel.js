function SaveDetailViewModel(model) {
    var self = this;
    self.UserId = ko.observable();
    self.UserFirstName = ko.observable();
    self.UserLastName = ko.observable();
    self.UserPhone = ko.observable();
    self.UserEMail = ko.observable();
    self.UserDeletePermission = ko.observable();
    self.UserManagementPermission = ko.observable();

    ko.mapping.fromJS(model, {}, self);
    if (self.UserId() != 0) {
        $("#divpassword").hide();
        $('#UserName').attr('readonly', true);


        $('#UserPassword').removeAttr('required');
    }
    self.AddUserSuccess = function (model) {
        if (model != null) {
            var _model = $.parseJSON(model);
            if (_model == "Saved Successfully" || _model == "Updated Successfully") {
                alertify.alert(_model, function (e) {
                    if (e) {
                        showLoader();
                        window.location = '/User/Index';
                    }
                });
            }
            else {
                alertify.alert(_model);
            }


        };
    };

    
    self.UserFormBeforeSubmit = function (arr, $form, options) {
        if ($("#UserSignature").val() != "") {
            var sFileName = $("#UserSignature").val();
            var sFileExtension = sFileName.split('.')[sFileName.split('.').length - 1].toLowerCase();
            if (!(sFileExtension === "ico" || sFileExtension === "png" || sFileExtension === "jpg")) {
                $("#lblFileValidation").html("Please make sure your file is in jpg or png or bitmap format");
                return false;
            }
            else {
                $("#lblFileValidation").html("");
            }

            if ($("#UserSignatureFileName").val() != '') {
                if (confirm("Do you wanna replace the old signature?"))
                    return true;
                else
                    return false;
            }
        }
        
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }        
        return true;        
    }
}

$(document).ready(function () {
    $('[data-toggle="popover"]').popover({
        placement: 'top',
        trigger: 'hover',
        html: true,
        content: '<div class="media"><img src="' + $('#UserSignatureFileName').val() + '" style="max-width:245px; max-height:250px;" /></div>'
    });
});