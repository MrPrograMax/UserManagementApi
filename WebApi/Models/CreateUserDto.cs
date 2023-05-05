using Application.Common.Mappings;
using Application.Users.Commands.CreateUser;
using AutoMapper;
using System;

namespace WebApi.Models
{
    public class CreateUserDto : IMapWith<CreateUserCommand>
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public Guid UserGroupId { get; set; }
        public Guid UserStateId { get; set; }

        public void Mapping(Profile profile) 
        {
            profile.CreateMap<CreateUserDto, CreateUserCommand>()
                .ForMember(userCommand => userCommand.Login,
                    opt => opt.MapFrom(userDto => userDto.Login))

                .ForMember(userCommand => userCommand.Password,
                    opt => opt.MapFrom(userDto => userDto.Password))

                .ForMember(userCommand => userCommand.UserGroupId,
                    opt => opt.MapFrom(userDto => userDto.UserGroupId));

        }
    }
}
