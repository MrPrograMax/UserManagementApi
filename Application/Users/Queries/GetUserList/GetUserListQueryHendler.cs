using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetUserList
{
    public class GetUserListQueryHendler 
        : IRequestHandler<GetUserListQuery, UserListVm>
    {
        private readonly IUserManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserListQueryHendler(IUserManagementDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserListVm> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            var usersQuery = await _dbContext.Users
                .ProjectTo<UserLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new UserListVm { Users = usersQuery };
                
        }
    }
}
