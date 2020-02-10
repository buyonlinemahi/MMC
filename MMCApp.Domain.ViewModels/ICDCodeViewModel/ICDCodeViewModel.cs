using MMCApp.Domain.Models.ICDCodeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMCApp.Domain.ViewModels.ICDCodeViewModel
{
    public class ICDCodeViewModel
    {
        public IEnumerable<ICDCode> ICDCodeDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
