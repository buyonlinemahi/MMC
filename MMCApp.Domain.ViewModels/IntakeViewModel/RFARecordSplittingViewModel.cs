using MMCApp.Domain.Models.DocumentCategoryModel;
using MMCApp.Domain.Models.DocumentTypeModel;
using MMCApp.Domain.Models.IntakeModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.IntakeViewModel
{
    public class RFARecordSplittingViewModel
    {
        public IEnumerable<RFARecordSpliting> rfaRecordSplitingDetails { get; set; }
        public IEnumerable<DocumentCategory> documentCategories { get; set; }
        public IEnumerable<DocumentType> documentTypes { get; set; }
        public RFARecordSpliting rfaRecordSpliting { get; set; }
        public string diagnosisAll { get; set; }
    }
}