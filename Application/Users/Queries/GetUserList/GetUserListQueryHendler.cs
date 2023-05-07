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
            var usersQuery = _dbContext.Users
                .Include(u => u.UserGroup)
                .Include(u => u.UserState)
                .OrderByDescending(u => u.CreatedDate)
                .ProjectTo<UserLookupDto>(_mapper.ConfigurationProvider);

            var totalCount = await usersQuery.CountAsync(cancellationToken);
            var totalPages = (int)Math.Ceiling((double)totalCount / request.PageSize);

            var users = await usersQuery
                .Skip((request.CurrentPage - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new UserListVm { 
                Users = users, 
                CurrentPage = request.CurrentPage, 
                PageSize = request.PageSize,
                TotalCountRecord = totalCount, 
                TotalPages = totalPages 
            };
        }
    }
}
