using MMCApp.Domain.Models.DocumentCategoryModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.DocumentCategoriesViewModel
{
    public class DocumentCategoriesAllViewModel
    {
        public IEnumerable<DocumentCategory> DocumentCategoryDetails { get; set; }
    }
}
