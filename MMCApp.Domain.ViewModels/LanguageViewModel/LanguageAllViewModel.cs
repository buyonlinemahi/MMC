using MMCApp.Domain.Models.LanguageModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.LanguageViewModel
{
    public class LanguageAllViewModel
    {
        public IEnumerable<Language> LanguageDetails { get; set; }
    }
}
