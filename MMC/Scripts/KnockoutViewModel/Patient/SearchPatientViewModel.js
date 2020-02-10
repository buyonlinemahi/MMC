function SearchViewModel(model) {
    var self = this;

    self.TotalItemCount = ko.observable(0);
    self.PatientSearchResults = ko.observableArray();  
    self.TotalItemCount(model.TotalCount);

    self.PatientSearchName = function () {
        if ($('#_SearchTextByName').val().trim() == "") {
            $('#_SearchTextByName').focus();
            alertify.alert("Enter any Patient or Claim.");
        }
        else {            
            GrdBinding();         
            self.Pager().CurrentPage(1);         
        }
    };
    var mappingOptions = {
        'PatDOB': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM-DD-YYYY");
            }
        },
        'PatDOI': {
            create: function (options) {                
                if (options.data != null)
                    return moment(options.data).format("MM-DD-YYYY");
            }
        }
    }
    ko.mapping.fromJS(model.PatientDetails, mappingOptions, self.PatientSearchResults);
    function GrdBinding() {
        var _searchtext = $('#_SearchTextByName').val();        
        $.post("/Patient/GetPatientByName", {
            _searchText: _searchtext, _skip: $('#hidskip').val()
        }, function (_data) {            
            var model = $.parseJSON(_data);
            self.PatientSearchResults.removeAll();
            ko.mapping.fromJS(model.PatientDetails, mappingOptions, self.PatientSearchResults);
            self.TotalItemCount(model.TotalCount);            
        });
    }

    self.DeletePatientByID = function (_data) {
        if (_data.PatientClaimID() != 0) {
            alertify.confirm("Are you sure to delete this Patient Claim?", function (e) {
                if (e) {
                    DeletePatientOrPatientClaim(_data.PatientID(), _data.PatientClaimID());
                }
            });
        }
        else {
            alertify.confirm("Are you sure to delete this Patient?", function (e) {
                if (e) {
                    DeletePatientOrPatientClaim(_data.PatientID(), _data.PatientClaimID());
                }
            });
        }
    }
    function DeletePatientOrPatientClaim( PatientID , PatientClaimID) {
        $.post("/Patient/DeletePatientByID", { _patientID: PatientID, _patientClaimID: PatientClaimID }, function (_responseText) {
            if (_responseText) {
                alertify.alert(_responseText);
                if (_responseText != "Error") {
                    GrdBinding();
                }
                if ((self.TotalItemCount() % $('#hidskip').val()) == 1)
                    self.Pager().CurrentPage(self.Pager().CurrentPage() - 1);
                else
                    self.Pager().CurrentPage();
            }
        });
    }

    self.GetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined)
        {
            self.Skip(0);
            self.Take(pagingSetting.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        GrdBinding();
    }
    var pagingSetting = {
        pageSize: 20,
        pageSlide: 2
    };

    self.Skip = ko.observable(0);
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

    $("#_SearchTextByName").keypress(function (e) {//To Search when enter is pressed
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            self.PatientSearchName();
        }
    });
}