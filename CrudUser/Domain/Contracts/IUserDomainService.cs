using CrudUser.DTOs;
using CrudUser.Entities;
using CrudUser.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudUser.Domain.Contracts
{
    public interface IUserDomainService
    {
        Task<RequestResult<IEnumerable<User>>> Get();

        Task<RequestResult<User>> Get(string id);

        Task<RequestResult<User>> Save(User user);

        Task<RequestResult<User>> Update(User user);

        Task<RequestResult<bool>> Delete(string id);

        Task<RequestResult<UserTokenDTO>> Login(RequestLoginDTO requestLogin);
    }
}
