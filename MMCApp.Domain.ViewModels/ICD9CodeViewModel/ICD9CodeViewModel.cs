using MMCApp.Domain.Models.ICD9CodeModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.ICD9CodeViewModel
{
    public class ICD9CodeViewModel
    {
        public IEnumerable<ICD9Code> ICD9CodeDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
