using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MMCApp.Domain.Models.UserModel
{
    public class User
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserFirstName { get; set; }
        [Required]
        public string UserLastName { get; set; }
        [Required]
        public string UserPassword { get; set; }
        [Required]
        public string UserPhone { get; set; }
        [Required]
        public string UserFax { get; set; }
        [Required]
        public string UserEmail { get; set; }
        public string Message { get; set; }
        public string UserConfirmPassword { get; set; }
        public string UserSignatureFileName { get; set; }
        public HttpPostedFileBase UserSignature { get; set; }
        public bool? UserDeletePermission { get; set; }
        public bool? UserManagementPermission { get; set; }  
    }
}
