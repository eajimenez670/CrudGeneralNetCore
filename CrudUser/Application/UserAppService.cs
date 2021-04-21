using AutoMapper;
using CrudUser.Application.Contracts;
using CrudUser.Domain.Contracts;
using CrudUser.DTOs;
using CrudUser.Entities;
using CrudUser.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudUser.Application
{
    public class UserAppService : IUserAppService
    {
        #region Fields
        private readonly IUserDomainService _userDomainService;
        private readonly IMapper _mapper;
        #endregion

        #region Methods
        public UserAppService(IUserDomainService userDomainService, IMapper mapper)
        {
            _userDomainService = userDomainService;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        async public Task<ActionResult<RequestResult<bool>>> Delete(string id)
        {
            try
            {
                var result = await _userDomainService.Delete(id);
                if (result.IsSuccessful)
                    return result;

                return new NotFoundObjectResult(result);
            }
            catch (Exception e)
            {
                return RequestResult<bool>.CreateError(e.Message);
            }
        }

        async public Task<ActionResult<RequestResult<IEnumerable<UserDTO>>>> Get()
        {
            try
            {
                var result = await _userDomainService.Get();
                if (!result.IsSuccessful)
                    return new NotFoundObjectResult(result);

                var users = _mapper.Map<List<UserDTO>>(result.Result);
                return RequestResult<IEnumerable<UserDTO>>.CreateSuccessful(users);
            }
            catch (Exception e)
            {
                return RequestResult<IEnumerable<UserDTO>>.CreateError(e.Message);
            }
        }

        async public Task<ActionResult<RequestResult<UserDTO>>> Get(string id)
        {
            try
            {
                var result = await _userDomainService.Get(id);
                if (!result.IsSuccessful)
                    return new NotFoundObjectResult(result);

                var user = _mapper.Map<UserDTO>(result.Result);
                return RequestResult<UserDTO>.CreateSuccessful(user);
            }
            catch (Exception e)
            {
                return RequestResult<UserDTO>.CreateError(e.Message);
            }
        }

        async public Task<ActionResult<RequestResult<UserDTO>>> Save(UserDTO userDTO)
        {
            try
            {
                var user = await _userDomainService.Save(_mapper.Map<User>(userDTO));
                var userDTOSave = _mapper.Map<UserDTO>(user.Result);
                return new CreatedAtRouteResult("Get", new { id = userDTOSave.Identification }, RequestResult<UserDTO>.CreateSuccessful(userDTOSave));
            }
            catch (Exception e)
            {
                return RequestResult<UserDTO>.CreateError(e.Message);
            }
        }

        async public Task<ActionResult<RequestResult<UserDTO>>> Update(UserDTO userDTO)
        {
            try
            {
                var user = await _userDomainService.Update(_mapper.Map<User>(userDTO));
                if (!user.IsSuccessful)
                    return new NotFoundObjectResult(user);

                return RequestResult<UserDTO>.CreateSuccessful(_mapper.Map<UserDTO>(user.Result));
            }
            catch (Exception e)
            {
                return RequestResult<UserDTO>.CreateError(e.Message);
            }
        }

        async public Task<ActionResult<RequestResult<UserTokenDTO>>> Login(RequestLoginDTO requestLogin)
        {
            try
            {
                var result = await _userDomainService.Login(requestLogin);
                if (!result.IsSuccessful)
                    return new UnauthorizedObjectResult(result);

                return RequestResult<UserTokenDTO>.CreateSuccessful(result.Result);
            }
            catch (Exception e)
            {
                return RequestResult<UserTokenDTO>.CreateError(e.Message);
            }
        }
        #endregion
    }
}
