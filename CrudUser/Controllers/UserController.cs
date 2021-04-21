using CrudUser.Application.Contracts;
using CrudUser.DTOs;
using CrudUser.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudUser.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        #region Fields
        private readonly IUserAppService _userAppService;
        #endregion

        #region Builders
        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }
        #endregion

        #region Methods        
        [HttpGet("getAll")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        async public Task<ActionResult<RequestResult<IEnumerable<UserDTO>>>> Get()
        {
            var result = await _userAppService.Get();
            if (result.Value != null && result.Value.IsError)
                return StatusCode(StatusCodes.Status500InternalServerError, result.Value);

            return result;
        }

        [HttpGet("get", Name = "Get")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        async public Task<ActionResult<RequestResult<UserDTO>>> Get(string id)
        {
            var result = await _userAppService.Get(id);
            if (result.Value != null && result.Value.IsError)
                return StatusCode(StatusCodes.Status500InternalServerError, result.Value);

            return result;
        }

        [HttpPost("save")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        async public Task<ActionResult<RequestResult<UserDTO>>> Save([FromBody] UserDTO userDTO)
        {
            var result = await _userAppService.Save(userDTO);
            if (result.Value != null && result.Value.IsError)
                return StatusCode(StatusCodes.Status500InternalServerError, result.Value);

            return result;
        }

        [HttpPut("update")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        async public Task<ActionResult<RequestResult<UserDTO>>> Update([FromBody] UserDTO userDTO)
        {
            var result = await _userAppService.Update(userDTO);
            if (result.Value != null && result.Value.IsError)
                return StatusCode(StatusCodes.Status500InternalServerError, result.Value);

            return result;
        }

        [HttpDelete("delete")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        async public Task<ActionResult<RequestResult<bool>>> Delete(string id)
        {
            var result = await _userAppService.Delete(id);
            if (result.Value != null && result.Value.IsError)
                return StatusCode(StatusCodes.Status500InternalServerError, result.Value);

            return result;
        }

        [HttpPost("login")]
        async public Task<ActionResult<RequestResult<UserTokenDTO>>> Login([FromBody] RequestLoginDTO requestLogin)
        {
            var result = await _userAppService.Login(requestLogin);
            if (result.Value != null && result.Value.IsError)
                return StatusCode(StatusCodes.Status500InternalServerError, result.Value);

            return result;
        }
        #endregion
    }
}
