using AutoMapper;
using MMC.Core.BL;
using MMCService.CustomServiceBehaviors;
using MMCService.DTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace MMCService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AutoMapperServiceBehavior]
	public class CurrentWorkloadService : ICurrentWorkloadService
	{
		 private readonly ICurrentWorkloadRepository _CurrentWorkloadRepository;

        public CurrentWorkloadService(ICurrentWorkloadRepository CurrentWorkloadRepository)
        {
            _CurrentWorkloadRepository = CurrentWorkloadRepository;
        }

        public void AddRptCurrentWorkloadReportParameters(List<MMCService.DTO.CurrentWorkloadReportParameter> _rptCurrentWorkloadReportParameters)
        {
           _CurrentWorkloadRepository.AddRptCurrentWorkloadReportParameters(Mapper.Map<List<MMC.Core.Data.Model.CurrentWorkloadReportParameter>>(_rptCurrentWorkloadReportParameters));
        }

        public int AddCurrentWorkloadReport(MMCService.DTO.CurrentWorkloadReport _currentworkloadReport)
        {
            return _CurrentWorkloadRepository.AddCurrentWorkloadReport(Mapper.Map<MMC.Core.Data.Model.CurrentWorkloadReport>(_currentworkloadReport));
        }
	}
}
