using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MMC.Core.Data.Model
{
    [Table(Constant.Constant.lookupTables.DurationType, Schema = Constant.Constant.Schemas.lookup)]
    public class DurationType
    {
        [Key]
        public int DurationTypeID { get; set; }
        public string DurationTypeName { get; set; }
    }
}