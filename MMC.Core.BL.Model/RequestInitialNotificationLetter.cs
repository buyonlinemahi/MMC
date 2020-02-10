
namespace MMC.Core.BL.Model
{
    public class RequestInitialNotificationLetter
    {
        public int RequestID { get; set; }
        public string Treatment { get; set; }
        public int Frequency { get; set; }
        public int Duration { get; set; }
        public string DurationType { get; set; }
        public string RequestType { get; set; }
        public string TreatmentType { get; set; }
        public int? QTY { get; set; }
    }
}
