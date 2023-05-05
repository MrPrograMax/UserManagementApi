using Application.Interfaces;
using Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHendler : IRequestHandler<DeleteUserCommand, Guid>
    {
        private readonly IUserManagementDbContext _dbContext;

        public CreateUserCommandHendler(IUserManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Login = request.Login,
                Password = request.Password,
                CreatedDate = DateTime.UtcNow,
                UserGroupId = request.UserGroupId,
                UserStateId = request.UserStateId
            };

            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
