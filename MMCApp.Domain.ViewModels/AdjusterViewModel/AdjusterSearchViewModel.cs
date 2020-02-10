using MMCApp.Domain.Models.AdjusterModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.AdjusterViewModel
{
    public class AdjusterSearchViewModel
    {
        public IEnumerable<Adjuster> AdjusterDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
