namespace MMCApp.Domain.Models.IntakeModel
{
    public class RFARequestCPTCode
    {
        public int RFACPTCodeID { get; set; }
        public int RFARequestID { get; set; }
        public string CPT_NDCCode { get; set; }
    }
}