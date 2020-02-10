using MMCApp.Domain.Models.UserModel;
using System.Collections.Generic;

namespace MMCApp.Domain.ViewModels.UserViewModel
{
   public class UserSearchViewModel
    {
       public IEnumerable<User> UserDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
