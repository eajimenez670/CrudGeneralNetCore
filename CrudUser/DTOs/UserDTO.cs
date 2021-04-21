using CrudUser.Entities;
using System;

namespace CrudUser.DTOs
{
    public class UserDTO
    {
        public string Identification { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string IdTypeIdentification { get; set; }
        public TypeIdentificationDTO TypeIdentification { get; set; }
    }

    public class UserTokenDTO : UserDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

        public UserTokenDTO(string identification, string name, string lastName, string password, string email, string idTypeIdentification, string token, DateTime expiration)
        {
            Identification = identification;
            Name = name;
            LastName = lastName;
            Password = password;
            Email = email;
            IdTypeIdentification = idTypeIdentification;
            Token = token;
            Expiration = expiration;
        }
    }
}
