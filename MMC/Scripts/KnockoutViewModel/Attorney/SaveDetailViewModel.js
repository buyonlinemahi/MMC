function SaveDetailViewModel(model) {
    var self = this;
    // Attorney Firm
    self.AttorneyFirmID = ko.observable();
    self.AttorneyFirmName = ko.observable();
    self.AttorneyFirmTypeID = ko.observable();
    self.AttAddress1 = ko.observable();
    self.AttAddress2 = ko.observable();
    self.AttCity = ko.observable();
    self.AttStateID = ko.observable();
    self.AttZip = ko.observable();
    self.AttPhone = ko.observable();
    self.AttFax = ko.observable();

    self.AttorneyFirmResults = ko.observableArray();

    //State
    self.State = ko.observable();
    self.States = ko.observableArray();
    self.States = ko.observableArray([self.States(0)]);
    self.selectedItem = ko.observable(2);

    //Attorney Firm Type
    self.AttorneyFirmType = ko.observable();
    self.AttorneyFirmTypes = ko.observableArray();
    self.AttorneyFirmTypes = ko.observableArray([self.AttorneyFirmTypes(0)]);
    self.selectedItemAttorneyFirmTypes = ko.observable(2);

    self.AttorneyResults = ko.observableArray();

    // Attorney
    self.AttorneyID = ko.observable();
    self.AttorneyFirmID = ko.observable();
    self.AttorneyName = ko.observable();
    self.AttPhone = ko.observable();
    self.AttFax = ko.observable();
    self.AttEmail = ko.observable();
    self.TotalItemCount = ko.observable();

    var _AttorneyFirmID = 0;

    $(document).ready(function () {
        _AttorneyFirmID = $("#AttorneyFirmID").val();
        self.AttorneyFirmID(_AttorneyFirmID);
        if (_AttorneyFirmID == 0)
            $(".DivHideAttorney").fadeOut();
        else
            $(".DivHideAttorney").fadeIn();


        $.getJSON("/Common/getStates", function (data) {
            self.States(data.slice());
            if (model != null)
                self.selectedItem(model.AttStateID);
        });

        $.getJSON("/Common/getAttorneyFirmTypeAll", function (dataAttorneyType) {
            self.AttorneyFirmTypes(dataAttorneyType.slice());
            if (model != null)
                self.selectedItemAttorneyFirmTypes(model.AttorneyFirmTypeID);
        });
    });

    if (model != null) {
        if (model.AttorneyFirmDetails != null)
            ko.mapping.fromJS(model.AttorneyFirmDetails, {}, self);

        if (model.TotalCount != null) {
            ko.mapping.fromJS(model.AttorneyDetails, {}, self.AttorneyResults);
            self.TotalItemCount(model.TotalCount);
        }
    }

    $('.OpenPopAttorney').click(function () {
        ClearAll();
        blockPopupBackground();
    });

    $('.ClosePopAttorney').click(function () {
        unblockPopupBackground();
        ClearAll();
    });

    self.AddAttorneyFirmSuccess = function (responseText) {
        hideLoader();
        if (responseText[0] != 0) {
            $("#AttorneyFirmID").val(responseText[0]);
            self.AttorneyFirmID(responseText[0]);
        }
        switch (responseText[1]) {
            case 0:
                alertify.alert("Error Occur");
                break;
            case 1:
                alertify.alert("Save Successfully");
                $(".DivHideAttorney").fadeIn();
                break;
            case 2:
                alertify.alert("Update Successfully");
                break;
        };
    };


    self.AttorneyFirmFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        else {
            showLoader();
        }
        return true;

    };


    function GrdBinding() {
        showLoader();
        $.post("/Attorney/GetAttorneyByAttorneyFirmID", {
            _attorneyFirmID: $('#AttorneyFirmID').val(), _skip: $('#hidskip').val()
        }, function (_data) {
            var model = $.parseJSON(_data);
            if (self.AttorneyResults()!=null)
                self.AttorneyResults.removeAll();
            ko.mapping.fromJS(model.AttorneyDetails, {}, self.AttorneyResults);
            self.TotalItemCount(model.TotalCount);

            var _skipValue = $('#hidskip').val();
            var _totalCount = self.TotalItemCount();

            if ((_skipValue % _totalCount) == 0)
                self.Pager().CurrentPage(1);
        });
        hideLoader();
    }



    self.AttorneySuccess = function (model) {
        hideLoader();
        if (model != null) {
            var _AttorneyFirmID = $('#AttorneyFirmID').val();
            var _model = model;
            if (_model == "Saved Successfully" || _model == "Updated Successfully") {
                $.post("/Attorney/GetAttorneyByAttorneyFirmID", {
                    _attorneyFirmID: _AttorneyFirmID, _skip: $('#hidskip').val()
                }, function (_data) {
                    var model = $.parseJSON(_data);
                    if (self.AttorneyResults() != null)
                        self.AttorneyResults.removeAll();
                    ko.mapping.fromJS(model.AttorneyDetails, {}, self.AttorneyResults);
                    self.TotalItemCount(model.TotalCount);

                    var _skipValue = $('#hidskip').val();
                    var _totalCount = self.TotalItemCount();

                    if ((_skipValue % _totalCount) == 0)
                        self.Pager().CurrentPage(1);
                });
                $('.ClosePopAttorney').click();
                alertify.alert(_model);
            }
            else {
                alertify.alert(_model);
            }
        };
    };

    self.AttorneyFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        else {
            showLoader();
        }
        return true;

    };
    self.GetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        GrdBinding();
    };


    var pagingSettings = {
        pageSize: 10,
        pageSlide: 2
    };
    self.Skip = ko.observable(0);
    self.Take = ko.observable(pagingSettings.pageSize);
    self.Pager = ko.pager(self.TotalItemCount);
    self.Pager().PageSize(pagingSettings.pageSize);
    self.Pager().PageSlide(pagingSettings.pageSlide);
    self.Pager().CurrentPage(1);
    self.Pager().CurrentPage.subscribe(function () {
        var skip = pagingSettings.pageSize * (self.Pager().CurrentPage() - 1);
        var take = pagingSettings.pageSize;
        self.GetRecordsWithSkipTake(skip, take);
    });

    function ClearAll() {
        $("#frmSaveAttorneyDetails")[0].reset();
        $('#AttorneyID').val('');
        $('#AttPhone').val('');
        $('#AttFax').val('');
        $('#AttEmail').val('');
    }

    self.ViewAttorenyDetails = function (data) {
        $('#OpenPopUpAttorney').click();
        var _attorneyID = data.AttorneyID();   
        $.post("/Attorney/GetAttorneyByID", {
            _attorneyID: _attorneyID
        }, function (_data) { 
            $('#AttorneyID').val(_data.AttorneyID);
            $('#AttorneyFirmID').val(_data.AttorneyFirmID);
            $('#AttPhone').val(_data.AttPhone);
            $('#AttorneyName').val(_data.AttorneyName);
            $('#AttFax').val(_data.AttFax);
            $('#AttEmail').val(_data.AttEmail);
        });
    }
    
}