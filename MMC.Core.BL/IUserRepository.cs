using MMC.Core.Data.Model;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL
{
    public interface IUserRepository
    {
        int addUser(User users);
        int updateUser(User users);
        int updateUserPassword(string userpassword, int userId);
        void deleteUser(int _userId);
        IEnumerable<User> getUsersAll();
        User getUserByID(int Id);
        User GetUserByUserName(string userName);
        BLModel.Paged.User getUsersByName(string SearchText, int _skip, int _take);
    }
}
