
namespace MMCApp.Domain.Models.MedicalGroup
{
    public class MedicalGroup
    {
        public int MedicalGroupID { get; set; }
        public string MedicalGroupName { get; set; }
        public string MGAddress { get; set; }
        public string MGAddress2 { get; set; }
        public string MGCity { get; set; }
        public int MGStateID { get; set; }
        public string MGZip { get; set; }
        public string MGNote { get; set; }
        public string StateName { get; set; }
    }
}
