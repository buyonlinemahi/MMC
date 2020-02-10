function CurrentMedical(CurrentMedicalName, CurrentMedicalId, PatCurrentMedicalConditionId,PatientID) {
    var self = this;
    self.PatCurrentMedicalConditionName = ko.observable(CurrentMedicalName);
    self.CurrentMedicalConditionId = ko.observable(CurrentMedicalId);
    self.PatCurrentMedicalConditionId = ko.observable(PatCurrentMedicalConditionId)
    self.PatientID = ko.observable(PatientID);
    
}

function SavePatientDetailViewModel(model) {
    var self = this;

    self.PatientID = ko.observable();
    self.PatFirstName = ko.observable();
    self.PatLastName = ko.observable();
    self.PatSSN = ko.observable();
    self.PatDOB = ko.observable();
    self.PatGender = ko.observable();
    self.PatAge = ko.observable();
    self.PatAddress1 = ko.observable();
    self.PatAddress2 = ko.observable();
    self.PatCity = ko.observable();
    self.PatStateId = ko.observable();
    self.PatZip = ko.observable();
    self.PatPhone = ko.observable();
    self.PatFax = ko.observable();
    self.PatEMail = ko.observable();
    self.PatEthnicBackground = ko.observable();
    self.PatPrimaryLanguageId = ko.observable();
    self.PatSecondaryLanguageId = ko.observable();
    self.PatMaritalStatus = ko.observable();
    self.PatMedicareEligible = ko.observable("No");
    //self.PatCurrentMedicalConditionIds = ko.observable();
    self.PatientClaimID = ko.observable();

    // Patient Current Medical Conditions
    self.PatCurrentMedicalConditionId = ko.observable();
    self.CurrentMedicalConditionId = ko.observable();
    self.PatCurrentMedicalConditionName = ko.observable();


    var _totalCount = 0;
    //var _PatCurrentMedicalConditionIds = "";

    // Current Medical Condition
    self.PatCurrentMedicalConditionName = ko.observable();
    self.CurrentMedicalConditionId = ko.observable();
    self.CurrentConditionResults = ko.observableArray();
    self.TotalItemCount = ko.observable(0);

    //State
    self.State = ko.observable();
    self.States = ko.observableArray();
    self.States = ko.observableArray([self.States(0)]);
    self.selectedItemState = ko.observable(0);

    //Primary Language
    self.PatPrimaryLanguage = ko.observable();
    self.PatPrimaryLanguages = ko.observableArray();
    self.PatPrimaryLanguages = ko.observableArray([self.PatPrimaryLanguages(0)]);
    self.selectedItem = ko.observable(0);

    //Secondary Language
    self.PatSecondaryLanguage = ko.observable();
    self.PatSecondaryLanguages = ko.observableArray();
    self.PatSecondaryLanguages = ko.observableArray([self.PatSecondaryLanguages(0)]);
    self.selectedItemSeconday = ko.observable(0);



    //State
    self.State = ko.observable();
    self.States = ko.observableArray();
    self.States = ko.observableArray([self.States(0)]);
    self.selectedItemState = ko.observable(0);


    //CurrentMedicalCondition
    self.CurrentMedicalCondition = ko.observable();
    self.CurrentMedicalConditions = ko.observableArray();
    self.CurrentMedicalConditions = ko.observableArray([self.CurrentMedicalConditions(0)]);
    self.selectedItemCurrentMedicalConditions = ko.observable(0);


    $(document).ready(function () {
       showLoader();
        if (self.PatientClaimID() != undefined) {
            $("#HDPatientClaimID").val(self.PatientClaimID());            
        }       
          
            $.getJSON("/Common/getLanguageAll", function (data1) {
                self.PatPrimaryLanguages(data1.slice());
                self.selectedItem(1);
                if (model != null) {
                    if (model.PatientDetails != null)
                        self.selectedItem(model.PatientDetails.PatPrimaryLanguageId);
                }
            });
            $.getJSON("/Common/getLanguageAll", function (data2) {
                self.PatSecondaryLanguages(data2.slice());
                self.selectedItemSeconday(17);
                if (model != null) {
                    if (model.PatientDetails != null)
                        self.selectedItemSeconday(model.PatientDetails.PatSecondaryLanguageId);
                }
            });
            $.getJSON("/Common/getStates", function (data3) {
                self.States(data3.slice());
                if (model != null) {
                    if (model.PatientDetails != null)
                        self.selectedItemState(model.PatientDetails.PatStateId);
                }
            });
            $.getJSON("/Common/getCurrentMedicalConditionAll", function (data4) {
                self.CurrentMedicalConditions(data4.slice());
                if (model != null) {
                    if (model.PatientDetails != null)
                        self.selectedItemCurrentMedicalConditions(model.CurrentMedicalConditionId);
                }
            });
        
        var _patID = $("#PatientID").val();
        if (_patID == 0)
            $(".DivHide").fadeOut();
        else
            $(".DivHide").removeClass("hide").fadeIn();
        hideLoader();
    });



    $('.OpenPop').click(function () {
      blockPopupBackground();
    });

    $('.ClosePop').click(function () {
      unblockPopupBackground();
    });

    $('#PatDOB').on('change', function () {       
        var _year = calculateAge($('#PatDOB').val());       
        $('#PatAge').val(_year);
        if (_year >= 65) {
            $("#PatMedicareEligible").val("Yes");
        }
        else {
            $("#PatMedicareEligible").val("No");
        }
    })

    
    if (model != null) {
        if (model.PatientDetails != null) {
            ko.mapping.fromJS(model.PatientDetails, {}, self);
            self.PatDOB(moment(model.PatientDetails.PatDOB).format("MM/DD/YYYY"));
            var _year = calculateAge(self.PatDOB());
            $('#PatAge').val(_year);
            self.PatAge($('#PatAge').val());
        }
        if (model.PatCurrentMedicalConditionsDetails != null) {
            ko.mapping.fromJS(model.PatCurrentMedicalConditionsDetails, {}, self.CurrentConditionResults);
            ko.mapping.fromJS(model.TotalCount, {}, self.TotalItemCount);
            _totalCount = model.TotalCount;
        }
    };

    
  
    self.AddPatientSuccess = function (responseText) {
        hideLoader();
        $("#PatientID").val(responseText[0]);
        switch (responseText[1]) {
            case 0:
                alertify.alert("Error Occur");
                break;
            case 1:
                self.PatAge($('#PatAge').val());
                alertify.alert("Save Successfully");                
                $(".DivHide").removeClass("hide");
                $("#PatientMedicalRecordID").hide();
                CheckCurrentMedicalConditions(responseText[2]);
                break;
            case 2:
                self.PatAge($('#PatAge').val());
                alertify.alert("Update Successfully");
                CheckCurrentMedicalConditions(responseText[2]);
                break;

        };
    }

    function CheckCurrentMedicalConditions(cnt)
    {
        if (cnt == -1)
            $("#PatMedicareEligible").val("Yes");
        else
            $("#PatMedicareEligible").val("No");
    }

    self.AddNewCurrentMedicalConditionCode = function () {        
        if ($("#CurrentMedicalConditionName").val() != "") {
            showLoader();
            var _CurrentMedicalConditionName = $("#CurrentMedicalConditionId").find("option:selected").text();
            var _CurrentMedicalConditionId = $("#CurrentMedicalConditionId").val();
            var _patientID = $("#PatientID").val();

            var viewModelCurrentConditionsDetails = {                
                CurrentMedicalConditionId: _CurrentMedicalConditionId,
                PatCurrentMedicalConditionId: 0,
                PatCurrentMedicalConditionName:_CurrentMedicalConditionName,
                PatientID: _patientID                
            }
            var plainJs = ko.toJS(viewModelCurrentConditionsDetails);
   
            $.ajax({
                url: "/Patient/SavePatientCurrentMedicalConditions",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify(plainJs),
                success: function (data) {
                    if (data != 0) {

                        var index = data.indexOf("#");
                        if (index != -1) {
                            var results = data.split("#");
                            if (results[1] == "1")
                                $("#PatMedicareEligible").val("Yes");
                        }
                        $.post("/Patient/GetPatientMedicalConditionDetail", {
                            id: $("#PatientID").val()
                        }, function (model) {
                            hideLoader();
                            if (model.PatCurrentMedicalConditionsDetails != null) {
                                self.CurrentConditionResults.removeAll();
                                ko.mapping.fromJS(model.PatCurrentMedicalConditionsDetails, {}, self.CurrentConditionResults);
                                ko.mapping.fromJS(model.TotalCount, {}, self.TotalItemCount);
                                _totalCount = model.TotalCount;
                                $('#addCurrentMedicalCondition').modal('hide');
                                self.Pager().CurrentPage(1);
                                self.Skip = ko.observable(0);
                                unblockPopupBackground();
                                alertify.alert("Save Successfully");
                            }
                        });
                    }
                    else {
                        alert("Error Occur")
                    }
                },
                error: function (data) {
                    alert("Error Occur");
                }
            });

        }
    };

    self.removeCurrentMedicalCondition = function (item) {
        alertify.confirm("Are you sure you want to delete this record?", function (e) {
            if (e) {
                if (item.PatCurrentMedicalConditionId() != 0) {
                    showLoader();
                    $.post("/Patient/DeletePatientCurrentMedicalConditions", {
                        _patientCurrentMedicalConditionID: item.PatCurrentMedicalConditionId()
                    }, function (data) {
                        if (data != 0) {
                            var index = data.indexOf("#");
                            if (index != -1) {
                                var results = data.split("#");
                                if (results[0] == "y")
                                    $("#PatMedicareEligible").val("Yes");
                                else
                                    $("#PatMedicareEligible").val("No");
                            }
                            $.post("/Patient/GetPatientCurrentMedicalConditions", {
                                _id: $("#PatientID").val(), _skip: $('#hidskip').val()
                            }, function (model) {
                                if (model.PatCurrentMedicalConditionsDetails != null) {
                                    self.CurrentConditionResults.removeAll();
                                    ko.mapping.fromJS(model.PatCurrentMedicalConditionsDetails, {}, self.CurrentConditionResults);
                                    ko.mapping.fromJS(model.TotalCount, {}, self.TotalItemCount);
                                    _totalCount = model.TotalCount;
                                    var _skipValue = $('#hidskip').val();
                                    var _totalCount = self.TotalItemCount();

                                    if ((_skipValue % _totalCount) == 0)
                                        self.Pager().CurrentPage(1);
                                }
                            });
                            self.Pager().CurrentPage(1);
                            self.Skip = ko.observable(0);
                            alertify.alert("Deleted Successfully");
                        }
                        else {
                            alert("Error Occur")
                        }

                    });
                    hideLoader();
                }
            }
        });
    }

    self.AddPatientFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        showLoader();
        return true;
    }


    function GrdBinding() {
        showLoader();
        var _patientID = $('#PatientID').val();
        $.post("/Patient/GetPatientCurrentMedicalConditions", {
            _id: _patientID, _skip: $('#hidskip').val()
        }, function (model) {
            self.CurrentConditionResults.removeAll();
            ko.mapping.fromJS(model.PatCurrentMedicalConditionsDetails, {}, self.CurrentConditionResults);
            self.TotalItemCount(model.TotalCount);
            hideLoader();
        });
    }

    self.GetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSetting.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
            $('#hidskip').val(skip);
        }
        GrdBinding();
    }
    var pagingSetting = {
        pageSize: 10,
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


       
};