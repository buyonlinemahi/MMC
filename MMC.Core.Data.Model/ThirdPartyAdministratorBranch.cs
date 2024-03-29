﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MMC.Core.Data.Model
{
    public class ThirdPartyAdministratorBranch
    {
        [Key]
        public int TPABranchID { get; set; }
        public int TPAID { get; set; }
        public string TPABranchName { get; set; }
        public string TPABranchAddress { get; set; }
        public string TPABranchAddress2 { get; set; }
        public string TPABranchCity { get; set; }
        public int TPABranchStateId { get; set; }
        public string TPABranchZip { get; set; }
        public string TPABranchPhone { get; set; }
        public string TPABranchFax { get; set; }

        [ForeignKey("TPAID")]
        public ThirdPartyAdministrator thirdPartyAdministrators { get; set; }

        [ForeignKey("TPABranchStateId")]
        public State states { get; set; }
    }
}
