using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MMC.Core.Data.Model
{
    [Table(Constant.Constant.lookupTables.TreatmentCategory, Schema = Constant.Constant.Schemas.lookup)]
    public class TreatmentCategory
    {
        [Key]
        public int TreatmentCategoryID { get; set; }
        public string TreatmentCategoryName { get; set; }
    }
}
