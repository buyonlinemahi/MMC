using MMC.Core.Data.Model.Constant;
using System.ComponentModel.DataAnnotations.Schema;

namespace MMC.Core.Data.Model
{
    [Table(Constant.Constant.lookupTables.Language, Schema = Constant.Constant.Schemas.lookup)]
    public class Language
    {
        public int LanguageID { get; set; }
        public string LanguageName { get; set; }
    }
}
