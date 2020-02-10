using MMCApp.Domain.Models.SpecialtyModel;
using System.Collections.Generic;
namespace MMCApp.Domain.ViewModels.SpecialtyViewModel
{
    public class SpecialtyAllViewModel
    {
        public IEnumerable<Specialty> SpecialtyDetails { get; set; }
    }
}
