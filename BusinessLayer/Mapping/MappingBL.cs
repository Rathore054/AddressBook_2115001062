﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ModelLayer.Model;
using RepositoryLayer.Entity;

namespace BusinessLayer.Mapping
{
    public class MappingProfileBL : Profile
    {
        public MappingProfileBL()
        {
            CreateMap<AddressBookEntity, AddressBookEntity>().ReverseMap();
            CreateMap<RequestModel, AddressBookEntity>().ReverseMap();
        }
    }
}