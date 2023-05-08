using Application.Interfaces;
using Application.Users.Queries.GetUserList;
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

namespace Application.Users.Queries.GetAllUsers
{
    public class GetAllUserQueryHendler : IRequestHandler<GetAllUserQuery, AllUserListVm>
    {
        private readonly IUserManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllUserQueryHendler(IUserManagementDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<AllUserListVm> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var usersQuery = await _dbContext.Users
                .ProjectTo<UserLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new AllUserListVm { Users = usersQuery };
        }
    }
}
