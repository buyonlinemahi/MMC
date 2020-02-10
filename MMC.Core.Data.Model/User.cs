
namespace MMC.Core.Data.Model
{
  public  class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserPassword { get; set; }
        public string UserPhone { get; set; }
        public string UserFax { get; set; }
        public string UserEmail { get; set; }
        public string UserSignatureFileName { get; set; }
        public bool? UserDeletePermission { get; set; }
        public bool? UserManagementPermission { get; set; }  
    }
}
