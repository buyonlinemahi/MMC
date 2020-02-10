using MMCService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MMCService
{
    [ServiceContract]
   public interface ICurrentWorkloadService
    {
        [OperationContract]
        void AddRptCurrentWorkloadReportParameters(List<CurrentWorkloadReportParameter> _rptCurrentWorkloadReportParameters);
        [OperationContract]
        int AddCurrentWorkloadReport(CurrentWorkloadReport _currentworkloadReport);
    }
}
