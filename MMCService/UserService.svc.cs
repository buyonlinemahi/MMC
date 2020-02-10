using AutoMapper;
using MMC.Core.BL;
using MMCService.CustomServiceBehaviors;
using MMCService.DTO;
using System.ServiceModel;


namespace MMCService  
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]

    [AutoMapperServiceBehavior]
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public int addUser(User user)
        {
            return _userRepo.addUser(Mapper.Map<User, MMC.Core.Data.Model.User>(user));
        }

        public int updateUser(User user)
        {
            return _userRepo.updateUser(Mapper.Map<User, MMC.Core.Data.Model.User>(user));
        }

        public int updateUserPassword(string userpassword, int userId)
        {
            return _userRepo.updateUserPassword(userpassword, userId);
        }

        public void  deleteUser(int _userId)
        {
            _userRepo.deleteUser(_userId);
        }
        public User getUserByID(int _userid)
        {
            return Mapper.Map<DTO.User>(_userRepo.getUserByID(_userid));
        }
        public User GetUserByUserName(string userName)
        {
            return Mapper.Map<DTO.User>(_userRepo.GetUserByUserName(userName));
        }

        public DTO.Paged.PagedUser getUsersByName(string SearchText, int _skip, int _take)
        {
            return Mapper.Map<DTO.Paged.PagedUser>(_userRepo.getUsersByName(SearchText, _skip, _take));
        }
    }
}
