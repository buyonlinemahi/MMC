function SearchViewModel() {
    var self = this;
    self.TotalItemCount = ko.observable(0);
    self.PhysicianSearchResults = ko.observableArray();
    self.Specialties = ko.observableArray([]);
    
    self.bindSpecialityDDL = function () {
        $.ajax({
            url: '/Common/getSpecialtyAll',
            cache: false,
            type: "GET",
            success: function (data) {
                ko.mapping.fromJS($.parseJSON(data), {}, self.Specialties);
            },
            error: function (reponse) {
                alert("error : " + reponse);
            }
        });
    }

    $("#SearchTextByName").keypress(function (e) {//To Search when enter is pressed
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            self.PhysicianSearchName();
        }
    });

    var _searchtext;
    var _searchType;    
    self.PhysicianSearchName = function () {
        if ($('#PhySearchTypeDDL').val() == "Speciality")
            _searchtext = $('#ddlSpeciality').val();
        else if ($('#SearchTextByName').val()!="")
            _searchtext = $('#SearchTextByName').val();
        else {            
            $("#divuploadFile").hide();
            alertify.alert("Please Enter Physician!", function () {
                $("#divuploadFile").show();
            });
            return;
        }
              
        _searchType = $('#PhySearchTypeDDL').val();
        if (_searchtext.trim() != "") {
            showLoader();
            checkSearchType(0); // passing skip 0, initial search
            hideLoader();
        }
        self.Pager().CurrentPage(1);        
    };

    self.bindPhysicianSearchGrid = function (_data) {        
        var model = $.parseJSON(_data);
        if (model != null) {
            self.PhysicianSearchResults.removeAll();
            ko.mapping.fromJS(model.PhysicianDetails, {}, self.PhysicianSearchResults);
            self.TotalItemCount(model.TotalCount);
        }
    }


    self.ClearSearchGrid = function () {
        self.PhysicianSearchResults.removeAll();
        self.TotalItemCount(0);
    };

    self.DeletedPhysicianByID = function (model) {
        var _physicianByID = model.PhysicianId();
        alertify.confirm("Are you sure you want to delete this record?", function (e) {
            if (e) {
                showLoader();
                $.post("/Physician/DeletePhysicianByID", {
                    _physicianID: _physicianByID
                }, function (_data) {
                    if (_data == 1) {
                        checkSearchType(self.Skip());
                        $.post("/Physician/GetPhysicianByName", {
                            _searchText: _searchtext, _skip: self.Skip()
                        }, function (_data) {
                            var model = $.parseJSON(_data);
                            if (model != null) {
                                ko.mapping.fromJS(model.PhysicianDetails, {}, self.PhysicianSearchResults);
                                self.TotalItemCount(model.TotalCount);
                                var _skipValue = self.Skip();                                
                                var _totalCount = self.TotalItemCount();

                                if ((_skipValue % _totalCount) == 0) {   // case when page changes on delete, eg delete record no. 21 or 41
                                    self.Pager().CurrentPage((_totalCount / self.Take()));
                                }
                            }
                        });
                        hideLoader();
                        alertify.alert("Deleted Successfully");
                    }
                    else if (_data == 0) {
                        alertify.alert("Error");
                    }

                });
            }
        });
    };

    //--- Common functions ---//
    function getPhysicianByName(_skip) {
        $.post("/Physician/GetPhysicianByName", {
            _searchText: _searchtext, _skip: _skip
        }, function (_data) {
            self.bindPhysicianSearchGrid(_data);
        });
    }

    function getPhysicianByNPI(_skip) {
        $.post("/Physician/GetPhysicianByNPI", {
            _searchText: _searchtext, _skip: _skip
        }, function (_data) {
            self.bindPhysicianSearchGrid(_data);
        });
    }

    function getPhysicianBySpeciality(_skip) {
        $.post("/Physician/GetPhysicianBySpeciality", {
            _searchID: _searchtext, _skip: _skip
        }, function (_data) {
            self.bindPhysicianSearchGrid(_data);
        });
    }

    function checkSearchType(_skip) {
        switch (_searchType) {
            case 'Name':
                getPhysicianByName(_skip);
                break;
            case 'NPI':
                getPhysicianByNPI(_skip);
                break
            case 'Speciality':
                getPhysicianBySpeciality(_skip);
                break;
            default:
                getPhysicianByName(_skip);
                break;
        }
    }

    self.selectPhysician = function (item) {
        _intakePhysicianViewModel.bindPhysician(item);
    }
    //-------------------------//

    self.GetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {            
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
            
        }
        showLoader();
        checkSearchType(skip);
        hideLoader();
    };
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

$("#PhySearchTypeDDL").change(function () {
    if ($("#PhySearchTypeDDL").val() == "Speciality") {
        $("#SearchTextByName").fadeOut(function () {
            $("#ddlSpeciality").fadeIn();
        });
    }
    else {
        $("#ddlSpeciality").fadeOut(function () {
            $("#SearchTextByName").fadeIn();
        });
    }
});