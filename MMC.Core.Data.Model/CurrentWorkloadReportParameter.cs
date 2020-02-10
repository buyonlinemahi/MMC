using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    [Table(Constant.Constant.dboTables.CurrentWorkloadReportParameter, Schema = Constant.Constant.Schemas.dbo)]
    public class CurrentWorkloadReportParameter
    {
        [Key]
        public int CurrentWorkloadReportParameterID { get; set; }
        public int? ClientID { get; set; }
        public int CurrentWorkloadReportID { get; set; }
        public int? StatusID { get; set; }
    }
}
