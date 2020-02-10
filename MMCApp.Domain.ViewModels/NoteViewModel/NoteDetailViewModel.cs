using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.NoteViewModel
{
    public class NoteDetailViewModel
    {
        public IEnumerable<MMCApp.Domain.Models.NoteModel.NoteDetail> NotesDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
