
namespace MMC.Core.BL.Model
{
    public class Employer
    {
        public int EmployerID { get; set; }
        public string EmpName { get; set; }
        public string EmpAddress1 { get; set; }
        public string EmpAddress2 { get; set; }
        public string EmpCity { get; set; }
        public int EmpStateId { get; set; }
        public string EmpZip { get; set; }
        public string EmpPhone { get; set; }
        public string EmpFax { get; set; }
        public string EmpEMail { get; set; }
        public string EmpContactName { get; set; }
        public string EmpPhoneExtension { get; set; }
        // State Name
        public string EmpStateName { get; set; }
    }
}
