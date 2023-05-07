using Application.Common.Mappings;
using AutoMapper;
using Domain;
using System;

namespace Application.Users.Queries.GetUserList
{
    public class UserLookupDto : IMapWith<User>
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UserGroupCode { get; set; }

        public string UserStateCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserLookupDto>()
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
