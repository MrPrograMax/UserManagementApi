using Application.Common.Mappings;
using Application.Users.Queries.GetUserDetails;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetUserList
{
    public class UserLookupDto : IMapWith<User>
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid UserGroupId { get; set; }

        public Guid UserStateId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserLookupDto>()
                .ForMember(userVm => userVm.Id,
                    opt => opt.MapFrom(user => user.Id))

                .ForMember(userVm => userVm.Login,
                    opt => opt.MapFrom(user => user.Login))

                .ForMember(userVm => userVm.Password,
                    opt => opt.MapFrom(user => user.Password))

                .ForMember(userVm => userVm.CreatedDate,
                    opt => opt.MapFrom(user => user.CreatedDate))

                .ForMember(userVm => userVm.UserGroupId,
                    opt => opt.MapFrom(user => user.UserGroupId))

                .ForMember(userVm => userVm.UserStateId,
                    opt => opt.MapFrom(user => user.UserStateId));
        }
    }
}
