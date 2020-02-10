using MMCApp.Domain.Models.DocumentTypeModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.DocumentTypeViewModel
{
    public class DocumentTypeViewModel
    {
        public IEnumerable<DocumentType> DocumentTypeDetails { get; set; }
    }
}
