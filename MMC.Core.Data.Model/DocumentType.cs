
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MMC.Core.Data.Model
{
    [Table(Constant.Constant.lookupTables.DocumentType, Schema = Constant.Constant.Schemas.lookup)]
    public class DocumentType
    {
        [Key]
        public int DocumentTypeID { get; set; }
        public string DocumentTypeDesc { get; set; }


        public int DocumentCategoryID  { get; set; }

        [ForeignKey("DocumentCategoryID")]
        public DocumentCategory documentCategorys { get; set; }

    }
}
