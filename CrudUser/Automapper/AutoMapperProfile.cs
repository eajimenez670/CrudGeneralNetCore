using AutoMapper;
using CrudUser.DTOs;
using CrudUser.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudUser.Automapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<TypeIdentification, TypeIdentificationDTO>();
        }
    }
}
