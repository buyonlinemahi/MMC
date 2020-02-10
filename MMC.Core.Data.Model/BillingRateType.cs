using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    [Table(Constant.Constant.lookupTables.BillingRateType, Schema = Constant.Constant.Schemas.lookup)]
    public class BillingRateType
    {
        [Key]
        public int BillingRateTypeID { get; set; }
        public string BillingRateTypeName { get; set; }
    }
}
