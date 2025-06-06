using System.Collections.Generic;
using System.ServiceModel;
using DomainService.Objects.DTOs.MyProject.DomainService.Objects;

namespace DomainService.Objects.Services
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        List<UserDto> GetAllUsers();

        [OperationContract]
        UserDto GetUserById(int id);

        [OperationContract]
        bool CreateUser(UserDto user);

        [OperationContract]
        bool UpdateUser(UserDto user);

        [OperationContract]
        bool DeleteUser(int id);
    }
}