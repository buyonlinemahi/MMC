using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    public class RFAReferralFile
    {
        [Key]
        public int RFAReferralFileID { get; set; }
        public int RFAReferralID { get; set; }
        public int RFAFileTypeID { get; set; }
        public string RFAReferralFileName { get; set; }
        public DateTime? RFAFileCreationDate { get; set; }
        public int RFAFileUserID { get; set; }

        [ForeignKey("RFAReferralID")]
        public RFAReferral rfaReferral { get; set; }

        [ForeignKey("RFAFileTypeID")]
        public FileType fileType { get; set; }

        [ForeignKey("RFAFileUserID")]
        public User rfaUser { get; set; }
    }
}