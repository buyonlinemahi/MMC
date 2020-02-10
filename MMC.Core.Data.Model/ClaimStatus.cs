using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    [Table(Constant.Constant.lookupTables.ClaimStatus, Schema = Constant.Constant.Schemas.lookup)]
    public class ClaimStatus
    {
        public int ClaimStatusID { get; set; }
        public string ClaimStatusName { get; set; }
    }
}
