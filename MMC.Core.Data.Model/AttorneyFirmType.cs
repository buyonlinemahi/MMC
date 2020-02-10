using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    [Table(Constant.Constant.lookupTables.AttorneyFirmType, Schema = Constant.Constant.Schemas.lookup)]
    public class AttorneyFirmType
    {
        [Key]
        public int AttorneyFirmTypeID { get; set; }
        public string AttorneyFirmTypeName { get; set; }

    }
}
