using MMCApp.Domain.Models.InsurerModel;
using System.Collections.Generic;


namespace MMCApp.Domain.ViewModels.InsurerViewModel
{
    public class InsurerSearchViewModel
    {
      public IEnumerable<Insurer> InsurerDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
