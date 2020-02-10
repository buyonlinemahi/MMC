using System;

namespace MMC.Core.BL.Model
{
    public class InitialNotificationLetter
    {
        public string PatientName { get; set; }
        public string ClaimNumber { get; set; }
        public DateTime DOI { get; set; }
        public string Adjuster { get; set; }
        public string ClaimAdministrator { get; set; }
        public DateTime CEReceivedDate { get; set; }
        public string Decision { get; set; }
                
        public string PhysFirstName { get; set; }
        public string PhysLastName { get; set; }
        public string PhysQualification { get; set; }        
        public string PhysAddress1 { get; set; }
        public string PhysCity { get; set; }
        public string PhysStates { get; set; }
        public string PhysZip { get; set; }
        public string PhysFax { get; set; }

    }
}
