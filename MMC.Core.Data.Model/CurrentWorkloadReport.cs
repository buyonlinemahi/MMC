using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MMC.Core.Data.Model
{
    [Table(Constant.Constant.dboTables.CurrentWorkloadReport, Schema = Constant.Constant.Schemas.dbo)]
    public class CurrentWorkloadReport
    {
        [Key]
        public int CurrentWorkloadReportID { get; set; }
        public string CurrentWorkloadReportName { get; set; }

    }
}
