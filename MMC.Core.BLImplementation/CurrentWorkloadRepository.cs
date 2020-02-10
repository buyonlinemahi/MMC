using MMC.Core.BL;
using MMC.Core.Data.Model;
using MMC.Core.Data.SQLServer;
using Omu.ValueInjecter;
using System.Collections.Generic;
using System.Linq;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BLImplementation
{
    public class CurrentWorkloadRepository : ICurrentWorkloadRepository
    {
        private readonly BaseRepository<CurrentWorkloadReportParameter> _rptCurrentWorkloadReportParametersRepository;
        private readonly BaseRepository<CurrentWorkloadReport> _CurrentWorkloadReportRepository;
        public CurrentWorkloadRepository()
        {
            _rptCurrentWorkloadReportParametersRepository = new BaseRepository<CurrentWorkloadReportParameter>();
            _CurrentWorkloadReportRepository = new BaseRepository<CurrentWorkloadReport>();
        }

        public void AddRptCurrentWorkloadReportParameters(List<CurrentWorkloadReportParameter> _rptCurrentWorkloadReportParameters)
        {
            foreach (CurrentWorkloadReportParameter cwrParameter in _rptCurrentWorkloadReportParameters)
            {
                _rptCurrentWorkloadReportParametersRepository.Add(cwrParameter);
            }            
        }

        public int AddCurrentWorkloadReport(CurrentWorkloadReport _currentworkloadReport)
        {
            int _currentworkloadReportID = _CurrentWorkloadReportRepository.Add(_currentworkloadReport).CurrentWorkloadReportID;
            return _currentworkloadReportID;
        }
    }
}
