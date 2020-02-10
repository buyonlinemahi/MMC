using MMC.Core.Data.Model.Constant;
using System.ComponentModel.DataAnnotations.Schema;


namespace MMC.Core.Data.Model
{
    [Table(Constant.Constant.lookupTables.BodyPartStatus, Schema = Constant.Constant.Schemas.lookup)]
    public class BodyPartStatus
    {
        public int BodyPartStatusID { get; set; }
        public string BodyPartStatusName { get; set; }
    }
}
