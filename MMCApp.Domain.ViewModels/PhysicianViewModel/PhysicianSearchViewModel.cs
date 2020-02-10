using MMCApp.Domain.Models.PhysicianModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.PhysicianViewModel
{
    public class PhysicianSearchViewModel
    {        
        public IEnumerable<Physician> PhysicianDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
