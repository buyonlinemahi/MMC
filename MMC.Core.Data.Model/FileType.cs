using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    [Table(Constant.Constant.lookupTables.FileType, Schema = Constant.Constant.Schemas.lookup)]
    public class FileType
    {   [Key]
        public int FileTypeID { get; set; }
        public string FileTypeName { get; set; }
    }
}

