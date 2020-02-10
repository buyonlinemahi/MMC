function PeerReviewActionViewModel(model) {   
    var self = this;
    self.RFAClinicalReasonforDecision = ko.observable();
    self.RequestDetail = ko.observableArray([]);
    var mappingOptions = {
        'PatDOI': {
            create: function (options) {                
                    return moment(options.data).format("MM/DD/YYYY ");
            }
        },
        'RFALatestDueDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        }
    }    
    ko.mapping.fromJS(model.Patients, mappingOptions, self);
    ko.mapping.fromJS(model.RequestDetail, mappingOptions, self.RequestDetail);

    var _requestID = 0;
    self.OpenrichtextPopUp1 = function (data) {
        _requestID = data.RFARequestID;
        blockPopupBackground();
        var editor1 = document.getElementById("Editor1").editor;
        editor1.SetText(($("<div>").html(data.RFAClinicalReasonforDecision()).text()));
    }

    self.btnUpdateProcessLevel = function () {
        
        // check all request reason are filled or not
        $.post("/PeerReview/UpdateProcessLevel", { RFAReferralID: model.Patients.RFAReferralID }, function (model) {
            alertify.alert(model, function () { window.location.href = "/PeerReview/PeerReviewLanding"; });
            
            
        });
    };

    checkIfClinicalReasonAreFilled();
    
    function checkIfClinicalReasonAreFilled()
    {
        var checkclinical = true;
        for (var i = 0; i < self.RequestDetail().length; i++) {
            if (self.RequestDetail()[i].RFAClinicalReasonforDecision() == "" || self.RequestDetail()[i].RFAClinicalReasonforDecision() == null) {
                checkclinical = false;
            }
        }

        if (checkclinical) {
            $("#btnContinue").removeClass("disabled");
        }
        else {
            $("#btnContinue").addClass("disabled");
        }
    }
    self.btnRFAClinicalReasonforDecision = function () {

        $.post("/PeerReview/SaveRequestClinicalReason", { reason: $("<div>").text($("#Editor1").val()).html(), requestID: _requestID, referralID: model.Patients.RFAReferralID }
            , function (model) {
                if (model) {
                    $("#btnContinue").removeClass("disabled");
                }
                else {
                    $("#btnContinue").addClass("disabled");
                }
        });
        
        $("#ClinicalReason_" + _requestID()).val($("<div>").text($("#Editor1").val()).html());

        if (self.RFAClinicalReasonforDecision() == "")
            self.RFAClinicalReasonforDecision(null);

        unblockPopupBackground();
    };
    self.closeClinicalReasonforDecision = function () {
        unblockPopupBackground();
    }
}