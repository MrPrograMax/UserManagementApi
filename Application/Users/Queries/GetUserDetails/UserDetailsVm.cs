using Application.Common.Mappings;
using AutoMapper;
using Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Users.Queries.GetUserDetails
{
    public class UserDetailsVm : IMapWith<User>
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UserGroupCode { get; set; }

        public string UserStateCode { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDetailsVm>()
                .ForMember(userVm => userVm.Id,
                    opt => opt.MapFrom(user => user.Id))

                .ForMember(userVm => userVm.Login,
                    opt => opt.MapFrom(user => user.Login))

                .ForMember(userVm => userVm.CreatedDate,
                    opt => opt.MapFrom(user => user.CreatedDate))

                .ForMember(userVm => userVm.UserGroupCode,
                    opt => opt.MapFrom(user => user.UserGroup.Code))

                .ForMember(userVm => userVm.UserStateCode,
                    opt => opt.MapFrom(user => user.UserState.Code));
        }
    }
}
