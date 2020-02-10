using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MMC.Core.Data.Model
{
    [Table(Constant.Constant.lookupTables.RequestType, Schema = Constant.Constant.Schemas.lookup)]
    public class RequestType
    {
        [Key]
        public int RequestTypeID { get; set; }
        public int RequestTypeName { get; set; }
        public string RequestTypeDesc { get; set; }
    }
}
