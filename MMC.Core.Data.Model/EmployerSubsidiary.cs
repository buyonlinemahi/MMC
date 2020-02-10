using System.ComponentModel.DataAnnotations;

namespace MMC.Core.Data.Model
{
    public class EmployerSubsidiary
    {
        [Key]
        public int EMPSubsidiaryID { get; set; }
        public int EmployerId { get; set; }
        public string EMPSubsidiaryName { get; set; }
        public string EMPSubsidiaryAddress { get; set; }
        public string EMPSubsidiaryAddress2 { get; set; }
        public string EMPSubsidiaryCity { get; set; }
        public int EMPSubsidiaryStateId { get; set; }
        public string EMPSubsidiaryZip { get; set; }
        public string EMPSubsidiaryPhone { get; set; }
        public string EMPSubsidiaryFax { get; set; }
        public string EMPSubsidiaryEmail { get; set; }

    }
}
