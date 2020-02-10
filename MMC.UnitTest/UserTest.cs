using Microsoft.VisualStudio.TestTools.UnitTesting;
using MMC.Core.BL;
using MMC.Core.BLImplementation;
using MMC.Core.Data.Model;
namespace MMC.UnitTest
{
    [TestClass]
    public class UserTest
    {
       IUserRepository _userRepository;
        
        public UserTest()
        {
            _userRepository = new UserRepository();
        }

        [TestMethod]
        public void adduser()
        {
            User _user = new User
            {
                UserFirstName = "genius",
                UserLastName = "jain",
                UserName = "g12323",
                UserPassword = "g",
                UserPhone = "999999990",
                UserEmail = "s56@s.com",
                UserFax = "456258900",
                UserDeletePermission=true,
                UserManagementPermission =false
            };
            
            var id = _userRepository.addUser(_user);
            Assert.IsTrue(id > 0, "failed");
        }

        [TestMethod]
        public void updateUser()
        {
            User _user = new User
            {
                UserFirstName = "gggg",
                UserLastName = "j",
                UserName = "gtest",
             //   UserPassword = "123456",
                UserPhone = "656",
                UserEmail = "sfhfghfghfgd@s.com",
                UserFax = "ggdfg",
                UserId=51,
                UserDeletePermission = true,
                UserManagementPermission = false
            };

            var id = _userRepository.updateUser(_user);
            Assert.IsTrue(id > 0, "failed");
        }

        [TestMethod]
        public void updateUserPassword()
        {        
            var id = _userRepository.updateUserPassword("pass123",15);
            Assert.IsTrue(id > 0, "failed");
        }

        [TestMethod]
        public void getAllUsers()
        {
            var users = _userRepository.getUsersAll();
            Assert.IsTrue(users != null, "failed");
        }

        [TestMethod]
        public void deleteUser()
        {
            _userRepository.deleteUser(11);
        }


        [TestMethod]
        public void getUsersByName()
        {
            var userByName = _userRepository.getUsersByName("Super User1", 0, 10);
            Assert.IsTrue(userByName != null, "failed");
        }
    }
}
