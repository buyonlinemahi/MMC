
namespace MMC.Core.Data.Model
{
    public class Physician
    {
        public int PhysicianId { get; set; }
        public string PhysFirstName { get; set; }
        public string PhysLastName { get; set; }
        public string PhysQualification { get; set; }
        public int PhysSpecialtyId { get; set; }
        public string PhysNPI { get; set; }
        public string PhysEMail { get; set; }
        public string PhysNotes { get; set; }
        public string PhysAddress1 { get; set; }
        public string PhysAddress2 { get; set; }
        public string PhysCity { get; set; }
        public int PhysStateId { get; set; }
        public string PhysZip { get; set; }
        public string PhysPhone { get; set; }
        public string PhysFax { get; set; }
    }
}
