using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Core.BL;
using MMC.Core.BLImplementation;
using MMC.Core.Data.Model;
using BLModel = MMC.Core.BL.Model;
using System.Collections.Generic;

namespace MMC.UnitTest
{
    [TestClass]
   public class CurrentWorkloadTest
    {
        ICurrentWorkloadRepository _CurrentWorkloadRepository;

        public CurrentWorkloadTest()
        {
            _CurrentWorkloadRepository = new CurrentWorkloadRepository();
        }

        [TestMethod]
        public void AddRptCurrentWorkloadReportParameters()
        {
            List<CurrentWorkloadReportParameter> _rptCurrentWorkloadReportParameters = new List<CurrentWorkloadReportParameter>();
            CurrentWorkloadReportParameter _CurrentWorkloadParameters = new CurrentWorkloadReportParameter();
            for (int i = 0; i < 4; i++)
            {
                _CurrentWorkloadParameters.ClientID = i;
                _CurrentWorkloadParameters.StatusID = i+1;
                _rptCurrentWorkloadReportParameters.Add(_CurrentWorkloadParameters);
            }

            _CurrentWorkloadRepository.AddRptCurrentWorkloadReportParameters(_rptCurrentWorkloadReportParameters);                        
        }
    }
}
