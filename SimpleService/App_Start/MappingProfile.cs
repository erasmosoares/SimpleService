using AutoMapper;
using SimpleService.Dto;
using SimpleService.Models;

namespace SimpleService.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /// Using Mapper 
            Mapper.CreateMap<JSONFile, JSONFileDto>();
            Mapper.CreateMap<JSONFileDto, JSONFile>();
        }
    }
}