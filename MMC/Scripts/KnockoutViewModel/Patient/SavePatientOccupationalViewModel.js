function SavePatientOccupationalViewModel(PatientID) {
    var self = this;    
    

    $(document).ready(function () {
        var test = PatientID;
       if(test > 0)
           bindOccupationalDemography(PatientID);
    });

    function bindOccupationalDemography(PatientID) {
        showLoader();
        if (PatientID != 0) {
            var url = "/Patient/GetPatientOccupationalByPatientClaimID/";
            $.ajax({
                url: url,
                cache: false,
                type: "POST",
                data: { 'patientID': PatientID },
                success: function (data) {
                    $("#PatientID").val(data.PatientID);
                    $("#PatientOccupationalID").val(data.PatientOccupationalID);
                    $("#PatJobtitle").val(data.PatOptJobTitle);
                    $("#PatJobDesc").val(data.PatOptJobDescription);
                    $("#PatInjuryType").val(data.PatOptInjuryType);
                    $("#PatInjuryDescription").val(data.PatOptInjuryDescription);
                }
            });
            hideLoader();
        }
    } 

    self.AddNewOccupationalClaimStatus = function () {
        showLoader();
        var viewModelOccupational = {
            PatientID: $('#PatientID').val(),
            PatientOccupationalID: $("#PatientOccupationalID").val(),
            PatOptJobTitle:$('#PatJobtitle').val(),
            PatOptJobDescription:$('#PatJobDesc').val(),
            PatOptInjuryDescription: $('#PatInjuryType').val(),
            PatOptInjuryType: $('#PatInjuryDescription').val()            
        }
        showLoader();
            $.ajax({
                url: "/Patient/SavePatientOccupational",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify(viewModelOccupational),
                success: function (data) {
                    hideLoader();
                    if ($("#PatientOccupationalID").val() != 0) {
                        alertify.alert("Update Successfully");//, function (e) {
                    }
                    else {
                        alertify.alert("Save Successfully");//, function (e) {
                        bindOccupationalDemography($('#PatientID').val());
                    }
                },
                error: function (data) {
                    hideLoader();
                    alertify.alert("Error Occur");
                }
            });
           

        }    
}

