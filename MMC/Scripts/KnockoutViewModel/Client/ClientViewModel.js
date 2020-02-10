function ClientViewModel(model) {
    var self = this;


    //--------------------------------------------------------------------------Client Start  -------------------------------------------------------------------------------------------//
    if (model.ClientDetail != null) {
        showTabs();
        self.ClientID = ko.observable(model.ClientDetail.ClientID);
        self.ClaimAdministratorName = ko.observable(model.ClientDetail.ClaimAdministratorName);
        self.ClaimAdministratorID = ko.observable(model.ClientDetail.ClaimAdministratorID);
        self.ClientName = ko.observable(model.ClientDetail.ClientName);

        if (model.ClientDetail.ClaimAdministratorType != null)
            self.ClaimAdministratorType = ko.observable(model.ClientDetail.ClaimAdministratorType.trim());
        else
            self.ClaimAdministratorType = ko.observable(0);
        $("#ClientID").val(model.ClientDetail.ClientID);

    }

    //--------------------------------------------------------------------------Client End -------------------------------------------------------------------------------------------//
    self.ClmAdSelectedValue = ko.observable(0);
    self.MmcAdSelectedValue = ko.observable(0);
    self.selectedItemClaimAdministrator = ko.observable(0);


    //Claim Administrator 
    if (model.ClientDetail != null) {
        self.ClaimAdministratorAllByClientIDDetails = ko.observableArray();
        self.ClaimAdministratorAllByClientIDDetails = ko.observableArray([self.ClaimAdministratorAllByClientIDDetails(0)]);
        self.ClmAdSelectedValue = ko.observable(0);
    }

    //Insurance
    self.InsName = ko.observable();

    self.InsuranceList = ko.observableArray();
    self.InsuranceList = ko.observableArray([self.InsuranceList(0)]);
    self.InsSelectedValue = ko.observable(0);
    self.ClientInsurerID = ko.observable();
    self.ClientInsurerDetails = ko.observableArray();
    self.cinsTotalItemCount = ko.observable();
    self.cinsSkip = ko.observable(0);

    if (model.ClientInsurerDetails.length != 0) {
        $("#HFInsurerID").val(model.ClientInsurerDetails[0].InsurerID);
        //$("#OpenPopUpClientInsurer").hide();
        //$("#OpenPopUpClientInsuranceBranch").show();
        ShowHideButtonForInsuranceBranch($("#HFInsurerID").val());
    }
    else {
        $("#OpenPopUpClientInsurer").show();
        $("#OpenPopUpClientInsuranceBranch").hide();
        $("#HFInsurerID").val("");
    }

    //Insurance Branch(s)

    self.InsBranchName = ko.observable();

    self.InsuranceBranchList = ko.observableArray();
    self.InsuranceBranchList = ko.observableArray([self.InsuranceBranchList(0)]);
    self.InsBranchSelectedValue = ko.observable(0);
    self.ClientInsurerBranchID = ko.observable();
    self.ClientInsurerBranchDetails = ko.observableArray();



    //Employer

    self.EmpName = ko.observable();
    self.EmployerList = ko.observableArray();
    self.ClientEmployerID = ko.observable();
    self.EmployerList = ko.observableArray([self.EmployerList(0)]);
    self.EmpSelectedValue = ko.observable(0);
    self.ClientEmployerDetails = ko.observableArray();
    self.cEmpTotalItemCount = ko.observable();
    self.cEmpSkip = ko.observable(0);

    //CompName

    self.MmcSelectedValue = ko.observable();


    self.CompName = ko.observable();
    self.ManagedCareCompanyList = ko.observableArray();
    self.ClientCompanyID = ko.observable();
    self.ManagedCareCompanyList = ko.observableArray([self.ManagedCareCompanyList(0)]);
    self.MmcSelectedValue = ko.observable(0);
    self.ClientManagedCareCompanyDetails = ko.observableArray();
    //self.cMmcTotalItemCount = ko.observable();
    self.cMmcSkip = ko.observable(0);



    //TPA

    self.TPAName = ko.observable();
    self.ThirdPartyAdministratorList = ko.observableArray();
    self.ClientTPAID = ko.observable();
    self.ThirdPartyAdministratorList = ko.observableArray([self.ThirdPartyAdministratorList(0)]);
    self.TpaSelectedValue = ko.observable(0);
    self.ClientThirdPartyAdministratorDetails = ko.observableArray();
    self.cTpaTotalItemCount = ko.observable();
    self.cTpaSkip = ko.observable(0);


    //Insurance Branch(s)

    self.TPABranchName = ko.observable();

    self.ThirdPartyAdministratorBranchList = ko.observableArray();
    self.ThirdPartyAdministratorBranchList = ko.observableArray([self.ThirdPartyAdministratorBranchList(0)]);
    self.TpaBranchSelectedValue = ko.observable(0);
    self.ClientTPABranchID = ko.observable();
    self.ClientThirdPartyAdministratorBranchDetails = ko.observableArray();


    if (model.ClientThirdPartyAdministratorDetails.length != 0) {
        $("#HFTPAID").val(model.ClientThirdPartyAdministratorDetails[0].TPAID);
        //$("#OpenPopUpClientThirdPartyAdministrator").hide();
        //$("#OpenPopUpClientThirdPartyAdministratorBranch").show();
        ShowHideButtonForTPABranch($("#HFTPAID").val());
    }
    else {
        $("#OpenPopUpClientThirdPartyAdministrator").show();
        $("#OpenPopUpClientThirdPartyAdministratorBranch").hide();
        $("#HFTPAID").val("");
    }

  

    ko.mapping.fromJS(model, {}, self);

   
    if (model.ClientDetail != null) {
        switch (self.ClaimAdministratorType()) {
            case 'emp':
                self.ClmAdSelectedValue(model.ClientDetail.ClaimAdministratorID + '-' + model.ClientDetail.ClaimAdministratorType.trim());
                break;
            case 'ins':
                self.ClmAdSelectedValue(model.ClientDetail.ClaimAdministratorID + '-' + model.ClientDetail.ClaimAdministratorType.trim());
                break;
            case 'insb':
                self.ClmAdSelectedValue(model.ClientDetail.ClaimAdministratorID + '-' + model.ClientDetail.ClaimAdministratorType.trim());
                break;
            case 'mcc':
                self.ClmAdSelectedValue(model.ClientDetail.ClaimAdministratorID + '-' + model.ClientDetail.ClaimAdministratorType.trim());
                break;
            case 'tpa':
                self.ClmAdSelectedValue(model.ClientDetail.ClaimAdministratorID + '-' + model.ClientDetail.ClaimAdministratorType.trim());
                break;
            case 'tpab':
                self.ClmAdSelectedValue(model.ClientDetail.ClaimAdministratorID + '-' + model.ClientDetail.ClaimAdministratorType.trim());

        }
    }

    self.cinsTotalItemCount(model.cinsTotalCount);
    self.cEmpTotalItemCount(model.cEmpTotalCount);
    //self.cMmcTotalItemCount(model.cMmcTotalCount);
    self.cTpaTotalItemCount(model.cTpaTotalCount);

    //================ Client ==================//

    self.ClientFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        showLoader();
        return true;
    }
    self.AddClientDetailSuccess = function (model) {
        showLoader();
        if (model != null) {
            var _model = $.parseJSON(model);

            if (_model != 0) {
                alertify.alert("Saved Successfully", function (e) {
                    if (e) {
                        $("#ClientID").val(_model);
                        GrdClientEmployerBinding(0);
                        self.cEmpPager().cEmpCurrentPage(1);
                        self.cEmpSkip = ko.observable(0);

                        GrdClientInsurerBinding(0);
                        self.cinsPager().cinsCurrentPage(1);
                        self.cinsSkip = ko.observable(0);

                        GrdClientThirdPartyAdministratorBinding(0);
                        self.cTpaPager().cTpaCurrentPage(1);
                        self.cTpaSkip = ko.observable(0);
                        $("#btnAddClient").hide();
                        $("#DivShowAllGrid").show();
                    }
                });
            }
            else if (_model == 0) {
                $("#ClientName").focus();
                alertify.alert("Client already exists.");

            }
            else {
                alertify.alert("Error");
            }
        }
        hideLoader();
    };

    //================ Client End ==================//

    //========= dropdownlist start here ===========//

    $(document).ready(function () {
        showLoader();
        $.getJSON("/Insurer/GetAllInsurers", function (dataInsurer) {
            self.InsuranceList(dataInsurer.slice());
        });

        $.getJSON("/Employer/GetAllEmployers", function (dataEmployer) {
            self.EmployerList(dataEmployer.slice());
        });

        $.getJSON("/ManagedCareCompany/GetManagedCareCompanyList", function (dataManagedCareCompany) {
            self.ManagedCareCompanyList(dataManagedCareCompany.slice());
            if (model.ClientManagedCareCompanyDetails[0] != null)
                self.MmcSelectedValue(model.ClientManagedCareCompanyDetails[0].CompanyID);
            else
                self.MmcSelectedValue(0);
        });

        $.getJSON("/ThirdPartyAdministrator/GetThirdPartyAdministratorsList", function (dataThirdPartyAdministrator) {
            self.ThirdPartyAdministratorList(dataThirdPartyAdministrator.slice());
        });
        if (model.ClientDetail != null) {
            $("#btnAddClient").hide();
            $("#DivShowAllGrid").show();

        }
        else {
            $("#DivShowAllGrid").hide();
        }
        hideLoader();
    });


   


    //========= dropdownlist end here ===========//

    function GetClaimAdministratorAllByClientID(ClientID) {
        $.post("/Client/GetClaimAdministratorAllByClientID", { ClientID: $("#ClientID").val() }, function (model) {
            if (model != "Error") {
                var _model = $.parseJSON(model);
                self.ClaimAdministratorAllByClientIDDetails(_model);
                //self.ClaimAdministratorAllByClientIDDetails = ko.observableArray([self.ClaimAdministratorAllByClientIDDetails(0)]);
                //self.ClmAdSelectedValue = ko.observable(0);
            }
        });
    }
    //========= Popup ===========//


    $(".CliamAdminAddPopup").click(function () {
        // GetClaimAdministratorAllByClientID();
    });

    //========= Popup end===========//


    //========= client Insurer Code start here ===========//

    self.ClientInsuerFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    self.AddClientInsurerDetailSuccess = function (model) {
        showLoader();
        if (model != null) {
            var _model = $.parseJSON(model);

            if (_model.ClientInsurerID != 0) {
                alertify.alert("Save Sucessfully", function (e) {
                    if (e) {
                        $("#HFInsurerID").val(_model.InsurerID);
                        GrdClientInsurerBinding(0);
                        GetClaimAdministratorAllByClientID($("#ClientID").val());
                        ShowHideButtonForInsuranceBranch($("#HFInsurerID").val());
                        $('.modal.in').modal('hide');
                    }

                });
            }
            else {
                alertify.alert("Error");
            }
        }
        hideLoader();
    };

    function ShowHideButtonForInsuranceBranch(_InsurerID) {
        $.post("/Client/GetClientInsuranceBranchesAllByInsurerID", { _clientID: $("#ClientID").val(), _InsurerID: _InsurerID }, function (dataInsurerBranch) {
            var _model = $.parseJSON(dataInsurerBranch);

            if (_model.length != 0) {
                $("#OpenPopUpClientInsurer").hide();
                $("#OpenPopUpClientInsuranceBranch").show();
            }
            else {
                $("#OpenPopUpClientInsurer").hide();
                $("#OpenPopUpClientInsuranceBranch").hide();
            }

        });
    }
    $("#OpenPopUpClientInsurer").click(function () {
        $("#InsurerClientID").val($("#ClientID").val());
        blockPopupBackground();
        self.InsSelectedValue(1);
    });




    $(".ClosePop").click(function () {
        unblockPopupBackground();
    });

    self.DeleteClientInsurerByID = function (_data) {

        if ((_data.InsType() == 'ins') || (_data.InsType() == 'INS')) {
            alertify.confirm("Are you sure you want to delete the client insurer and its branch(s)?", function (e) {
                showLoader();
                if (e) {
                    $.post("/Client/DeleteClientInsurerByID", { _ClientInsurerID: _data.ClientInsurerID, _insurerID: _data.InsurerID, _insType: _data.InsType }, function (_responseText) {

                        if (e) {
                            alertify.alert(_responseText);
                            if (_responseText != "Error") {
                                GrdClientInsurerBinding(0);
                                GetClaimAdministratorAllByClientID($("#ClientID").val());
                                self.cinsPager().cinsCurrentPage(1);
                                self.cinsSkip = ko.observable(0);
                                $("#OpenPopUpClientInsurer").show();
                                $("#OpenPopUpClientInsuranceBranch").hide();
                            }
                        }
                    });
                }
                hideLoader();
            });
        }
        else {
            alertify.confirm("Are you sure you want to delete the client insurer branch?", function (e) {
                showLoader();
                if (e) {
                    $.post("/Client/DeleteClientInsurerByID", { _ClientInsurerID: _data.ClientInsurerID, _insurerID: _data.InsurerID, _insType: _data.InsType }, function (_responseText) {

                        if (e) {
                            alertify.alert(_responseText);
                            if (_responseText != "Error") {
                                GrdClientInsurerBinding(0);
                                GetClaimAdministratorAllByClientID($("#ClientID").val());
                                self.cinsPager().cinsCurrentPage(1);
                                self.cinsSkip = ko.observable(0);
                                $("#OpenPopUpClientInsurer").hide();
                                $("#OpenPopUpClientInsuranceBranch").show();
                            }
                        }
                    });
                }
                hideLoader();
            });
        }
        //hideLoader();*/
    }

    function GrdClientInsurerBinding(_skip) {
        $.post("/Client/GetClientInsurerByClientID", {
            _clientID: $("#ClientID").val(), skip: _skip
        }, function (_data) {
            var model = $.parseJSON(_data);
            self.ClientInsurerDetails.removeAll();
            ko.mapping.fromJS(model.ClientInsurerDetails, {}, self.ClientInsurerDetails);
            self.cinsTotalItemCount(model.cinsTotalCount);

        });

    }

    //========= client Insurer Code end here ===========//



    //========= client InsurerBranch Code start here ===========//

    self.ClientInsuranceBranchFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    self.AddClientInsuranceBranchDetailSuccess = function (model) {
        showLoader();
        if (model != null) {
            var _model = $.parseJSON(model);

            if (_model != 0) {
                alertify.alert("Saved Successfully", function (e) {
                    if (e) {
                        GrdClientInsurerBinding(0);
                        GetClaimAdministratorAllByClientID($("#ClientID").val());
                        $('.modal.in').modal('hide');
                        var insurerID = _model;
                        $.post("/Client/GetClientInsuranceBranchesAllByInsurerID", { _clientID: $("#ClientID").val(), _insurerID: insurerID }, function (dataInsurerBranch) {
                            var _cnt = $.parseJSON(dataInsurerBranch);
                            if (_cnt.length == 0) {
                                $("#OpenPopUpClientInsurer").hide();
                                $("#OpenPopUpClientInsuranceBranch").hide();
                            }
                            else {
                                $("#OpenPopUpClientInsurer").hide();
                                $("#OpenPopUpClientInsuranceBranch").show();
                            }
                        });
                    }
                    self.cinsPager().cinsCurrentPage(1);
                    self.cinsSkip = ko.observable(0);
                });
            }
            else {
                alertify.alert("Error");
            }

        };
        hideLoader();
    };



    $("#OpenPopUpClientInsuranceBranch").click(function () {
        // $("#HFInsurerID").val();
        $("#InsuranceBranchClientID").val($("#ClientID").val());
        $.post("/Client/GetClientInsuranceBranchesAllByInsurerID", { _clientID: $("#ClientID").val(), _insurerID: $("#HFInsurerID").val() }, function (dataInsurerBranch) {
            var _model = $.parseJSON(dataInsurerBranch);
            self.InsuranceBranchList(_model.slice());
        });

        blockPopupBackground();
        self.InsBranchSelectedValue(1);
    });




    $(".ClosePop").click(function () {
        unblockPopupBackground();
    });

    self.deleteClientInsuranceBranchByID = function (_data) {
        showLoader();
        alertify.confirm("Are you sure  you want to delete the client insurer banch?", function (e) {
            if (e) {
                $.post("/Client/deleteClientInsuranceBranchByID", { _ClientInsurerBranchID: _data.ClientInsurerBranchID }, function (_responseText) {

                    if (e) {
                        alertify.alert(_responseText);
                        if (_responseText != "Error") {
                            GrdClientInsurerBranchBinding(0);
                            GetClaimAdministratorAllByClientID($("#ClientID").val());
                        }
                    }
                });
            }
        });
        hideLoader();
    }


    //========= client InsurerBranch Code end here ===========//



    //========= client Employer Code start here ===========//

    self.ClientEmployerFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    self.AddClientEmployerDetailSuccess = function (model) {
        showLoader();
        if (model != null) {
            var _model = $.parseJSON(model);

            if (_model == "Saved Successfully" || _model == "Updated Successfully") {
                alertify.alert(_model, function (e) {
                    if (e) {
                        GrdClientEmployerBinding(0);
                        GetClaimAdministratorAllByClientID($("#ClientID").val());
                        self.cEmpPager().cEmpCurrentPage(1);
                        self.cEmpSkip = ko.observable(0);
                        $('.modal.in').modal('hide');
                    }

                });
            }
            else {
                alertify.alert(_model);
            }
        };
        hideLoader();
    };



    $("#OpenPopUpClientEmployer").click(function () {
        $("#EmployerClientID").val($("#ClientID").val());
        blockPopupBackground();
        self.EmpSelectedValue(1);
    });


    $(".ClosePop").click(function () {
        unblockPopupBackground();
    });

    self.DeleteClientEmployerByID = function (_data) {
        showLoader();
        alertify.confirm("Are you sure  you want to delete the client employer?", function (e) {
            if (e) {
                $.post("/Client/DeleteClientEmployerByID", { _ClientEmployerID: _data.ClientEmployerID }, function (_responseText) {

                    if (e) {
                        alertify.alert(_responseText);
                        if (_responseText != "Error") {
                            GrdClientEmployerBinding(0);
                            GetClaimAdministratorAllByClientID($("#ClientID").val());
                            self.cEmpPager().cEmpCurrentPage(1);
                            self.cEmpSkip = ko.observable(0);
                        }
                    }
                });
            }
        });
        hideLoader();
    }

    function GrdClientEmployerBinding(_skip) {
        $.post("/Patient/getEmployerByClientIdAll", {
            clientID: $("#ClientID").val(), skip: _skip
        }, function (_data) {
            var model = $.parseJSON(_data);
            self.ClientEmployerDetails.removeAll();
            self.ClientEmployerDetails(model.ClientEmployerDetails);
            self.cEmpTotalItemCount(model.TotalCount);

        });

    }

    //========= client Employer Code end here ===========//



    //========= client ManagedCareCompany Code start here ===========//

    self.ClientManagedCareCompanyFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    self.AddClientManagedCareCompanyDetailSuccess = function (model) {
        showLoader();
        if (model != null) {
            var _model = $.parseJSON(model);

            if (_model == "Saved Successfully" || _model == "Updated Successfully") {
                alertify.alert(_model, function (e) {
                    if (e) {
                        GrdClientManagedCareCompanyBinding(0);
                        self.cMmcPager().cMmcCurrentPage(1);
                        self.cMmcSkip = ko.observable(0);
                        //$("#addClientManagedCareCompany").hide();
                        $('.modal.in').modal('hide');
                    }

                });
            }
            else {
                alertify.alert(_model);
            }
        };
        hideLoader();
    };



    $("#OpenPopUpClientManagedCareCompany").click(function () {
        $("#CompanyClientID").val($("#ClientID").val());
        blockPopupBackground();
        self.MmcSelectedValue(1);
    });

    $("#btnAddClient").click(function () {

        if ($("#ClientName").val() == "") {
            $("#ClientName").focus();
            alertify.alert("Enter any client name");
            return false;
        }
        else {
            showTabs();
            return true;
        }

    });


    $("#btnAddClientMMC").click(function () {
        showLoader();
        $("#CompanyClientID").val($("#ClientID").val());
        $.post("/Client/UpdateClientManagedCareCompany", {
            _ClientID: $("#ClientID").val() != null ? $("#ClientID").val() : 0, _ManagedCareCompanyID: $("#ddlManagedCareCompany").val()
        }, function (_data) {
            var _model = $.parseJSON(_data);
            if (_model == "Saved Successfully" || _model == "Updated Successfully") {
                alertify.alert(_model, function (e) {
                    if (e) {
                        GetClaimAdministratorAllByClientID($("#ClientID").val());
                    }
                });
            }
            else {
                alertify.alert(_model);
            }
        });
        hideLoader();
    });

    $("#btnUpdateClient").click(function () {
        if ($("#ClientName").val() == "") {
            $("#ClientName").focus();
            alertify.alert("Enter any client name");
        }
        else if ($("#ClaimAdministrator").val() == "") {
            $("#ClaimAdministrator").focus();
            alertify.alert("Select any claim administrator");
        }
        else {
            showLoader();
            $.post("/Client/UpdateClaimAdministratorByClientID", { _ClientID: $("#ClientID").val(), _ClientName: $("#ClientName").val(), _ids: $("#ClaimAdministrator").val() }, function (_data) {
                var _model = $.parseJSON(_data);
                if (_model != 0) {
                    alertify.alert("Updated Successfully");
                    //window.location = '/Client/Index';
                }
                else if (_model == 0) {
                    $("#ClientName").focus();
                    alertify.alert("Client already exists.");
                }
                else {
                    alertify.alert(_model);
                }
            });
            hideLoader();
        }
    });

    $(".ClosePop").click(function () {
        unblockPopupBackground();
    });

    self.DeleteClientManagedCareCompanyByID = function (_data) {
        showLoader();
        alertify.confirm("Are you sure  you want to delete the client managed care company?", function (e) {
            if (e) {
                $.post("/Client/DeleteClientManagedCareCompanyByID", { _ClientCompanyID: _data.ClientCompanyID }, function (_responseText) {

                    if (e) {
                        alertify.alert(_responseText);
                        if (_responseText != "Error") {
                            GrdClientManagedCareCompanyBinding(0);
                            self.cMmcPager().cMmcCurrentPage(1);
                            self.cMmcSkip = ko.observable(0);
                        }
                    }
                });
            }
        });
        hideLoader();
    }

    function GrdClientManagedCareCompanyBinding(_skip) {
        $.post("/Patient/getManagedCareCompanyByClientIdAll", {
            clientID: $("#ClientID").val(), skip: _skip
        }, function (_data) {
            var model = $.parseJSON(_data);
            self.ClientManagedCareCompanyDetails.removeAll();
            self.ClientManagedCareCompanyDetails(model.ClientManagedCareCompanyDetails);
            self.cMmcTotalItemCount(model.TotalCount);

        });

    }

    //========= client ManagedCareCompany Code end here ===========//



    //========= client ThirdPartyAdministrator Code start here ===========//

    self.ClientThirdPartyAdministratorFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    self.AddClientThirdPartyAdministratorDetailSuccess = function (model) {
        showLoader();

        if (model != null) {
            var _model = $.parseJSON(model);

            if (_model.ClientTPAID != 0) {
                alertify.alert("Save Sucessfully", function (e) {
                    if (e) {
                        $("#HFTPAID").val(_model.TPAID);
                        GrdClientThirdPartyAdministratorBinding(0);
                        GetClaimAdministratorAllByClientID($("#ClientID").val());
                        //self.cTpaPager().cTpaCurrentPage(1);
                        //self.cTpaPager = ko.observable(0);
                        ShowHideButtonForTPABranch($("#HFTPAID").val());
                        $('.modal.in').modal('hide');
                    }

                });
            }
            else {
                alertify.alert("Error");
            }
        };
        hideLoader();
    };

    function ShowHideButtonForTPABranch(_TPAID) {
        $.post("/Client/GetClientTPABranchesAllByTPAID", { _clientId: $("#ClientID").val(), _tpaID: _TPAID }, function (dataTPABranch) {
            var _model = $.parseJSON(dataTPABranch);
            if (_model.length != 0) {
                $("#OpenPopUpClientThirdPartyAdministrator").hide();
                $("#OpenPopUpClientThirdPartyAdministratorBranch").show();
            }
            else {
                $("#OpenPopUpClientThirdPartyAdministrator").hide();
                $("#OpenPopUpClientThirdPartyAdministratorBranch").hide();
            }
        });
    }


    $("#OpenPopUpClientThirdPartyAdministrator").click(function () {
        $("#TPAClientID").val($("#ClientID").val());
        blockPopupBackground();
        self.TpaSelectedValue(1);
    });


    $(".ClosePop").click(function () {
        unblockPopupBackground();
    });

    self.DeleteClientThirdPartyAdministratorByID = function (_data) {
        showLoader();
        if ((_data.TPAType() == 'tpa') || (_data.TPAType() == 'TPA'))  {
            alertify.confirm("Are you sure  you want to delete the client third party administrator And its branch(s)?", function (e) {
                if (e) {
                    $.post("/Client/DeleteClientThirdPartyAdministratorByID", { _ClientTPAID: _data.ClientTPAID, _TPAID: _data.TPAID, _tpaType: _data.TPAType }, function (_responseText) {
                        if (e) {
                            alertify.alert(_responseText);
                            if (_responseText != "Error") {
                                GrdClientThirdPartyAdministratorBinding(0);
                                GetClaimAdministratorAllByClientID($("#ClientID").val());
                                self.cTpaPager().cTpaCurrentPage(1);
                                self.cTpaSkip = ko.observable(0);
                                $("#OpenPopUpClientThirdPartyAdministrator").show();
                                $("#OpenPopUpClientThirdPartyAdministratorBranch").hide();
                            }
                        }
                    });
                }
            });
        }
        else {
            alertify.confirm("Are you sure  you want to delete the client third party administrator branch?", function (e) {
                if (e) {
                    $.post("/Client/DeleteClientThirdPartyAdministratorByID", { _ClientTPAID: _data.ClientTPAID, _TPAID: _data.TPAID, _tpaType: _data.TPAType }, function (_responseText) {
                        if (e) {
                            alertify.alert(_responseText);
                            if (_responseText != "Error") {
                                GrdClientThirdPartyAdministratorBinding(0);
                                GetClaimAdministratorAllByClientID($("#ClientID").val());
                                self.cTpaPager().cTpaCurrentPage(1);
                                self.cTpaSkip = ko.observable(0);
                                $("#OpenPopUpClientThirdPartyAdministrator").hide();
                                $("#OpenPopUpClientThirdPartyAdministratorBranch").show();
                            }
                        }
                    });
                }
            });
        }
        hideLoader();
    }

    //function GrdClientThirdPartyAdministratorBinding(_skip) {
    //    $.post("/Patient/getThirdPartyAdministratorByClientIdAll", {
    //        clientID: $("#ClientID").val(), skip: _skip
    //    }, function (_data) {
    //        var model = $.parseJSON(_data);
    //        self.ClientThirdPartyAdministratorDetails.removeAll();
    //        self.ClientThirdPartyAdministratorDetails(model.ClientTPADetails);
    //        self.cTpaTotalItemCount(model.TotalCount);

    //    });

    //}


    function GrdClientThirdPartyAdministratorBinding(_skip) {
        $.post("/Client/GetClientThirdPartyAdministratorByClientID", {
            _clientID: $("#ClientID").val(), skip: _skip
        }, function (_data) {
            var model = $.parseJSON(_data);
            self.ClientThirdPartyAdministratorDetails.removeAll();
            ko.mapping.fromJS(model.ClientThirdPartyAdministratorDetails, {}, self.ClientThirdPartyAdministratorDetails);
            self.cTpaTotalItemCount(model.cTpaTotalCount);

        });

    }

    //========= client ThirdPartyAdministratorCode end here ===========//



    //========= client ThirdPartyAdministratorBranch Code start here ===========//

    self.ClientThirdPartyAdministratorBranchFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    };
    self.AddClientThirdPartyAdministratorBranchDetailSuccess = function (model) {
        showLoader();
        if (model != null) {
            var _model = $.parseJSON(model);
            if (_model != 0) {
                alertify.alert("Saved Successfully", function (e) {
                    if (e) {
                        GrdClientThirdPartyAdministratorBinding(0);
                        GetClaimAdministratorAllByClientID($("#ClientID").val());
                        $('.modal.in').modal('hide');

                        var tpaID = _model;
                        $.post("/Client/GetClientTPABranchesAllByTPAID", { _clientID: $("#ClientID").val(), _tpaID: tpaID }, function (dataInsurerBranch) {
                            var _cnt = $.parseJSON(dataInsurerBranch);
                            if (_cnt.length == 0) {
                                $("#OpenPopUpClientThirdPartyAdministrator").hide();
                                $("#OpenPopUpClientThirdPartyAdministratorBranch").hide();
                            }
                            else {
                                $("#OpenPopUpClientThirdPartyAdministrator").hide();
                                $("#OpenPopUpClientThirdPartyAdministratorBranch").show();
                            }
                        });
                    }
                    self.cTpaPager().cTpaCurrentPage(1);
                    self.cTpaSkip = ko.observable(0);
                });
            }
            else {
                alertify.alert("Error");
            }
        };
        hideLoader();
    };

    $("#OpenPopUpClientThirdPartyAdministratorBranch").click(function () {
        showLoader();
        $("#TPABranchClientID").val($("#ClientID").val());
        $.post("/Client/GetClientTPABranchesAllByTPAID", { _clientId: $("#ClientID").val(), _tpaID: $("#HFTPAID").val() }, function (dataTPABranch) {
            var _model = $.parseJSON(dataTPABranch);
            self.ThirdPartyAdministratorBranchList(_model.slice());
        });
        blockPopupBackground();
        self.TpaBranchSelectedValue(1);
        hideLoader();
    });


    $(".ClosePop").click(function () {
        unblockPopupBackground();
    });

    //========= client ThirdPartyAdministratorBranchCode end here ===========//




    //========= paging code for cins grid only ===========//




    self.cinsGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.cinsSkip(0);
            self.cinsTake(cinspagingSetting.cinspageSize);
        }
        else {
            self.cinsSkip(skip);
            self.cinsTake(take);
        }
        GrdClientInsurerBinding(skip);
    }

    var cinspagingSetting = {
        cinspageSize: 5,
        cinspageSlide: 2
    };
    self.cinsSkip = ko.observable(0);
    self.cinsTake = ko.observable(cinspagingSetting.cinspageSize);
    self.cinsPager = ko.cinspager(self.cinsTotalItemCount);

    self.cinsPager().cinsPageSize(cinspagingSetting.cinspageSize);
    self.cinsPager().cinsPageSlide(cinspagingSetting.cinspageSlide);
    self.cinsPager().cinsCurrentPage(1);

    self.cinsPager().cinsCurrentPage.subscribe(function () {
        var skip = cinspagingSetting.cinspageSize * (self.cinsPager().cinsCurrentPage() - 1);
        var take = cinspagingSetting.cinspageSlide;
        self.cinsGetRecordsWithSkipTake(skip, take);
    });




    // ============= paging code for cins ends =============== //

    //========= paging code for cEmp grid only ===========//




    self.cEmpGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.cEmpSkip(0);
            self.cEmpTake(cEmppagingSetting.cEmppageSize);
        }
        else {
            self.cEmpSkip(skip);
            self.cEmpTake(take);
        }
        GrdClientEmployerBinding(skip);
    }

    var cEmppagingSetting = {
        cEmppageSize: 5,
        cEmppageSlide: 2
    };
    self.cEmpSkip = ko.observable(0);
    self.cEmpTake = ko.observable(cEmppagingSetting.cEmppageSize);
    self.cEmpPager = ko.cEmppager(self.cEmpTotalItemCount);

    self.cEmpPager().cEmpPageSize(cEmppagingSetting.cEmppageSize);
    self.cEmpPager().cEmpPageSlide(cEmppagingSetting.cEmppageSlide);
    self.cEmpPager().cEmpCurrentPage(1);

    self.cEmpPager().cEmpCurrentPage.subscribe(function () {
        var skip = cEmppagingSetting.cEmppageSize * (self.cEmpPager().cEmpCurrentPage() - 1);
        var take = cEmppagingSetting.cEmppageSlide;
        self.cEmpGetRecordsWithSkipTake(skip, take);
    });




    // ============= paging code for cEmp ends =============== //


    //========= paging code for cMmc grid only ===========//


    /*

    self.cMmcGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.cMmcSkip(0);
            self.cMmcTake(cMmcpagingSetting.cMmcpageSize);
        }
        else {
            self.cMmcSkip(skip);
            self.cMmcTake(take);
        }
        GrdClientManagedCareCompanyBinding(skip);
    }

    var cMmcpagingSetting = {
        cMmcpageSize: 5,
        cMmcpageSlide: 2
    };
    self.cMmcSkip = ko.observable(0);
    self.cMmcTake = ko.observable(cMmcpagingSetting.cMmcpageSize);
    self.cMmcPager = ko.cMmcpager(self.cMmcTotalItemCount);

    self.cMmcPager().cMmcPageSize(cMmcpagingSetting.cMmcpageSize);
    self.cMmcPager().cMmcPageSlide(cMmcpagingSetting.cMmcpageSlide);
    self.cMmcPager().cMmcCurrentPage(1);

    self.cMmcPager().cMmcCurrentPage.subscribe(function () {
        var skip = cMmcpagingSetting.cMmcpageSize * (self.cMmcPager().cMmcCurrentPage() - 1);
        var take = cMmcpagingSetting.cMmcpageSlide;
        self.cMmcGetRecordsWithSkipTake(skip, take);
    });


    */

    // ============= paging code for cMmc ends =============== //



    //========= paging code for cTpa grid only ===========//




    self.cTpaGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.cTpaSkip(0);
            self.cTpaTake(cTpapagingSetting.cTpapageSize);
        }
        else {
            self.cTpaSkip(skip);
            self.cTpaTake(take);
        }
        GrdClientThirdPartyAdministratorBinding(skip);
    }

    var cTpapagingSetting = {
        cTpapageSize: 5,
        cTpapageSlide: 2
    };
    self.cTpaSkip = ko.observable(0);
    self.cTpaTake = ko.observable(cTpapagingSetting.cTpapageSize);
    self.cTpaPager = ko.cTpapager(self.cTpaTotalItemCount);

    self.cTpaPager().cTpaPageSize(cTpapagingSetting.cTpapageSize);
    self.cTpaPager().cTpaPageSlide(cTpapagingSetting.cTpapageSlide);
    self.cTpaPager().cTpaCurrentPage(1);

    self.cTpaPager().cTpaCurrentPage.subscribe(function () {
        var skip = cTpapagingSetting.cTpapageSize * (self.cTpaPager().cTpaCurrentPage() - 1);
        var take = cTpapagingSetting.cTpapageSlide;
        self.cTpaGetRecordsWithSkipTake(skip, take);
    });


}

    // ============= paging code for cTpa ends =============== //


$("#tabClientBilling").click(function () {
    showLoader();
    $.ajax({
        url: '/Client/GetClientBillingDetail',
        cache: false,
        type: "GET",
        data: { _clientID: $("#ClientID").val() },
        success: function (data) {
            var _data = $.parseJSON(data);
            var _ClientBillingViewModel = new ClientBillingViewModel();
            _ClientBillingViewModel.Init(_data);
            ko.cleanNode($("#divClientBilling").get(0));
            ko.applyBindings(_ClientBillingViewModel, $("#divClientBilling").get(0));
            $("#tabClientBilling").off("click");
            $("#ClientBillingClientID").val($("#ClientID").val());
            //$("#CPTTags").tagsInput();
            hideLoader();
        },
        error: function (reponse) {
            alert("error : " + reponse);
            hideLoader();
        }
    });
});
 
function enableClientStatementStart() {
    if ($("#chkStatementStart").is(":checked")) {
        $("#divStatementStart").css("display", "block");
        $("#ClientStatementStart").removeAttr("readonly");
        $("#ClientStatementStart").focus();
    } else {
        $("#divStatementStart").css("display", "none");
      //  $("#divStatementStart").hide();
        $("#ClientStatementStart").attr("readonly", "readonly");
    }
}

function ClientBillingViewModel() {
    var self = this;
    self.enableinput = ko.observable(false);
    self.enableRadio = ko.observable("1");
    self.requiredWholesaleImput = ko.observable(false);

    self.States = ko.observableArray();
    self.States = ko.observableArray([self.States(0)]);
    self.selectedItem = ko.observable(2);

    self.PrivateLabelStateId = ko.observable();

    self.ClientID = ko.observable();

    self.ClientAttentionToID = ko.observable();

    self.ClientPrivateLabelName = ko.observable();
    self.ClientPrivateEmailID = ko.observable();
    self.ClientPrivateLabelID = ko.observable();
    self.ClientPrivateLabelAddress = ko.observable();
    self.ClientPrivateLabelCity = ko.observable();
    self.ClientPrivateLabelZip = ko.observable();
    self.ClientPrivateLabelStateID = ko.observable();
    self.ClientPrivateLabelLogoName = ko.observable();
    self.ClientPrivateLabelPhone = ko.observable();
    self.ClientPrivateLabelFax = ko.observable();
    self.ClientPrivateLabelTax = ko.observable();
    self.ClientAttentionToSelectedItem = ko.observable(0);
    self.ClientStatementStart = ko.observable();
    self.ClientAttentionTos = ko.observableArray([{ ClientAttentionToID: 1, ClientAttentionTo: "NULL" }, { ClientAttentionToID: 2, ClientAttentionTo: "Adjuster" }, { ClientAttentionToID: 3, ClientAttentionTo: "Free Text" }]);



    self.Init = function (model) {
        if (model.ClientBillingDetail != null) {
            if (model.ClientBillingDetail.ClientIsPrivateLabel == true) {
                self.enableinput(true);
                $('.required-link-hide').show();
            }
            else {
                self.enableinput(false); fnClearWholesaleInput();
                $('.required-link-hide').hide();
            }
            self.enableRadio(model.ClientBillingDetail.ClientBillingRateTypeID.toString());
            self.ClientAttentionToSelectedItem(model.ClientBillingDetail.ClientAttentionToID);

            if (model.ClientBillingDetail.ClientAttentionToID == 3)
                $("#divClientAttentionToFreeText").fadeIn(800)
            else
                $("#divClientAttentionToFreeText").fadeOut();

            if ((model.ClientBillingDetail.ClientInvoiceNumber != null) || (model.ClientBillingDetail.ClientInvoiceNumber != ''))
                //  $("#ClientInvoiceNumber").removeAttr("disabled");
                $("#ClientInvoiceNumber").attr("readonly", "readonly");
                //  var a = 1;
            else
                $("#divClientAttentionToFreeText").removeAttr("disabled");
        }
        else
            $("#divClientAttentionToFreeText").fadeOut();

        $.getJSON("/Common/getStates", function (data) {
            self.States(data.slice());
            if (model.ClientPrivateLabelDetail != null)
                self.selectedItem(model.ClientPrivateLabelDetail.ClientPrivateLabelStateID);
        });
        ko.mapping.fromJS(model, {}, self);
    };

    self.ClearWholesaleInput = function () {
        fnClearWholesaleInput();
        self.selectedItem(61);
        if ($("#checkboxPrivateLabel").is(':checked'))
            $('.required-link-hide').show();
        else
            $('.required-link-hide').hide();
        return true;
    };

    self.ShowClientPrivateLabel = function () {
        $("#txtZip").mask("99999?-9999")
             .blur(function () {
                 var lastFour = $(this).val().substr(6, 4);
                 if (lastFour != "") {
                     if (lastFour.length != 4) {
                         $(this).val("");
                     }
                 }
             });

        $(".phoneMaskformat").mask("(999) 999-9999");
        $(".tin").mask("99-9999999");
        $('.required_removebdr').addClass("required_remove_bdr");

        $.ajax({
            url: '/Client/GetClientPrivateLabelByClientID',
            cache: false,
            type: "GET",
            data: { _clientID: $("#ClientID").val() },
            success: function (data) {
                if (data != null) {
                    var _data = $.parseJSON(data);
                    self.ClientPrivateLabelName(_data.ClientPrivateLabelName);
                    self.ClientPrivateEmailID(_data.ClientPrivateEmailID);
                    self.ClientPrivateLabelID(_data.ClientPrivateLabelID);
                    self.ClientPrivateLabelAddress(_data.ClientPrivateLabelAddress);
                    self.ClientPrivateLabelCity(_data.ClientPrivateLabelCity);
                    self.ClientPrivateLabelZip(_data.ClientPrivateLabelZip);
                    self.selectedItem(_data.ClientPrivateLabelStateID);
                    self.ClientPrivateLabelPhone(_data.ClientPrivateLabelPhone)
                    self.ClientPrivateLabelFax(_data.ClientPrivateLabelFax)
                    self.ClientID($("#ClientID").val());
                    self.ClientPrivateLabelLogoName(_data.ClientPrivateLabelLogoName);
                    self.ClientPrivateLabelTax(_data.ClientPrivateLabelTax);                  
                    if (_data.ClientStatementStart != "" && _data.ClientStatementStart != null) {
                        self.ClientStatementStart(_data.ClientStatementStart);
                        $("#chkStatementStart").attr("disabled", "disabled");
                        $("#chkStatementStart").prop("checked", true);
                        $("#divStatementStart").css("display", "block"); 
                        $("#ClientStatementStart").attr("readonly", "readonly");
                    }

                   

                    $('[data-toggle="popover"]').popover("destroy");
                    $('[data-toggle="popover"]').popover({
                        placement: 'top',
                        trigger: 'hover',
                        html: true,
                        content: '<div class="media"><img src="' + $('#ClientPrivateLabelLogoName').val() + '" style="max-width:245px; max-height:250px;" /></div>'
                    })
                    $("#ClientPrivateLabelLogo").replaceWith($("#ClientPrivateLabelLogo").clone(true));
                }
                else {
                    self.ClientPrivateLabelName('');
                    self.ClientPrivateEmailID('');
                    self.ClientPrivateLabelID('');
                    self.ClientPrivateLabelAddress('');
                    self.ClientPrivateLabelCity('');
                    self.ClientPrivateLabelZip('');
                    self.selectedItem(61);
                    self.ClientPrivateLabelLogoName('');
                    self.ClientPrivateLabelPhone('')
                    self.ClientPrivateLabelFax('')
                    self.ClientID($("#ClientID").val());
                    self.ClientPrivateLabelTax('');
                }
                $("#txtClientPrivateLabelName").focus();
            }
        });
        blockPopupBackground();
    };

    function fnClearWholesaleInput() {
        if (self.ClientBillingWholesaleRateDetail != null) {
            $('.required_removebdr').addClass("required_remove_bdr");
            $('.clearWholesaleInput').val('');
        }
    };



    self.SelectClientAttentionTo = function () {
        if ($("#ClientAttentionTo").val() == "3")
            $("#divClientAttentionToFreeText").fadeIn(800)
        else
            $("#divClientAttentionToFreeText").fadeOut(1000);
    };


    self.ClientBillingFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        if ($("#ClientAttentionTo").val() == "3") {
            if ($("#ClientAttentionToFreeText").val().trim() == "") {
                $("#ClientAttentionToFreeText").focus();
                alertify.alert("Please enter Free Text Notes");
                return false;
            }
        }
        showLoader();
        return true;
    }
    self.AddClientBillingDetailSuccess = function (model) {
        showLoader();
        if (model != null) {
            var _model = $.parseJSON(model);
            if (_model != 0) {
                if (_model[0] == "Add") {
                    alertify.alert("Inserted Successfully", function (e) {
                        if (e) {
                            $("#ClientBillingID").val(_model[1]);
                            $("#ClientBillingRetailRateID").val(_model[2]);
                            $("#ClientBillingWholesaleRateID").val(_model[3]);

                        }
                    });
                }
                else if (_model[0] == "Update") {
                    alertify.alert("Updated Successfully", function (e) {
                        if (e) {
                            $("#ClientBillingID").val(_model[1]);
                            $("#ClientBillingRetailRateID").val(_model[2]);
                            $("#ClientBillingWholesaleRateID").val(_model[3]);
                        }
                    });
                }
                $("#ClientInvoiceNumber").attr("readonly", "readonly");
            }
            else {
                alertify.alert("Error");
            }
        }
        hideLoader();
    };

    self.ClientPrivateLabelFormBeforeSubmit = function (arr, $form, options) {

        if ($("#ClientPrivateLabelLogo").val() != "") {
            var sFileName = $("#ClientPrivateLabelLogo").val();
            var sFileExtension = sFileName.split('.')[sFileName.split('.').length - 1].toLowerCase();
            if (!(sFileExtension === "ico" || sFileExtension === "png" || sFileExtension === "jpg")) {
                $("#lblFileValidation").html("Please make sure logo is in jpg or png or bitmap format");
                return false;
            }
            else {
                $("#lblFileValidation").html("");
            }

            if ($("#ClientPrivateLabelLogoName").val() != '') {
                if (confirm("Do you wanna replace the old logo?"))
                    return true;
                else
                    return false;
            }
        }



        if ($form.jqBootstrapValidation('hasErrors')) {
            return false;
        }
        showLoader();
        return true;
    }
    self.AddClientPrivateLabelDetailSuccess = function (model) {
        showLoader();
        if (model != null) {
            var _model = $.parseJSON(model);
            if (_model != 0) {
                if (_model[0] == "Add") {
                    alertify.alert("Inserted Successfully", function (e) {
                        if (e) {
                            $("#ClientPrivateLabelID").val(_model[1]);
                            $('.modal.in').modal('hide');
                        }
                    });
                }
                else if (_model[0] == "Update") {
                    alertify.alert("Updated Successfully", function (e) {
                        if (e) {
                            $("#ClientPrivateLabelID").val(_model[1]);
                            $('.modal.in').modal('hide');
                        }
                    });
                }
                unblockPopupBackground();
            }
            else {
                alertify.alert("Error");
            }
        }
        hideLoader();
    };

    self.ClosePop = function () {
        unblockPopupBackground();
        if (((self.ClientPrivateLabelName() == "") && (self.ClientPrivateLabelAddress() == "") && (self.ClientPrivateEmailID()=="") && (self.ClientPrivateLabelCity() == "") && (self.ClientPrivateLabelZip() == "") && self.selectedItem() == "61") && (self.ClientPrivateLabelTax() == "") ||
        ((self.ClientPrivateLabelName() == null) && (self.ClientPrivateLabelAddress() == null) && (self.ClientPrivateEmailID() == null) && (self.ClientPrivateLabelCity() == null) && (self.ClientPrivateLabelZip() == null) && self.selectedItem() == "61") && (self.ClientPrivateLabelTax() == "")) {
            self.enableinput(false); fnClearWholesaleInput();
        }
    };



}


function showTabs() {
    $("#tabClientBilling").removeClass("hide");
}

    //========== pager js for   cins  grid only ========//
    (function (ko) {
        var cinsnumericObservable = function (initialValue) {
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

        function cinsPager(totalItemCount) {
            var self = this;
            self.cinsCurrentPage = cinsnumericObservable(1);

            self.cinsTotalItemCount = ko.computed(totalItemCount);

            self.cinsPageSize = cinsnumericObservable(10);
            self.cinsPageSlide = cinsnumericObservable(2);

            self.cinsLastPage = ko.computed(function () {
                return Math.floor((self.cinsTotalItemCount() - 1) / self.cinsPageSize()) + 1;
            });

            self.cinsHasNextPage = ko.computed(function () {
                return self.cinsCurrentPage() < self.cinsLastPage();
            });

            self.cinsHasPrevPage = ko.computed(function () {
                return self.cinsCurrentPage() > 1;
            });

            self.cinsFirstItemIndex = ko.computed(function () {
                return self.cinsPageSize() * (self.cinsCurrentPage() - 1) + 1;
            });

            self.cinsLastItemIndex = ko.computed(function () {
                return Math.min(self.cinsFirstItemIndex() + self.cinsPageSize() - 1, self.cinsTotalItemCount());
            });

            self.cinsPages = ko.computed(function () {
                var cinspageCount = self.cinsLastPage();
                var cinspageFrom = Math.max(1, self.cinsCurrentPage() - self.cinsPageSlide());
                var cinspageTo = Math.min(cinspageCount, self.cinsCurrentPage() + self.cinsPageSlide());
                cinspageFrom = Math.max(1, Math.min(cinspageTo - 2 * self.cinsPageSlide(), cinspageFrom));
                cinspageTo = Math.min(cinspageCount, Math.max(cinspageFrom + 2 * self.cinsPageSlide(), cinspageTo));

                var result = [];
                for (var i = cinspageFrom; i <= cinspageTo; i++) {
                    result.push(i);
                }
                return result;
            });
        }

        ko.cinspager = function (totalItemCount) {
            var cinspager = new cinsPager(totalItemCount);
            return ko.observable(cinspager);
        };
    }(ko));
    ////============= pager js for cins ends  ============//


//========== pager js for   cEmp  grid only ========//
(function (ko) {
    var cEmpnumericObservable = function (initialValue) {
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

    function cEmpPager(totalItemCount) {
        var self = this;
        self.cEmpCurrentPage = cEmpnumericObservable(1);

        self.cEmpTotalItemCount = ko.computed(totalItemCount);

        self.cEmpPageSize = cEmpnumericObservable(10);
        self.cEmpPageSlide = cEmpnumericObservable(2);

        self.cEmpLastPage = ko.computed(function () {
            return Math.floor((self.cEmpTotalItemCount() - 1) / self.cEmpPageSize()) + 1;
        });

        self.cEmpHasNextPage = ko.computed(function () {
            return self.cEmpCurrentPage() < self.cEmpLastPage();
        });

        self.cEmpHasPrevPage = ko.computed(function () {
            return self.cEmpCurrentPage() > 1;
        });

        self.cEmpFirstItemIndex = ko.computed(function () {
            return self.cEmpPageSize() * (self.cEmpCurrentPage() - 1) + 1;
        });

        self.cEmpLastItemIndex = ko.computed(function () {
            return Math.min(self.cEmpFirstItemIndex() + self.cEmpPageSize() - 1, self.cEmpTotalItemCount());
        });

        self.cEmpPages = ko.computed(function () {
            var cEmppageCount = self.cEmpLastPage();
            var cEmppageFrom = Math.max(1, self.cEmpCurrentPage() - self.cEmpPageSlide());
            var cEmppageTo = Math.min(cEmppageCount, self.cEmpCurrentPage() + self.cEmpPageSlide());
            cEmppageFrom = Math.max(1, Math.min(cEmppageTo - 2 * self.cEmpPageSlide(), cEmppageFrom));
            cEmppageTo = Math.min(cEmppageCount, Math.max(cEmppageFrom + 2 * self.cEmpPageSlide(), cEmppageTo));

            var result = [];
            for (var i = cEmppageFrom; i <= cEmppageTo; i++) {
                result.push(i);
            }
            return result;
        });
    }

    ko.cEmppager = function (totalItemCount) {
        var cEmppager = new cEmpPager(totalItemCount);
        return ko.observable(cEmppager);
    };
}(ko));
//============= pager js for cEmp ends  ============//



//========== pager js for   cMmc  grid only ========//
/*
(function (ko) {
    var cMmcnumericObservable = function (initialValue) {
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

    function cMmcPager(totalItemCount) {
        var self = this;
        self.cMmcCurrentPage = cMmcnumericObservable(1);

        self.cMmcTotalItemCount = ko.computed(totalItemCount);

        self.cMmcPageSize = cMmcnumericObservable(10);
        self.cMmcPageSlide = cMmcnumericObservable(2);

        self.cMmcLastPage = ko.computed(function () {
            return Math.floor((self.cMmcTotalItemCount() - 1) / self.cMmcPageSize()) + 1;
        });

        self.cMmcHasNextPage = ko.computed(function () {
            return self.cMmcCurrentPage() < self.cMmcLastPage();
        });

        self.cMmcHasPrevPage = ko.computed(function () {
            return self.cMmcCurrentPage() > 1;
        });

        self.cMmcFirstItemIndex = ko.computed(function () {
            return self.cMmcPageSize() * (self.cMmcCurrentPage() - 1) + 1;
        });

        self.cMmcLastItemIndex = ko.computed(function () {
            return Math.min(self.cMmcFirstItemIndex() + self.cMmcPageSize() - 1, self.cMmcTotalItemCount());
        });

        self.cMmcPages = ko.computed(function () {
            var cMmcpageCount = self.cMmcLastPage();
            var cMmcpageFrom = Math.max(1, self.cMmcCurrentPage() - self.cMmcPageSlide());
            var cMmcpageTo = Math.min(cMmcpageCount, self.cMmcCurrentPage() + self.cMmcPageSlide());
            cMmcpageFrom = Math.max(1, Math.min(cMmcpageTo - 2 * self.cMmcPageSlide(), cMmcpageFrom));
            cMmcpageTo = Math.min(cMmcpageCount, Math.max(cMmcpageFrom + 2 * self.cMmcPageSlide(), cMmcpageTo));

            var result = [];
            for (var i = cMmcpageFrom; i <= cMmcpageTo; i++) {
                result.push(i);
            }
            return result;
        });
    }

    ko.cMmcpager = function (totalItemCount) {
        var cMmcpager = new cMmcPager(totalItemCount);
        return ko.observable(cMmcpager);
    };
}(ko));*/
//============= pager js for cMmc ends  ============//




//========== pager js for   cTpa  grid only ========//
(function (ko) {
    var cTpanumericObservable = function (initialValue) {
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

    function cTpaPager(totalItemCount) {
        var self = this;
        self.cTpaCurrentPage = cTpanumericObservable(1);

        self.cTpaTotalItemCount = ko.computed(totalItemCount);

        self.cTpaPageSize = cTpanumericObservable(10);
        self.cTpaPageSlide = cTpanumericObservable(2);

        self.cTpaLastPage = ko.computed(function () {
            return Math.floor((self.cTpaTotalItemCount() - 1) / self.cTpaPageSize()) + 1;
        });

        self.cTpaHasNextPage = ko.computed(function () {
            return self.cTpaCurrentPage() < self.cTpaLastPage();
        });

        self.cTpaHasPrevPage = ko.computed(function () {
            return self.cTpaCurrentPage() > 1;
        });

        self.cTpaFirstItemIndex = ko.computed(function () {
            return self.cTpaPageSize() * (self.cTpaCurrentPage() - 1) + 1;
        });

        self.cTpaLastItemIndex = ko.computed(function () {
            return Math.min(self.cTpaFirstItemIndex() + self.cTpaPageSize() - 1, self.cTpaTotalItemCount());
        });

        self.cTpaPages = ko.computed(function () {
            var cTpapageCount = self.cTpaLastPage();
            var cTpapageFrom = Math.max(1, self.cTpaCurrentPage() - self.cTpaPageSlide());
            var cTpapageTo = Math.min(cTpapageCount, self.cTpaCurrentPage() + self.cTpaPageSlide());
            cTpapageFrom = Math.max(1, Math.min(cTpapageTo - 2 * self.cTpaPageSlide(), cTpapageFrom));
            cTpapageTo = Math.min(cTpapageCount, Math.max(cTpapageFrom + 2 * self.cTpaPageSlide(), cTpapageTo));

            var result = [];
            for (var i = cTpapageFrom; i <= cTpapageTo; i++) {
                result.push(i);
            }
            return result;
        });
    }

    ko.cTpapager = function (totalItemCount) {
        var cTpapager = new cTpaPager(totalItemCount);
        return ko.observable(cTpapager);
    };
}(ko));
//============= pager js for cTpa ends  ============//