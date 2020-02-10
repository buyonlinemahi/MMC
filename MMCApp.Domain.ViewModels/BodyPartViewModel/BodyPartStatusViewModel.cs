using MMCApp.Domain.Models.BodyPartModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.BodyPartViewModel
{
    public class BodyPartStatusViewModel
    {
        public IEnumerable<BodyPartStatus> BodyPartStatusDetails { get; set; }
    }
}
