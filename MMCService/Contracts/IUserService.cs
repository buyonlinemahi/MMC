using MMCService.DTO;
using System.ServiceModel;

namespace MMCService
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        int addUser(User user);
        [OperationContract]
        int updateUser(User users);
        [OperationContract]
        void deleteUser(int _userID);
        [OperationContract]
        User getUserByID(int Id);
        [OperationContract]
        User GetUserByUserName(string userName);
        [OperationContract]
        MMCService.DTO.Paged.PagedUser getUsersByName(string SearchText, int _skip, int _take);
        [OperationContract]
        int updateUserPassword(string userpassword, int userId);
    }
}