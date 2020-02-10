using MMC.Core.Data.Model.Constant;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{

    [Table(Constant.Constant.lookupTables.CurrentMedicalCondition, Schema = Constant.Constant.Schemas.lookup)]
    public class CurrentMedicalCondition
    {
        public int CurrentMedicalConditionId { get; set; }
        public string CurrentMedicalConditionName { get; set; }
    }
}
