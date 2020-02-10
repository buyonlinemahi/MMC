using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    [Table(Constant.Constant.lookupTables.IMRDecision, Schema = Constant.Constant.Schemas.lookup)]
    public class IMRDecision
    {
        [Key]
        public int IMRDecisionID { get; set; }
        public string IMRDecisionDesc { get; set; }
    }
}
