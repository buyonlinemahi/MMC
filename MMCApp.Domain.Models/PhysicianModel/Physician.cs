using System.ComponentModel.DataAnnotations;
namespace MMCApp.Domain.Models.PhysicianModel
{
    public class Physician
    {
        [Required]
        public int PhysicianId { get; set; }
        [Required]
        public string PhysFirstName { get; set; }
        [Required]
        public string PhysLastName { get; set; }
        public string PhysQualification { get; set; }
        [Required]
        public int PhysSpecialtyId { get; set; }
        [Required]
        public string PhysNPI { get; set; }
        public string PhysEMail { get; set; }
        public string PhysNotes { get; set; }
        [Required]
        public string PhysAddress1 { get; set; }       
        public string PhysAddress2 { get; set; }
        [Required]
        public string PhysCity { get; set; }
        [Required]
        public int PhysStateId { get; set; }
        [Required]
        public string PhysZip { get; set; }
        public string PhysPhone { get; set; }    
        public string PhysFax { get; set; }        
        public string PhysStateName { get; set; }        
        public string PhysSpecialtyName { get; set; }
    }
}
