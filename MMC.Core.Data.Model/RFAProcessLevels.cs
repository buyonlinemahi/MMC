using System.ComponentModel.DataAnnotations;

namespace MMC.Core.Data.Model
{
    public class RFAProcessLevels
    {
        [Key]
        public int RFAProcessLevelID { get; set; }
        public int RFAReferralID { get; set; }
        public int ProcessLevel { get; set; }
    }
}
