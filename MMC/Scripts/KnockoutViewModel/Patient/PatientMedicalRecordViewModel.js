function PatientMedicalRecordSplitInsert(_RFARecSpltID, _PatientID, _PatientClaimID, _DocumentCategoryID, _DocumentTypeID, _RFARecDocumentName, _RFARecDocumentDate, _RFARecPageStart, _RFARecPageEnd, _DocumentCategoryName, _DocumentTypeDesc, _PatientClaimNumber, _RFAReferralFileName, _AuthorName, _RFARequestID) {
    var self = this;
    self.RFARecSpltID = ko.observable(0);
    self.PatientID = ko.observable(_PatientID);
    self.PatientClaimID = ko.observable(_PatientClaimID);
    self.DocumentCategoryID = ko.observable(_DocumentCategoryID);
    self.DocumentTypeID = ko.observable(_DocumentTypeID);
    self.RFARecDocumentName = ko.observable(_RFARecDocumentName);
    self.RFARecDocumentDate = ko.observable(moment(_RFARecDocumentDate).format("MM/DD/YYYY"));
    self.RFARecPageStart = ko.observable(_RFARecPageStart);
    self.RFARecPageEnd = ko.observable(_RFARecPageEnd);
    self.DocumentCategoryName = ko.observable(_DocumentCategoryName);
    self.DocumentTypeDesc = ko.observable(_DocumentTypeDesc);
    self.PatientClaimNumber = ko.observable(_PatientClaimNumber);
    self.RFAReferralFileName = ko.observable(_RFAReferralFileName);
    self.AuthorName = ko.observable(_AuthorName);
    self.RFARequestID = ko.observable(_RFARequestID);

}
function PatientMedicalRecordSplitUpdate(_RFARecSpltID, _PatientID, _PatientClaimID, _DocumentCategoryID, _DocumentTypeID, _RFARecDocumentName, _RFARecDocumentDate, _RFARecPageStart, _RFARecPageEnd, _DocumentCategoryName, _DocumentTypeDesc, _PatientClaimNumber, _RFAReferralFileName, _AuthorName, _RFARequestID) {
    var self = this;
    self.RFARecSpltID = ko.observable(0);
    self.PatientID = ko.observable(_PatientID);
    self.PatientClaimID = ko.observable(_PatientClaimID);
    self.DocumentCategoryID = ko.observable(_DocumentCategoryID);
    self.DocumentTypeID = ko.observable(_DocumentTypeID);
    self.RFARecDocumentName = ko.observable(_RFARecDocumentName);
    self.RFARecDocumentDate = ko.observable(moment(_RFARecDocumentDate).format("MM/DD/YYYY"));
    self.RFARecPageStart = ko.observable(_RFARecPageStart);
    self.RFARecPageEnd = ko.observable(_RFARecPageEnd);
    self.DocumentCategoryName = ko.observable(_DocumentCategoryName);
    self.DocumentTypeDesc = ko.observable(_DocumentTypeDesc);
    self.PatientClaimNumber = ko.observable(_PatientClaimNumber);
    self.RFAReferralFileName = ko.observable(_RFAReferralFileName);
    self.AuthorName = ko.observable(_AuthorName);
    self.RFARequestID = ko.observable(_RFARequestID);

}
function PatientMedicalRecordViewModel() {
    var self = this;
    self.TotalItemCount = ko.observable(0);
    self.PatientMedicalRecordSearchResults = ko.observableArray();

    // Claim Acc to Patient ID
    self.ClaimCategorie = ko.observable();
    self.ClaimCategories = ko.observableArray();
    self.ClaimCategories = ko.observableArray([self.ClaimCategories(0)]);
    self.selectedItemClaimCategories = ko.observable(0);

    // Request list's
    self.RequestCategories = ko.observable();
    self.RequestCategories = ko.observableArray();
    self.RequestCategories = ko.observableArray([self.RequestCategories(0)]);
    self.selectedItemRequestCategories = ko.observable(0);
   
    // Document Categories
    self.DocumentCategorie = ko.observable();
    self.DocumentCategories = ko.observableArray();
    self.DocumentCategories = ko.observableArray([self.DocumentCategories(0)]);
    self.selectedItemDocumentCategories = ko.observable(0);

    // DocumentTypes
    self.DocumentType = ko.observable();
    self.DocumentTypes = ko.observableArray();
    self.DocumentTypes = ko.observableArray([self.DocumentTypes(0)]);
    self.selectedItemDocumentTypes = ko.observable(0);    
    var selectedSortBy = "Date";
    var CurrentSortBy;
    var CurrentSortOrder;
    self.Skip = ko.observable(0);
    var mappingOptions = {
        'PatMRDocumentDate': {
            create: function (options) {
                if (options.data != '/Date(-62135596800000)/')
                    return moment(options.data).format("MM-DD-YYYY ");
            }
        }
    }
    jQuery(function(){    
        var _skip = 0;
        self.GrdBinding(_skip);     
        $.getJSON("/Common/getDocumentCategoriesAll", function (dataCategories) {
            self.DocumentCategories(dataCategories.slice());
            
        });

        //$.getJSON("/Common/getDocumentTypeAll", function (dataTypeAll) {
        //    self.DocumentTypes(dataTypeAll.slice());           
        //});
    });

    self.GrdBinding = function (skip) {        
        var _patientID = $("#PatientID").val();
        $("#PMRPatientID").val(_patientID);
        var _patientClaimID = $("#HDPatientClaimID").val();
        $("#PMRPatientCliamID").val(_patientClaimID);        
        $.post("/PatientMedicalRecord/GetPatientMedicalRecordByPatientID", {
            _patientID: _patientID, _skip: skip, _sortBy: selectedSortBy, _order: (CurrentSortOrder == null)?null:CurrentSortOrder
        }, function (_data) {
            var model = $.parseJSON(_data);
            if (model.TotalCount != "0") {
                $("#PatientMedicalRecordID").show();
                self.PatientMedicalRecordSearchResults.removeAll();
                ko.mapping.fromJS(model.PatientMedicalRecordByPatientIDDetails, mappingOptions, self.PatientMedicalRecordSearchResults);
                self.TotalItemCount(model.TotalCount);  
            }
            else {
                $("#PatientMedicalRecordID").hide();
            }
        });
    };

    self.SortByHeader = function(_SelectedSortBy)
    {
        showLoader();
        selectedSortBy = _SelectedSortBy;
        CurrentSortOrder = (CurrentSortBy == selectedSortBy && CurrentSortOrder == 'ASC' ? 'DESC' : 'ASC');
        if (self.Pager().CurrentPage() != 1)
        {            
            self.Pager().CurrentPage(1);         
        }
        else
        {            
            self.GrdBinding(0);         
        }
        CurrentSortBy = selectedSortBy;
        if (CurrentSortOrder == 'DESC') {
            $(".orderIcon").removeClass("glyphicon-chevron-up");
            $(".orderIcon").removeClass("glyphicon-chevron-down");
            $("#spanOrderIcon" + CurrentSortBy).removeClass("glyphicon-chevron-up");
            $("#spanOrderIcon" + CurrentSortBy).addClass("glyphicon-chevron-down");
        }
        else {
            $(".orderIcon").removeClass("glyphicon-chevron-up");
            $(".orderIcon").removeClass("glyphicon-chevron-down");
            $("#spanOrderIcon" + CurrentSortBy).removeClass("glyphicon-chevron-down");
            $("#spanOrderIcon" + CurrentSortBy).addClass("glyphicon-chevron-up");
        }
        hideLoader();
    }

    self.deleteRFARecordSplitting = function (item) {
        alertify.confirm("Are you sure to delete this record?", function (e) {
            if (e) {
                $.ajax({
                    url: '/Intake/DeleteRFARecordSplitting',
                    cache: false,
                    type: "POST",
                    data: { '_rfaRecSpltID': item.PatientMedicalRecordID(), 'patientID': item.PatientID(), 'claimID': item.PatientClaimID(), 'fileName': item.PatMRDocumentName() + '.pdf' },
                    success: function (model) {
                       // var model = $.parseJSON(_data);
                        self.GrdBinding(self.Skip());
                        alertify.alert("Delete Successfully");
                    },
                    error: function (reponse) {
                        alert("error : " + reponse);
                    }
                });
            }
           
        });

    }
    self.deleteInActive = function () {
        alertify.alert("User doesn't have permission to delete a record");
        return false;
    }

    self.GetRecordsWithSkipTake = function (skip, take) { 
        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSetting.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        self.GrdBinding(skip);
    }
    var pagingSetting = {
        pageSize: 20,
        pageSlide: 2
    };


   
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



    //---------------Upload Patient Medical Record Spilliting..............MS

    self.RFARecSpltID = ko.observable();
    self.PatientID = ko.observable();
    self.PatientClaimID = ko.observable();
    self.DocumentCategoryID  = ko.observable();
    self.DocumentTypeID  = ko.observable();
    self.RFARecDocumentName = ko.observable();
    self.RFARecDocumentDate = ko.observable();
    self.RFARecPageStart = ko.observable();
    self.RFARecPageEnd = ko.observable();
    self.PageTotal  = ko.observable();
    self.PageRemaining = ko.observable();
    self.RFAReferralFileName = ko.observable();
    self.DocumentCategoryName = ko.observable();
    self.DocumentTypeDesc = ko.observable();
    self.PatientClaimNumber = ko.observable();
    self.AuthorName = ko.observable();
    self.PatientMedicalRecordSpilt = ko.observableArray([]);
    self.RFARequestID = ko.observable();

    var index = 0;

    self.GreaterFromThisNum = ko.observable(0);
    self.EditMode = ko.observable(0);

    $('.OpenPopMedical').click(function () {
        blockPopupBackground();
        
    });
    $('#OpenPopMedicalButton').click(function () {   
        $.post("/PatientMedicalRecord/GetPatientClaimsByPatientIDOnDrop", {
            _patientID: $("#PatientID").val()
        }, function (dataTypeClaimsAll) {
            if (dataTypeClaimsAll != null) {                
                self.ClaimCategories(dataTypeClaimsAll.slice());
            }
        });
    });


    $('#buttonClosePatientSplitting').click(function () {
        unblockPopupBackground();
        ResetAllRecordSplit();
        $('#divMedicalSpilliting').hide();
        $('#DivMedicalSpillitingGrid').hide();

        if (self.PatientMedicalRecordSpilt() != null) {
            self.PatientMedicalRecordSpilt.removeAll();
        }
    });

    function ResetAllInput() {
        $('#RFARecPageStart').attr('readonly', false);
        $('#RFARecPageEnd').attr('readonly', false);
        $('#RFARecPageStart').val('');
        $('#RFARecPageEnd').val('');
        $('#RFARecDocumentName').val('');
        $('#RFARecDocumentDate').val('');
        $('#DocumentCategoryID').val('');
        $('#RFARequestID').val('');
        $('#AuthorName').val('');
        self.DocumentTypes(null);
    }

    function ResetAllRecordSplit() {
        $('#RFARecPageStart').attr('readonly', false);
        $('#RFARecPageEnd').attr('readonly', false);
        $('#RFARecPageStart').val('');
        $('#RFARecPageEnd').val('');
        $('#RFARecDocumentName').val('');
        $('#RFARecDocumentDate').val('');
        $('#AuthorName').val('');
        $("#GreaterFromThisNum").val('');
        $("#PageRemaining").val('');
        $('#upMedicalRecordFile').each(function () {
            $(this).after($(this).clone(true)).remove();
        });

        var control = $('#upMedicalRecordFile');
        control.replaceWith(control.val('').clone(true));

        control.on({
               change: function () {console.log("Changed") },
                focus: function () {console.log("Focus") }
                });
    }

    function BindSplitGrid() {
        var _patientID = $("#PatientID").val();
        $.post("/PatientMedicalRecord/getPatientMedicalRecordSplit", {
            _patientID: _patientID
        }, function (_data) {
            var model = $.parseJSON(_data);
            if (self.PatientMecdicalRecordSpilt() != null)
                self.PatientMecdicalRecordSpilt.removeAll();
            if (model.patientMedicalRecordSplitingDetails != null) {
                $('#DivMedicalSpillitingGrid').show();
                ko.mapping.fromJS(model.patientMedicalRecordSplitingDetails, {}, self.PatientMecdicalRecordSpilt);
            }
        })
    }


    self.PatientMedicalRecordFileInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        showLoader();
        return true;
    }
    self.PatientMedicalRecordUploadSuccess = function (_data) {
        var responseText = $.parseJSON(_data);
            hideLoader();
            var _patientID = $("#PatientID").val();
            self.PatientID(_patientID);
            self.PageTotal(responseText.TotalPages);
            self.PageRemaining(responseText.TotalPages);
            self.EditMode(0);
            self.GreaterFromThisNum(0);
            self.RFAReferralFileName(responseText.RFAReferralFileName);
            alertify.success("Uploaded Sucessfully");
            $('#divMedicalSpilliting').show();
    }

    self.PatientMedicalRecordSplitSuccess = function () {
        var _allvalid = 1;
        var _checked = 1;

        var _PageTotal = parseInt($("#PageTotal").val());
        var _PageRemaining = parseInt($("#PageRemaining").val());
        var _EditMode = parseInt($('#EditMode').val());

        if (_PageRemaining == 0 && _EditMode ==0) {
            alertify.alert("Zero page remaining for split");
        }
        else
        {
          
            var _PatientID = $("#PatientID ").val();
            var _PatientClaimID = parseInt($("#PatientClaimIDtt").val());
            var _DocumentCategoryID = parseInt($("#DocumentCategoryID").val());
            var _DocumentTypeID = parseInt($("#DocumentTypeID").val());
            var _RFARecDocumentName = $("#RFARecDocumentName").val();
            var _RFARecDocumentDate = $("#RFARecDocumentDate").val();
            var _RFARecPageStart = parseInt($("#RFARecPageStart").val());
            var _RFARecPageEnd = parseInt($("#RFARecPageEnd").val());
            var _DocumentCategoryName = $("#DocumentCategoryID").find("option:selected").text();
            var _DocumentTypeDesc =   $("#DocumentTypeID").find("option:selected").text();
            var _PatientClaimNumber = $("#PatientClaimIDtt").find("option:selected").text();
            var _RFAReferralFileName = $("#RFAReferralFileName").val();
            var _GreaterFromThisNum = parseInt($("#GreaterFromThisNum").val());
            var _AuthorName = $('#AuthorName').val();
            var _RFARequestID = parseInt($("#RFARequestID").val());            

            if (_RFARecPageStart == '' && _checked == 1) {
                if (_EditMode == 0) {
                    _allvalid = 0;
                    _checked = 0;
                    alertify.alert("Page Start required");
                }
            }
            else if (_RFARecPageEnd == '' && _checked == 1) {
                if (_EditMode == 0) {
                    _allvalid = 0;
                    _checked = 0;
                    alertify.alert("Page End required");
                }
            }
            else if (_RFARecDocumentName == '' && _checked == 1) {
                _allvalid = 0;
                _checked = 0;
                alertify.alert("Document Name required");
            }
            else if (_RFARecDocumentDate == '' && _checked == 1) {
                _allvalid = 0;
                _checked = 0;
                alertify.alert("Document Date required");
            }
            else if (_RFARecPageStart <= _GreaterFromThisNum && _checked == 1) {
                if (_EditMode == 0) {
                    _allvalid = 0;
                    _checked = 0;
                    alertify.alert("Start Page is already split");
                    $('#RFARecPageStart').val('');
                }
            }
            else if (_RFARecPageEnd <= _GreaterFromThisNum && _checked == 1) {
                if (_EditMode == 0) {
                    _allvalid = 0;
                    _checked = 0;
                    alertify.alert("End Page is already split");
                    $('#RFARecPageEnd').val('');
                }
            }
            else if (_PageTotal < _RFARecPageStart && _checked == 1) {
                if (_EditMode == 0) {
                    _allvalid = 0;
                    _checked = 0;
                    alertify.alert("Start Page is not exists");
                    $('#RFARecPageStart').val('');
                }
            }
            else if (_PageTotal < _RFARecPageEnd && _checked == 1) {
                if (_EditMode == 0) {
                    _allvalid = 0;
                    _checked = 0;
                    alertify.alert("End Page is not exists");
                    $('#RFARecPageEnd').val('');
                }
            }
            else if (_RFARecPageEnd < _RFARecPageStart && _checked == 1) {
                if (_EditMode == 0) {
                    _allvalid = 0;
                    _checked = 0;
                    alertify.alert("Start Page is not greater than end page");
                    $('#RFARecPageStart').val('');
                }
            }


            if (_allvalid == 1) {

                //---calculation by---------------MS
                if (_EditMode == 0) {
                    var _splitPageTotal = _RFARecPageEnd - _RFARecPageStart + 1;
                    var _totalPageRemaining = _PageRemaining - _splitPageTotal;

                    $('#DivMedicalSpillitingGrid').show();
                    $("#GreaterFromThisNum").val(_RFARecPageEnd);
                    $("#PageRemaining").val(_totalPageRemaining);
                    self.PageRemaining(_totalPageRemaining);
                    self.PatientMedicalRecordSpilt.push(new PatientMedicalRecordSplitInsert(0, _PatientID, _PatientClaimID, _DocumentCategoryID, _DocumentTypeID, _RFARecDocumentName, _RFARecDocumentDate, _RFARecPageStart, _RFARecPageEnd, _DocumentCategoryName, _DocumentTypeDesc, _PatientClaimNumber, _RFAReferralFileName, _AuthorName, _RFARequestID));
                    ResetAllInput();
                    alertify.alert("Add Successfully");
                }
                else {
                    self.PatientMedicalRecordSpilt.splice(index, 1);
                    $('#RFARecPageStart').attr('readonly', false);
                    $('#RFARecPageEnd').attr('readonly', false);
                    self.EditMode(0);
                    self.PatientMedicalRecordSpilt.splice(index, 0, new PatientMedicalRecordSplitUpdate(0, _PatientID, _PatientClaimID, _DocumentCategoryID, _DocumentTypeID, _RFARecDocumentName, _RFARecDocumentDate, _RFARecPageStart, _RFARecPageEnd, _DocumentCategoryName, _DocumentTypeDesc, _PatientClaimNumber, _RFAReferralFileName, _AuthorName, _RFARequestID));
                    ResetAllInput();
                    alertify.alert("Updated Successfully");
                }
            }
        }

    }

// validation for upload medical record for pdf format only
  $('#upMedicalRecordFile').change(function () {
    if ($("#upMedicalRecordFile").val() != "") {
        var fileExtension = ['pdf', 'PDF'];
        if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
            alert("Only pdf format are allowed.");
            var control = $('#upMedicalRecordFile');
            control.replaceWith(control.val('').clone(true));

            control.on({
                change: function () { console.log("Changed") },
                focus: function () { console.log("Focus") }
            });
        }
    }
})

    self.EditPatientMedicalRecordSplit = function (data) {

        index = self.PatientMedicalRecordSpilt.indexOf(data);
        self.EditMode(1);        
        self.RFARecSpltID(0);
        $('#PatientID').val(data.PatientID());
        $("#PatientClaimIDtt").val(data.PatientClaimID());
        $("#RFARecDocumentName").val(data.RFARecDocumentName());
        $("#DocumentCategoryID").val(data.DocumentCategoryID());
        $("#DocumentTypeID").val(data.DocumentTypeID());
        $("#RFARecDocumentDate").val(data.RFARecDocumentDate());
        $("#RFARecPageStart").val(data.RFARecPageStart());
        $("#RFARecPageEnd").val(data.RFARecPageEnd());
        $("#RFAReferralFileName").val(data.RFAReferralFileName());
        $('#AuthorName').val(data.AuthorName());
        $('#RFARecPageStart').attr('readonly', true);
        $('#RFARecPageEnd').attr('readonly', true);
        $('#RFARequestID').val(data.RFARequestID());
    }

    self.PatientMedicalRecordGridSuccess = function (response) {
        var result = $.parseJSON(response);
        if (result == 1) {
            ResetAllRecordSplit();
            $('#buttonClosePatientSplitting').click();            
            alertify.success("Split Successfully");
            self.GrdBinding(0);
        }
        else {
            alertify.alert("Error Occured");
        }
    }

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

    self.PatientClaimChanged = function () {
        //alert("PatientClaimID " + $("#PatientClaimIDtt").val());
        $.getJSON("/PatientMedicalRecord/getRFARequestRecordsByPatientClaim", {
            _patiantClaim: $('#PatientClaimIDtt :selected').text()
        }, function (Requests) {
            self.RequestCategories(Requests.slice());
        });
    };

   

   
}
$("#PrintPatientMedicalRecord").click(function () {
    var _patientID = $("#PatientID").val();
   
    $.post("/PatientMedicalRecord/GeneratePatientMedRecordTemplateByPatientID", {
        _patientID: _patientID
    }, function (_data) {
        var model = $.parseJSON(_data);
        if (model == "0") {
            alertfy.alert("File Does not Exist.");
        }
        else if (model == "1") {
            alertfy.alert("File Generated.");
        }
    });
});
