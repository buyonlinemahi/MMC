using MMC.Core.BL;
using MMC.Core.Data.Model;
using MMC.Core.Data.SQLServer;
using System.Collections.Generic;
using System.Linq;
using BLModel = MMC.Core.BL.Model;


namespace MMC.Core.BLImplementation
{
    public class UserRepository : IUserRepository
    {
        private readonly BaseRepository<User> _userRepository;
        public UserRepository()
        {
            _userRepository = new BaseRepository<User>();

        }
        public int addUser(User _users)
        {
            var userExist = GetUserByUserName(_users.UserName);
            if (userExist == null)
            {
                return _userRepository.Add(_users).UserId;
            }
            else
            {
                return 0;
            }
        }

        public int updateUser(User _users)
        {
            //if (_userRepository.GetAll(users => (users.UserName == _users.UserName) && (users.UserId == _users.UserId)).Count() == 0)
            return _userRepository.Update(_users, (users => users.UserFirstName), (users => users.UserLastName), (users => users.UserEmail), (users => users.UserFax), (users => users.UserPhone), (users => users.UserName), (users => users.UserId), (users => users.UserSignatureFileName), (users => users.UserManagementPermission), (users => users.UserDeletePermission));
            //else
            //    return 0;
        }

        public int updateUserPassword(string userpassword, int userId)
        {
            User _users = new User();
            _users.UserPassword = userpassword;
            _users.UserId = userId;
            return _userRepository.Update(_users, (users => users.UserPassword), (users => users.UserId));
        }

        public void deleteUser(int _userId)
        {
            _userRepository.Delete(_userId);
        }

        public IEnumerable<User> getUsersAll()
        {
            return _userRepository.GetAll();
        }

        public User getUserByID(int _id)
        {
            return _userRepository.GetById(_id);
        }
        public User GetUserByUserName(string userName)
        {
            var User = _userRepository.GetAll().Where(user => user.UserName == userName).FirstOrDefault();
            return User != null ? User : null;
        }

        public BLModel.Paged.User getUsersByName(string SearchText, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            return new BLModel.Paged.User
           {
               UserDetails = _MMCDbContext.users.Where(usr => usr.UserFirstName.StartsWith(SearchText) || usr.UserLastName.StartsWith(SearchText) || usr.UserName.StartsWith(SearchText) || (usr.UserFirstName + " " + usr.UserLastName).StartsWith(SearchText)).OrderByDescending(usr => usr.UserId).Skip(_skip).Take(_take).ToList(),
               TotalCount = _MMCDbContext.users.Where(usr => usr.UserFirstName.StartsWith(SearchText) || usr.UserLastName.StartsWith(SearchText) || usr.UserName.StartsWith(SearchText) || (usr.UserFirstName + " " + usr.UserLastName).StartsWith(SearchText)).OrderBy(usr => usr.UserId).Count()
           };
        }

    }
}
