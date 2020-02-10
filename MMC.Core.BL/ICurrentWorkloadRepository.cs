using MMC.Core.Data.Model;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL
{
    public interface ICurrentWorkloadRepository
    {
        void AddRptCurrentWorkloadReportParameters(List<CurrentWorkloadReportParameter> _rptCurrentWorkloadReportParameters);
        int AddCurrentWorkloadReport(CurrentWorkloadReport _currentworkloadReport);
    }
}
