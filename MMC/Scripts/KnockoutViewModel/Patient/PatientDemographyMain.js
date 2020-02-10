var checkClaimSearch = 0;
var checkClaimDemo = 0;
function bindTab(index) {    
    getDivLoad(index);    
}

function getDivLoad(index) {
    switch (index) {
        case 1:
            if (checkClaimSearch == 0) {
                var _searchViewModel = new SearchPatientClaimViewModel();
                ko.applyBindings(_searchViewModel, $("#PatientClaimDiv").get(0));
                if ($("#HDPatientClaimID").val() != 0)
                    bindClaimDemography($("#HDPatientClaimID").val());
                else
                    _searchViewModel.backtoClaimSearch();

                checkClaimSearch = 1;
            }
            else {
                if ($("#HDPatientClaimID").val() != 0)
                    switchToClaimDemography();
                else
                    _searchViewModel.backtoClaimSearch();
            }
            break;
        case 2:
            var hdClaimID = $("#PatientID").val();
            _patientOccupational = new SavePatientOccupationalViewModel(hdClaimID);
            ko.applyBindings(_patientOccupational, $("#PatientOccupationalDetailDiv").get(0));
            SavePatientOccupationalViewModel();          
            _url = "/Patient/PatientDemographics/";
            break;
        case 3:
            _url = "/Patient/PatientDemographics/";
            break;
        case 4:
            {
                _patientHistoryViewModel = new PatientHistoryViewModel();
                ko.applyBindings(_patientHistoryViewModel, $("#PatientHistoryDiv").get(0));
                PatientHistoryViewModel();
                _url = "/Patient/PatientDemographics/";
                break;
            }
        case 5:
            _url = "/Patient/PatientDemographics/";
            break;
    }
    
}

$("#PrintPatientMedicalRecord").click(function () {
    var _patientID = $("#PatientID").val();
    $.post("/PatientMedicalRecord/GeneratePatientMedRecordTemplateByPatientID", {
        _patientID: _patientID
    }, function (_data) {
        var model = $.parseJSON(_data);
    });
});