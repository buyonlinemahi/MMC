using MMC.Core.Data.Model.Constant;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    [Table(Constant.Constant.lookupTables.Specialties, Schema = Constant.Constant.Schemas.lookup)]
    public class Specialty
    {
        public int SpecialtyID { get; set; }
        public string SpecialtyName { get; set; }
    }
}
