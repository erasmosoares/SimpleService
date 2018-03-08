using AutoMapper;
using SimpleService.Dto;
using SimpleService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleService.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /// Using Mapper 
            Mapper.CreateMap<MainObject, MainObjectDto>();
            Mapper.CreateMap<MainObjectDto, MainObject>();
        }
    }
}