function CurrentWorkloadViewModel(model)
{
    var self = this;
    self.ClientID = ko.observable();
    self.StatusID = ko.observable();
    self.StartDate = ko.observable();
    self.EndDate = ko.observable();

    //Clients
    self.Client = ko.observable();
    self.Clients = ko.observableArray();
    self.Clients = ko.observableArray([self.Clients(0)]);
    self.selectedclient = ko.observable(0);

    //Status
    self.Status = ko.observable();
    self.Statuses = ko.observableArray();
    self.Statuses = ko.observableArray([self.Statuses(0)]);
    self.selectedstatus = ko.observable(0);

    $(document).ready(function () {
        $.getJSON("/Client/getAllClaimAdministrator", function (data) {
            self.Clients(data.slice());
            $('#objClientID').multiselect('destroy');
            $('#objClientID').multiselect('refresh');
        });

        $.getJSON("/Common/getStatus", function (Statusdata) {
            self.Statuses(Statusdata.slice());
            $('#ddlStatusID').multiselect('destroy');
            $('#ddlStatusID').multiselect('refresh');
        });

    });
   
    self.GetReport = function () {
        if ($("#StartDate").val() != null && $("#EndDate").val() != null && $("#objClientID").val() != null && $("#ddlStatusID").val() != null) {
            self.StartDate($("#StartDate").val());
            self.EndDate($("#EndDate").val());
            self.ClientID($("#objClientID").val());
            self.StatusID($("#ddlStatusID").val());
            showLoader();
            $.post("/CurrentWorkload/GetResultReport?startDate=" + self.StartDate() + "&endDate=" + self.EndDate() + "&ClientId=" + self.ClientID() + "&StatusId=" + self.StatusID(),
                function (data) {
                    if (data.search(".xls")) {
                        window.location = data;
                    }
                    else {
                        alert("Error: " + data);
                        return false;
                    }
                    hideLoader();
                });
        }
        else {
            alert("Select All dropdowns");
        }
    }


};