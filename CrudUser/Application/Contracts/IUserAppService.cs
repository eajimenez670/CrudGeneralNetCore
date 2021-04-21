using CrudUser.DTOs;
using CrudUser.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudUser.Application.Contracts
{
    public interface IUserAppService
    {
        Task<ActionResult<RequestResult<IEnumerable<UserDTO>>>> Get();

        Task<ActionResult<RequestResult<UserDTO>>> Get(string id);

        Task<ActionResult<RequestResult<UserDTO>>> Save(UserDTO userDTO);

        Task<ActionResult<RequestResult<UserDTO>>> Update(UserDTO userDTO);

        Task<ActionResult<RequestResult<bool>>> Delete(string id);

        Task<ActionResult<RequestResult<UserTokenDTO>>> Login(RequestLoginDTO requestLogin);
    }
}
