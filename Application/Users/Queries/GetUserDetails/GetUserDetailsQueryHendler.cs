using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetUserDetails
{
    public class GetUserDetailsQueryHendler
        : IRequestHandler<GetUserDetailsQuery, UserDetailsVm>
    {
        private readonly IUserManagementDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserDetailsQueryHendler(IUserManagementDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserDetailsVm> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(user => user.Id == request.UserId, cancellationToken);

            if (user == null || user.Id != request.UserId) 
            {
                throw new NotFoundException(nameof(User), request.UserId);
            }

            return _mapper.Map<UserDetailsVm>(user);
        }
    }
}
