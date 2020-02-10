
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MMC.Core.Data.Model
{
    public class ThirdPartyAdministrator
    {
        [Key]
        public int TPAID { get; set; }
        public string TPAName { get; set; }
        public string TPAAddress { get; set; }
        public string TPAAddress2 { get; set; }
        public string TPACity { get; set; }
        public int TPAStateId { get; set; }
        public string TPAZip { get; set; }
        public string TPAPhone { get; set; }
        public string TPAFax { get; set; }

        [ForeignKey("TPAStateId")]
        public State states { get; set; }
    }
}
