using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    [Table(Constant.Constant.lookupTables.BodyPart, Schema = Constant.Constant.Schemas.lookup)]
    public class BodyPart
    {
        [Key]
        public int BodyPartID { get; set; }
        public string BodyPartName { get; set; }
    }
}
