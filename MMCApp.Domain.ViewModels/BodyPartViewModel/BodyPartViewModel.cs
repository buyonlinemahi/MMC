using MMCApp.Domain.Models.BodyPartModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.BodyPartViewModel
{
    public class BodyPartViewModel
    {
        public IEnumerable<BodyPart> BodyPartDetails { get; set; }
    }
}
