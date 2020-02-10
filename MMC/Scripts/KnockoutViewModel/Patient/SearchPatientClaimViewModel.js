
function SearchPatientClaimViewModel() {
    var self = this;
    
    self.TotalItemCount = ko.observable(0);
    self.PatientClaimSearchResults = ko.observableArray();  
    var mappingOptions = {
        'PatDOI': {
            create: function (options) {
                if (options.data != '/Date(-62135596800000)/')
                    return moment(options.data).format("MM-DD-YYYY ");
            }
        }

    }
    
    self.GrdBinding = function () {
        var _patientID = $("#PatientID").val();        
        $.post("/Patient/GetpatientClaimsByPatientID", {
            _patientID: _patientID, _skip: ($("#chidskip").val() == "" ? 0 : $("#chidskip").val())
        }, function (_data) {
            var model = $.parseJSON(_data);
            self.PatientClaimSearchResults.removeAll();
            ko.mapping.fromJS(model.PatientClaimResults, mappingOptions, self.PatientClaimSearchResults);
            self.TotalItemCount(model.TotalCountClaim);
        });        
    };

    self.GetRecordsWithSkipTake = function (skip, take) {        
        if (skip == undefined || take == undefined) {
            self.cSkip(0);
            self.Take(pagingSetting.pageSize);
        }
        else {
            self.cSkip(skip);
            self.Take(take);
        }
        GrdBinding();
    }
    var pagingSetting = {
        pageSize: 20,
        pageSlide: 2
    };

    self.cSkip = ko.observable();
    self.Take = ko.observable(pagingSetting.pageSize);
    self.Pager = ko.pager(self.TotalItemCount);

    self.Pager().PageSize(pagingSetting.pageSize);
    self.Pager().PageSlide(pagingSetting.pageSlide);
    self.Pager().CurrentPage(1);

    self.Pager().CurrentPage.subscribe(function () {
        var skip = pagingSetting.pageSize * (self.Pager().CurrentPage() - 1);
        var take = pagingSetting.pageSlide;
        self.GetRecordsWithSkipTake(skip, take);
    });

    $("#btnpatientClaims").click(function () {
        self.backtoClaimSearch();
    });
    self.backtoClaimSearch = function () {        
        $("#ClaimDemographyDivMain").hide();
            $("#PatientClaimDiv").show();
            self.GrdBinding();                
    }
}

var _savePatientClaimViewModel = ko.observable();

function bindClaimDemography(PatientClaimID) {
    if (PatientClaimID != 0) {        
        var url = "/Patient/getPatientClaimByID/";
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            data: { 'patientClaimNumberID': PatientClaimID },
            success: function (data) {
                $("#HDPatientClaimID").val(data.PatientClaimDetails.PatientClaimID);
                $('#HDFPatientClaimID').val(data.PatientClaimDetails.PatientClaimID);
                switchToClaimDemography();
                _savePatientClaimViewModel.Init(data);                
            },
            error: function (reponse) {
                alert("error : " + reponse);
            }
        });
    }
    else {
        
            $("#PatientClaimDiv").css("display", "none");
            $("#ClaimDemographyDivMain").css("display", "block");
            $("#PatientClaimID").val(0);
            _savePatientClaimViewModel.Init(null);
           
    }
}

function switchToClaimDemography(){
    $("#PatientClaimDiv").hide();
    $("#ClaimDemographyDivMain").fadeIn();
}


