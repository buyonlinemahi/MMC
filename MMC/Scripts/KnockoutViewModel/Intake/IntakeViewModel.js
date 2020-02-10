function IntakeFileUploadViewModel() {
    if (window.location.pathname.split('/').length == 5) {
        //file upoliad tab
        document.getElementById("uploadFile").disabled = true
        // intale cliam search tab
        $('.diableInput').attr('disabled', 'disabled');
        $('.readonlyInput').attr('readonly', 'readonly');
        $('.diableBtn').attr('disabled', 'disabled');
        $('.diableBtn').addClass("btn-info");
        $("td .diableAnchor").hide();
        //$("td a").off("click");// .removeAttr("href")
        $("td .spandisable").removeClass("spanHide");
    }
    else {
        //file upoliad tab
        document.getElementById("uploadFile").disabled = false;
        // intale cliam search tab
        $('.diableInput').removeAttr('disabled');
        $('.readonlyInput').removeAttr('readonly');
        $('.diableBtn').removeAttr('disabled');
        $('.diableBtn').removeClass("btn-info");
        $("td .spandisable").addClass("spanHide");
        $("td .diableAnchor").show();
    }

    var self = this;
    self.RFACPT_NDC = ko.observable();
  
    self.Init = function (model) {
        if (model != null) {
            ko.mapping.fromJS(model, {}, self);
            $('#objectPDF').attr('src', self.RFAReferralFileFullPath());
            showTabsByMaxProcessLevel(self.ProcessLevel())
        }
    };

    self.IntakeFileInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        showLoader();
        return true;
    }
    self.UploadIntakeFileSuccess = function (response) {
        var data = $.parseJSON(response);
        ko.mapping.fromJS(data, {}, self);
        $('#objectPDF').attr('src', data.RFAReferralFileFullPath);
        hideLoader();
        alertify.success("Saved Sucessfully");
        if (data.Mode != "Add") {
            location.reload();
        }
        //bind referral id to rfa tab..hp
        _intakeRFAViewModel.bindReferralID(self.RFAReferralID);
        showTabsByProcessLevel(20);
    }

    


}


function IntakeRFAViewModel() {

  


    var self = this;
    
    self.RequestIMRRecordResults = ko.observableArray();
    self.cdTotalItemCount = ko.observable(0);
    self.TimeValue = ko.observableArray([{ TimeVal: "AM", Time: 'AM' }, { TimeVal: "PM", Time: 'PM' }]);
    self.TimeSel = ko.observable();
    self.PatClaimNumber = ko.observable();
    var mappingOptions = {
        'RFAReferralDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        },
        'RFACEDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        },
        'EvaluationDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        },
        'RFAHCRGDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        }
    }

    self.Init = function (model) {
        if (model != null) {
            ko.mapping.fromJS(model, mappingOptions, self);
            self.TimeSel(model.RFATimeValue);
            $.post("/Intake/GetpatientClaimByID", { claimID: model.PatientClaimID }, function (data) {
                self.PatClaimNumber(data);
            });
         _intakeViewModel.ClaimID(self.PatientClaimID());
            _intakeViewModel.PatientID(self.PatientID());
       
        }
    };

    self.bindReferralID = function (rfaReferralD) {
        if (rfaReferralD != null) {
            self.RFAReferralID(rfaReferralD());
        }
    };


    self.RFAIntakeInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    self.UpdateRFAIntakeSuccess = function (response) {
        alertify.success(response);
        showTabsByProcessLevel(40);
    }

//POP UP AP
    var SelectedRefferalFlags = "";
    self.SearchRequestByPatientClaim = function () {
        $("#divuploadFile").hide();
        blockPopupBackground();
        SelectedRefferalFlags = "";
        $('#btnSendToIMR').html('Save');
         GrdtRequestIMRRecordsBinding(0);
            };

       function GrdtRequestIMRRecordsBinding(skip) {
           showLoader();
           var _searchtext = "";
           if ($('#claimNo').text() == "")
           {
               _searchtext = self.PatClaimNumber();
           }
           else
           {
               _searchtext = $('#claimNo').text();
           }
                    
       
        $.post("/IMR/GetRequestIMRRecordByPatientClaim", {
            _searchText: _searchtext, _skip: skip
            }, function (_data) {
                var model = $.parseJSON(_data);
                    if (model.TotalCount != 0) {
                    self.RequestIMRRecordResults.removeAll();
                    ko.mapping.fromJS(model.RequestIMRRecordDetails, { }, self.RequestIMRRecordResults);
                    self.cdTotalItemCount(model.TotalCount);
                    }
                    else {

                    self.RequestIMRRecordResults.removeAll();
                    self.cdTotalItemCount(0);
                    }
                    });
        hideLoader();
        }



    self.SendToIMEProcessLevel = function () {
        var RefIDs = "";
        $('.unique:checked').each(function () {
            var ref = $(this).val().split('/')
            RefIDs += ref[1] + "#";
            });
        if (RefIDs != "") {
            $.post("/IMR/SaveReferralIARecord", {
                _rFAReferralIDs: RefIDs,
                _newReferral: $('#RFAReferralID').val()
            }, function(_data) {
                if (_data != null) {
                    var message = $.parseJSON(_data);
                    alertify.alert(message, function (e) {
                        if (e) {
                          //  GrdtRequestIMRRecordsBinding(0);
                            $('.modal.in').modal('hide');
                            unblockPopupBackground();
                            SelectedRefferalFlags = "";
                            $("#divuploadFile").show();

                            }
                    });
                }
                            });
        }
        else {
            alertify.alert("Please select any Request(s) to proceed.");
                            }
    };
   
    self.ClosePopup = function () {
        $('.modal.in').modal('hide');
        unblockPopupBackground();
        SelectedRefferalFlags = "";
        $("#divuploadFile").show();
        }
             

    var cnt = 0;

    self.SelectRefferalGroup = function (data) {
        var groupFlags = data.RFAReferralID()
        if (SelectedRefferalFlags == "")
            SelectedRefferalFlags = groupFlags;

        if (groupFlags == SelectedRefferalFlags) {
            if (!$("#" +data.RFARequestID()).prop('checked')) {
                $("#" +data.RFARequestID()).prop('checked', false);
                cnt--;
                if(cnt == 0)
                    SelectedRefferalFlags = "";
                return true;
                    }
            $("#" +data.RFARequestID()).prop("checked", true);
            cnt++;
            }
            else {
            $('input[data-group="' +groupFlags + '"]:checked').prop('checked', false);
            alertify.alert("you can select only Request(s) for Refferal i.e." +SelectedRefferalFlags);
            }
        return true;
        };




            //**-----------------------------Paging Pages------------------------------------**//

    //========= paging code for claim diagnosis grid only ===========//

    self.cdGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.cdSkip(0);
            self.cdTake(cdpagingSetting.cdpageSize);
        }
        else {
            self.cdSkip(skip);
            self.cdTake(take);
            }
        GrdtRequestIMRRecordsBinding(skip);

        }

    var cdpagingSetting = {
                cdpageSize: 20,
                cdpageSlide: 2
                };
            self.cdSkip = ko.observable(0);
    self.cdTake = ko.observable(cdpagingSetting.cdpageSize);
        self.cdPager = ko.cdpager(self.cdTotalItemCount);

            self.cdPager().cdPageSize(cdpagingSetting.cdpageSize);
            self.cdPager().cdPageSlide(cdpagingSetting.cdpageSlide);
    self.cdPager().cdCurrentPage(1);

    self.cdPager().cdCurrentPage.subscribe(function() {
        var skip = cdpagingSetting.cdpageSize * (self.cdPager().cdCurrentPage() - 1);
        var take = cdpagingSetting.cdpageSlide;
        self.cdGetRecordsWithSkipTake(skip, take);
        });

    self.cdLastPage = ko.computed(function () {
        return Math.floor((self.cdTotalItemCount() -1) / cdpagingSetting.cdpageSize) +1;
        });

                // ============= paging code for claim diagnosis ends =============== //

                }

                    //////========== pager js for   claim diagnosis  grid only ========//
(function(ko) {
    var cdnumericObservable = function (initialValue) {
        var _actual = ko.observable(initialValue);

        var result = ko.dependentObservable({
                    read: function() {
                return _actual();
                },
                write: function (newValue) {
                    var parsedValue = parseFloat(newValue);
                    _actual(isNaN(parsedValue) ? newValue: parsedValue);
                    }
                    });

        return result;
        };

    function cdPager(totalItemCount) {
        var self = this;
        self.cdCurrentPage = cdnumericObservable(1);

        self.cdTotalItemCount = ko.computed(totalItemCount);

        self.cdPageSize = cdnumericObservable(10);
        self.cdPageSlide = cdnumericObservable(2);

        self.cdLastPage = ko.computed(function () {
            return Math.floor((self.cdTotalItemCount() -1) / self.cdPageSize()) +1;
            });

        self.cdHasNextPage = ko.computed(function () {
            return self.cdCurrentPage() < self.cdLastPage();
            });

        self.cdHasPrevPage = ko.computed(function() {
            return self.cdCurrentPage() > 1;
            });

        self.cdFirstItemIndex = ko.computed(function () {
            return self.cdPageSize() * (self.cdCurrentPage() -1) +1;
        });

        self.cdLastItemIndex = ko.computed(function () {
            return Math.min(self.cdFirstItemIndex() +self.cdPageSize() -1, self.cdTotalItemCount());
            });

        self.cdPages = ko.computed(function () {
            var cdpageCount = self.cdLastPage();
            var cdpageFrom = Math.max(1, self.cdCurrentPage() -self.cdPageSlide());
            var cdpageTo = Math.min(cdpageCount, self.cdCurrentPage() +self.cdPageSlide());
            cdpageFrom = Math.max(1, Math.min(cdpageTo -2 * self.cdPageSlide(), cdpageFrom));
            cdpageTo = Math.min(cdpageCount, Math.max(cdpageFrom +2 * self.cdPageSlide(), cdpageTo));

            var result =[];
            for(var i = cdpageFrom; i <= cdpageTo; i++) {
                result.push(i);
                }
            return result;
            });
        }

    ko.cdpager = function (totalItemCount) {
        var cdpager = new cdPager(totalItemCount);
        return ko.observable(cdpager);
        };
        }(ko));
        //////============= pager js for claim diagnosis ends  ============//


$(document).ready(function () {
    activaTab();

    function activaTab() {
        $("#tabFileUpload").tab('show');
    };
    var flag = false;
    
    $("#tabrfaRequest").click(function () {
       
        if (flag == true)
        {
            $('#btnRebind').click();
        }
      
    else
        {
            showLoader();
          $.ajax({
              url: '/Intake/getRFARequestRecords',
              cache: false,
              type: "GET",
              data: { '_rfaReferralID': $("#RFAReferralID").val() },
              success: function (data) {
             
                  var _intakeRFARequestViewModel = new IntakeRFARequestViewModel();
                  _intakeRFARequestViewModel.Init(data);
                  _intakeRFARequestViewModel.bindReferralID($("#RFAReferralID").val());
                  ko.applyBindings(_intakeRFARequestViewModel, $("#divRFARequest").get(0));
                  flag = true;
                  // $("#tabrfaRequest").off("click");                
                  //$("#CPTTags").tagsInput();
                  hideLoader();
              },
              error: function (reponse) {
                  alert("error : " + reponse);
                  hideLoader();
              }
          });
        }
      
    });

    $("#tabPhysician").click(function () {
        var _searchViewModel = new SearchViewModel();
        _searchViewModel.bindSpecialityDDL();
        ko.applyBindings(_searchViewModel, $("#PhysicianSearchDiv").get(0));

        _intakePhysicianViewModel.bindReferralID($("#RFAReferralID").val());

        $("#tabPhysician").off("click");
    });


    $("#tabrfaRecordSplt").click(function () {
        showLoader();
        $.ajax({
            url: '/Intake/getRFARecordSplit',
            cache: false,
            type: "GET",
            data: { '_rfaReferralID': $("#RFAReferralID").val() },
            success: function (data) {

                var _intakeRFARecordSplitViewModel = new IntakeRFARecordSplitViewModel();
                _intakeRFARecordSplitViewModel.Init(data);
                _intakeRFARecordSplitViewModel.bindReferralID(_intakeRFAViewModel.RFAReferralID());
                ko.applyBindings(_intakeRFARecordSplitViewModel, $("#divRFARecordSplt").get(0));

                $("#tabrfaRecordSplt").off("click");
                hideLoader();
            },
            error: function (reponse) {
                alert("error : " + reponse);
                hideLoader();
            }
        });

    });

    $("#icdICDNumber").bind("focusout", function () {
        if ($("#icdICDNumberID").val() == "") {
            $("#icdICDNumber").val('');
        }
    });
});

function showTabsByMaxProcessLevel(processLevel) {
    for (var i = 20; i <= processLevel; i = i + 10) {
        showTabsByProcessLevel(i);
    }
}
function showTabsByProcessLevel(processLevel) {
    switch (processLevel) {
        case 20:
            $("#tabIntakeClaim").removeClass("hide");
            break;
        case 30:
            $("#tabIntakeRFA").removeClass("hide");
            break;
        case 40:
            $("#tabPhysician").removeClass("hide");
            break;
        case 50:
            $("#tabrfaRequest").removeClass("hide");
            break;
        case 60:
            $("#tabrfaRecordSplt").removeClass("hide");
            break;
    }
}

function IntakePhysicianViewModel() {
    var self = this;
    self.PhysicianName = ko.observable();
    self.PhysicianId = ko.observable();
    self.PhysSpecialtyName = ko.observable();
    self.PhysEMail = ko.observable();
    self.PhysZip = ko.observable();
    self.RFAReferralID = ko.observable();

    self.IntakePhysicianInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {            
            $("#divuploadFile").hide();
            alertify.alert("Select Physician first!", function () {
                $("#divuploadFile").show();
            });
            return false;
        }
        return true;
    }


    $('#SubmitFromFARequestID').click(function () {
        $("#frmRFAReqeust").submit();
        $("#btnClearGrid").click();
        $("#SearchTextByName").val('');
    });

    self.UpdatePhysicianSuccess = function (response) {
        alertify.success("Saved Sucessfully");
        showTabsByProcessLevel(50);
    }

    self.bindReferralID = function (rfaReferralD) {
        if (rfaReferralD != null) {
            $.post("/Intake/getPhysicianidByReferralID", {
                _rfaReferralID: rfaReferralD
            }, function (_data) {
                var objphysicianID = _data.PhysicianID;
                $.post("/Physician/GetPhysicianByID", {
                    _physicianID: objphysicianID
                }, function (item) {
                    self.PhysicianId(item.PhysicianId);
                    self.PhysicianName(item.PhysFirstName + ' ' + item.PhysLastName);
                    self.PhysSpecialtyName(item.PhysSpecialtyName);
                    self.PhysEMail(item.PhysEMail);
                    self.PhysZip(item.PhysZip);
                });
            });
            self.RFAReferralID(rfaReferralD);

        }
    };
    self.bindPhysician = function (item) {
        self.PhysicianId(item.PhysicianId());
        self.PhysicianName(item.PhysFirstName() + ' ' + item.PhysLastName());
        self.PhysSpecialtyName(item.PhysSpecialtyName());
        self.PhysEMail(item.PhysEMail());
        self.PhysZip(item.PhysZip());
    };
}

function IntakeRFARequestViewModel() {
    var self = this;
    self.filteredTreatmentTypes = ko.observableArray([]);
    self.copyEditItemRfaRequest = ko.observableArray([]);
    self.copyEditItemRfaCPTCode = ko.observableArray(null);
    self.OldRFAStrenght = ko.observable();
    self.Qty = ko.observable();
    self.RFACPT_NDC = ko.observable();
    self.flag = ko.observable(0);
    self.BodyPartDetailsByClaim = ko.observableArray([]);
    self.ReqBodyPart = ko.observable();
    self.IsBodyPartAtZeroPosition = ko.observable();
    self.Init = function (model) {
        if (model != null) {
            ko.mapping.fromJS(model, {}, self);

            self.TreatmentTypesFiltered = ko.computed(function () {
                return self.filterTreatmentTypeByCategory(self.rfaRequest.TreatmentCategoryID());
            });
            self.Qty = ko.computed(function () {
                var RFAFrequency = self.rfaRequest.RFAFrequency() == "" || self.rfaRequest.RFAFrequency() == null ? 1 : self.rfaRequest.RFAFrequency();
                var RFADuration = self.rfaRequest.RFADuration() == "" || self.rfaRequest.RFADuration() == null ? 1 : self.rfaRequest.RFADuration();
                return (RFAFrequency * RFADuration);
            }, self);
        }
        bindBodyPart();     
        self.IsBodyPartAtZeroPosition(true);
    };

    //Binding Body Part...
    function bindBodyPart() {
        $.post("/Intake/getAllBodyPartsForReqByClaimID", {
            _claimID: $('#claimIDHID').val()
        }, function (_data) {
            self.BodyPartDetailsByClaim.removeAll();
            //ko.mapping.fromJS(_data.BodyPartDetails, {}, self.BodyPartDetailsByClaim);
            self.BodyPartDetailsByClaim.push(new BodyPartInsertByClaimOnZero(value));
            $.each(_data.BodyPartDetails, function (index, value) {
                self.BodyPartDetailsByClaim.push(new BodyPartInsertByClaim(value));
            });
            $('#bodyPart').multiselect('destroy');
            $('#bodyPart').multiselect('refresh');
        });
    }


    function BodyPartInsertByClaim(value) {
        var self = this;
        self.BodyPartID = ko.observable(value.BodyPartID);
        self.BodyPartName = ko.observable(value.BodyPartName);
        self.BodyPartStatusName = ko.observable(value.BodyPartStatusName);
        self.TableName = ko.observable(value.TableName);
    }

    function BodyPartInsertByClaimOnZero(value) {
        var self = this; // This only use for inserting Unspecified at zero position .....mahi
        self.BodyPartID = ko.observable(47);
        self.BodyPartName = ko.observable("Unspecified");
        self.BodyPartStatusName = ko.observable("Accepted");
        self.TableName = ko.observable("Add On");
    }

    $('#btnRebind').click(function () {
        bindBodyPart();
    });

    
    self.CheckMultiSelectBind = function (obj, event) {
        //This is for checking change or not......mahi
        bindDropDownAtPositionZero(event);
    }

    function bindDropDownAtPositionZero(event) {
        if (event.originalEvent) { //user changed
            if (self.IsBodyPartAtZeroPosition() == true && self.IsBodyPartAtZeroPosition() != null) {
                $('#bodyPart').multiselect('select', ['47-Add On']);
                self.ReqBodyPart($("#bodyPart").val());
                self.IsBodyPartAtZeroPosition(false);
            }
        }
        else {// program changed
        }
    }


    $("#bodyPart").on("change", function () {
        if ($("#bodyPart").val() == null) {
            alertify.alert("Select Atleast one Body Part ");
            return false;
        }
        else {
            self.ReqBodyPart($("#bodyPart").val());
        }
      
    });
    //filters starts
    self.filterRequestType = function (_requestID) {
        return ko.utils.arrayFilter(self.reqeustTypes(), function (item) {
            return item.RequestTypeID() == _requestID;
        });
    };
    self.filterTreatmentCategories = function (_treatmentCategoryID) {
        return ko.utils.arrayFilter(self.treatmentCategories(), function (item) {
            return item.TreatmentCategoryID() == _treatmentCategoryID;
        });
    };
    self.filterTreatmentType = function (_treatmentTypeID) {
        return ko.utils.arrayFilter(self.TreatmentTypesFiltered(), function (item) {
            return item.TreatmentTypeID() == _treatmentTypeID;
        });
    };
    self.filterTreatmentTypeByCategory = function (_treatmentCategoryID) {
        return ko.utils.arrayFilter(self.treatementTypes(), function (item) {

            if (_treatmentCategoryID == 9) {
                $('#RFAStrenght').prop("disabled", false);
                self.rfaRequest.RFAStrenght(self.OldRFAStrenght());
            }
            else {
                $('#RFAStrenght').prop("disabled", true);
                self.OldRFAStrenght(self.rfaRequest.RFAStrenght());
                self.rfaRequest.RFAStrenght("");
            }
            return item.TreatmentCategoryID() == _treatmentCategoryID;
        });
    };
    //filters end

    self.bindReferralID = function (rfaReferralD) {
        if (rfaReferralD != null) {
            self.rfaRequest.RFAReferralID(rfaReferralD);
        }
    };

    self.RFARequestInfoFormBeforeSubmit = function (arr, $form, options) {       
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        if ($("#bodyPart").val() == null) {
            alertify.alert("Select Atleast one Body Part ");
            return false;
        }
        return true;
    }

    self.editRfaRequest = function (item) {
        $('#CPTTags').val(item.RFACPT_NDC());
        ko.mapping.fromJS(item, { }, self.rfaRequest);
        self.copyEditItemRfaRequest = item;
        $.ajax({
            url: '/Intake/getRFARequestBodyPartByRequestID',
            cache: false,
            type: "POST",
            data: {
                '_requestID': item.RFARequestID
            },
            success: function (data) {
                $('#bodyPart').multiselect('select', data);
                self.ReqBodyPart($("#bodyPart").val());
            },
            error: function (reponse) {
                alert("error : " + reponse);
            }
        });
       // getRFARequestBodyPartByRequestID//
      //  $('#bodyPart').multiselect('select', ['200-Plead', '157-Add On']);
    }

    self.UpdateRFARequestSuccess = function (response) {
        var data = $.parseJSON(response);
        data.RequestTypeDesc = self.filterRequestType(data.RequestTypeID)[0].RequestTypeDesc();
        data.TreatmentCategoryName = self.filterTreatmentCategories(data.TreatmentCategoryID)[0].TreatmentCategoryName();
        data.TreatmentTypeDesc = self.filterTreatmentType(data.TreatmentTypeID)[0].TreatmentTypeDesc();

        if (self.copyEditItemRfaRequest != null && self.copyEditItemRfaRequest.RFARequestID != undefined && self.copyEditItemRfaRequest.RFARequestID() > 0) {
            ko.mapping.fromJS(data, { }, self.copyEditItemRfaRequest);
            self.copyEditItemRfaRequest = null;
        }
        else
            self.rfaRequestsDetails.push(ko.mapping.fromJS(data));
        self.restRFARequestForm();
        alertify.success("Saved Sucessfully");
        $('#CPTTags').val('');
        self.RFACPT_NDC('');
        $("#bodyPart").val('');
        $('#bodyPart').multiselect('refresh');
        self.ReqBodyPart('');
        showTabsByProcessLevel(60);
        self.IsBodyPartAtZeroPosition(true);

    }

    self.deleteRfaRequest = function (item) {
        if (self.rfaRequestsDetails().length > 1) {
            $("#divuploadFile").hide();

            var flag = 0;
            if (window.location.pathname.split('/').length == 5)
                flag = 1;


            alertify.confirm("Are you sure to delete this record?", function (e) {
                if (e) {
                    $.ajax({
                        url: '/Intake/deleteRFARequest',
                        cache: false,
                        type: "POST",
                        data: {
                            '_rfaRequestID': item.RFARequestID, '_flag': flag
                        },
                        success: function () {
                            self.rfaRequestsDetails.remove(item);
                            alertify.success("Request Deleted Sucessfully");
                            $('.tagsinput').val('');
                            $("#divuploadFile").show();
                        },
                        error: function (reponse) {
                            alert("error : " + reponse);
                        }
                    });
                }
                else
                    $("#divuploadFile").show();
            });
        }
        else {
            $("#divuploadFile").hide();
            alertify.alert("One request is required!", function (e) {
                $("#divuploadFile").show();
            });
        }
    }


    self.restRFARequestForm = function () {
        self.rfaRequest.RFARequestedTreatment(null);
        self.rfaRequest.RFARequestID(null);
        self.rfaRequest.RFAFrequency(null);
        self.rfaRequest.RequestTypeID(1);
        self.rfaRequest.RFADuration(null);
        self.rfaRequest.RFAQuantity(1);
        self.rfaRequest.TreatmentCategoryID(null);
        self.rfaRequest.TreatmentTypeID(null);
        self.rfaRequest.RFAExpediteReferral(0);
        self.rfaRequest.RFACPT_NDC(null);
        self.rfaRequest.RFAStrenght(null);
    }

        //icd9 section code..hp
        //code for autocompete icd9 start..hp
    self.icdICDNumber = ko.observable();
    self.icdICDNumberID = ko.observable();
    self.RFACPTCodeID = ko.observable();

    self.getICD9Codes = function (request, response) {
        $("#icdICDNumberID").val("");
        var text = request.term;
        $.post('/Common/getICD9CodesByCode', { _searchtext: request.term
        }, function (data) {
            var d = data;
            response($.map(data, function (item) {
                return {
                        labelID: item.icdICDNumberID,
                        label: item.icdICDNumber.trim()
            };
            }));
        });
    };
    self.selectICD9 = function (event, ui) {
        self.icdICDNumberID(ui.item.labelID);
        self.icdICDNumber(ui.item.label);
    };
        //code for autocompete icd9 end..hp 

    self.RFAReferralCPTCodeInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
    }
        return true;
    }

    self.editRFAReferralCPTCode = function (item) {
        self.icdICDNumber(item.icdICDNumber())
        self.RFACPTCodeID(item.RFACPTCodeID());
        self.icdICDNumberID(item.RFACPTCodeID());
        self.copyEditItemRfaCPTCode = item;
    }

    self.UpdateRFAReferralCPTCodeSuccess = function (response) {
        var data = $.parseJSON(response);

        if (self.copyEditItemRfaCPTCode != null && self.copyEditItemRfaCPTCode.RFACPTCodeID != undefined && self.copyEditItemRfaCPTCode.RFACPTCodeID() > 0) {
            ko.mapping.fromJS(data, { }, self.copyEditItemRfaCPTCode);
            self.copyEditItemRfaCPTCode = null;
        }
        else
            self.rfaReferralCPTCodes.push(ko.mapping.fromJS(data));
        self.resetICDForm();
        alertify.success("Saved Sucessfully");
    }

    self.deleteRFAReferralCPTCode = function (item) {
        $.ajax({
                url: '/Intake/deleteRFAReferralCPTCode',
                cache: false,
                type: "POST",
                data: { '_rfaCPTCodeID': item.RFACPTCodeID
        },
                success: function () {
                self.rfaReferralCPTCodes.remove(item);
                alertify.alert("CPT Code Deleted Sucessfully");
        },
                error: function (reponse) {
                alert("error : " +reponse);
        }
        });
    }
    self.resetICDForm = function () {
        self.icdICDNumber(null)
        self.RFACPTCodeID(null);
    }


}

function IntakeRFARecordSplitViewModel() {
    var self = this;
    self.PatientID = ko.observable();
    self.PatientClaimID = ko.observable();
    self.RFAReferralID = ko.observable();
    self.uploadFilePages = ko.computed(function () { return _intakeFileUploadViewModel.TotalPages(); });
    self.RFAReferralFileName = ko.computed(function () { return _intakeFileUploadViewModel.RFAReferralFileName(); });

    var mappingOptions = {
        'RFARecDocumentDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        }
    }
    self.Init = function (model) {
        if (model != null) {
            ko.mapping.fromJS(model, mappingOptions, self);
        }
    }
    //filtter settings start..hp
    self.filterDocumentCategories = function (_documentCategoryID) {
        return ko.utils.arrayFilter(self.documentCategories(), function (item) {
            return item.DocumentCategoryID() == _documentCategoryID;
        });
    };
    //self.filterDocumentTypes = function (_documentTypeID) {
    //    return ko.utils.arrayFilter(self.documentTypes(), function (item) {
    //        return item.DocumentTypeID() == _documentTypeID;
    //    });
    //};
    //filtter settings end..hp

    self.RFARecordSplittingInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        else if (!((parseInt($("#RFARecPageStart").val()) > 0) && (parseInt($("#RFARecPageEnd").val()) <= self.uploadFilePages()))) {
            alertify.alert("Invalid Range of pages");
            return false;
        }
        else if (!((parseInt($("#RFARecPageEnd").val()) >= parseInt($("#RFARecPageStart").val())) && (parseInt($("#RFARecPageEnd").val()) <= self.uploadFilePages()))) {
            alertify.alert("Invalid Range of pages");
            return false;
        }
        return true;
    }

    self.UpdateRFARecordSplittingSuccess = function (response) {      
        var data = $.parseJSON(response);
        data.DocumentCategoryName = self.filterDocumentCategories(data.DocumentCategoryID)[0].DocumentCategoryName();
        //data.DocumentTypeDesc = self.DocumentTypes(data.DocumentTypeID)[0].DocumentTypeDesc();
        data.DocumentTypeDesc = $("#DocumentTypeID  option:selected").html();
        self.rfaRecordSplitingDetails.push(ko.mapping.fromJS(data, mappingOptions));
        self.restRFARecordSPLTForm();

        alertify.success("Saved Sucessfully");
    }

    self.bindReferralID = function (rfaReferralD) {
        if (rfaReferralD != null) {
            self.RFAReferralID(rfaReferralD);
        }
    };

    self.PatientClaimID = ko.computed(function () {
        return _intakeViewModel.ClaimID();
    });

    self.PatientID = ko.computed(function () {
        return _intakeViewModel.PatientID();
    });
    
    self.deleteRFARecordSplitting = function (item) {
        $("#divuploadFile").hide();
        alertify.confirm("Are you sure to delete this record?", function (e) {
            if (e) {
                $.ajax({
                    url: '/Intake/deleteRFARecordSplitting',
                    cache: false,
                    type: "POST",
                    data: { '_rfaRecSpltID': item.RFARecSpltID, 'patientID': $("#HDPatientID").val(), 'claimID': item.PatientClaimID, 'fileName': item.RFARecDocumentName },
                    success: function () {
                        self.rfaRecordSplitingDetails.remove(item);
                        alertify.success("Document Deleted Sucessfully");
                        $("#divuploadFile").show();
                    },
                    error: function (reponse) {
                        alert("error : " + reponse);
                    }
                });
            }
            else
                $("#divuploadFile").show();
        });
    }
    self.viewRecSpltFile = function (item) {
        $.ajax({
            url: '/Intake/viewRecSpltFile',
            cache: false,
            type: "POST",
            data: { 'fileName': item.RFARecDocumentName },
            success: function () {
            },
            error: function (reponse) {
                alert("error : " + reponse);
            }
        });
    }


    self.restRFARecordSPLTForm = function () {
        $("#RFARecPageStart").val("");
        $("#RFARecPageEnd").val("");
        $("#DocumentCategoryID").val("");
        $("#RFARecDocumentName").val("");
        $("#RFARecDocumentDate").val("");
        $("#AuthorName").val("");
        self.DocumentTypes(null);
    };

    self.DocumentType = ko.observable();
    self.DocumentTypes = ko.observableArray();
    self.DocumentTypes = ko.observableArray([self.DocumentTypes(0)]);
    self.selectedItemDocumentTypes = ko.observable(0);

    $("#DocumentCategoryID").change(function () {
        showLoader();
        if ($("#DocumentCategoryID").val() != "") {
            $.getJSON("/common/GetDocumentTypeByDocumentCategoryID", {
                _documentCategoryID: $("#DocumentCategoryID").val()
            }, function (DocumentType) {
                self.DocumentTypes(DocumentType.slice());
            });
        }
        else {
            self.DocumentTypes(null);
        }
        hideLoader();
    });
}
