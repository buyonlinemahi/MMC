function patientNoteViewModel() {
    var self = this;
    self.pnSkip = ko.observable(0);
    self.pnTotalItemCount = ko.observable(0);
    self.NotesDetails = ko.observableArray();
    bindNoteDetail();
    $('#hidPatientID').val($('#PatientID').val());
    var pnpagingSetting = {
        pnpageSize: 20,
        pnpageSlide: 2
    };
    self.pnSkip = ko.observable(0);
    self.pnTake = ko.observable(pnpagingSetting.pnpageSize);
    self.pnPager = ko.pnpager(self.pnTotalItemCount);
    self.pnPager().pnPageSize(pnpagingSetting.pnpageSize);
    self.pnPager().pnPageSlide(pnpagingSetting.pnpageSlide);
    self.pnPager().pnCurrentPage(1);
    self.pnPager().pnCurrentPage.subscribe(function () {
        var skip = pnpagingSetting.pnpageSize * (self.pnPager().pnCurrentPage() - 1);
        var take = pnpagingSetting.pnpageSlide;
        self.pnGetRecopnsWithSkipTake(skip, take);
    });
    self.pnGetRecopnsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.pnSkip(0);
            self.pnTake(pnpagingSetting.pnpageSize);
        }
        else {
            self.pnSkip(skip);
            self.pnTake(take);
        }
        bindNoteDetail();

    }

    self.EditNotes = function (model) {
        blockPopupBackground();
        //  $('#dllClaim').val(model.PatientClaimID());
        $('#hidNoteID').val(model.NoteID());
        $('#dllClaim').prop('disabled', true);
        $('#dllRequest').prop('disabled', true);
        var editor1 = document.getElementById("EditorNoteUpdate").editor;
        //  editor1.SetText(model.Notes());
        editor1.SetText(($("<div>").html(model.Notes()).text()));
    }

    self.UpdateNotes = function () {

        if ($("#EditorNoteUpdate").val() == '') {
            alertify.alert("Enter Notes");
            return false;
        }
        if ($("#hidNoteID").val() == '0') {
            if ($('#dllClaim').val() == '') {
                $("#dllClaim").addClass("border_r");
                return false;
            }
            else {
                $("#dllClaim").removeClass("border_r");
            }
        }

        showLoader();
        var viewModelNotesDetails = {
            NoteID: $('#hidNoteID').val(),
            Notes: $("<div>").text($("#EditorNoteUpdate").val()).html(),
            UserID: 0,
            PatientClaimID: 0,
            RFARequestID: 0,
            Date: Date.now()
        }
        var plainJs = ko.toJS(viewModelNotesDetails);
        $.ajax({
            url: "/Intake/SaveNotes",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(plainJs),
            success: function (data) {
                hideLoader();
                if (data != 0) {
                    $('#UpdateNoteDiv').modal('hide');
                    alertify.alert("Notes Saved Successfully", function (e) {
                        if (e) {
                            bindNoteDetail();
                        }
                    });
                }
                else {
                    alert("Error Occur");
                }
            },
            error: function (data) {
                hideLoader();
                alert("Error Occur");
            }
        });
    }

    self.CloseNotePop = function () {
        unblockPopupBackground();
    }

    function bindNoteDetail() {
        $.post("/Patient/getNotesByPatientID", {
            patientID: $('#PatientID').val(), skip: self.pnSkip()
        }, function (_data) {
            self.NotesDetails.removeAll();           
            ko.mapping.fromJS(_data.NotesDetails, {}, self.NotesDetails);         
            self.pnTotalItemCount(_data.TotalCount);
            unblockPopupBackground();
        });
    }

    //    --------------

}

//========== pager js for   Patient History  grid only ========//
(function (ko) {
    var pnnumericObservable = function (initialValue) {
        var _actual = ko.observable(initialValue);
        var result = ko.dependentObservable({
            read: function () {
                return _actual();
            },
            write: function (newValue) {
                var parsedValue = parseFloat(newValue);
                _actual(isNaN(parsedValue) ? newValue : parsedValue);
            }
        });
        return result;
    };
    function pnPager(totalItemCount) {
        var self = this;
        self.pnCurrentPage = pnnumericObservable(1);
        self.pnTotalItemCount = ko.computed(totalItemCount);
        self.pnPageSize = pnnumericObservable(10);
        self.pnPageSlide = pnnumericObservable(2);
        self.pnLastPage = ko.computed(function () {
            return Math.floor((self.pnTotalItemCount() - 1) / self.pnPageSize()) + 1;
        });
        self.pnHasNextPage = ko.computed(function () {
            return self.pnCurrentPage() < self.pnLastPage();
        });
        self.pnHasPrevPage = ko.computed(function () {
            return self.pnCurrentPage() > 1;
        });
        self.pnFirstItemIndex = ko.computed(function () {
            return self.pnPageSize() * (self.pnCurrentPage() - 1) + 1;
        });
        self.pnLastItemIndex = ko.computed(function () {
            return Math.min(self.pnFirstItemIndex() + self.pnPageSize() - 1, self.pnTotalItemCount());
        });
        self.pnPages = ko.computed(function () {
            var pnpageCount = self.pnLastPage();
            var pnpageFrom = Math.max(1, self.pnCurrentPage() - self.pnPageSlide());
            var pnpageTo = Math.min(pnpageCount, self.pnCurrentPage() + self.pnPageSlide());
            pnpageFrom = Math.max(1, Math.min(pnpageTo - 2 * self.pnPageSlide(), pnpageFrom));
            pnpageTo = Math.min(pnpageCount, Math.max(pnpageFrom + 2 * self.pnPageSlide(), pnpageTo));
            var result = [];
            for (var i = pnpageFrom; i <= pnpageTo; i++) {
                result.push(i);
            }
            return result;
        });
    }
    ko.pnpager = function (totalItemCount) {
        var pnpager = new pnPager(totalItemCount);
        return ko.observable(pnpager);
    };
}(ko));
//============= pager js for Ipn ends  ============//
