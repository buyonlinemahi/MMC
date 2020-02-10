﻿using System;

namespace MMCApp.Domain.Models.IntakeModel
{
    public class PatientDetailsByReferralID
    {
        public string PatientName { get; set; }
        public string PatClaimNumber { get; set; }
        public DateTime? PatDOI { get; set; }
        public int PatientClaimID { get; set; }
        public int RFAReferralID { get; set; }
        public int PatientID { get; set; }
    }
}
