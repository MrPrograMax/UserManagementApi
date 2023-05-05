using Application.Common.Mappings;
using Application.Users.Commands.DeleteUser;
using AutoMapper;
using System;

namespace WebApi.Models
{
    public class DeleteUserDto : IMapWith<DeleteUserCommand>
    {
        public Guid UserId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteUserDto, DeleteUserCommand>()
                .ForMember(userCommand => userCommand.UserId,
                    opt => opt.MapFrom(userDto => userDto.UserId));
        }
    }
}
