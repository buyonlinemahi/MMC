
namespace MMCApp.Domain.Models.ClientModel
{
    public class ClientEmployer
    {
        public int ClientEmployerID { get; set; }
        public int ClientID { get; set; }
        public int EmployerID { get; set; }

        public int ClientName { get; set; }
        public string EmpName { get; set; }
        public string EmpAddress1 { get; set; }
        public string EmpCity { get; set; }
        public string EmpState { get; set; }
        public string EmpZip { get; set; }
        public string EmpValue { get; set; }
    }

}
