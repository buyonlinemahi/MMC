function SearchViewModel() {
    var self = this;
    self.TotalItemCount = ko.observable(0);
    self.UserSearchResults = ko.observableArray();
    self.UserSearchName = function () {
        if ($('#_SearchTextByName').val() == "") {
            $('#_SearchTextByName').focus();
            alertify.alert("Enter any User.");
        }
        else {
            GrdBinding();
            self.Pager().CurrentPage(1);
        }
    };

    $("#_SearchTextByName").keypress(function (e) {//To Search when enter is pressed
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            self.UserSearchName();
        }
    });

    ko.bindingHandlers.formatNumberText = {
        update: function (element, valueAccessor) {
            var phone = ko.utils.unwrapObservable(valueAccessor());
            var formatPhone = function () {
                return phone.replace(/(\d{3})(\d{3})(\d{4})/, "($1)-$2-$3");
            }
            ko.bindingHandlers.text.update(element, formatPhone);
        }
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

    self.DeleteUserByID = function (_data) {

        alertify.confirm("Are you sure to delete this record?", function (e) {
            if (e) {
                $.post("/User/DeleteUserByID", { _UserID: _data.UserId() }, function (_responseText) {

                    if (e) {
                        var _model = $.parseJSON(_responseText);
                        alertify.alert(_model);
                        if (_model != "Error") {
                            GrdBinding();
                        }
                        if ((self.TotalItemCount() % $('#hidskip').val()) == 1)
                            self.Pager().CurrentPage(self.Pager().CurrentPage() - 1);
                        else
                            self.Pager().CurrentPage();
                    }
                });
            }
        });
    }
    self.btnclosepop = function () {
        unblockPopupBackground();
        $("#frmUserChangePassword")[0].reset();
        $("#frmUserChangePassword :input").jqBootstrapValidation("destroy");

    }

    self.OpenPopUserByID = function (_data) {
        blockPopupBackground();
        $("#UserId").val(_data.UserId());
        $("#frmUserChangePassword :input").jqBootstrapValidation();
        //    $("#changepassword").show();
    }


    self.ChangePasswordSuccess = function (model) {
        if (model != null) {
            var _model = $.parseJSON(model);

            if (_model != "Error") {
                alertify.alert(_model);
                $('#changepassword').modal('toggle');
                //   $("#changepassword").hide();
                $("#UserPassword").val("");
                $("#UserConfirmPassword").val("");
            }
        }
        else {
            alertify.alert(_model);
        }
        unblockPopupBackground();
    };

    self.ChangePasswordBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    function GrdBinding() {
        showLoader();
        var _searchtext = $('#_SearchTextByName').val();
        $.post("/User/GetUserByName", {
            _searchText: _searchtext, _skip: $('#hidskip').val()
        }, function (_data) {
            var model = $.parseJSON(_data);
            self.UserSearchResults.removeAll();
            ko.mapping.fromJS(model.UserDetails, {}, self.UserSearchResults);
            self.TotalItemCount(model.TotalCount);
        });
        hideLoader();
    }


    var pagingSettings = {
        pageSize: 20,
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
};