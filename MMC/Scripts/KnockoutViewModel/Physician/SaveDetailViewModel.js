function SaveDetailViewModel(model) {
    var self = this;
    self.PhysicianId = ko.observable();
    self.PhysFirstName = ko.observable();
    self.PhysLastName = ko.observable();
    self.PhysQualification = ko.observable();
    self.PhysSpecialtyId = ko.observable();
    self.PhysNPI = ko.observable();
    self.PhysEMail = ko.observable();
    self.PhysNotes = ko.observable();
    self.PhysTIN = ko.observable();
    self.PhysAddress1 = ko.observable();
    self.PhysAddress2 = ko.observable();
    self.PhysCity = ko.observable();
    self.PhysStateId = ko.observable();
    self.PhysZip = ko.observable();
    self.PhysPhone = ko.observable();
    self.PhysFax = ko.observable();

    //State
    self.State = ko.observable();
    self.States = ko.observableArray();
    self.States = ko.observableArray([self.States(0)]);
    self.selectedItem = ko.observable(0);

    //Specialty
    self.Speciality = ko.observable();
    self.Specialityes = ko.observableArray();
    self.Specialityes = ko.observableArray([self.Specialityes(0)]);
    self.selectedItemSpeciality = ko.observable(0);

    $(document).ready(function () {
        $.getJSON("/Common/getStates", function (data) {
            self.States(data.slice());
            if (model != null)
                self.selectedItem(model.PhysStateId);
        });      

        $.getJSON("/Common/getSpecialtyAll", function (data) {
            self.Specialityes(data.slice());
            if (model != null)
                self.selectedItemSpeciality(model.PhysSpecialtyId);
        });
    });

    if (model != null) {
        ko.mapping.fromJS(model, {}, self);
    }


    self.AddPhysicianSuccess = function (responseText) {
        alertify.confirm(responseText, function (e) {
            if (e) {
                showLoader();
                window.location = '/Physician/Index';
            }
        });        
    };



    self.PhysicianInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($form.jqBootstrapValidation('hasErrors')) {            
            return false;
        }
        return true;
    }
   
};