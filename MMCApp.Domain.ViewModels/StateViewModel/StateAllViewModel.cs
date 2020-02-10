using MMCApp.Domain.Models.StateModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.StateViewModel
{
    public class StateAllViewModel
    {
        public IEnumerable<State> StateDetails { get; set; }
    }
}
