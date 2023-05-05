using Application.Common.Exceptions;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHendler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserManagementDbContext _dbContext;

        public CreateUserCommandHendler(IUserManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(5000, cancellationToken); // задержка на 5 секунд

            var userExists = await _dbContext.Users.AnyAsync(u => u.Login == request.Login, cancellationToken);
                
            if (userExists)
            {
                throw new UserAlreadyExists(request.Login);
            }

            var adminExists = await _dbContext.Users.AnyAsync(u => u.UserGroup.Code == "Admin", cancellationToken);

            var adminGroup = await _dbContext.UserGroups
                .FirstOrDefaultAsync(g => g.Code == "Admin", cancellationToken);

            if (adminExists && request.UserGroupId == adminGroup.Id)
            {
                throw new AdminAlreadyExists();
            }

            var user = new User()
            {
                Id = Guid.NewGuid(),
                Login = request.Login,
                Password = request.Password,
                CreatedDate = DateTime.UtcNow,
                UserGroupId = request.UserGroupId,
            };

            var activeState = await _dbContext.UserStates
                .FirstOrDefaultAsync(g => g.Code == "Active", cancellationToken);

            user.UserStateId = activeState.Id;

            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
