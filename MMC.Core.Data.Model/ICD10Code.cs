using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    [Table(Constant.Constant.lookupTables.ICD10Code, Schema = Constant.Constant.Schemas.lookup)]
    public class ICD10Code
    {
        [Key]
        public int icdICD10NumberID { get; set; }
        public string icdICD10Number { get; set; }
        public string ICD10Description { get; set; }
        public string ICD10Type { get; set; }
    }
}
