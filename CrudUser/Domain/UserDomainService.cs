using AutoMapper;
using CrudUser.Context;
using CrudUser.Domain.Contracts;
using CrudUser.DTOs;
using CrudUser.Entities;
using CrudUser.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CrudUser.Domain
{
    public class UserDomainService : IUserDomainService
    {
        #region Fields
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        #endregion

        #region Builders
        public UserDomainService(ApplicationDBContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        #endregion

        #region Methods        
        async public Task<RequestResult<bool>> Delete(string id)
        {
            var userId = await _context.User.FirstOrDefaultAsync(x => x.Identification == id);
            if (userId == null)
                return RequestResult<bool>.CreateEntityNotExists(id);

            _context.User.Remove(userId);
            await _context.SaveChangesAsync();
            return RequestResult<bool>.CreateSuccessful(true);
        }

        async public Task<RequestResult<IEnumerable<User>>> Get()
        {
            var users = await _context.User.Include(x => x.TypeIdentification).ToListAsync();
            if (users != null && users.Count > 1)
                return RequestResult<IEnumerable<User>>.CreateSuccessful(users);

            return RequestResult<IEnumerable<User>>.CreateUnsuccessful(new string[] { "No hay usuarios registrados" });
        }

        async public Task<RequestResult<User>> Get(string id)
        {
            var user = await _context.User.Include(x => x.TypeIdentification).FirstOrDefaultAsync(x => x.Identification == id);
            if (user == null)
                return RequestResult<User>.CreateEntityNotExists(id);

            return RequestResult<User>.CreateSuccessful(user);
        }

        async public Task<RequestResult<User>> Save(User user)
        {
            var newUser = await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            return RequestResult<User>.CreateSuccessful(newUser.Entity);
        }

        async public Task<RequestResult<User>> Update(User user)
        {
            var userExist = await _context.User.FirstOrDefaultAsync(x => x.Identification == user.Identification);
            if (userExist == null)
                return RequestResult<User>.CreateEntityNotExists(user.Identification);

            userExist = ReplaceData(userExist, user);
            await _context.SaveChangesAsync();
            return RequestResult<User>.CreateSuccessful(userExist);
        }

        async public Task<RequestResult<UserTokenDTO>> Login(RequestLoginDTO requestLogin)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Email == requestLogin.Email && x.Password == requestLogin.Password);
            if (user == null)
                return RequestResult<UserTokenDTO>.CreateUnsuccessful(new string[] { "El usuario o la contraseña son incorrectas" });

            return RequestResult<UserTokenDTO>.CreateSuccessful(CreateToken(user));
        }
        #endregion

        #region Private Methods
        private User ReplaceData(User userDb, User userUpt)
        {
            userDb.Email = userUpt.Email;
            userDb.Identification = userUpt.Identification;
            userDb.IdTypeIdentification = userUpt.IdTypeIdentification;
            userDb.LastName = userUpt.LastName;
            userDb.Name = userUpt.Name;
            userDb.Password = userUpt.Password;

            return userDb;
        }

        private UserTokenDTO CreateToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Identification)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(2);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new UserTokenDTO(user.Identification, user.Name, user.LastName, user.Password, user.Email, user.IdTypeIdentification, tokenString, expiration);
        }
        #endregion
    }
}
