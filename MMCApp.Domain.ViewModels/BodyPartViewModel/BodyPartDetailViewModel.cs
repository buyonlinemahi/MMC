using MMCApp.Domain.Models.BodyPartModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.BodyPartViewModel
{
    public class BodyPartDetailViewModel
    {
        public IEnumerable<BodyPartDetail> BodyPartDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
