
namespace MMC.Core.Data.Model
{
  public class RFARequestBodyPart
    {
        public int RFARequestBodyPartID { get; set; }
        public int ClaimBodyPartID { get; set; }
        public string  BodyPartType { get; set; }
        public int RFARequestID { get; set; }
    }
}
