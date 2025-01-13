using AutoMapper;
using User.Resource.Application.DTOs;
using User.Resource.Domain.Entities;
namespace AutoMapperDemo
{
    public class UserMapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            //Provide all the Mapping Configuration
            var config = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<UserData, ReponseDTO>().
                ForMember(dest => dest.user, act => act.MapFrom(src => src.userName));
                cfg.CreateMap<SetUserDataDTO, UserData>();

            });
            //Create an Instance of Mapper and return that Instance
            var mapper = new Mapper(config);
            return mapper;
        }
    }
}
