using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    [Table(Constant.Constant.lookupTables.ICD9Code, Schema = Constant.Constant.Schemas.lookup)]
    public class ICD9Code
    {
        [Key]
        public int icdICD9NumberID { get; set; }
        public string icdICD9Number { get; set; }
        public string ICD9Description { get; set; }
        public string ICD9Type { get; set; }
    }
}
