using AutoMapper;
using ModelLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<UserEntity, UserModel>();
            CreateMap<UserEntity, RegisterUser>().ReverseMap();
            CreateMap<UserEntity, UserModel>().ReverseMap();

        }
    }
}