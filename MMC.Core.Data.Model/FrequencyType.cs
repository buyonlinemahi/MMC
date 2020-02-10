using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MMC.Core.Data.Model
{
    [Table(Constant.Constant.lookupTables.FrequencyType, Schema = Constant.Constant.Schemas.lookup)]
    public class FrequencyType
    {
        [Key]
        public int FrequencyTypeID { get; set; }
        public string FrequencyTypeName { get; set; }
    }
}