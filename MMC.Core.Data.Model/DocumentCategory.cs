
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MMC.Core.Data.Model
{
    [Table(Constant.Constant.lookupTables.DocumentCategory, Schema = Constant.Constant.Schemas.lookup)]
    public class DocumentCategory
    {
        [Key]
        public int DocumentCategoryID { get; set; }
        public string DocumentCategoryName { get; set; }

    }
}
